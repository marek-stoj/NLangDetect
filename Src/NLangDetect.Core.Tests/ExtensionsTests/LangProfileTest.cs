using NUnit.Framework;
using NLangDetect.Core.Extensions;

namespace NLangDetect.Core.Tests.ExtensionsTests
{
  [TestFixture]
  public class StringExtensionsTests
  {
    [Test]
    public void Test_SubSequence()
    {
      Assert.AreEqual("", "abc".SubSequence(0, 0));
      Assert.AreEqual("", "abc".SubSequence(1, 1));
      Assert.AreEqual("abc", "XabcX".SubSequence(1, 4));
      // TODO IMM ME: more tests
    }
  }
}
