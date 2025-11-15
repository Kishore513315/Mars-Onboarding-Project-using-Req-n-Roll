using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MarsProject.Pages
{
    public class LanguagesPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public LanguagesPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // Locators
        private readonly By languagesTab = By.CssSelector("a[data-tab='first']");
        private readonly By addNewButton = By.XPath("//html/body/div[1]/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table/thead/tr/th[3]/div");
        private readonly By languageInput = By.CssSelector("input[placeholder='Add Language']");
        private readonly By levelDropdown = By.CssSelector("select[name='level']");
        private readonly By addButton = By.CssSelector("input[value='Add']");
        private readonly By updateButton = By.CssSelector("input[value='Update']");
        private readonly By popupMessage = By.CssSelector("div.ns-box-inner, div[role='alert']");
        private readonly By validationMessage = By.XPath("//div[contains(@class, 'ui negative message') or contains(@class, 'validation')]");
        private readonly By tableRows = By.XPath("//table/tbody/tr");

        // Dynamic Locators
        private By EditIcon(string language) => By.XPath($"//td[text()='{language}']/following-sibling::td//i[contains(@class,'write')]");
        private By DeleteIcon(string language) => By.XPath($"//td[text()='{language}']/following-sibling::td//i[contains(@class,'remove')]");

        // Actions
        public void GoToLanguagesTab()
        {
            var tab = wait.Until(ExpectedConditions.ElementToBeClickable(languagesTab));
            tab.Click();
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
                throw new Exception("Add New Language button was not found or clickable.");
            }
        }

        public void EnterLanguageDetails(string language, string level)
        {
            var input = wait.Until(ExpectedConditions.ElementIsVisible(languageInput));
            input.Clear();

            if (!string.IsNullOrWhiteSpace(language))
                input.SendKeys(language);

            if (!string.IsNullOrWhiteSpace(level))
            {
                var dropdown = new SelectElement(wait.Until(ExpectedConditions.ElementIsVisible(levelDropdown)));
                dropdown.SelectByText(level);
            }
        }

        public void ClickAddButton()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(addButton)).Click();
        }

        public void ClickUpdateButton()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(updateButton)).Click();
        }

        public void EditLanguage(string oldLang, string newLang)
        {
            GoToLanguagesTab();
            wait.Until(ExpectedConditions.ElementToBeClickable(EditIcon(oldLang))).Click();

            var input = wait.Until(ExpectedConditions.ElementIsVisible(languageInput));
            input.Clear();
            input.SendKeys(newLang);

            ClickUpdateButton();
        }

        public void DeleteLanguage(string language)
        {
            GoToLanguagesTab();
            wait.Until(ExpectedConditions.ElementToBeClickable(DeleteIcon(language))).Click();
        }

        // Cleanup - delete all rows
        public void DeleteAllLanguages()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(tableRows));
            var rows = driver.FindElements(tableRows);
            Console.WriteLine($"Found {rows.Count} languages to delete.");

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

                    Thread.Sleep(800);
                    rows = driver.FindElements(tableRows);
                }
                catch (StaleElementReferenceException)
                {
                    rows = driver.FindElements(tableRows);
                }
            }

            Console.WriteLine("All languages deleted successfully.");
        }

        public string GetPopupMessage()
        {
            try
            {
                Thread.Sleep(500);

                // Check for inline validation message first (e.g., duplicate entry)
                var validation = driver.FindElements(validationMessage);
                if (validation.Count > 0)
                {
                    string valText = validation.First().Text.Trim();
                    Console.WriteLine("Validation Message: " + valText);
                    return valText;
                }

                // Check for popup (e.g., "English has been added" or "already exists")
                var popup = driver.FindElements(popupMessage);
                if (popup.Count > 0)
                {
                    string popupText = popup.First().Text.Trim();
                    Console.WriteLine("Popup Message: " + popupText);
                    return popupText;
                }

                return "No message displayed";
            }
            catch (Exception ex)
            {
                Console.WriteLine("No popup or validation message found: " + ex.Message);
                return "No message displayed";
            }
        }

    }
}



