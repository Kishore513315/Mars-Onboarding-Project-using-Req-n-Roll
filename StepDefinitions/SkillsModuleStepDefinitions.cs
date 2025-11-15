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
    public class SkillsModuleStepDefinitions
    {
        private readonly IWebDriver _driver;
        private readonly SkillsPage _skillsPage;
        private readonly SignInPage _signInPage;

        public SkillsModuleStepDefinitions()
        {
            _driver = Driver.driver!;
            _signInPage = new SignInPage(_driver);
            _skillsPage = new SkillsPage(_driver);
        }


        [Given(@"I am logged into the Mars portal skills")]
        public void GivenIAmLoggedIntoTheMarsPortal()
        {
            _signInPage.GoToLoginPage();
            _signInPage.EnterCredentials("chigurupati28555@gmail.com", "Poker@007");
            _signInPage.ClickLogin();

            Assert.That(_signInPage.IsDashboardVisible(), Is.True,
                "❌ Login failed — dashboard not visible.");
        }

        [Given(@"I navigate to the Skills tab")]
        public void GivenINavigateToTheSkillsTab()
        {
            _skillsPage.GoToSkillsTab();
        }


        [When(@"I click on ""(.*)"" button")]
        public void WhenIClickOnButton(string buttonName)
        {
            switch (buttonName.Trim().ToLower())
            {
                case "add new skill":
                case "add new":
                    _skillsPage.ClickAddNewButton();
                    break;

                case "add":
                    break;

                case "update":
                    break;

                default:
                    throw new ArgumentException($"❌ Unknown button name: {buttonName}");
            }
        }


        [When(@"I enter skill name ""(.*)"" with level ""(.*)""")]
        public void WhenIEnterSkillNameWithLevel(string skillName, string level)
        {
            _skillsPage.AddSkill(skillName, level);
        }

        [When(@"I edit skill ""(.*)"" to ""(.*)""")]
        public void WhenIEditSkillTo(string oldSkill, string newSkill)
        {
            _skillsPage.EditSkill(oldSkill, newSkill);
        }

        [When(@"I delete the skill ""(.*)""")]
        public void WhenIDeleteTheSkill(string skillName)
        {
            _skillsPage.DeleteSkill(skillName);
        }


        [Then(@"I should see a message ""(.*)""")]
        public void ThenIShouldSeeAMessage(string expectedMessage)
        {
            string actualMessage = _skillsPage.GetPopupMessage();
            Assert.That(actualMessage, Does.Contain(expectedMessage),
                $"❌ Expected: '{expectedMessage}', but got: '{actualMessage}'");
        }

        [Then(@"I should see a validation message ""(.*)""")]
        public void ThenIShouldSeeAValidationMessage(string expectedValidation)
        {
            string actualMessage = _skillsPage.GetPopupMessage();
            Assert.That(actualMessage, Does.Contain(expectedValidation),
                $"❌ Expected Validation: '{expectedValidation}', but got: '{actualMessage}'");
        }


        [When(@"I delete all skills from the Skills tab")]
        public void WhenIDeleteAllSkillsFromTheSkillsTab()
        {
            _skillsPage.DeleteAllSkills();
        }

        [Then(@"all skills should be removed successfully")]
        public void ThenAllSkillsShouldBeRemovedSuccessfully()
        {
            Console.WriteLine("Cleanup completed — all skills deleted successfully.");
        }
    }
}







