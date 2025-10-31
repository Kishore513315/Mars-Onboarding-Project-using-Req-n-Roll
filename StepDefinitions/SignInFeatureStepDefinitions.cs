using Mars_Onboarding_Project.Pages;
using MarsOnBoardingReqnRollProject.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;

namespace Mars_Onboarding_Project.StepDefinitions
{
    [Binding]
    public class SignInFeatureStepDefinitions
    {
        private readonly IWebDriver driver;
        private readonly SignInPage signInPage;

        public SignInFeatureStepDefinitions()
        {
            driver = Driver.driver;
            signInPage = new SignInPage(driver);
        }

        [Given(@"I navigate to the Mars portal login page")]
        public void GivenINavigateToTheMarsPortalLoginPage()
        {
            signInPage.GoToLoginPage();
        }

        [When(@"I enter valid username and password")]
        public void WhenIEnterValidUsernameAndPassword()
        {
            signInPage.EnterCredentials("chigurupati28555@gmail.com", "Poker@007");
        }

        [When(@"I click on the login button")]
        public void WhenIClickOnTheLoginButton()
        {
            signInPage.ClickLogin();
        }

        [Then(@"I should be redirected to the dashboard")]
        public void ThenIShouldBeRedirectedToTheDashboard()
        {
            Assert.That(signInPage.IsDashboardVisible(), Is.True, "Dashboard was not displayed after login");
        }

        [Then(@"the user name should be displayed on the top right corner")]
        public void ThenTheUserNameShouldBeDisplayedOnTheTopRightCorner()
        {
            Assert.That(signInPage.IsUserNameDisplayed(), Is.True, "User name was not displayed on the dashboard");
        }
    }
}

