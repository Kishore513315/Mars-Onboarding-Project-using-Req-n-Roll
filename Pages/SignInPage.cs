using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace Mars_Onboarding_Project.Pages
{
    public class SignInPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public SignInPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // Locators
        private readonly By signInLink = By.XPath("//a[text()='Sign In']");
        private readonly By emailInput = By.Name("email");
        private readonly By passwordInput = By.Name("password");
        private readonly By loginButton = By.XPath("//button[text()='Login']");
        private readonly By dashboardUserLocator = By.XPath("//span[contains(text(),'Hi')]");

        // Navigate to the login page
        public void GoToLoginPage()
        {
            driver.Navigate().GoToUrl("http://localhost:5003/");
            wait.Until(ExpectedConditions.ElementToBeClickable(signInLink)).Click();
        }

        // Enter credentials
        public void EnterCredentials(string email, string password)
        {
            var emailField = wait.Until(ExpectedConditions.ElementIsVisible(emailInput));
            emailField.Clear();
            emailField.SendKeys(email);

            var passwordField = wait.Until(ExpectedConditions.ElementIsVisible(passwordInput));
            passwordField.Clear();
            passwordField.SendKeys(password);
        }

        // Click login button
        public void ClickLogin()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(loginButton)).Click();
        }

        // Check if dashboard is visible after login
        public bool IsDashboardVisible()
        {
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(dashboardUserLocator));
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Check if username is displayed on the dashboard
        public bool IsUserNameDisplayed()
        {
            try
            {
                var userName = wait.Until(ExpectedConditions.ElementIsVisible(dashboardUserLocator));
                return userName.Displayed;
            }
            catch
            {
                return false;
            }
        }
    }
}
