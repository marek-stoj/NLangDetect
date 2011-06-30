using NLangDetect.Core.Utils;
using NUnit.Framework;

namespace NLangDetect.Core.Tests.UtilsTests
{
  [TestFixture]
  public class LangProfileTest
  {
    [Test]
    public void testLangProfile()
    {
      var profile = new LangProfile();

      Assert.AreEqual(null, profile.name);
    }

    [Test]
    public void testLangProfileStringInt()
    {
      var profile = new LangProfile("en");

      Assert.AreEqual("en", profile.name);
    }

    [Test]
    public void testAdd()
    {
      var profile = new LangProfile("en");

      profile.add("a");
      Assert.AreEqual(1, profile.freq["a"]);

      profile.add("a");
      Assert.AreEqual(2, profile.freq["a"]);

      profile.omitLessFreq();
    }

    [Test]
    public void testAddIllegally1()
    {
      var profile = new LangProfile(); // Illegal (available for only JSONIC) but ignore  

      profile.add("a"); // ignore

      Assert.AreEqual(null, profile.freq["a"]); // ignored
    }

    [Test]
    public void testAddIllegally2()
    {
      var profile = new LangProfile("en");

      profile.add("a");
      profile.add("");  // Illegal (string's length of parameter must be between 1 and 3) but ignore
      profile.add("abcd");  // as well

      Assert.AreEqual(1, profile.freq["a"]);
      Assert.AreEqual(null, profile.freq[""]);     // ignored
      Assert.AreEqual(null, profile.freq["abcd"]); // ignored
    }

    [Test]
    public void testOmitLessFreq() {
      var profile = new LangProfile("en");
      string[] grams = "a b c \u3042 \u3044 \u3046 \u3048 \u304a \u304b \u304c \u304d \u304e \u304f".Split(' ');

      for (int i = 0; i < 5; ++i)
      {
        foreach (string g in grams)
        {
          profile.add(g);
        }
      }

      profile.add("\u3050");
      Assert.AreEqual(5, profile.freq["a"]);
      Assert.AreEqual(5, profile.freq["\u3042"]);
      Assert.AreEqual(1, profile.freq["\u3050"]);

      profile.omitLessFreq();
      Assert.AreEqual(null, profile.freq["a"]); // omitted
      Assert.AreEqual(5, profile.freq["\u3042"]);
      Assert.AreEqual(null, profile.freq["\u3050"]); // omitted
    }

    [Test]
    public void testOmitLessFreqIllegally()
    {
      var profile = new LangProfile();

      profile.omitLessFreq();  // ignore
    }
  }
}
