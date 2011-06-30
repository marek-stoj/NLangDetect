// TODO IMM HI: check which classes can be made internal?

using System.Collections.Generic;
using System.Text;
using NLangDetect.Core.Extensions;

namespace NLangDetect.Core.Utils
{
  public class NGram
  {
    public const int N_GRAM = 3;

    private static readonly string LATIN1_EXCLUDED = Messages.getString("NGram.LATIN1_EXCLUDE");

    private static readonly string[] CJK_CLASS =
      {
        #region CJK classes

        Messages.getString("NGram.KANJI_1_0"),
        Messages.getString("NGram.KANJI_1_2"),
        Messages.getString("NGram.KANJI_1_4"),
        Messages.getString("NGram.KANJI_1_8"),
        Messages.getString("NGram.KANJI_1_11"),
        Messages.getString("NGram.KANJI_1_12"),
        Messages.getString("NGram.KANJI_1_13"),
        Messages.getString("NGram.KANJI_1_14"),
        Messages.getString("NGram.KANJI_1_16"),
        Messages.getString("NGram.KANJI_1_18"),
        Messages.getString("NGram.KANJI_1_22"),
        Messages.getString("NGram.KANJI_1_27"),
        Messages.getString("NGram.KANJI_1_29"),
        Messages.getString("NGram.KANJI_1_31"),
        Messages.getString("NGram.KANJI_1_35"),
        Messages.getString("NGram.KANJI_2_0"),
        Messages.getString("NGram.KANJI_2_1"),
        Messages.getString("NGram.KANJI_2_4"),
        Messages.getString("NGram.KANJI_2_9"),
        Messages.getString("NGram.KANJI_2_10"),
        Messages.getString("NGram.KANJI_2_11"),
        Messages.getString("NGram.KANJI_2_12"),
        Messages.getString("NGram.KANJI_2_13"),
        Messages.getString("NGram.KANJI_2_15"),
        Messages.getString("NGram.KANJI_2_16"),
        Messages.getString("NGram.KANJI_2_18"),
        Messages.getString("NGram.KANJI_2_21"),
        Messages.getString("NGram.KANJI_2_22"),
        Messages.getString("NGram.KANJI_2_23"),
        Messages.getString("NGram.KANJI_2_28"),
        Messages.getString("NGram.KANJI_2_29"),
        Messages.getString("NGram.KANJI_2_30"),
        Messages.getString("NGram.KANJI_2_31"),
        Messages.getString("NGram.KANJI_2_32"),
        Messages.getString("NGram.KANJI_2_35"),
        Messages.getString("NGram.KANJI_2_36"),
        Messages.getString("NGram.KANJI_2_37"),
        Messages.getString("NGram.KANJI_2_38"),
        Messages.getString("NGram.KANJI_3_1"),
        Messages.getString("NGram.KANJI_3_2"),
        Messages.getString("NGram.KANJI_3_3"),
        Messages.getString("NGram.KANJI_3_4"),
        Messages.getString("NGram.KANJI_3_5"),
        Messages.getString("NGram.KANJI_3_8"),
        Messages.getString("NGram.KANJI_3_9"),
        Messages.getString("NGram.KANJI_3_11"),
        Messages.getString("NGram.KANJI_3_12"),
        Messages.getString("NGram.KANJI_3_13"),
        Messages.getString("NGram.KANJI_3_15"),
        Messages.getString("NGram.KANJI_3_16"),
        Messages.getString("NGram.KANJI_3_18"),
        Messages.getString("NGram.KANJI_3_19"),
        Messages.getString("NGram.KANJI_3_22"),
        Messages.getString("NGram.KANJI_3_23"),
        Messages.getString("NGram.KANJI_3_27"),
        Messages.getString("NGram.KANJI_3_29"),
        Messages.getString("NGram.KANJI_3_30"),
        Messages.getString("NGram.KANJI_3_31"),
        Messages.getString("NGram.KANJI_3_32"),
        Messages.getString("NGram.KANJI_3_35"),
        Messages.getString("NGram.KANJI_3_36"),
        Messages.getString("NGram.KANJI_3_37"),
        Messages.getString("NGram.KANJI_3_38"),
        Messages.getString("NGram.KANJI_4_0"),
        Messages.getString("NGram.KANJI_4_9"),
        Messages.getString("NGram.KANJI_4_10"),
        Messages.getString("NGram.KANJI_4_16"),
        Messages.getString("NGram.KANJI_4_17"),
        Messages.getString("NGram.KANJI_4_18"),
        Messages.getString("NGram.KANJI_4_22"),
        Messages.getString("NGram.KANJI_4_24"),
        Messages.getString("NGram.KANJI_4_28"),
        Messages.getString("NGram.KANJI_4_34"),
        Messages.getString("NGram.KANJI_4_39"),
        Messages.getString("NGram.KANJI_5_10"),
        Messages.getString("NGram.KANJI_5_11"),
        Messages.getString("NGram.KANJI_5_12"),
        Messages.getString("NGram.KANJI_5_13"),
        Messages.getString("NGram.KANJI_5_14"),
        Messages.getString("NGram.KANJI_5_18"),
        Messages.getString("NGram.KANJI_5_26"),
        Messages.getString("NGram.KANJI_5_29"),
        Messages.getString("NGram.KANJI_5_34"),
        Messages.getString("NGram.KANJI_5_39"),
        Messages.getString("NGram.KANJI_6_0"),
        Messages.getString("NGram.KANJI_6_3"),
        Messages.getString("NGram.KANJI_6_9"),
        Messages.getString("NGram.KANJI_6_10"),
        Messages.getString("NGram.KANJI_6_11"),
        Messages.getString("NGram.KANJI_6_12"),
        Messages.getString("NGram.KANJI_6_16"),
        Messages.getString("NGram.KANJI_6_18"),
        Messages.getString("NGram.KANJI_6_20"),
        Messages.getString("NGram.KANJI_6_21"),
        Messages.getString("NGram.KANJI_6_22"),
        Messages.getString("NGram.KANJI_6_23"),
        Messages.getString("NGram.KANJI_6_25"),
        Messages.getString("NGram.KANJI_6_28"),
        Messages.getString("NGram.KANJI_6_29"),
        Messages.getString("NGram.KANJI_6_30"),
        Messages.getString("NGram.KANJI_6_32"),
        Messages.getString("NGram.KANJI_6_34"),
        Messages.getString("NGram.KANJI_6_35"),
        Messages.getString("NGram.KANJI_6_37"),
        Messages.getString("NGram.KANJI_6_39"),
        Messages.getString("NGram.KANJI_7_0"),
        Messages.getString("NGram.KANJI_7_3"),
        Messages.getString("NGram.KANJI_7_6"),
        Messages.getString("NGram.KANJI_7_7"),
        Messages.getString("NGram.KANJI_7_9"),
        Messages.getString("NGram.KANJI_7_11"),
        Messages.getString("NGram.KANJI_7_12"),
        Messages.getString("NGram.KANJI_7_13"),
        Messages.getString("NGram.KANJI_7_16"),
        Messages.getString("NGram.KANJI_7_18"),
        Messages.getString("NGram.KANJI_7_19"),
        Messages.getString("NGram.KANJI_7_20"),
        Messages.getString("NGram.KANJI_7_21"),
        Messages.getString("NGram.KANJI_7_23"),
        Messages.getString("NGram.KANJI_7_25"),
        Messages.getString("NGram.KANJI_7_28"),
        Messages.getString("NGram.KANJI_7_29"),
        Messages.getString("NGram.KANJI_7_32"),
        Messages.getString("NGram.KANJI_7_33"),
        Messages.getString("NGram.KANJI_7_35"),
        Messages.getString("NGram.KANJI_7_37"),

        #endregion
      };

