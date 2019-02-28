﻿using System;
using TechTalk.SpecFlow;

namespace spec
{
    [Binding]
    public class LogUserInSteps
    {
        [Given(@"the User is not logged in")]
        public void GivenTheUserIsNotLoggedIn()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"the User registers with valid username and password")]
        public void WhenTheUserRegistersWithValidUsernameAndPassword()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"a User registers with invalid username and password")]
        public void WhenAUserRegistersWithInvalidUsernameAndPassword()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"a User registers with valid username and invalid password")]
        public void WhenAUserRegistersWithValidUsernameAndInvalidPassword()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"his session opens for (.*) week")]
        public void ThenHisSessionOpensForWeek(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"he gets a wrong credentials response")]
        public void ThenHeGetsAWrongCredentialsResponse()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"he gets a wrong credententials response")]
        public void ThenHeGetsAWrongCredententialsResponse()
        {
            ScenarioContext.Current.Pending();
        }
    }
}