namespace spec
{
  public class FixtureVariableNotFound : System.Exception
  {
    public FixtureVariableNotFound() : base() { }
    public FixtureVariableNotFound(string message) : base(message) { }
    public FixtureVariableNotFound(string message, System.Exception inner) : base(message, inner) { }
    protected FixtureVariableNotFound(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }
}
