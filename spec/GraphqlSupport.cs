using BoDi;
using TechTalk.SpecFlow;
using Utils;

namespace spec
{
  [Binding]
  public sealed class GraphqlSupport
  {
    private readonly IObjectContainer objContainer;
    private readonly TestServer server;

    public GraphqlSupport(IObjectContainer objContainer)
    {
      this.objContainer = objContainer;
      server = new TestServer();
    }

    [BeforeScenario]
    public void InitializeClient()
    {
      var loader = new GraphqlLoader(server.RelativePathToGraphqlRequests);
      var client = new GraphqlClient(server.Client, loader);
      objContainer.RegisterInstanceAs<GraphqlClient>(client);
    }
  }
}
