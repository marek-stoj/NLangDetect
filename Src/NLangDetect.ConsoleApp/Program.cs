using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using NLangDetect.Core;
using NLangDetect.Core.Extensions;
using NLangDetect.Core.Utils;

namespace NLangDetect.ConsoleApp
{
  internal class Program
  {
    private const double DEFAULT_ALPHA = 0.5;

    private readonly Dictionary<string, string> opt_with_value = new Dictionary<string, string>();
    private readonly Dictionary<string, string> values = new Dictionary<string, string>();
    private readonly HashSet<string> opt_without_value = new HashSet<string>();
    private readonly List<string> arglist = new List<string>();

    #region Application entry point

    private static void Main(string[] args)
    {
      var program = new Program();

      program.addOpt("-d", "directory", "./");
      program.addOpt("-a", "alpha", "" + DEFAULT_ALPHA);
      program.addOpt("-s", "seed", null);
      program.parse(args);

      if (program.hasOpt("--genprofile"))
      {
        program.generateProfile();
      }
      else if (program.hasOpt("--detectlang"))
      {
        program.detectLang();
      }
      else if (program.hasOpt("--batchtest"))
      {
        program.batchTest();
      }
    }

    #endregion

    #region Private helper methods

    private static string searchFile(string directory, string pattern)
    {
      return Directory.GetFiles(directory, pattern).FirstOrDefault();
    }

    private void parse(string[] args)
    {
      for (int i = 0; i < args.Length; ++i)
      {
        if (opt_with_value.ContainsKey(args[i]))
        {
          string key = opt_with_value[args[i]];

          values[key] = args[i + 1];
          ++i;
        }
        else if (args[i].StartsWith("-"))
        {
          opt_without_value.Add(args[i]);
        }
        else
        {
          arglist.Add(args[i]);
        }
      }
    }

    private void addOpt(string opt, string key, string value)
    {
      opt_with_value.Add(opt, key);
      values.Add(key, value);
    }

    private string get(string key)
    {
      string value;

      if (!values.TryGetValue(key, out value))
      {
        return null;
      }

      return value;
    }

    private int? getInt(string key)
    {
      string value;

      if (!values.TryGetValue(key, out value))
      {
        return null;
      }

      int intValue;

      return int.TryParse(value, out intValue) ? intValue : (int?)null;
    }

    private double getDouble(string key, double defaultValue)
    {
      if (!values.ContainsKey(key))
      {
        return defaultValue;
      }

      double doubleValue;

      return double.TryParse(values[key], out doubleValue) ? doubleValue : defaultValue;
    }

    private bool hasOpt(string opt)
    {
      return opt_without_value.Contains(opt);
    }

    private bool loadProfile()
    {
      string profileDirectory = get("directory") + "/";

      try
      {
        DetectorFactory.loadProfile(profileDirectory);

        int? seed = getInt("seed");

        if (seed.HasValue)
        {
          DetectorFactory.setSeed(seed.Value);
        }

        return false;
      }
      catch (LangDetectException e)
      {
        Console.Error.WriteLine("ERROR: " + e.Message);

        return true;
      }
    }

    public void generateProfile()
    {
      string directory = get("directory");

      foreach (string lang in arglist)
      {
        string file = searchFile(directory, lang + "wiki-.*-abstract\\.xml.*");

        if (file == null)
        {
          Console.Error.WriteLine("Not Found abstract xml : lang = " + lang);
          continue;
        }

        // TODO IMM HI: as a static field?
        var jsonSerializer = new JsonSerializer();

        try
        {
          LangProfile profile = GenProfile.load(lang, file);

          profile.omitLessFreq();

          string profile_path = directory + "/profiles/" + lang;

          using (var sw = new StreamWriter(profile_path))
          {
            jsonSerializer.Serialize(sw, profile);
          }
        }
        catch (LangDetectException e)
        {
          // TODO IMM HI: what about this?
          throw;
        }
      }
    }

    public void detectLang()
    {
      if (loadProfile())
      {
        return;
      }

      foreach (string filename in arglist)
      {
        try
        {
          Detector detector = DetectorFactory.create(getDouble("alpha", DEFAULT_ALPHA));

          if (hasOpt("--debug"))
          {
            detector.setVerbose();
          }

          using (var sr = new StreamReader(filename))
          {
            detector.append(sr);
          }

          Console.Write(filename + ": ");

          foreach (Language language in detector.getProbabilities())
          {
            Console.Write(language);
          }

          Console.WriteLine();
        }
        catch (LangDetectException e)
        {
          // TODO IMM HI: what about this?
          throw;
        }
      }
    }

    public void batchTest()
    {
      if (loadProfile())
      {
        return;
      }

      var result = new Dictionary<string, List<string>>();

      foreach (string filename in arglist)
      {
        try
        {
          using (var sr = new StreamReader(filename))
          {
            while (!sr.EndOfStream)
            {
              string line = sr.ReadLine();
              int idx = line.IndexOf('\t');
              if (idx <= 0) continue;
              string correctLang = line.SubSequence(0, idx);
              string text = line.Substring(idx + 1);

              Detector detector = DetectorFactory.create(getDouble("alpha", DEFAULT_ALPHA));

              detector.append(text);

              string lang = detector.detect();

              if (!result.ContainsKey(correctLang))
              {
                result.Add(correctLang, new List<string>());
              }

              result[correctLang].Add(lang);

              if (hasOpt("--debug"))
              {
                Console.WriteLine(correctLang + "," + lang + "," + (text.Length > 100 ? text.SubSequence(0, 100) : text));
              }
            }
          }
        }
        catch (LangDetectException e)
        {
          // TODO IMM HI:
          throw;
        }

        var langlist = new List<string>(result.Keys);

        langlist.Sort();

        int totalCount = 0, totalCorrect = 0;

        foreach (string lang in langlist)
        {
          var resultCount = new Dictionary<string, int>();
          int count = 0;
          List<string> list = result[lang];

          foreach (string detectedLang in list)
          {
            ++count;

            if (resultCount.ContainsKey(detectedLang))
            {
              resultCount[detectedLang] = resultCount[detectedLang] + 1;
            }
            else
            {
              resultCount.Add(detectedLang, 1);
            }
          }

          int correct = resultCount.ContainsKey(lang) ? resultCount[lang] : 0;
          double rate = correct / (double)count;

          // TODO IMM HI: format
          Console.WriteLine(string.Format("%s (%d/%d=%.2f): %s", lang, correct, count, rate, resultCount));

          totalCorrect += correct;
          totalCount += count;
        }

        // TODO IMM HI: format
        Console.WriteLine(string.Format("total: %d/%d = %.3f", totalCorrect, totalCount, totalCorrect / (double)totalCount));
      }
    }

    #endregion
  }
}
