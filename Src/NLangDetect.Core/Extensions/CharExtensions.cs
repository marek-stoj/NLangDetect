﻿using System;

namespace NLangDetect.Core.Extensions
{
  public static class CharExtensions
  {
    private const int MIN_CODE_POINT = 0x000000;
    private const int MAX_CODE_POINT = 0x10ffff;

    private static readonly int[] _unicodeBlockStarts =
      {
        #region Unicode block starts

        0x0000, // Basic Latin
        0x0080, // Latin-1 Supplement
        0x0100, // Latin Extended-A
        0x0180, // Latin Extended-B
        0x0250, // IPA Extensions
        0x02B0, // Spacing Modifier Letters
        0x0300, // Combining Diacritical Marks
        0x0370, // Greek and Coptic
        0x0400, // Cyrillic
        0x0500, // Cyrillic Supplementary
        0x0530, // Armenian
        0x0590, // Hebrew
        0x0600, // Arabic
        0x0700, // Syriac
        0x0750, // unassigned
        0x0780, // Thaana
        0x07C0, // unassigned
        0x0900, // Devanagari
        0x0980, // Bengali
        0x0A00, // Gurmukhi
        0x0A80, // Gujarati
        0x0B00, // Oriya
        0x0B80, // Tamil
        0x0C00, // Telugu
        0x0C80, // Kannada
        0x0D00, // Malayalam
        0x0D80, // Sinhala
        0x0E00, // Thai
        0x0E80, // Lao
        0x0F00, // Tibetan
        0x1000, // Myanmar
        0x10A0, // Georgian
        0x1100, // Hangul Jamo
        0x1200, // Ethiopic
        0x1380, // unassigned
        0x13A0, // Cherokee
        0x1400, // Unified Canadian Aboriginal Syllabics
        0x1680, // Ogham
        0x16A0, // Runic
        0x1700, // Tagalog
        0x1720, // Hanunoo
        0x1740, // Buhid
        0x1760, // Tagbanwa
        0x1780, // Khmer
        0x1800, // Mongolian
        0x18B0, // unassigned
        0x1900, // Limbu
        0x1950, // Tai Le
        0x1980, // unassigned
        0x19E0, // Khmer Symbols
        0x1A00, // unassigned
        0x1D00, // Phonetic Extensions
        0x1D80, // unassigned
        0x1E00, // Latin Extended Additional
        0x1F00, // Greek Extended
        0x2000, // General Punctuation
        0x2070, // Superscripts and Subscripts
        0x20A0, // Currency Symbols
        0x20D0, // Combining Diacritical Marks for Symbols
        0x2100, // Letterlike Symbols
        0x2150, // Number Forms
        0x2190, // Arrows
        0x2200, // Mathematical Operators
        0x2300, // Miscellaneous Technical
        0x2400, // Control Pictures
        0x2440, // Optical Character Recognition
        0x2460, // Enclosed Alphanumerics
        0x2500, // Box Drawing
        0x2580, // Block Elements
        0x25A0, // Geometric Shapes
        0x2600, // Miscellaneous Symbols
        0x2700, // Dingbats
        0x27C0, // Miscellaneous Mathematical Symbols-A
        0x27F0, // Supplemental Arrows-A
        0x2800, // Braille Patterns
        0x2900, // Supplemental Arrows-B
        0x2980, // Miscellaneous Mathematical Symbols-B
        0x2A00, // Supplemental Mathematical Operators
        0x2B00, // Miscellaneous Symbols and Arrows
        0x2C00, // unassigned
        0x2E80, // CJK Radicals Supplement
        0x2F00, // Kangxi Radicals
        0x2FE0, // unassigned
        0x2FF0, // Ideographic Description Characters
        0x3000, // CJK Symbols and Punctuation
        0x3040, // Hiragana
        0x30A0, // Katakana
        0x3100, // Bopomofo
        0x3130, // Hangul Compatibility Jamo
        0x3190, // Kanbun
        0x31A0, // Bopomofo Extended
        0x31C0, // unassigned
        0x31F0, // Katakana Phonetic Extensions
        0x3200, // Enclosed CJK Letters and Months
        0x3300, // CJK Compatibility
        0x3400, // CJK Unified Ideographs Extension A
        0x4DC0, // Yijing Hexagram Symbols
        0x4E00, // CJK Unified Ideographs
        0xA000, // Yi Syllables
        0xA490, // Yi Radicals
        0xA4D0, // unassigned
        0xAC00, // Hangul Syllables
        0xD7B0, // unassigned
        0xD800, // High Surrogates
        0xDB80, // High Private Use Surrogates
        0xDC00, // Low Surrogates
        0xE000, // Private Use
        0xF900, // CJK Compatibility Ideographs
        0xFB00, // Alphabetic Presentation Forms
        0xFB50, // Arabic Presentation Forms-A
        0xFE00, // Variation Selectors
        0xFE10, // unassigned
        0xFE20, // Combining Half Marks
        0xFE30, // CJK Compatibility Forms
        0xFE50, // Small Form Variants
        0xFE70, // Arabic Presentation Forms-B
        0xFF00, // Halfwidth and Fullwidth Forms
        0xFFF0, // Specials
        0x10000, // Linear B Syllabary
        0x10080, // Linear B Ideograms
        0x10100, // Aegean Numbers
        0x10140, // unassigned
        0x10300, // Old Italic
        0x10330, // Gothic
        0x10350, // unassigned
        0x10380, // Ugaritic
        0x103A0, // unassigned
        0x10400, // Deseret
        0x10450, // Shavian
        0x10480, // Osmanya
        0x104B0, // unassigned
        0x10800, // Cypriot Syllabary
        0x10840, // unassigned
        0x1D000, // Byzantine Musical Symbols
        0x1D100, // Musical Symbols
        0x1D200, // unassigned
        0x1D300, // Tai Xuan Jing Symbols
        0x1D360, // unassigned
        0x1D400, // Mathematical Alphanumeric Symbols
        0x1D800, // unassigned
        0x20000, // CJK Unified Ideographs Extension B
        0x2A6E0, // unassigned
        0x2F800, // CJK Compatibility Ideographs Supplement
        0x2FA20, // unassigned
        0xE0000, // Tags
        0xE0080, // unassigned
        0xE0100, // Variation Selectors Supplement
        0xE01F0, // unassigned
        0xF0000, // Supplementary Private Use Area-A
        0x100000, // Supplementary Private Use Area-B

        #endregion
      };

