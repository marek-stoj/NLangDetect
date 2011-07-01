using NLangDetect.Core.Utils;
using NUnit.Framework;

namespace NLangDetect.Core.Tests.UtilsTests
{
  [TestFixture]
  public class TagExtractorTest
  {
    [Test]
    public void testTagExtractor()
    {
      TagExtractor extractor1 = new TagExtractor(null, 0);

      Assert.AreEqual(null, extractor1.Target);
      Assert.AreEqual(0, extractor1.Threshold);

      TagExtractor extractor2 = new TagExtractor("abstract", 10);

      Assert.AreEqual("abstract", extractor2.Target);
      Assert.AreEqual(10, extractor2.Threshold);
    }

    [Test]
    public void testSetTag()
    {
      TagExtractor extractor = new TagExtractor(null, 0);

      extractor.SetTag("");

      Assert.AreEqual("", extractor.Tag);

      extractor.SetTag(null);

      Assert.AreEqual(null, extractor.Tag);
    }

    [Test]
    public void testAdd()
    {
      TagExtractor extractor = new TagExtractor(null, 0);

      extractor.Add("");
      extractor.Add(null);    // ignore
    }

    [Test]
    public void testCloseTag()
    {
      TagExtractor extractor = new TagExtractor(null, 0);
      LangProfile profile = null;

      extractor.CloseTag(profile);    // ignore
    }

    [Test]
    public void testNormalScenario()
    {
      TagExtractor extractor = new TagExtractor("abstract", 10);

      Assert.AreEqual(0, extractor.Count);

      LangProfile profile = new LangProfile("en");

      // normal
      extractor.SetTag("abstract");
      extractor.Add("This is a sample text.");
      extractor.CloseTag(profile);

      Assert.AreEqual(1, extractor.Count);
      Assert.AreEqual(17, profile.n_words[0]);  // Thisisasampletext
      Assert.AreEqual(22, profile.n_words[1]);  // _T, Th, hi, ...
      Assert.AreEqual(17, profile.n_words[2]);  // _Th, Thi, his, ...

      // too short
      extractor.SetTag("abstract");
      extractor.Add("sample");
      extractor.CloseTag(profile);
      Assert.AreEqual(1, extractor.Count);

      // other tags
      extractor.SetTag("div");
      extractor.Add("This is a sample text which is enough long.");
      extractor.CloseTag(profile);
      Assert.AreEqual(1, extractor.Count);
    }

    [Test]
    public void testClear()
    {
      TagExtractor extractor = new TagExtractor("abstract", 10);

      extractor.SetTag("abstract");
      extractor.Add("This is a sample text.");
      Assert.AreEqual("This is a sample text.", extractor.StringBuilder.ToString());
      Assert.AreEqual("abstract", extractor.Tag);

      extractor.Clear();
      Assert.AreEqual("", extractor.StringBuilder.ToString());
      Assert.AreEqual(null, extractor.Tag);
    }
  }
}
