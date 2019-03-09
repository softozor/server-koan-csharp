using System.IO;
using Microsoft.Extensions.Configuration;

namespace spec
{
  class JsonDataFixture : System.Attribute
  {
    private IConfigurationRoot Config { get; set; }

    public string FixtureName { get; set; }

    public string PathToFixture
    {
      get
      {
        return Path.Combine(Config["PathToDataFixtures"], $"{FixtureName}.json");
      }
    }

    private static IConfigurationRoot BuildConfiguration() => 
      new ConfigurationBuilder()
        .AddJsonFile("fixturesSettings.json")
        .Build();

    public JsonDataFixture()
    {
      Config = BuildConfiguration();
    }
  }
}
