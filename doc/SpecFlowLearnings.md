# Learnings on SpecFlow

## Dependency injection

As mentioned by [Gaspard](http://gasparnagy.com/2017/02/specflow-tips-baseclass-or-context-injection/) as well as in [SpecFlow's documentation itself](https://specflow.org/documentation/Context-Injection/), SpecFlow makes the very simple dependency injection framework [BoDi](https://www.nuget.org/packages/BoDi/) available by default. In my opinion, it pops up in a not very natural way, as this Koan implements a .NET application where the `Microsoft.Extensions.DependencyInjection` is used by default. I struggled a lot figuring out why my application's `IConfiguration` wasn't injectable into my step definitions. I was finally able to make the link between both DI frameworks in my `TestServer` class' [RelativePathToGraphqlRequests](https://github.com/softozor/server-koan-csharp/blob/master/spec/TestServer.cs) property. I've indeed had to get my configuration from Microsoft's DI:

```
using (var scope = server.Host.Services.CreateScope())
{
  var services = scope.ServiceProvider;
  var config = services.GetService<IConfiguration>();
  return config.GetSection("GraphQL")["PathToGraphqlRequests"];
}
```

and then inject it into SpecFlow's in a `BeforeScenario` [hook](https://github.com/softozor/server-koan-csharp/blob/master/spec/GraphqlSupport.cs):

```
[BeforeScenario]
public void InitializeRequestLoader()
{
  var loader = new GraphqlLoader(server.RelativePathToGraphqlRequests);
  objContainer.RegisterInstanceAs<GraphqlLoader>(loader);
}
```
