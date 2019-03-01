using GraphQL.Types;

namespace Api.Models
{
  public class Query : ObjectGraphType
  {
    public Query()
    {
      Field<UserType>(
        "user",
        resolve: context => new User { Id = "1", Username = "zad" }
      );
    }
  }
}
