using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MarsOnBoardingReqnRollProject.Drivers
{
    public class Driver
    {
        public static IWebDriver driver;

        public static void Initialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        public static void Close()
        {
            driver.Quit();
        }
    }
}

