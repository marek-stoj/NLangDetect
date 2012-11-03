using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Newtonsoft.Json;
using NLangDetect.Core.Utils;

namespace NLangDetect.Core
{
  public class DetectorFactory
  {
    public Dictionary<string, ProbVector> WordLangProbMap;
    public List<LanguageName> Langlist;

    private static readonly DetectorFactory _instance = new DetectorFactory();

    #region Constructor(s)

    private DetectorFactory()
    {
      WordLangProbMap = new Dictionary<string, ProbVector>();
      Langlist = new List<LanguageName>();
    }

    #endregion

    #region Public methods

    public static void LoadProfiles(string profileDirectory)
    {
      string[] listFiles = Directory.GetFiles(profileDirectory);

      if (listFiles == null)
      {
        throw new NLangDetectException("Not found profile: " + profileDirectory, ErrorCode.NeedLoadProfileError);
      }

      int langsize = listFiles.Length, index = 0;
      var jsonSerializer = new JsonSerializer();

      foreach (string file in listFiles)
      {
        string fileName = Path.GetFileName(file);

        if (!string.IsNullOrEmpty(fileName) && fileName.StartsWith("."))
        {
          continue;
        }

        LangProfile langProfile;

        using (var s = File.OpenRead(file))
        using (var gs = new GZipStream(s, CompressionMode.Decompress))
        using (var sr = new StreamReader(gs))
        {
          langProfile = (LangProfile)jsonSerializer.Deserialize(sr, typeof(LangProfile));
        }

        AddProfile(langProfile, index, langsize);

        index++;
      }
    }

    public static Detector Create()
    {
      return CreateDetector();
    }

    public static Detector Create(double alpha)
    {
      Detector detector = CreateDetector();

      detector.SetAlpha(alpha);

      return detector;
    }

    public static void SetSeed(int? seed)
    {
      _instance.Seed = seed;
    }

    #endregion

    #region Internal methods

    internal static void AddProfile(LangProfile profile, int index, int langsize)
    {
      var lang = (LanguageName)Enum.Parse(typeof(LanguageName), profile.name, true);

      if (_instance.Langlist.Contains(lang))
      {
        throw new NLangDetectException("duplicate the same language profile", ErrorCode.DuplicateLangError);
      }

      _instance.Langlist.Add(lang);

      foreach (string word in profile.freq.Keys)
      {
        if (!_instance.WordLangProbMap.ContainsKey(word))
        {
          _instance.WordLangProbMap.Add(word, new ProbVector());
        }

        double prob = (double)profile.freq[word] / profile.n_words[word.Length - 1];

        _instance.WordLangProbMap[word][index] = prob;
      }
    }

    internal static void Clear()
    {
      _instance.Langlist.Clear();
      _instance.WordLangProbMap.Clear();
    }

    #endregion

    #region Private helper methods

    private static Detector CreateDetector()
    {
      if (_instance.Langlist.Count == 0)
      {
        throw new NLangDetectException("need to load profiles", ErrorCode.NeedLoadProfileError);
      }

      return new Detector(_instance);
    }

    #endregion

    #region Properties

    public int? Seed { get; private set; }

    #endregion
  }
}
