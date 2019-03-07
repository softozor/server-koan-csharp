using BoDi;
using GraphQL.Client;
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
      objContainer.RegisterInstanceAs<IGraphQLClient>(server.Client);
    }

    [BeforeScenario]
    public void InitializeRequestLoader()
    {
      var loader = new GraphqlLoader(server.RelativePathToGraphqlRequests);
      objContainer.RegisterInstanceAs<GraphqlLoader>(loader);
    }
  }
}
