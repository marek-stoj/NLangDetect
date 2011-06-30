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
      Assert.AreEqual(3, NGram.N_GRAM);
    }

    [Test]
    public void testNormalizeWithLatin()
    {
      Assert.AreEqual(' ', NGram.normalize('\u0000'));
      Assert.AreEqual(' ', NGram.normalize('\u0009'));
      Assert.AreEqual(' ', NGram.normalize('\u0020'));
      Assert.AreEqual(' ', NGram.normalize('\u0030'));
      Assert.AreEqual(' ', NGram.normalize('\u0040'));
      Assert.AreEqual('\u0041', NGram.normalize('\u0041'));
      Assert.AreEqual('\u005a', NGram.normalize('\u005a'));
      Assert.AreEqual(' ', NGram.normalize('\u005b'));
      Assert.AreEqual(' ', NGram.normalize('\u0060'));
      Assert.AreEqual('\u0061', NGram.normalize('\u0061'));
      Assert.AreEqual('\u007a', NGram.normalize('\u007a'));
      Assert.AreEqual(' ', NGram.normalize('\u007b'));
      Assert.AreEqual(' ', NGram.normalize('\u007f'));
      Assert.AreEqual('\u0080', NGram.normalize('\u0080'));
      Assert.AreEqual(' ', NGram.normalize('\u00a0'));
      Assert.AreEqual('\u00a1', NGram.normalize('\u00a1'));
    }

    [Test]
    public void testNormalizeWithCJKKanji()
    {
      Assert.AreEqual('\u4E00', NGram.normalize('\u4E00'));
      Assert.AreEqual('\u4E01', NGram.normalize('\u4E01'));
      Assert.AreEqual('\u4E02', NGram.normalize('\u4E02'));
      Assert.AreEqual('\u4E01', NGram.normalize('\u4E03'));
      Assert.AreEqual('\u4E04', NGram.normalize('\u4E04'));
      Assert.AreEqual('\u4E05', NGram.normalize('\u4E05'));
      Assert.AreEqual('\u4E06', NGram.normalize('\u4E06'));
      Assert.AreEqual('\u4E07', NGram.normalize('\u4E07'));
      Assert.AreEqual('\u4E08', NGram.normalize('\u4E08'));
      Assert.AreEqual('\u4E09', NGram.normalize('\u4E09'));
      Assert.AreEqual('\u4E10', NGram.normalize('\u4E10'));
      Assert.AreEqual('\u4E11', NGram.normalize('\u4E11'));
      Assert.AreEqual('\u4E12', NGram.normalize('\u4E12'));
      Assert.AreEqual('\u4E13', NGram.normalize('\u4E13'));
      Assert.AreEqual('\u4E14', NGram.normalize('\u4E14'));
      Assert.AreEqual('\u4E15', NGram.normalize('\u4E15'));
      Assert.AreEqual('\u4E1e', NGram.normalize('\u4E1e'));
      Assert.AreEqual('\u4E1f', NGram.normalize('\u4E1f'));
      Assert.AreEqual('\u4E20', NGram.normalize('\u4E20'));
      Assert.AreEqual('\u4E21', NGram.normalize('\u4E21'));
      Assert.AreEqual('\u4E22', NGram.normalize('\u4E22'));
      Assert.AreEqual('\u4E23', NGram.normalize('\u4E23'));
      Assert.AreEqual('\u4E13', NGram.normalize('\u4E24'));
      Assert.AreEqual('\u4E13', NGram.normalize('\u4E25'));
      Assert.AreEqual('\u4E30', NGram.normalize('\u4E30'));
    }

    [Test]
    public void testNGram()
    {
      NGram ngram = new NGram();

      Assert.AreEqual(null, ngram.get(0));
      Assert.AreEqual(null, ngram.get(1));
      Assert.AreEqual(null, ngram.get(2));
      Assert.AreEqual(null, ngram.get(3));
      Assert.AreEqual(null, ngram.get(4));

      ngram.addChar(' ');
      Assert.AreEqual(null, ngram.get(1));
      Assert.AreEqual(null, ngram.get(2));
      Assert.AreEqual(null, ngram.get(3));

      ngram.addChar('A');
      Assert.AreEqual("A", ngram.get(1));
      Assert.AreEqual(" A", ngram.get(2));
      Assert.AreEqual(null, ngram.get(3));

      ngram.addChar('\u06cc');
      Assert.AreEqual("\u064a", ngram.get(1));
      Assert.AreEqual("A\u064a", ngram.get(2));
      Assert.AreEqual(" A\u064a", ngram.get(3));

      ngram.addChar('\u1ea0');
      Assert.AreEqual("\u1ec3", ngram.get(1));
      Assert.AreEqual("\u064a\u1ec3", ngram.get(2));
      Assert.AreEqual("A\u064a\u1ec3", ngram.get(3));

      ngram.addChar('\u3044');
      Assert.AreEqual("\u3042", ngram.get(1));
      Assert.AreEqual("\u1ec3\u3042", ngram.get(2));
      Assert.AreEqual("\u064a\u1ec3\u3042", ngram.get(3));

      ngram.addChar('\u30a4');
      Assert.AreEqual("\u30a2", ngram.get(1));
      Assert.AreEqual("\u3042\u30a2", ngram.get(2));
      Assert.AreEqual("\u1ec3\u3042\u30a2", ngram.get(3));

      ngram.addChar('\u3106');
      Assert.AreEqual("\u3105", ngram.get(1));
      Assert.AreEqual("\u30a2\u3105", ngram.get(2));
      Assert.AreEqual("\u3042\u30a2\u3105", ngram.get(3));

      ngram.addChar('\uac01');
      Assert.AreEqual("\uac00", ngram.get(1));
      Assert.AreEqual("\u3105\uac00", ngram.get(2));
      Assert.AreEqual("\u30a2\u3105\uac00", ngram.get(3));
      
      ngram.addChar('\u2010');
      Assert.AreEqual(null, ngram.get(1));
      Assert.AreEqual("\uac00 ", ngram.get(2));
      Assert.AreEqual("\u3105\uac00 ", ngram.get(3));

      ngram.addChar('a');
      Assert.AreEqual("a", ngram.get(1));
      Assert.AreEqual(" a", ngram.get(2));
      Assert.AreEqual(null, ngram.get(3));
    }
  }
}