    private static readonly Dictionary<char, char> cjk_map;

    private StringBuilder grams_;
    private bool capitalword_;

    static NGram()
    {
      cjk_map = new Dictionary<char, char>();

      foreach (string cjk_list in CJK_CLASS)
      {
        char representative = cjk_list[0];

        for (int i = 0; i < cjk_list.Length; ++i)
        {
          cjk_map.Add(cjk_list[i], representative);
        }
      }
    }

    public NGram()
    {
      grams_ = new StringBuilder(" ");
      capitalword_ = false;
    }

    public void addChar(char ch)
    {
      ch = normalize(ch);
      char lastchar = grams_[grams_.Length - 1];
      if (lastchar == ' ')
      {
        grams_ = new StringBuilder(" ");
        capitalword_ = false;
        if (ch == ' ') return;
      }
      else if (grams_.Length >= N_GRAM)
      {
        grams_.Remove(0, 1);
      }
      grams_.Append(ch);

      if (char.IsUpper(ch))
      {
        if (char.IsUpper(lastchar)) capitalword_ = true;
      }
      else
      {
        capitalword_ = false;
      }
    }

    public string get(int n)
    {
      if (capitalword_)
      {
        return null;
      }

      int len = grams_.Length;

      if (n < 1 || n > 3 || len < n)
      {
        return null;
      }

      if (n == 1)
      {
        char ch = grams_[len - 1];

        if (ch == ' ')
        {
          return null;
        }

        return ch.ToString();
      }

      // TODO IMM HI: is ToString() here effective?
      return grams_.ToString().SubSequence(len - n, len);
    }

    // TODO IMM HI: implement
    public static char normalize(char ch)
    {
      UnicodeBlock? unicodeBlock = ch.GetUnicodeBlock();

      if (!unicodeBlock.HasValue)
      {
        return ch;
      }

      switch (unicodeBlock.Value)
      {
        case UnicodeBlock.BASIC_LATIN:
          {
            if (ch < 'A' || (ch < 'a' && ch > 'Z') || ch > 'z')
            {
              return ' ';
            }

            break;
          }

        case UnicodeBlock.LATIN_1_SUPPLEMENT:
          {
            if (LATIN1_EXCLUDED.IndexOf(ch) >= 0)
            {
              return ' ';
            }

            break;
          }

        case UnicodeBlock.GENERAL_PUNCTUATION:
          {
            return ' ';
          }

        case UnicodeBlock.ARABIC:
          {
            if (ch == '\u06cc')
            {
              return '\u064a';
            }

            break;
          }

        case UnicodeBlock.LATIN_EXTENDED_ADDITIONAL:
          {
            if (ch >= '\u1ea0')
            {
              return '\u1ec3';
            }

            break;
          }

        case UnicodeBlock.HIRAGANA:
          {
            return '\u3042';
          }

        case UnicodeBlock.KATAKANA:
          {
            return '\u30a2';
          }

        case UnicodeBlock.BOPOMOFO:
        case UnicodeBlock.BOPOMOFO_EXTENDED:
          {
            return '\u3105';
          }

        case UnicodeBlock.CJK_UNIFIED_IDEOGRAPHS:
          {
            if (cjk_map.ContainsKey(ch))
            {
              return cjk_map[ch];
            }

            break;
          }

        case UnicodeBlock.HANGUL_SYLLABLES:
          {
            return '\uac00';
          }
      }

      return ch;
    }
  }
}