    private static readonly UnicodeBlock?[] _unicodeBlocks =
      {
        #region Unicode blocks
        UnicodeBlock.BASIC_LATIN,
        UnicodeBlock.LATIN_1_SUPPLEMENT,
        UnicodeBlock.LATIN_EXTENDED_A,
        UnicodeBlock.LATIN_EXTENDED_B,
        UnicodeBlock.IPA_EXTENSIONS,
        UnicodeBlock.SPACING_MODIFIER_LETTERS,
        UnicodeBlock.COMBINING_DIACRITICAL_MARKS,
        UnicodeBlock.GREEK,
        UnicodeBlock.CYRILLIC,
        UnicodeBlock.CYRILLIC_SUPPLEMENTARY,
        UnicodeBlock.ARMENIAN,
        UnicodeBlock.HEBREW,
        UnicodeBlock.ARABIC,
        UnicodeBlock.SYRIAC,
        null,
        UnicodeBlock.THAANA,
        null,
        UnicodeBlock.DEVANAGARI,
        UnicodeBlock.BENGALI,
        UnicodeBlock.GURMUKHI,
        UnicodeBlock.GUJARATI,
        UnicodeBlock.ORIYA,
        UnicodeBlock.TAMIL,
        UnicodeBlock.TELUGU,
        UnicodeBlock.KANNADA,
        UnicodeBlock.MALAYALAM,
        UnicodeBlock.SINHALA,
        UnicodeBlock.THAI,
        UnicodeBlock.LAO,
        UnicodeBlock.TIBETAN,
        UnicodeBlock.MYANMAR,
        UnicodeBlock.GEORGIAN,
        UnicodeBlock.HANGUL_JAMO,
        UnicodeBlock.ETHIOPIC,
        null,
        UnicodeBlock.CHEROKEE,
        UnicodeBlock.UNIFIED_CANADIAN_ABORIGINAL_SYLLABICS,
        UnicodeBlock.OGHAM,
        UnicodeBlock.RUNIC,
        UnicodeBlock.TAGALOG,
        UnicodeBlock.HANUNOO,
        UnicodeBlock.BUHID,
        UnicodeBlock.TAGBANWA,
        UnicodeBlock.KHMER,
        UnicodeBlock.MONGOLIAN,
        null,
        UnicodeBlock.LIMBU,
        UnicodeBlock.TAI_LE,
        null,
        UnicodeBlock.KHMER_SYMBOLS,
        null,
        UnicodeBlock.PHONETIC_EXTENSIONS,
        null,
        UnicodeBlock.LATIN_EXTENDED_ADDITIONAL,
        UnicodeBlock.GREEK_EXTENDED,
        UnicodeBlock.GENERAL_PUNCTUATION,
        UnicodeBlock.SUPERSCRIPTS_AND_SUBSCRIPTS,
        UnicodeBlock.CURRENCY_SYMBOLS,
        UnicodeBlock.COMBINING_MARKS_FOR_SYMBOLS,
        UnicodeBlock.LETTERLIKE_SYMBOLS,
        UnicodeBlock.NUMBER_FORMS,
        UnicodeBlock.ARROWS,
        UnicodeBlock.MATHEMATICAL_OPERATORS,
        UnicodeBlock.MISCELLANEOUS_TECHNICAL,
        UnicodeBlock.CONTROL_PICTURES,
        UnicodeBlock.OPTICAL_CHARACTER_RECOGNITION,
        UnicodeBlock.ENCLOSED_ALPHANUMERICS,
        UnicodeBlock.BOX_DRAWING,
        UnicodeBlock.BLOCK_ELEMENTS,
        UnicodeBlock.GEOMETRIC_SHAPES,
        UnicodeBlock.MISCELLANEOUS_SYMBOLS,
        UnicodeBlock.DINGBATS,
        UnicodeBlock.MISCELLANEOUS_MATHEMATICAL_SYMBOLS_A,
        UnicodeBlock.SUPPLEMENTAL_ARROWS_A,
        UnicodeBlock.BRAILLE_PATTERNS,
        UnicodeBlock.SUPPLEMENTAL_ARROWS_B,
        UnicodeBlock.MISCELLANEOUS_MATHEMATICAL_SYMBOLS_B,
        UnicodeBlock.SUPPLEMENTAL_MATHEMATICAL_OPERATORS,
        UnicodeBlock.MISCELLANEOUS_SYMBOLS_AND_ARROWS,
        null,
        UnicodeBlock.CJK_RADICALS_SUPPLEMENT,
        UnicodeBlock.KANGXI_RADICALS,
        null,
        UnicodeBlock.IDEOGRAPHIC_DESCRIPTION_CHARACTERS,
        UnicodeBlock.CJK_SYMBOLS_AND_PUNCTUATION,
        UnicodeBlock.HIRAGANA,
        UnicodeBlock.KATAKANA,
        UnicodeBlock.BOPOMOFO,
        UnicodeBlock.HANGUL_COMPATIBILITY_JAMO,
        UnicodeBlock.KANBUN,
        UnicodeBlock.BOPOMOFO_EXTENDED,
        null,
        UnicodeBlock.KATAKANA_PHONETIC_EXTENSIONS,
        UnicodeBlock.ENCLOSED_CJK_LETTERS_AND_MONTHS,
        UnicodeBlock.CJK_COMPATIBILITY,
        UnicodeBlock.CJK_UNIFIED_IDEOGRAPHS_EXTENSION_A,
        UnicodeBlock.YIJING_HEXAGRAM_SYMBOLS,
        UnicodeBlock.CJK_UNIFIED_IDEOGRAPHS,
        UnicodeBlock.YI_SYLLABLES,
        UnicodeBlock.YI_RADICALS,
        null,
        UnicodeBlock.HANGUL_SYLLABLES,
        null,
        UnicodeBlock.HIGH_SURROGATES,
        UnicodeBlock.HIGH_PRIVATE_USE_SURROGATES,
        UnicodeBlock.LOW_SURROGATES,
        UnicodeBlock.PRIVATE_USE_AREA,
        UnicodeBlock.CJK_COMPATIBILITY_IDEOGRAPHS,
        UnicodeBlock.ALPHABETIC_PRESENTATION_FORMS,
        UnicodeBlock.ARABIC_PRESENTATION_FORMS_A,
        UnicodeBlock.VARIATION_SELECTORS,
        null,
        UnicodeBlock.COMBINING_HALF_MARKS,
        UnicodeBlock.CJK_COMPATIBILITY_FORMS,
        UnicodeBlock.SMALL_FORM_VARIANTS,
        UnicodeBlock.ARABIC_PRESENTATION_FORMS_B,
        UnicodeBlock.HALFWIDTH_AND_FULLWIDTH_FORMS,
        UnicodeBlock.SPECIALS,
        UnicodeBlock.LINEAR_B_SYLLABARY,
        UnicodeBlock.LINEAR_B_IDEOGRAMS,
        UnicodeBlock.AEGEAN_NUMBERS,
        null,
        UnicodeBlock.OLD_ITALIC,
        UnicodeBlock.GOTHIC,
        null,
        UnicodeBlock.UGARITIC,
        null,
        UnicodeBlock.DESERET,
        UnicodeBlock.SHAVIAN,
        UnicodeBlock.OSMANYA,
        null,
        UnicodeBlock.CYPRIOT_SYLLABARY,
        null,
        UnicodeBlock.BYZANTINE_MUSICAL_SYMBOLS,
        UnicodeBlock.MUSICAL_SYMBOLS,
        null,
        UnicodeBlock.TAI_XUAN_JING_SYMBOLS,
        null,
        UnicodeBlock.MATHEMATICAL_ALPHANUMERIC_SYMBOLS,
        null,
        UnicodeBlock.CJK_UNIFIED_IDEOGRAPHS_EXTENSION_B,
        null,
        UnicodeBlock.CJK_COMPATIBILITY_IDEOGRAPHS_SUPPLEMENT,
        null,
        UnicodeBlock.TAGS,
        null,
        UnicodeBlock.VARIATION_SELECTORS_SUPPLEMENT,
        null,
        UnicodeBlock.SUPPLEMENTARY_PRIVATE_USE_AREA_A,
        UnicodeBlock.SUPPLEMENTARY_PRIVATE_USE_AREA_B,

        #endregion
      };

    #region Public methods

    /// <remarks>
    /// Taken from JDK source: http://grepcode.com/file/repository.grepcode.com/java/root/jdk/openjdk/6-b14/java/lang/Character.java#Character.UnicodeBlock.0LATIN_EXTENDED_ADDITIONAL
    /// </remarks>
    public static UnicodeBlock? GetUnicodeBlock(this char ch)
    {
      int codePoint = ch;

      if (!IsValidCodePoint(codePoint))
      {
        throw new ArgumentException("Argument is not a valid code point.", "ch");
      }

      int top, bottom, current;

      bottom = 0;
      top = _unicodeBlockStarts.Length;
      current = top / 2;

      // invariant: top > current >= bottom && codePoint >= unicodeBlockStarts[bottom]
      while (top - bottom > 1)
      {
        if (codePoint >= _unicodeBlockStarts[current])
        {
          bottom = current;
        }
        else
        {
          top = current;
        }

        current = (top + bottom) / 2;
      }

      return _unicodeBlocks[current];
    }

    #endregion

    #region Private helper methods

    private static bool IsValidCodePoint(int codePoint)
    {
      return codePoint >= MIN_CODE_POINT && codePoint <= MAX_CODE_POINT;
    }

    #endregion
  }
}
