namespace spec
{
  public class FixtureVariableNotFound : System.Exception
  {
    public FixtureVariableNotFound() : base() { }
    public FixtureVariableNotFound(string message) : base(message) { }
    public FixtureVariableNotFound(string message, System.Exception inner) : base(message, inner) { }
    protected FixtureVariableNotFound(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }

  public class UnsupportedFixtureType : System.Exception
  {
    public UnsupportedFixtureType() : base() { }
    public UnsupportedFixtureType(string message) : base(message) { }
    public UnsupportedFixtureType(string message, System.Exception inner) : base(message, inner) { }
    protected UnsupportedFixtureType(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }
}
