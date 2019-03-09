using System.Threading.Tasks;
using Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Utils;

namespace spec
{
  [JsonDataFixture(FixtureName = "newUser")]
  [Binding]
  public class RegisterAUserSteps : BaseSteps
  {
    private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(typeof(RegisterAUserSteps));

    // TODO: this here is not very elegant --> we probably want to use a property here instead!
    private User newUser = null;

    public RegisterAUserSteps(GraphqlClient client)
      : base(client)
    {
    }

    [Given(@"an unregistered User")]
    public void GivenAnUnregisteredUser()
    {
      log.Info("The user has no token defined anywhere");
      log.Info($"newUserData = {newUser.Username}");
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
      ServerResponse = await Client.SendQuery("Test");
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
      log.Info($"Server response = {ServerResponse.ToString()}");
      log.Info($"User id  = {ServerResponse.Data.user.id}");
      log.Info($"Username = {ServerResponse.Data.user.username}");

      var user = ServerResponse.GetDataFieldAs<User>("user");
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
