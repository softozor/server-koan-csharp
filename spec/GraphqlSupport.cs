using BoDi;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;
using Utils;

namespace spec
{
  [Binding]
  public sealed class GraphqlSupport
  {
    private readonly IObjectContainer objContainer;
    private readonly CoreServices services = new CoreServices();

    private IConfigurationRoot Config { get; set; }

    private static IConfigurationRoot BuildConfiguration() =>
      new ConfigurationBuilder()
        .AddJsonFile("fixturesSettings.json")
        .Build();

    public GraphqlSupport(IObjectContainer objContainer)
    {
      this.objContainer = objContainer;
      Config = BuildConfiguration();
    }

    [BeforeScenario]
    public void InitializeClient()
    {
      var loader = new GraphqlLoader(Config["PathToGraphqlFixtures"]);
      var client = new GraphqlClient(services.Client, loader);
      objContainer.RegisterInstanceAs<GraphqlClient>(client);
    }
  }
}
