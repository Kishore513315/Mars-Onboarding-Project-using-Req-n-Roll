using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MarsProject.Pages
{
    public class SkillsPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public SkillsPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private readonly By skillTab = By.CssSelector("a[data-tab='second']");
        private readonly By addNewButton = By.XPath("//div[@class='ui teal button']");
        private readonly By skillInput = By.CssSelector("input[placeholder='Add Skill']");
        private readonly By skillLevelDropdown = By.CssSelector("select[name='level']");
        private readonly By addButton = By.CssSelector("input[value='Add']");
        private readonly By updateButton = By.CssSelector("input[value='Update']");
        private readonly By popupMessage = By.CssSelector("div.ns-box-inner, div[role='alert']");
        private readonly By validationMessage = By.XPath("//div[contains(@class, 'ui negative message') or contains(@class, 'validation')]");
        private readonly By skillTableRows = By.XPath("//table/tbody/tr");

        public void GoToSkillsTab()
        {
            IWebElement skillsTab = wait.Until(ExpectedConditions.ElementToBeClickable(skillTab));
            skillsTab.Click();
        }

        public void ClickAddNewButton()
        {
            try
            {
                var addNewBtn = wait.Until(ExpectedConditions.ElementIsVisible(addNewButton));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", addNewBtn);
                try
                {
                    addNewBtn.Click();
                }
                catch (ElementClickInterceptedException)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", addNewBtn);
                }
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception("Add New Skill button was not found or not clickable after waiting.");
            }
        }

        public void AddSkill(string skillName, string level)
        {
            var skillField = wait.Until(ExpectedConditions.ElementIsVisible(skillInput));
            skillField.Clear();

            if (!string.IsNullOrWhiteSpace(skillName))
            {
                skillField.SendKeys(skillName);
            }

            if (!string.IsNullOrWhiteSpace(level))
            {
                var dropdown = new SelectElement(wait.Until(ExpectedConditions.ElementIsVisible(skillLevelDropdown)));
                dropdown.SelectByText(level);
            }

            wait.Until(ExpectedConditions.ElementToBeClickable(addButton)).Click();
        }

        public void EditSkill(string oldSkill, string newSkill)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath($"//td[text()='{oldSkill}']/following-sibling::td//i[contains(@class,'write icon')]")
            )).Click();

            var skillField = wait.Until(ExpectedConditions.ElementIsVisible(skillInput));
            skillField.Clear();
            skillField.SendKeys(newSkill);

            wait.Until(ExpectedConditions.ElementToBeClickable(updateButton)).Click();
        }

        public void DeleteSkill(string skillName)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath($"//td[text()='{skillName}']/following-sibling::td//i[contains(@class,'remove icon')]")
            )).Click();
        }

        //  CleanUp after all tests
        public void DeleteAllSkills()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//table/tbody/tr")));

            var rows = driver.FindElements(By.XPath("//table/tbody/tr"));
            Console.WriteLine($"Found {rows.Count} skill(s) to delete.");

            while (rows.Count > 0)
            {
                try
                {
                    var deleteButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//table/tbody/tr[1]//i[@class='remove icon']")));
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", deleteButton);
                    wait.Until(ExpectedConditions.ElementToBeClickable(deleteButton));

                    try
                    {
                        deleteButton.Click();
                    }
                    catch (ElementNotInteractableException)
                    {
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", deleteButton);
                    }

                    Thread.Sleep(800); // short wait for table refresh
                    rows = driver.FindElements(By.XPath("//table/tbody/tr"));
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine("Table refreshed — re-fetching rows...");
                    rows = driver.FindElements(By.XPath("//table/tbody/tr"));
                }
            }

            Console.WriteLine("All skills deleted successfully.");
        }



        public string GetPopupMessage()
        {
            try
            {
                Thread.Sleep(500);

                // Check if validation message exists
                var validation = driver.FindElements(validationMessage);
                if (validation.Count > 0)
                {
                    string valText = validation.First().Text.Trim();
                    Console.WriteLine("Validation Message: " + valText);
                    return valText;
                }

                // Otherwise, get standard popup message
                var popup = wait.Until(ExpectedConditions.ElementIsVisible(popupMessage));
                string message = popup.Text.Trim();
                Console.WriteLine("Popup Message: " + message);
                return message;
            }
            catch (Exception ex)
            {
                Console.WriteLine("No popup or validation message found: " + ex.Message);
                return "No message displayed";
            }
        }
    }
}


