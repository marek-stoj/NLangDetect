using NLangDetect.Core.Utils;
using NUnit.Framework;

namespace NLangDetect.Core.Tests
{
  [TestFixture]
  public class DetectorTest
  {
    private const string Training_EN = "a a a b b c c d e";
    private const string Training_FR = "a b b c c c d d d";
    private const string Training_JA = "\u3042 \u3042 \u3042 \u3044 \u3046 \u3048 \u3048";

    [SetUp]
    public void setUp()
    {
      DetectorFactory.clear();

      LangProfile profile_en = new LangProfile("en");

      foreach (string w in Training_EN.Split(' '))
      {
        profile_en.add(w);
      }

      DetectorFactory.addProfile(profile_en, 0, 3);

      LangProfile profile_fr = new LangProfile("fr");

      foreach (string w in Training_FR.Split(' '))
      {
        profile_fr.add(w);
      }

      DetectorFactory.addProfile(profile_fr, 1, 3);

      LangProfile profile_ja = new LangProfile("ja");

      foreach (string w in Training_JA.Split(' '))
      {
        profile_ja.add(w);
      }

      DetectorFactory.addProfile(profile_ja, 2, 3);
    }

    [Test]
    public void testDetector1()
    {
      Detector detect = DetectorFactory.create();

      detect.append("a");

      Assert.AreEqual("en", detect.detect());
    }

    [Test]
    public void testDetector2()
    {
      Detector detect = DetectorFactory.create();

      detect.append("b d");

      Assert.AreEqual("fr", detect.detect());
    }

    [Test]
    public void testDetector3()
    {
      Detector detect = DetectorFactory.create();

      detect.append("d e");

      Assert.AreEqual("en", detect.detect());
    }

    [Test]
    public void testDetector4()
    {
      Detector detect = DetectorFactory.create();

      detect.append("\u3042\u3042\u3042\u3042a");

      Assert.AreEqual("ja", detect.detect());
    }
  }
}
