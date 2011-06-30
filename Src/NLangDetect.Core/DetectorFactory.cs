using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NLangDetect.Core.Utils;

namespace NLangDetect.Core
{
  public class DetectorFactory
  {
    public Dictionary<string, double[]> wordLangProbMap;
    public List<string> langlist;
    public int? seed;

    private static readonly DetectorFactory instance_ = new DetectorFactory();

    private DetectorFactory()
    {
      wordLangProbMap = new Dictionary<string, double[]>();
      langlist = new List<string>();
    }

    public static void loadProfile(string profileDirectory)
    {
      string[] listFiles = Directory.GetFiles(profileDirectory);

      if (listFiles == null)
      {
        throw new LangDetectException("Not found profile: " + profileDirectory, ErrorCode.NeedLoadProfileError);
      }

      int langsize = listFiles.Length, index = 0;

      foreach (string file in listFiles)
      {
        if (Path.GetFileName(file).StartsWith("."))
        {
          continue;
        }

        // TODO IMM HI: field?
        var jsonSerializer = new JsonSerializer();
        LangProfile langProfile;

        using (var sr = new StreamReader(file))
        {
          langProfile = (LangProfile)jsonSerializer.Deserialize(sr, typeof(LangProfile));
        }

        addProfile(langProfile, index, langsize);
        index++;
      }
    }

    internal static void addProfile(LangProfile profile, int index, int langsize)
    {
      string lang = profile.name;

      if (instance_.langlist.Contains(lang))
      {
        throw new LangDetectException("duplicate the same language profile", ErrorCode.DuplicateLangError);
      }

      instance_.langlist.Add(lang);

      foreach (string word in profile.freq.Keys)
      {
        if (!instance_.wordLangProbMap.ContainsKey(word))
        {
          instance_.wordLangProbMap.Add(word, new double[langsize]);
        }

        double prob = (double)profile.freq[word] / profile.n_words[word.Length - 1];

        instance_.wordLangProbMap[word][index] = prob;
      }
    }

    internal static void clear()
    {
      instance_.langlist.Clear();
      instance_.wordLangProbMap.Clear();
    }

    public static Detector create()
    {
      return createDetector();
    }

    public static Detector create(double alpha)
    {
      Detector detector = createDetector();

      detector.setAlpha(alpha);

      return detector;
    }

    private static Detector createDetector()
    {
      if (instance_.langlist.Count == 0)
      {
        throw new LangDetectException("need to load profiles", ErrorCode.NeedLoadProfileError);
      }

      return new Detector(instance_);
    }

    public static void setSeed(int? seed)
    {
      instance_.seed = seed;
    }
  }
}
