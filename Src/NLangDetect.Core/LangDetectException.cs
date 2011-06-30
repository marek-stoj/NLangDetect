using System;

namespace NLangDetect.Core
{
  [Serializable]
  public class LangDetectException : Exception
  {
    public LangDetectException(string message, ErrorCode errorCode)
      : base(message)
    {
      ErrorCode = errorCode;
    }

    public ErrorCode ErrorCode { get; private set; }
  }
}
