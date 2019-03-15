using System;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Utils;

namespace spec
{
  [JsonDataFixture(Name = "UnknownUser")]
  [JsonDataFixture(Name = "RegisteredUser")]
  [Binding]
  class RegisterAUserSteps : BaseSteps
  {
    private const string CURRENT_USER_KEY = "CurrentUser";
    private const string ORIGINAL_PASSWORD_KEY = "OriginalPassword";
    private const string SIGNUP_OPERATION_NAME = "Signup";

    private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(typeof(RegisterAUserSteps));

    private User UnknownUser { get; set; }
    private User RegisteredUser { get; set; }

    public RegisterAUserSteps(GraphqlClient client)
      : base(client)
    {
    }

    [Given(@"an unregistered User")]
    public void GivenAnUnregisteredUser()
    {
      ScenarioContext.Current.Set(UnknownUser, CURRENT_USER_KEY);
      Assert.IsFalse(AuthUtils.IsRegistered(UnknownUser.Username));
    }

    private string GetUserPasswordFromDatabase(string username)
    {
      // TODO
      // 1. fetch user in database 
      // 2. get user password
      throw new NotImplementedException("Getting user password from the database has not yet been implemented.");
    }

    [Given(@"a registered User")]
    public void GivenARegisteredUser()
    {
      ScenarioContext.Current.Set(RegisteredUser, CURRENT_USER_KEY);
      var username = RegisteredUser.Username;
      Assert.IsTrue(AuthUtils.IsRegistered(username));
      ScenarioContext.Current.Set(GetUserPasswordFromDatabase(username), ORIGINAL_PASSWORD_KEY);
    }

    [When(@"she registers with (?:a|her) username and (?:a )?password")]
    public async Task WhenSheRegistersWithAUsernameAndAPassword()
    {
      var user = ScenarioContext.Current.Get<User>(CURRENT_USER_KEY);
      var variables = new
      {
        username = user.Username,
        password = user.Password
      };
      ServerResponse = await Client.SendMutation(SIGNUP_OPERATION_NAME, variables);
    }

    [Then(@"she gets a success response")]
    public void ThenSheGetsASuccessResponse()
    {
      Assert.IsNull(ServerResponse.Data.registerUser.error);
    }

    private bool IsActive(string username)
    {
      throw new NotImplementedException("Verification in the database whether the user is active has not yet been implemented.");
    }

    [Then(@"her account gets activated")]
    public void ThenHerAccountGetsActivated()
    {
      var usr = ScenarioContext.Current.Get<User>(CURRENT_USER_KEY);
      Assert.IsTrue(IsActive(usr.Username));
    }

    [Then(@"she isn't logged in")]
    public void ThenSheIsnTLoggedIn()
    {
      Assert.IsTrue(String.IsNullOrEmpty(Client.JwtToken));
    }

    [Then(@"her password remains unchanged")]
    public void ThenHerAccountRemainsUnchanged()
    {
      var usr = ScenarioContext.Current.Get<User>(CURRENT_USER_KEY);
      var expectedPassword = ScenarioContext.Current.Get<string>(ORIGINAL_PASSWORD_KEY);
      var actualPassword = GetUserPasswordFromDatabase(usr.Username);
      Assert.AreEqual(expectedPassword, actualPassword);
    }
  }
}
