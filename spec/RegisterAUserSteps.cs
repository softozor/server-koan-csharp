using System.Threading.Tasks;
using Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Utils;

namespace spec
{
  [JsonDataFixture(Name = "NewUser")]
  [Binding]
  public class RegisterAUserSteps : BaseSteps
  {
    private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(typeof(RegisterAUserSteps));

    private User NewUser { get; set; }

    public RegisterAUserSteps(GraphqlClient client)
      : base(client)
    {
    }

    [Given(@"an unregistered User")]
    public void GivenAnUnregisteredUser()
    {
      // TODO: double-check that the NewUser is not present in the database
      ScenarioContext.Current.Pending();
    }

    [Given(@"a registered User")]
    public void GivenARegisteredUser()
    {
      // TODO: double-check that the RegisteredUser is present in the database
      ScenarioContext.Current.Pending();
    }

    [When(@"she registers with a username and a password")]
    public async Task WhenSheRegistersWithAUsernameAndAPassword()
    {
      log.Info("Define query");
      ServerResponse = await Client.SendQuery("Test");
      //ScenarioContext.Current.Pending();
    }

    [When(@"she registers with her username and password")]
    public void WhenSheRegistersWithHerUsernameAndPassword()
    {
      ScenarioContext.Current.Pending();
    }

    [Then(@"she gets a success response")]
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

    [Then(@"her account gets activated")]
    public void ThenHisAccountGetsActivated()
    {
      ScenarioContext.Current.Pending();
    }

    [Then(@"she isn't logged in")]
    public void ThenHeIsnTLoggedIn()
    {
      ScenarioContext.Current.Pending();
    }

    [Then(@"her account remains unchanged")]
    public void ThenHisAccountRemainsUnchanged()
    {
      ScenarioContext.Current.Pending();
    }
  }
}
