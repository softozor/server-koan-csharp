using System;
using System.Collections.Generic;
using System.Text;
using GraphQL.Types;

namespace Api.Models
{
  public class UserType : ObjectGraphType<User>
  {
    public UserType()
    {
      Field(usr => usr.Id).Description("The user's id");
      Field(usr => usr.Username).Description("The user's username");
    }
  }
}
