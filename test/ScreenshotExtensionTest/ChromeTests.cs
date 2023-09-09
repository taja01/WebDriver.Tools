using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using ScreenshotExtension;

namespace ScreenshotExtensionTest
{
    public class ChromeTests
    {

        ChromeDriver driver;

        [SetUp]
        public void Setup()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--no-sandbox");
            chromeOptions.AddArgument("--headless");
            chromeOptions.AddArgument("--allow-running-insecure-content");
            chromeOptions.AddArgument("--ignore-gpu-blocklis");
            chromeOptions.AcceptInsecureCertificates = true;

            // Disable automation info-bar message
            chromeOptions.AddExcludedArgument("enable-automation");
            // Disable pop up 'Disable developer mode extensions'
            chromeOptions.AddAdditionalOption("useAutomationExtension", false);

            // Disable chrome save your password pop up
            chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            chromeOptions.AddArgument("disable-blink-features=AutomationControlled");
            chromeOptions.AddArgument("--window-size=1920,1080");
            chromeOptions.AcceptInsecureCertificates = true;
            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void AfterTest()
        {
            driver.Close();
            driver.Dispose();
        }

        [Test]
        public void Test1()
        {
            driver.Url = "https://en.wikipedia.org/wiki/Main_Page";

            driver.GetFullPageScreenshot("chrome_test_long_page.png");
        }

        [Test]
        public void Test2()
        {
            driver.Url = "http://info.cern.ch/";

            driver.GetFullPageScreenshot("chrome_test_simple_page.png");
        }
    }
}