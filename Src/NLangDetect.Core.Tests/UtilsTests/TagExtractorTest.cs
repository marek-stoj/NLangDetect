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

      Assert.AreEqual(null, extractor1.target_);
      Assert.AreEqual(0, extractor1.threshold_);

      TagExtractor extractor2 = new TagExtractor("abstract", 10);

      Assert.AreEqual("abstract", extractor2.target_);
      Assert.AreEqual(10, extractor2.threshold_);
    }

    [Test]
    public void testSetTag()
    {
      TagExtractor extractor = new TagExtractor(null, 0);

      extractor.setTag("");

      Assert.AreEqual("", extractor.tag_);

      extractor.setTag(null);

      Assert.AreEqual(null, extractor.tag_);
    }

    [Test]
    public void testAdd()
    {
      TagExtractor extractor = new TagExtractor(null, 0);

      extractor.add("");
      extractor.add(null);    // ignore
    }

    [Test]
    public void testCloseTag()
    {
      TagExtractor extractor = new TagExtractor(null, 0);
      LangProfile profile = null;

      extractor.closeTag(profile);    // ignore
    }

    [Test]
    public void testNormalScenario()
    {
      TagExtractor extractor = new TagExtractor("abstract", 10);

      Assert.AreEqual(0, extractor.count());

      LangProfile profile = new LangProfile("en");

      // normal
      extractor.setTag("abstract");
      extractor.add("This is a sample text.");
      extractor.closeTag(profile);

      Assert.AreEqual(1, extractor.count());
      Assert.AreEqual(17, profile.n_words[0]);  // Thisisasampletext
      Assert.AreEqual(22, profile.n_words[1]);  // _T, Th, hi, ...
      Assert.AreEqual(17, profile.n_words[2]);  // _Th, Thi, his, ...

      // too short
      extractor.setTag("abstract");
      extractor.add("sample");
      extractor.closeTag(profile);
      Assert.AreEqual(1, extractor.count());

      // other tags
      extractor.setTag("div");
      extractor.add("This is a sample text which is enough long.");
      extractor.closeTag(profile);
      Assert.AreEqual(1, extractor.count());
    }

    [Test]
    public void testClear()
    {
      TagExtractor extractor = new TagExtractor("abstract", 10);

      extractor.setTag("abstract");
      extractor.add("This is a sample text.");
      Assert.AreEqual("This is a sample text.", extractor.buf_.ToString());
      Assert.AreEqual("abstract", extractor.tag_);

      extractor.clear();
      Assert.AreEqual("", extractor.buf_.ToString());
      Assert.AreEqual(null, extractor.tag_);
    }
  }
}
