using NLangDetect.Core.Utils;
using NUnit.Framework;

namespace NLangDetect.Core.Tests
{
  [TestFixture]
  public class DetectorTests
  {
    private const string Training_EN = "a a a b b c c d e";
    private const string Training_FR = "a b b c c c d d d";
    private const string Training_JA = "\u3042 \u3042 \u3042 \u3044 \u3046 \u3048 \u3048";

    [SetUp]
    public void SetUp()
    {
      LangProfile profile_en = new LangProfile("en");

      foreach (string w in Training_EN.Split(' '))
      {
        profile_en.Add(w);
      }

      DetectorFactory.AddProfile(profile_en, 0, 3);

      LangProfile profile_fr = new LangProfile("fr");

      foreach (string w in Training_FR.Split(' '))
      {
        profile_fr.Add(w);
      }

      DetectorFactory.AddProfile(profile_fr, 1, 3);

      LangProfile profile_ja = new LangProfile("ja");

      foreach (string w in Training_JA.Split(' '))
      {
        profile_ja.Add(w);
      }

      DetectorFactory.AddProfile(profile_ja, 2, 3);
    }

    [TearDown]
    public void TearDown()
    {
      DetectorFactory.Clear();
    }

    [Test]
    public void TestDetector1()
    {
      Detector detect = DetectorFactory.Create();

      detect.Append("a");

      Assert.AreEqual(LanguageName.En, detect.Detect());
    }

    [Test]
    public void TestDetector2()
    {
      Detector detect = DetectorFactory.Create();

      detect.Append("b d");

      Assert.AreEqual(LanguageName.Fr, detect.Detect());
    }

    [Test]
    public void TestDetector3()
    {
      Detector detect = DetectorFactory.Create();

      detect.Append("d e");

      Assert.AreEqual(LanguageName.En, detect.Detect());
    }

    [Test]
    public void TestDetector4()
    {
      Detector detect = DetectorFactory.Create();

      detect.Append("\u3042\u3042\u3042\u3042a");

      Assert.AreEqual(LanguageName.Ja, detect.Detect());
    }
  }
}
