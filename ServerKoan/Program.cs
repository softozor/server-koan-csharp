using Api;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace App
{
  class Program
  {
    static void Main(string[] args)
    {
      BuildWebHost(args).Run();
    }

    private static IWebHost BuildWebHost(string[] args) =>
      WebHost.CreateDefaultBuilder(args)
       .ConfigureLogging(logging =>
       {
         logging.AddLog4Net();
         logging.SetMinimumLevel(LogLevel.Debug);
       })
       .UseStartup<Startup>()
       .UseUrls("http://localhost:8000")
       .Build();
  }
}
