using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
  public class InvalidOperationName : System.Exception
  {
    public InvalidOperationName() : base() { }
    public InvalidOperationName(string message) : base(message) { }
    public InvalidOperationName(string message, System.Exception inner) : base(message, inner) { }
    protected InvalidOperationName(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }
}
