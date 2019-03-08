using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using GraphQL.Common.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using Utils;

namespace spec
{
  using Fixtures = Dictionary<string, object>;

  [JsonDataFixture(FixtureName = "NewUser", DataType = typeof(User))]
  [Binding]
  public class RegisterAUserSteps
  {
    private readonly GraphqlClient client;
    private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(typeof(RegisterAUserSteps));
    private GraphQLResponse graphqlResponse;
    private Fixtures fixtures;

    private void InterpretFixtures()
    {
      var classAttrs = GetType().GetCustomAttributes(typeof(JsonDataFixture), true);
      if (classAttrs != null)
      {
        fixtures = classAttrs.Select(attribute => 
        {
          var attr = attribute as JsonDataFixture;
          var jsonData = JsonConvert.DeserializeObject(File.ReadAllText(attr.PathToFixture), attr.DataType);
          return new { attr.FixtureName, jsonData };
        }).ToDictionary(t => t.FixtureName, t => t.jsonData);
      }
    }

    public RegisterAUserSteps(GraphqlClient client)
    {
      this.client = client;

      InterpretFixtures();
    }


    [Given(@"an unregistered User")]
    public void GivenAnUnregisteredUser()
    {
      log.Info("The user has no token defined anywhere");
    }

    [Given(@"a registered User")]
    public void GivenARegisteredUser()
    {
      ScenarioContext.Current.Pending();
    }

    [When(@"he registers with a username and a password")]
    public async Task WhenHeRegistersWithAUsernameAndAPassword()
    {
      log.Info("Define query");
      graphqlResponse = await client.SendQuery("Test");
      //ScenarioContext.Current.Pending();
    }

    [When(@"he registers with his username and a password")]
    public void WhenHeRegistersWithHisUsernameAndAPassword()
    {
      ScenarioContext.Current.Pending();
    }

    [Then(@"he gets a success response")]
    public void ThenHeGetsASuccessResponse()
    {
      //ScenarioContext.Current.Pending();
      log.Info($"Server response = {graphqlResponse.ToString()}");
      log.Info($"User id  = {graphqlResponse.Data.user.id}");
      log.Info($"Username = {graphqlResponse.Data.user.username}");

      var user = graphqlResponse.GetDataFieldAs<User>("user");
      Assert.AreEqual("1", user.Id);
      Assert.AreEqual("zad", user.Username);
    }

    [Then(@"his account gets activated")]
    public void ThenHisAccountGetsActivated()
    {
      ScenarioContext.Current.Pending();
    }

    [Then(@"he isn't logged in")]
    public void ThenHeIsnTLoggedIn()
    {
      ScenarioContext.Current.Pending();
    }

    [Then(@"his account remains unchanged")]
    public void ThenHisAccountRemainsUnchanged()
    {
      ScenarioContext.Current.Pending();
    }
  }
}
