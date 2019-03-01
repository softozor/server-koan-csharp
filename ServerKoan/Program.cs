using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ServerKoan
{
  class Program
  {
    static void Main(string[] args)
    {
      BuildWebHost(args).Run();
    }

    private static IWebHost BuildWebHost(string[] args) =>
      WebHost.CreateDefaultBuilder(args)
       .UseStartup<Startup>()
       .UseUrls("http://localhost:8000")
       .Build();
  }
}
