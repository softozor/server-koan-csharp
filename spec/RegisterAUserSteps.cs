using System;
using TechTalk.SpecFlow;

namespace spec
{
    [Binding]
    public class RegisterAUserSteps
    {
        [Given(@"an unregistered User")]
        public void GivenAnUnregisteredUser()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"a registered User")]
        public void GivenARegisteredUser()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"he registers with a username and a password")]
        public void WhenHeRegistersWithAUsernameAndAPassword()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"he registers with his username and a password")]
        public void WhenHeRegistersWithHisUsernameAndAPassword()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"he gets a success response")]
        public void ThenHeGetsASuccessResponse()
        {
            ScenarioContext.Current.Pending();
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
