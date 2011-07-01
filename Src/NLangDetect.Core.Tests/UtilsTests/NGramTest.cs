using NLangDetect.Core.Utils;
using NUnit.Framework;

namespace NLangDetect.Core.Tests.UtilsTests
{
  [TestFixture]
  public class NGramTest
  {
    [Test]
    public void testConstants()
    {
      Assert.AreEqual(3, NGram.GramsCount);
    }

    [Test]
    public void testNormalizeWithLatin()
    {
      Assert.AreEqual(' ', NGram.Normalize('\u0000'));
      Assert.AreEqual(' ', NGram.Normalize('\u0009'));
      Assert.AreEqual(' ', NGram.Normalize('\u0020'));
      Assert.AreEqual(' ', NGram.Normalize('\u0030'));
      Assert.AreEqual(' ', NGram.Normalize('\u0040'));
      Assert.AreEqual('\u0041', NGram.Normalize('\u0041'));
      Assert.AreEqual('\u005a', NGram.Normalize('\u005a'));
      Assert.AreEqual(' ', NGram.Normalize('\u005b'));
      Assert.AreEqual(' ', NGram.Normalize('\u0060'));
      Assert.AreEqual('\u0061', NGram.Normalize('\u0061'));
      Assert.AreEqual('\u007a', NGram.Normalize('\u007a'));
      Assert.AreEqual(' ', NGram.Normalize('\u007b'));
      Assert.AreEqual(' ', NGram.Normalize('\u007f'));
      Assert.AreEqual('\u0080', NGram.Normalize('\u0080'));
      Assert.AreEqual(' ', NGram.Normalize('\u00a0'));
      Assert.AreEqual('\u00a1', NGram.Normalize('\u00a1'));
    }

    [Test]
    public void testNormalizeWithCJKKanji()
    {
      Assert.AreEqual('\u4E00', NGram.Normalize('\u4E00'));
      Assert.AreEqual('\u4E01', NGram.Normalize('\u4E01'));
      Assert.AreEqual('\u4E02', NGram.Normalize('\u4E02'));
      Assert.AreEqual('\u4E01', NGram.Normalize('\u4E03'));
      Assert.AreEqual('\u4E04', NGram.Normalize('\u4E04'));
      Assert.AreEqual('\u4E05', NGram.Normalize('\u4E05'));
      Assert.AreEqual('\u4E06', NGram.Normalize('\u4E06'));
      Assert.AreEqual('\u4E07', NGram.Normalize('\u4E07'));
      Assert.AreEqual('\u4E08', NGram.Normalize('\u4E08'));
      Assert.AreEqual('\u4E09', NGram.Normalize('\u4E09'));
      Assert.AreEqual('\u4E10', NGram.Normalize('\u4E10'));
      Assert.AreEqual('\u4E11', NGram.Normalize('\u4E11'));
      Assert.AreEqual('\u4E12', NGram.Normalize('\u4E12'));
      Assert.AreEqual('\u4E13', NGram.Normalize('\u4E13'));
      Assert.AreEqual('\u4E14', NGram.Normalize('\u4E14'));
      Assert.AreEqual('\u4E15', NGram.Normalize('\u4E15'));
      Assert.AreEqual('\u4E1e', NGram.Normalize('\u4E1e'));
      Assert.AreEqual('\u4E1f', NGram.Normalize('\u4E1f'));
      Assert.AreEqual('\u4E20', NGram.Normalize('\u4E20'));
      Assert.AreEqual('\u4E21', NGram.Normalize('\u4E21'));
      Assert.AreEqual('\u4E22', NGram.Normalize('\u4E22'));
      Assert.AreEqual('\u4E23', NGram.Normalize('\u4E23'));
      Assert.AreEqual('\u4E13', NGram.Normalize('\u4E24'));
      Assert.AreEqual('\u4E13', NGram.Normalize('\u4E25'));
      Assert.AreEqual('\u4E30', NGram.Normalize('\u4E30'));
    }

    [Test]
    public void testNGram()
    {
      NGram ngram = new NGram();

      Assert.AreEqual(null, ngram.Get(0));
      Assert.AreEqual(null, ngram.Get(1));
      Assert.AreEqual(null, ngram.Get(2));
      Assert.AreEqual(null, ngram.Get(3));
      Assert.AreEqual(null, ngram.Get(4));

      ngram.AddChar(' ');
      Assert.AreEqual(null, ngram.Get(1));
      Assert.AreEqual(null, ngram.Get(2));
      Assert.AreEqual(null, ngram.Get(3));

      ngram.AddChar('A');
      Assert.AreEqual("A", ngram.Get(1));
      Assert.AreEqual(" A", ngram.Get(2));
      Assert.AreEqual(null, ngram.Get(3));

      ngram.AddChar('\u06cc');
      Assert.AreEqual("\u064a", ngram.Get(1));
      Assert.AreEqual("A\u064a", ngram.Get(2));
      Assert.AreEqual(" A\u064a", ngram.Get(3));

      ngram.AddChar('\u1ea0');
      Assert.AreEqual("\u1ec3", ngram.Get(1));
      Assert.AreEqual("\u064a\u1ec3", ngram.Get(2));
      Assert.AreEqual("A\u064a\u1ec3", ngram.Get(3));

      ngram.AddChar('\u3044');
      Assert.AreEqual("\u3042", ngram.Get(1));
      Assert.AreEqual("\u1ec3\u3042", ngram.Get(2));
      Assert.AreEqual("\u064a\u1ec3\u3042", ngram.Get(3));

      ngram.AddChar('\u30a4');
      Assert.AreEqual("\u30a2", ngram.Get(1));
      Assert.AreEqual("\u3042\u30a2", ngram.Get(2));
      Assert.AreEqual("\u1ec3\u3042\u30a2", ngram.Get(3));

      ngram.AddChar('\u3106');
      Assert.AreEqual("\u3105", ngram.Get(1));
      Assert.AreEqual("\u30a2\u3105", ngram.Get(2));
      Assert.AreEqual("\u3042\u30a2\u3105", ngram.Get(3));

      ngram.AddChar('\uac01');
      Assert.AreEqual("\uac00", ngram.Get(1));
      Assert.AreEqual("\u3105\uac00", ngram.Get(2));
      Assert.AreEqual("\u30a2\u3105\uac00", ngram.Get(3));
      
      ngram.AddChar('\u2010');
      Assert.AreEqual(null, ngram.Get(1));
      Assert.AreEqual("\uac00 ", ngram.Get(2));
      Assert.AreEqual("\u3105\uac00 ", ngram.Get(3));

      ngram.AddChar('a');
      Assert.AreEqual("a", ngram.Get(1));
      Assert.AreEqual(" a", ngram.Get(2));
      Assert.AreEqual(null, ngram.Get(3));
    }
  }
}
