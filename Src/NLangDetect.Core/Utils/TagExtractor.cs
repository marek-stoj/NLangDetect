using System.Text;

namespace NLangDetect.Core.Utils
{
  public class TagExtractor
  {
    // TODO IMM HI: do the really need to be internal?
    internal string target_;
    internal int threshold_;
    internal StringBuilder buf_;
    internal string tag_;

    private int count_;

    public TagExtractor(string tag, int threshold)
    {
      target_ = tag;
      threshold_ = threshold;
      count_ = 0;
      clear();
    }

    public int count()
    {
      return count_;
    }

    public void clear()
    {
      buf_ = new StringBuilder();
      tag_ = null;
    }

    public void setTag(string tag)
    {
      tag_ = tag;
    }

    public void add(string line)
    {
      if (tag_ == target_ && line != null)
      {
        buf_.Append(line);
      }
    }

    public void closeTag(LangProfile profile)
    {
      if (profile != null && tag_ == target_ && buf_.Length > threshold_)
      {
        var gram = new NGram();

        for (int i = 0; i < buf_.Length; ++i)
        {
          gram.addChar(buf_[i]);

          for (int n = 1; n <= NGram.N_GRAM; ++n)
          {
            profile.add(gram.get(n));
          }
        }

        ++count_;
      }

      clear();
    }
  }
}
