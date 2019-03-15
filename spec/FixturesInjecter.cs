using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace spec
{
  static class FixturesInjecter
  {
    static private void DoFillPropertyWithFixtureData(BaseSteps instance, PropertyInfo property, JsonDataFixture fixture)
    {
      var jsonData = JsonConvert.DeserializeObject(File.ReadAllText(fixture.FullPath), property.PropertyType);
      property.SetValue(instance, jsonData);
    }

    static private void FillPropertyWithFixtureData(BaseSteps instance, JsonDataFixture fixture)
    {
      var property = instance.GetType().GetProperty(fixture.Name, BindingFlags.NonPublic | BindingFlags.Instance);

      if(property != null)
      {
        DoFillPropertyWithFixtureData(instance, property, fixture);
      }
      else
      {
        throw new FixtureVariableNotFound($"No property with name <{fixture.Name}> found in class <{instance.GetType()}>");
      }
    }

    static private IEnumerable<Attribute> GetDataFixtureAttributes(BaseSteps instance)
    {
      return instance.GetType().GetCustomAttributes()
          .Where(attr => attr.GetType() == typeof(JsonDataFixture));
    }

    static public void InjectFixturesIntoProperties(BaseSteps instance)
    {
      var fixtureAttributes = GetDataFixtureAttributes(instance);
      foreach (JsonDataFixture fixture in fixtureAttributes)
      {
        FillPropertyWithFixtureData(instance, fixture);
      }
    }
  }
}
