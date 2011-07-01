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
    private const double DefaultAlpha = 0.5;

    private readonly Dictionary<string, string> _optWithValue = new Dictionary<string, string>();
    private readonly Dictionary<string, string> _values = new Dictionary<string, string>();
    private readonly HashSet<string> _optWithoutValue = new HashSet<string>();
    private readonly List<string> _argList = new List<string>();
    private readonly JsonSerializer _jsonSerializer = new JsonSerializer();

    #region Application entry point

    private static void Main(string[] args)
    {
      var program = new Program();

      program.AddOpt("-d", "directory", "./");
      program.AddOpt("-a", "alpha", "" + DefaultAlpha);
      program.AddOpt("-s", "seed", null);
      program.Parse(args);

      if (program.HasOpt("--genprofile"))
      {
        program.GenerateProfile();
      }
      else if (program.HasOpt("--detectlang"))
      {
        program.DetectLang();
      }
      else if (program.HasOpt("--batchtest"))
      {
        program.BatchTest();
      }
    }

    #endregion

    #region Private helper methods

    private static string SearchFile(string directory, string pattern)
    {
      return Directory.GetFiles(directory, pattern).FirstOrDefault();
    }

    private void Parse(string[] args)
    {
      for (int i = 0; i < args.Length; i++)
      {
        if (_optWithValue.ContainsKey(args[i]))
        {
          string key = _optWithValue[args[i]];

          _values[key] = args[i + 1];
          i++;
        }
        else if (args[i].StartsWith("-"))
        {
          _optWithoutValue.Add(args[i]);
        }
        else
        {
          _argList.Add(args[i]);
        }
      }
    }

    private void AddOpt(string opt, string key, string value)
    {
      _optWithValue.Add(opt, key);
      _values.Add(key, value);
    }

    private string Get(string key)
    {
      string value;

      if (!_values.TryGetValue(key, out value))
      {
        return null;
      }

      return value;
    }

    private int? GetInt(string key)
    {
      string value;

      if (!_values.TryGetValue(key, out value))
      {
        return null;
      }

      int intValue;

      return int.TryParse(value, out intValue) ? intValue : (int?)null;
    }

    private double GetDouble(string key, double defaultValue)
    {
      if (!_values.ContainsKey(key))
      {
        return defaultValue;
      }

      double doubleValue;

      return double.TryParse(_values[key], out doubleValue) ? doubleValue : defaultValue;
    }

    private bool HasOpt(string opt)
    {
      return _optWithoutValue.Contains(opt);
    }

    private bool LoadProfile()
    {
      string profileDirectory = Get("directory") + "/";

      try
      {
        DetectorFactory.LoadProfile(profileDirectory);

        int? seed = GetInt("seed");

        if (seed.HasValue)
        {
          DetectorFactory.SetSeed(seed.Value);
        }

        return false;
      }
      catch (NLangDetectException e)
      {
        Console.Error.WriteLine("ERROR: " + e.Message);

        return true;
      }
    }

    private void GenerateProfile()
    {
      string directory = Get("directory");

      foreach (string lang in _argList)
      {
        string file = SearchFile(directory, lang + "wiki-.*-abstract\\.xml.*");

        if (file == null)
        {
          Console.Error.WriteLine("Not Found abstract xml : lang = " + lang);
          continue;
        }

        try
        {
          LangProfile profile = GenProfile.load(lang, file);

          profile.OmitLessFreq();

          string profile_path = directory + "/profiles/" + lang;

          using (var sw = new StreamWriter(profile_path))
          {
            _jsonSerializer.Serialize(sw, profile);
          }
        }
        catch (NLangDetectException e)
        {
          // TODO IMM HI: what about this?
          throw;
        }
      }
    }

    private void DetectLang()
    {
      if (LoadProfile())
      {
        return;
      }

      foreach (string filename in _argList)
      {
        try
        {
          Detector detector = DetectorFactory.Create(GetDouble("alpha", DefaultAlpha));

          if (HasOpt("--debug"))
          {
            detector.SetVerbose();
          }

          using (var sr = new StreamReader(filename))
          {
            detector.Append(sr);
          }

          Console.Write(filename + ": ");

          foreach (Language language in detector.GetProbabilities())
          {
            Console.Write(language);
          }

          Console.WriteLine();
        }
        catch (NLangDetectException e)
        {
          // TODO IMM HI: what about this?
          throw;
        }
      }
    }

    private void BatchTest()
    {
      if (LoadProfile())
      {
        return;
      }

      var result = new Dictionary<string, List<string>>();

      foreach (string filename in _argList)
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

              Detector detector = DetectorFactory.Create(GetDouble("alpha", DefaultAlpha));

              detector.Append(text);

              string lang = detector.Detect();

              if (!result.ContainsKey(correctLang))
              {
                result.Add(correctLang, new List<string>());
              }

              result[correctLang].Add(lang);

              if (HasOpt("--debug"))
              {
                Console.WriteLine(correctLang + "," + lang + "," + (text.Length > 100 ? text.SubSequence(0, 100) : text));
              }
            }
          }
        }
        catch (NLangDetectException e)
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
            count++;

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
