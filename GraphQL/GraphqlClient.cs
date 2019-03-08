using System.Threading.Tasks;
using GraphQL.Client;
using GraphQL.Common.Request;
using GraphQL.Common.Response;

namespace Utils
{
  public class GraphqlClient
  {
    private readonly IGraphQLClient client;
    private readonly GraphqlLoader requestLoader;

    public GraphqlClient(IGraphQLClient client, GraphqlLoader loader)
    {
      this.client = client;
      this.requestLoader = loader;
    }

    private string GetRequestFromOperationName(string operationName)
    {
      var query = requestLoader.FromFile(operationName);
      if(!query.Contains(operationName))
      {
        throw new InvalidOperationName($"Operation {operationName} not found in graphql mutation.");
      }
      return query;
    }

    private GraphQLRequest GetRequest(string operationName, dynamic variables)
    {
      return new GraphQLRequest
      {
        Query = GetRequestFromOperationName(operationName),
        OperationName = operationName,
        Variables = variables
      };
    }

    public Task<GraphQLResponse> SendMutation(string operationName, dynamic variables = null)
    {
      var req = GetRequest(operationName, variables);
      return client.SendMutationAsync(req);
    }

    public Task<GraphQLResponse> SendQuery(string operationName, dynamic variables = null)
    {
      var req = GetRequest(operationName, variables);
      return client.SendQueryAsync(req);
    }
  }
}
