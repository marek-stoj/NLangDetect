using System;

namespace NLangDetect.Core
{
  // TODO IMM HI: change to non-static class
  // TODO IMM HI: hide other, unnecassary classes via internal?
  public static class LanguageDetector
  {
    private const double _DefaultAlpha = 0.5;

    private static bool _isInitialized;

    private static readonly object _mutex = new object();

    #region Public methods

    public static void Initialize(string profilesDirectory)
    {
      if (string.IsNullOrEmpty(profilesDirectory))
      {
        throw new ArgumentException("Argument can't be null nor empty.", "profilesDirectory");
      }

      lock (_mutex)
      {
        if (_isInitialized)
        {
          throw new InvalidOperationException("The component has already been initialized.");
        }

        DetectorFactory.LoadProfiles(profilesDirectory);

        _isInitialized = true;
      }
    }

    public static void Release()
    {
      DetectorFactory.Clear();
    }

    public static LanguageName? DetectLanguage(string plainText)
    {
      if (string.IsNullOrEmpty(plainText)) { throw new ArgumentException("Argument can't be null nor empty.", "plainText"); }

      lock (_mutex)
      {
        if (!_isInitialized)
        {
          throw new InvalidOperationException("The component has not been initialized.");
        }
      }

      Detector detector = DetectorFactory.Create(_DefaultAlpha);

      detector.Append(plainText);
      
      return detector.Detect();
    }

    #endregion
  }
}
