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
  public class LogUserInSteps : BaseSteps
  {
    private const string CURRENT_USER_KEY = "CurrentUser";
    private const string LOGIN_OPERATION_NAME = "Login";

    private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(typeof(LogUserInSteps));
    private User UnknownUser { get; set; }
    private User RegisteredUser { get; set; }

    public LogUserInSteps(GraphqlClient client)
      : base(client)
    {
    }

    [Given(@"the User is not logged in")]
    public void GivenTheUserIsNotLoggedIn()
    {
      Assert.IsTrue(String.IsNullOrEmpty(Client.JwtToken));
    }

    private async Task logCurrentUserIn()
    {
      var usr = ScenarioContext.Current.Get<User>(CURRENT_USER_KEY);
      var variables = new
      {
        username = usr.Username,
        password = usr.Password
      };
      ServerResponse = await Client.SendMutation(LOGIN_OPERATION_NAME, variables);
    }

    [When(@"the User logs in with valid username and password")]
    public async Task WhenTheUserLogsInWithValidUsernameAndPassword()
    {
      ScenarioContext.Current.Add(CURRENT_USER_KEY, RegisteredUser);
      Assert.IsTrue(AuthUtils.IsRegistered(RegisteredUser.Username));
      await logCurrentUserIn();
    }

    [When(@"a User logs in with invalid username and password")]
    public async Task WhenAUserLogsInWithInvalidUsernameAndPassword()
    {
      ScenarioContext.Current.Add(CURRENT_USER_KEY, UnknownUser);
      Assert.IsFalse(AuthUtils.IsRegistered(UnknownUser.Username));
      await logCurrentUserIn();
    }

    [When(@"a User logs in with valid username and invalid password")]
    public async Task WhenAUserLogsInWithValidUsernameAndInvalidPassword()
    {
      var usr = RegisteredUser;
      // any modification of the correct password will be wrong
      usr.Password = usr.Password.Insert(0, "a");
      ScenarioContext.Current.Add(CURRENT_USER_KEY, usr);
      Assert.IsTrue(AuthUtils.IsRegistered(usr.Username));
      await logCurrentUserIn();
    }

    private int GetSessionDurationFromToken(string token)
    {
      throw new NotImplementedException("Decoding the token has not been implemented yet.");
    }

    [Then(@"her session opens for (.*) week")]
    public void ThenHerSessionOpensForWeek(int expectedSessionDurationInWeeks)
    {
      Assert.IsNull(ServerResponse.Data.login.error);
      var token = ServerResponse.Data.login.token;
      Assert.IsNotNull(token);
      var actualSessionDurationInWeeks = GetSessionDurationFromToken(token);
      Assert.AreEqual(expectedSessionDurationInWeeks, actualSessionDurationInWeeks);
    }

    [Then(@"she gets a wrong credentials response")]
    public void ThenSheGetsAWrongCredentialsResponse()
    {
      var error = ServerResponse.Data.login.error;
      Assert.IsNotNull(error);
      var expectedError = "WRONG_CREDENTIALS";
      Assert.AreEqual(expectedError, error.description);
    }
  }
}
