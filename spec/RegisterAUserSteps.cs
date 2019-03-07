using System.Threading.Tasks;
using Api.Models;
using GraphQL.Common.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Utils;

namespace spec
{
  [Binding]
  public class RegisterAUserSteps
  {
    private readonly GraphqlClient client;
    private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(typeof(RegisterAUserSteps));
    private GraphQLResponse graphqlResponse;

    public RegisterAUserSteps(GraphqlClient client)
    {
      this.client = client;
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
