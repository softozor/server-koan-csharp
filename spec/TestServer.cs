using System.Net.Http.Headers;
using Api;
using GraphQL.Client;
using GraphQL.Client.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace spec
{
  public class TestServer
  {
    private IGraphQLClient client;
    private Microsoft.AspNetCore.TestHost.TestServer server;

    public string RelativePathToGraphqlRequests
    {
      get
      {
        using (var scope = server.Host.Services.CreateScope())
        {
          var services = scope.ServiceProvider;
          var config = services.GetService<IConfiguration>();
          return config.GetSection("GraphQL")["PathToGraphqlRequests"];
        }
      }
    }

    private static IWebHostBuilder GetWebHostBuilder() =>
     WebHost.CreateDefaultBuilder()
      .ConfigureLogging(logging =>
      {
        logging.AddLog4Net();
        logging.SetMinimumLevel(LogLevel.Debug);
      })
      .UseEnvironment("Test")
      .UseStartup<Startup>();

    private static Microsoft.AspNetCore.TestHost.TestServer CreateTestServer()
    {
      var builder = GetWebHostBuilder();
      return new Microsoft.AspNetCore.TestHost.TestServer(builder);
    }

    public TestServer()
    {
      server = CreateTestServer();
    }

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
