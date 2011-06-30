using NUnit.Framework;

namespace NLangDetect.Core.Tests
{
  [TestFixture]
  public class LanguageTests {

    [Test]
    public void TestLanguage() {
      Language language1 = new Language(null, 0);

      Assert.AreEqual(null, language1.Name);
      Assert.AreEqual(0.0, language1.Probability, 0.0001);
      Assert.AreEqual("", language1.ToString());
        
      Language language2 = new Language("en", 1.0);

      Assert.AreEqual("en", language2.Name);
      Assert.AreEqual(1.0, language2.Probability, 0.0001);
      Assert.AreEqual("en:1.00", language2.ToString());
    }
  }
}
