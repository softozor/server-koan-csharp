using System.Net.Http;
using Api;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace spec
{
  public static class TestServer
  {
    private static HttpClient client;

    private static IWebHostBuilder GetWebHostBuilder() =>
     WebHost.CreateDefaultBuilder()
      .ConfigureLogging(logging =>
      {
        logging.AddLog4Net();
        logging.SetMinimumLevel(LogLevel.Debug);
      })
      .UseEnvironment("Test")
      .UseStartup<Startup>();

    static public HttpClient Client
    {
      get
      {
        if (client == null)
        {
          var builder = GetWebHostBuilder();
          var server = new Microsoft.AspNetCore.TestHost.TestServer(builder);
          client = server.CreateClient();
        }
        return client;
      }
    }
  }
}
