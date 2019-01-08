using System;

namespace Quiz.BLL.Exceptions
{
  public class DoesNotContainException : Exception
  {
    public DoesNotContainException() : base() { }

    public DoesNotContainException(string message) : base(message) { }
  }
}
