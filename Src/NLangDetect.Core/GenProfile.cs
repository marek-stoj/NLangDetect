using System;
using System.IO.Compression;
using System.Xml;
using NLangDetect.Core.Utils;
using System.IO;

namespace NLangDetect.Core
{
  // TODO IMM HI: xml reader not tested
  public class GenProfile
  {
    public static LangProfile load(string lang, string file)
    {
      LangProfile profile = new LangProfile(lang);
      TagExtractor tagextractor = new TagExtractor("abstract", 100);
      Stream inputStream = null;

      try
      {
        inputStream = File.OpenRead(file);

        if (Path.GetExtension(file).ToUpper() == ".GZ")
        {
          inputStream = new GZipStream(inputStream, CompressionMode.Decompress);
        }

        using (XmlReader xmlReader = XmlReader.Create(inputStream))
        {
          while (xmlReader.Read())
          {
            switch (xmlReader.NodeType)
            {
              case XmlNodeType.Element:
                tagextractor.setTag(xmlReader.Name);
                break;

              case XmlNodeType.Text:
                tagextractor.add(xmlReader.Value);
                break;

              case XmlNodeType.EndElement:
                tagextractor.closeTag(profile);
                break;

              default:
                break;
            }
          }
        }
      }
      finally
      {
        if (inputStream != null)
        {
          inputStream.Close();
        }
      }

      Console.WriteLine(lang + ": " + tagextractor.count());

      return profile;
    }
  }
}
