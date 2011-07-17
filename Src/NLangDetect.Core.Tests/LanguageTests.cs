using NUnit.Framework;

namespace NLangDetect.Core.Tests
{
  [TestFixture]
  public class LanguageTests {

    [Test]
    public void TestLanguage() {
      Language language = new Language(LanguageName.En, 1.0);

      Assert.AreEqual(LanguageName.En, language.Name);
      Assert.AreEqual(1.0, language.Probability, 0.0001);
      Assert.AreEqual(LanguageName.En + ":1.000000", language.ToString());
    }
  }
}
