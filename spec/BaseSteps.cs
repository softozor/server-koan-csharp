using GraphQL.Common.Response;
using Utils;

namespace spec
{
  public class BaseSteps
  {
    protected GraphQLResponse ServerResponse { get; set; }
    protected GraphqlClient Client { get; private set; }

    public BaseSteps(GraphqlClient client)
    {
      Client = client;
      FixturesInjecter.FillFixtureDataFromAttributes(this);
    }
  }
}
