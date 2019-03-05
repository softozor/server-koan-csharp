using System.Net.Http.Headers;
using Api;
using GraphQL.Client;
using GraphQL.Client.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace spec
{
  public static class TestServer
  {
    private static IGraphQLClient client;

    private static IWebHostBuilder GetWebHostBuilder() =>
     WebHost.CreateDefaultBuilder()
      .ConfigureLogging(logging =>
      {
        logging.AddLog4Net();
        logging.SetMinimumLevel(LogLevel.Debug);
      })
      .UseEnvironment("Test")
      .UseStartup<Startup>();

    static public IGraphQLClient Client
    {
      get
      {
        if (client == null)
        {
          var builder = GetWebHostBuilder();
          var server = new Microsoft.AspNetCore.TestHost.TestServer(builder);
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
