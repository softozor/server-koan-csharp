using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace spec
{
  public static class FixturesInjecter
  {
    static private void DoFillFieldWithFixtureData(BaseSteps instance, FieldInfo field, JsonDataFixture fixture)
    {
      var jsonData = JsonConvert.DeserializeObject(File.ReadAllText(fixture.PathToFixture), field.FieldType);
      field.SetValue(instance, jsonData);
    }

    static private void FillFieldWithFixtureData(BaseSteps instance, JsonDataFixture fixture)
    {
      var field = instance.GetType().GetField(fixture.FixtureName, BindingFlags.NonPublic | BindingFlags.Instance);
      if (field != null)
      {
        DoFillFieldWithFixtureData(instance, field, fixture);
      }
      else
      {
        throw new FixtureVariableNotFound($"No variable with name <{fixture.FixtureName}> found in class <{instance.GetType()}>");
      }
    }

    static private IEnumerable<Attribute> GetDataFixtureAttributes(BaseSteps instance)
    {
      return instance.GetType().GetCustomAttributes()
          .Where(attr => attr.GetType() == typeof(JsonDataFixture));
    }

    static public void FillFixtureDataFromAttributes(BaseSteps instance)
    {
      var fixtureAttributes = GetDataFixtureAttributes(instance);
      foreach (JsonDataFixture fixture in fixtureAttributes)
      {
        FillFieldWithFixtureData(instance, fixture);
      }
    }
  }
}
