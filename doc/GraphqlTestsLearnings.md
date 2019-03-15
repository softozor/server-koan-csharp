# Learnings on GraphQL tests

## Authenticated requests

* the [graphql-dotnet issue](https://github.com/graphql-dotnet/graphql-client/issues/32) gives documentation about how to specify an authentication token upon sending a graphql request

## Asynchronous steps

In C#, asynchronous methods are decorated with an `async` modifier. Inside of the method, the asynchronous call is specified with the `await` modifier, as in the following example:

```
public async Task WhenIRunTheTestQuery()
{
  ServerResponse = await Client.SendQuery("Test");
}
```

In addition to that, the step method needs to return an object of type `Task<T>`. Would the method return nothing, the return type would be just `Task`. 