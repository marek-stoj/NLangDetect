using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NLangDetect.Core.Extensions;
using NLangDetect.Core.Utils;

namespace NLangDetect.Core
{
  public class Detector
  {
    private const double ALPHA_DEFAULT = 0.5;
    private const double ALPHA_WIDTH = 0.05;

    private const int ITERATION_LIMIT = 1000;
    private const double PROB_THRESHOLD = 0.1;
    private const double CONV_THRESHOLD = 0.99999;
    private const int BASE_FREQ = 10000;
    private const string UNKNOWN_LANG = "unknown";

    private static readonly Regex URL_REGEX = new Regex("https?://[-_.?&~;+=/#0-9A-Za-z]+", RegexOptions.Compiled);
    private static readonly Regex MAIL_REGEX = new Regex("[-_.0-9A-Za-z]+@[-_0-9A-Za-z]+[-_.0-9A-Za-z]+", RegexOptions.Compiled);

    private readonly Dictionary<string, double[]> wordLangProbMap;
    private readonly List<string> langlist;

    private StringBuilder text;
    private double[] langprob;

    private double alpha = ALPHA_DEFAULT;
    private const int n_trial = 7;
    private int max_text_length = 10000;
    private double[] priorMap;
    private bool verbose;
    private int? seed;

    public Detector(DetectorFactory factory)
    {
      wordLangProbMap = factory.wordLangProbMap;
      langlist = factory.langlist;
      text = new StringBuilder();
      this.seed = factory.seed;
    }

    public void setVerbose()
    {
      this.verbose = true;
    }

    public void setAlpha(double alpha)
    {
      this.alpha = alpha;
    }

    public void setPriorMap(Dictionary<string, double> priorMap)
    {
      this.priorMap = new double[langlist.Count];
      double sump = 0;
      for (int i = 0; i < this.priorMap.Length; ++i)
      {
        string lang = langlist[i];
        if (priorMap.ContainsKey(lang))
        {
          double p = priorMap[lang];

          if (p < 0)
          {
            throw new LangDetectException("Prior probability must be non-negative.", ErrorCode.InitParamError);
          }

          this.priorMap[i] = p;

          sump += p;
        }
      }

      if (sump <= 0)
      {
        throw new LangDetectException("More one of prior probability must be non-zero.", ErrorCode.InitParamError);
      }

      for (int i = 0; i < this.priorMap.Length; ++i)
      {
        this.priorMap[i] /= sump;
      }
    }

    public void setMaxTextLength(int max_text_length)
    {
      this.max_text_length = max_text_length;
    }


    // TODO IMM HI: TextReader?
    public void append(StreamReader streamReader)
    {
      var buf = new char[max_text_length / 2];

      while (text.Length < max_text_length && !streamReader.EndOfStream)
      {
        int length = streamReader.Read(buf, 0, buf.Length);

        append(new string(buf, 0, length));
      }
    }

    public void append(string text)
    {
      text = URL_REGEX.Replace(text, " ");
      text = MAIL_REGEX.Replace(text, " ");

      char pre = '\0';

      for (int i = 0; i < text.Length && i < max_text_length; ++i)
      {
        char c = NGram.normalize(text[i]);

        if (c != ' ' || pre != ' ')
        {
          this.text.Append(c);
        }

        pre = c;
      }
    }

    private void cleaningText()
    {
      int latinCount = 0, nonLatinCount = 0;

      for (int i = 0; i < text.Length; ++i)
      {
        char c = text[i];

        if (c <= 'z' && c >= 'A')
        {
          ++latinCount;
        }
        else if (c >= '\u0300' && c.GetUnicodeBlock() != UnicodeBlock.LATIN_EXTENDED_ADDITIONAL)
        {
          ++nonLatinCount;
        }
      }

      if (latinCount * 2 < nonLatinCount)
      {
        var textWithoutLatin = new StringBuilder();

        for (int i = 0; i < text.Length; ++i)
        {
          char c = text[i];

          if (c > 'z' || c < 'A')
          {
            textWithoutLatin.Append(c);
          }
        }

        text = textWithoutLatin;
      }
    }

    public string detect()
    {
      List<Language> probabilities = getProbabilities();

      if (probabilities.Count > 0)
      {
        return probabilities[0].Name;
      }

      return UNKNOWN_LANG;
    }

    public List<Language> getProbabilities()
    {
      if (langprob == null)
      {
        detectBlock();
      }

      List<Language> list = sortProbability(langprob);

      return list;
    }

