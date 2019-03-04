using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using TechTalk.SpecFlow;

namespace spec
{
  [Binding]
  public class RegisterAUserSteps
  {
    private readonly HttpClient client = TestServer.Client;
    private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(typeof(RegisterAUserSteps));
    private HttpResponseMessage graphqlResponse;

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
      const string query = @"{
                ""query"": ""query { user { id username } }""
            }";
      var content = new StringContent(query, Encoding.UTF8, "application/json");

      // When
      log.Info("Querying the server");
      graphqlResponse = await client.PostAsync("/graphql", content);
      //ScenarioContext.Current.Pending();
    }

    [When(@"he registers with his username and a password")]
    public void WhenHeRegistersWithHisUsernameAndAPassword()
    {
      ScenarioContext.Current.Pending();
    }

    [Then(@"he gets a success response")]
    public async Task ThenHeGetsASuccessResponse()
    {
      //ScenarioContext.Current.Pending();
      graphqlResponse.EnsureSuccessStatusCode();
      var responseString = await graphqlResponse.Content.ReadAsStringAsync();
      log.Info($"Server response = {responseString}");
      Assert.IsNotNull(responseString);
      //const string responseString = @"{""data"":{""user"":{""id"":""1"",""name"":""zad""}}}";
      var jobj = JObject.Parse(responseString);
      Assert.IsNotNull(jobj);
      Assert.AreEqual(1, (int)jobj["data"]["user"]["id"]);
      Assert.AreEqual("zad", (string)jobj["data"]["user"]["username"]);
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
