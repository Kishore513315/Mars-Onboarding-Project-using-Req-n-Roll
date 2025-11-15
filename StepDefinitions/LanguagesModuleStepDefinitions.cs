using Mars_Onboarding_Project.Pages;
using MarsOnBoardingReqnRollProject.Drivers;
using MarsProject.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using System;

namespace MarsProject.StepDefinitions
{
    [Binding]
    public class LanguagesModuleStepDefinitions
    {
        private readonly IWebDriver _driver;
        private readonly LanguagesPage _languagesPage;
        private readonly SignInPage _signInPage;

        public LanguagesModuleStepDefinitions()
        {
            _driver = Driver.driver!;
            _signInPage = new SignInPage(_driver);
            _languagesPage = new LanguagesPage(_driver);
        }

        [Given(@"I am logged into the Mars portal languages")]
        public void GivenIAmLoggedIntoTheMarsPortal()
        {
            _signInPage.GoToLoginPage();
            _signInPage.EnterCredentials("chigurupati28555@gmail.com", "Poker@007");
            _signInPage.ClickLogin();

            Assert.That(_signInPage.IsDashboardVisible(), Is.True,
                "❌ Login failed — dashboard not visible.");
        }

        [Given(@"I navigate to the Languages tab")]
        public void GivenINavigateToTheLanguagesTab()
        {
            _languagesPage.GoToLanguagesTab();
        }

        [When(@"I click ""(.*)"" button")]
        public void WhenIClickOnButton(string buttonName)
        {
            switch (buttonName.Trim().ToLower())
            {
                case "add new":
                    _languagesPage.ClickAddNewButton();
                    break;

                case "add":
                    _languagesPage.ClickAddButton();
                    break;

                case "update":
                    _languagesPage.ClickUpdateButton();
                    break;

                default:
                    throw new ArgumentException($"❌ Unknown button name: {buttonName}");
            }
        }

        [When(@"I enter language ""(.*)"" with level ""(.*)""")]
        public void WhenIEnterLanguageWithLevel(string language, string level)
        {
            _languagesPage.EnterLanguageDetails(language, level);
        }

        [When(@"I edit language ""(.*)"" to ""(.*)""")]
        public void WhenIEditLanguageTo(string oldLang, string newLang)
        {
            _languagesPage.EditLanguage(oldLang, newLang);
        }

        [When(@"I delete the language ""(.*)""")]
        public void WhenIDeleteTheLanguage(string language)
        {
            _languagesPage.DeleteLanguage(language);
        }

        [Then(@"I should see a languages message ""(.*)""")]
        public void ThenIShouldSeeALanguagesMessage(string expectedMessage)
        {
            string actualMessage = _languagesPage.GetPopupMessage();
            Assert.That(actualMessage, Does.Contain(expectedMessage),
                $"❌ Expected: '{expectedMessage}', but got: '{actualMessage}'");
        }

        [Then(@"I should see a languages validation message ""(.*)""")]
        public void ThenIShouldSeeAValidationMessage(string expectedValidation)
        {
            string actualMessage = _languagesPage.GetPopupMessage();
            Assert.That(actualMessage, Does.Contain(expectedValidation),
                $"❌ Expected Validation: '{expectedValidation}', but got: '{actualMessage}'");
        }

        [When(@"I delete all languages from the Languages tab")]
        public void WhenIDeleteAllLanguagesFromTheLanguagesTab()
        {
            _languagesPage.DeleteAllLanguages();
        }

        [Then(@"all languages should be removed successfully")]
        public void ThenAllLanguagesShouldBeRemovedSuccessfully()
        {
            Console.WriteLine("Cleanup completed — all languages deleted successfully.");
        }
    }
}



