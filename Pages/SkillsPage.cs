using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
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

        // Dynamic locators using CSS
        private By editIcon(string skillName) =>
            By.CssSelector($"td:has(> text('{skillName}')) + td i.write.icon");

        private By deleteIcon(string skillName) =>
            By.CssSelector($"td:has(> text('{skillName}')) + td i.remove.icon");

        // Methods
        public void GoToSkillsTab()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement skillsTab = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("a[data-tab='second']")));
            skillsTab.Click();
        }

        public void ClickAddNewButton()
        {
            try
            {
                // Wait for the button to be present and visible
                var addNewBtn = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='ui teal button']")));

                // Scroll into view in case it's hidden under another element or not in viewport
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", addNewBtn);

                // Try normal click first
                try
                {
                    addNewBtn.Click();
                }
                catch (ElementClickInterceptedException)
                {
                    // Fallback to JS click if normal click fails
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
            skillField.SendKeys(skillName);

            var dropdown = new SelectElement(wait.Until(ExpectedConditions.ElementIsVisible(skillLevelDropdown)));
            dropdown.SelectByText(level);

            wait.Until(ExpectedConditions.ElementToBeClickable(addButton)).Click();
        }

        public void EditSkill(string oldSkill, string newSkill)
        {
            // Note: CSS doesn’t support text() lookup natively, so this line is just for structure.
            // Use XPath for these two or switch to JS helper below.
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

        public string GetPopupMessage()
        {
            try
            {
                Thread.Sleep(500);
                var popup = wait.Until(ExpectedConditions.ElementIsVisible(popupMessage));
                string message = popup.Text.Trim();
                Console.WriteLine("Popup message: " + message);
                return message;
            }
            catch (WebDriverTimeoutException)
            {
                return "Popup message not found within the wait period.";
            }
        }
    }
}

