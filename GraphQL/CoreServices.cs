using System.Net.Http.Headers;
using Api;
using GraphQL.Client;
using GraphQL.Client.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Utils
{
  public class CoreServices
  {
    private IGraphQLClient client;

    private static Microsoft.AspNetCore.TestHost.TestServer CreateTestServer()
    {
      var builder = GetWebHostBuilder();
      return new Microsoft.AspNetCore.TestHost.TestServer(builder);
    }

    private Microsoft.AspNetCore.TestHost.TestServer server = CreateTestServer();

    private static IWebHostBuilder GetWebHostBuilder() =>
     WebHost.CreateDefaultBuilder()
      .ConfigureLogging(logging =>
      {
        logging.AddLog4Net();
        logging.SetMinimumLevel(LogLevel.Debug);
      })
      .UseEnvironment("Test")
      .UseStartup<Startup>();

    public IGraphQLClient Client
    {
      get
      {
        if (client == null)
        {
          var options = new GraphQLHttpClientOptions
          {
            EndPoint = new System.Uri("http://localhost/graphql"),
            MediaType = new MediaTypeHeaderValue("application/json"),
            HttpMessageHandler = server.CreateHandler()
          };
          client = new GraphQLHttpClient(options);
        }
        return client;
      }
    }
  }
}
