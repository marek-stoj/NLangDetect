using System.Globalization;

namespace NLangDetect.Core
{
  // TODO IMM HI: name??
  public class Language
  {
    public Language(string name, double probability)
    {
      Name = name;
      Probability = probability;
    }

    public override string ToString()
    {
      if (Name == null)
      {
        return "";
      }

      return
        string.Format(
          CultureInfo.InvariantCulture.NumberFormat,
          "{0}:{1:F6}",
          Name,
          Probability);
    }

    public string Name { get; set; }

    public double Probability { get; set; }
  }
}