    private void detectBlock()
    {
      cleaningText();

      List<string> ngrams = extractNGrams();

      if (ngrams.Count == 0)
      {
        throw new LangDetectException("no features in text", ErrorCode.CantDetectError);
      }

      langprob = new double[langlist.Count];

      Random rand = (seed.HasValue ? new Random(seed.Value) : new Random());

      for (int t = 0; t < n_trial; ++t)
      {
        double[] prob = initProbability();

        // TODO IMM HI: verify it works
        double alpha = this.alpha + rand.NextGaussian() * ALPHA_WIDTH;

        for (int i = 0; ; ++i)
        {
          int r = rand.Next(ngrams.Count);

          updateLangProb(prob, ngrams[r], alpha);

          if (i % 5 == 0)
          {
            if (normalizeProb(prob) > CONV_THRESHOLD || i >= ITERATION_LIMIT)
            {
              break;
            }

            if (verbose)
            {
              Console.WriteLine("> " + sortProbability(prob));
            }
          }
        }

        for (int j = 0; j < langprob.Length; ++j)
        {
          langprob[j] += prob[j] / n_trial;
        }

        if (verbose)
        {
          Console.WriteLine("==> " + sortProbability(prob));
        }
      }
    }

    private double[] initProbability()
    {
      var prob = new double[langlist.Count];

      if (priorMap != null)
      {
        for (int i = 0; i < prob.Length; ++i)
        {
          prob[i] = priorMap[i];
        }
      }
      else
      {
        for (int i = 0; i < prob.Length; ++i)
        {
          prob[i] = 1.0 / langlist.Count;
        }
      }
      return prob;
    }

    private List<string> extractNGrams()
    {
      var list = new List<string>();
      NGram ngram = new NGram();

      for (int i = 0; i < text.Length; ++i)
      {
        ngram.addChar(text[i]);

        for (int n = 1; n <= NGram.N_GRAM; ++n)
        {
          string w = ngram.get(n);

          if (w != null && wordLangProbMap.ContainsKey(w))
          {
            list.Add(w);
          }
        }
      }

      return list;
    }

    private bool updateLangProb(double[] prob, string word, double alpha)
    {
      if (word == null || !wordLangProbMap.ContainsKey(word))
      {
        return false;
      }

      double[] langProbMap = wordLangProbMap[word];

      if (verbose)
      {
        Console.WriteLine(word + "(" + unicodeEncode(word) + "):" + wordProbToString(langProbMap));
      }

      double weight = alpha / BASE_FREQ;

      for (int i = 0; i < prob.Length; ++i)
      {
        prob[i] *= weight + langProbMap[i];
      }

      return true;
    }

    private string wordProbToString(double[] prob)
    {
      var resultSb = new StringBuilder();

      for (int j = 0; j < prob.Length; ++j)
      {
        double p = prob[j];

        if (p >= 0.00001)
        {
          // TODO IMM HI: map the formatting
          //formatter.format(" %s:%.5f", langlist[j], p);
          resultSb.AppendFormat(" {0}:{1:F2}", langlist[j], p);
          // TODO IMM HI: should we add newline?
        }
      }

      return resultSb.ToString();
    }

    private static double normalizeProb(double[] prob)
    {
      double maxp = 0, sump = 0;

      for (int i = 0; i < prob.Length; ++i)
      {
        sump += prob[i];
      }

      for (int i = 0; i < prob.Length; ++i)
      {
        double p = prob[i] / sump;

        if (maxp < p)
        {
          maxp = p;
        }

        prob[i] = p;
      }

      return maxp;
    }

    private List<Language> sortProbability(double[] prob)
    {
      var list = new List<Language>();

      for (int j = 0; j < prob.Length; ++j)
      {
        double p = prob[j];

        if (p > PROB_THRESHOLD)
        {
          for (int i = 0; i <= list.Count; ++i)
          {
            if (i == list.Count || list[i].Probability < p)
            {
              list.Insert(i, new Language(langlist[j], p));

              break;
            }
          }
        }
      }

      return list;
    }

    private static string unicodeEncode(string word)
    {
      var resultSb = new StringBuilder();

      for (int i = 0; i < word.Length; ++i)
      {
        char ch = word[i];

        if (ch >= '\u0080')
        {
          // TODO IMM HI: how to do it in C#?
          //string st = Integer.toHexString(0x10000 + (int)ch);
          string st = string.Format("{0:x}", 0x10000 + ch);
          
          while (st.Length < 4)
          {
            st = "0" + st;
          }

          resultSb
            .Append("\\u")
            .Append(st.SubSequence(1, 5));
        }
        else
        {
          resultSb.Append(ch);
        }
      }

      return resultSb.ToString();
    }
  }
}
