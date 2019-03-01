using System;
using System.Collections.Generic;
using System.Text;
using GraphQL;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
  using GraphQL.Http;
  using GraphQL.Server.Ui.Playground;
  using Models;

  class Startup : StartupBase
  {
    public override void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
      services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
      services.AddSingleton<IDocumentWriter, DocumentWriter>();
      services.AddSingleton<Query>();
      services.AddSingleton<UserType>();
      services.AddSingleton<ISchema, Schema>();
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
