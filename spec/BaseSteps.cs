using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using GraphQL.Common.Response;
using Newtonsoft.Json;
using Utils;

namespace spec
{
  public class BaseSteps
  {
    protected GraphQLResponse ServerResponse { get; set; }
    protected GraphqlClient Client { get; private set; }

    private void DoFillFieldWithFixtureData(FieldInfo field, JsonDataFixture fixture)
    {
      // TODO: PathToFixture should be replaced by a read of the config
      var jsonData = JsonConvert.DeserializeObject(File.ReadAllText(fixture.PathToFixture), field.FieldType);
      field.SetValue(this, jsonData);
    }

    private void FillFieldWithFixtureData(JsonDataFixture fixture)
    {
      var field = GetType().GetField(fixture.FixtureName, BindingFlags.NonPublic | BindingFlags.Instance);
      if (field != null)
      {
        DoFillFieldWithFixtureData(field, fixture);
      }
      else
      {
        throw new FixtureVariableNotFound($"No variable with name <{fixture.FixtureName}> found in class <{GetType()}>");
      }
    }

    private IEnumerable<Attribute> GetDataFixtureAttributes()
    {
      return GetType().GetCustomAttributes()
          .Where(attr => attr.GetType() == typeof(JsonDataFixture));
    }

    private void FillFixtureDataFromAttributes()
    {
      var fixtureAttributes = GetDataFixtureAttributes();
      foreach (JsonDataFixture fixture in fixtureAttributes)
      {
        FillFieldWithFixtureData(fixture);
      }
    }

    // TODO: put all the reflection code into another class
    public BaseSteps(GraphqlClient client)
    {
      Client = client;
      FillFixtureDataFromAttributes();
    }
  }
}
