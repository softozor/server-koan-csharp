using System.Threading.Tasks;
using GraphQL.Client.Http;
using GraphQL.Common.Request;
using GraphQL.Common.Response;

namespace Utils
{
  public class GraphqlClient
  {
    private readonly GraphQLHttpClient client;
    private readonly GraphqlLoader requestLoader;

    public string JwtToken { get; private set; }

    public GraphqlClient(GraphQLHttpClient client, GraphqlLoader loader)
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

    private void RemoveAuthenticationHeader()
    {
      client.DefaultRequestHeaders.Remove("Authorization"); // is that working when no "Authorization" element is present in the collection?
    }

    public void clearAuth()
    {
      JwtToken = string.Empty;
      RemoveAuthenticationHeader();
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

    private bool NeedToAddAuthenticationHeader()
    {
      return !(client.DefaultRequestHeaders.Contains("Authorization") || JwtToken == string.Empty);
    }

    private void AddAuthenticationHeaderIfNecessary()
    {
      if (NeedToAddAuthenticationHeader())
      {
        client.DefaultRequestHeaders.Add("Authorization", $"bearer {JwtToken}");
      }
    }

    public Task<GraphQLResponse> SendMutation(string operationName, dynamic variables = null)
    {
      AddAuthenticationHeaderIfNecessary();
      var req = GetRequest(operationName, variables);
      return client.SendMutationAsync(req);
    }

    public Task<GraphQLResponse> SendQuery(string operationName, dynamic variables = null)
    {
      AddAuthenticationHeaderIfNecessary();
      var req = GetRequest(operationName, variables);
      return client.SendQueryAsync(req);
    }
  }
}
