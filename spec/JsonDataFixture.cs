using System.IO;

namespace spec
{
  class JsonDataFixture : System.Attribute
  {
    public string FixtureName { get; set; }
    public System.Type DataType { get; set; }

    public string PathToFixture
    {
      get
      {
        // TODO: this should come from the configuration!
        return Path.Combine("fixtures", "Authentication", $"{FixtureName}.json");
      }
    }

    public JsonDataFixture()
    {
    }
  }
}
