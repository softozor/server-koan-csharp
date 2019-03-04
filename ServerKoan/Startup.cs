using Api.Models;
using GraphQL;
using GraphQL.Http;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{

  public class ServerKoanContext : DbContext
  {

  }

  public class Startup : StartupBase
  {
    private IHostingEnvironment Env { get; set; }
    private IConfiguration Configuration { get; set; }

    public Startup(IHostingEnvironment env, IConfiguration config)
      : base()
    {
      Env = env;
      Configuration = config;
    }

    private void ConfigureTestDatabase(IServiceCollection services)
    {
      //services.AddDbContext<ServerKoanContext>(options =>
      //              options.UseInMemoryDatabase(databaseName: "ServerKoan"));
    }

    private void ConfigureDefaultDatabase(IServiceCollection services)
    {
      //var rootConf = Configuration as IConfigurationRoot;
      //services.AddDbContext<ServerKoanContext>(options =>
      //              options.UseMySql(rootConf.GetConnectionString("DefaultConnection")));
    }

    public override void ConfigureServices(IServiceCollection services)
    {
      if(Env.IsEnvironment("Test"))
      {
        ConfigureTestDatabase(services);
      }
      else
      {
        ConfigureDefaultDatabase(services);
      }

      services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
      services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
      services.AddSingleton<IDocumentWriter, DocumentWriter>();
      services.AddSingleton<Query>();
      services.AddSingleton<UserType>();
      services.AddSingleton<ISchema, Api.Models.Schema>();
      services.AddGraphQL(options =>
      {
        options.EnableMetrics = true;
        options.ExposeExceptions = true;
      });
    }

    public override void Configure(IApplicationBuilder app)
    {
      app.UseDeveloperExceptionPage();
      app.UseGraphQL<ISchema>("/graphql");
      app.UseGraphQLPlayground(new GraphQLPlaygroundOptions
      {
        Path = "/ui/playground"
      });
    }
  }
}
