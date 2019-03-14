using System.IO;
using Microsoft.Extensions.Configuration;

namespace spec
{
  class JsonDataFixture : System.Attribute
  {
    private IConfigurationRoot Config { get; set; }

    public string Name { get; set; }

    public string FullPath
    {
      get
      {
        return Path.Combine(Config["PathToDataFixtures"], $"{Name}.json");
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
