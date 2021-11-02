using NUnit.Framework;
using OpenQA.Selenium.Firefox;
using ScreenshotExtension;

namespace ScreenshotExtensionTest
{
    public class FirefoxTests
    {

        FirefoxDriver driver;

        [SetUp]
        public void Setup()
        {
            var firefoxOptions = new FirefoxOptions();
            firefoxOptions.AddArgument("--no-sandbox");
            firefoxOptions.AddArgument("--allow-running-insecure-content");
            firefoxOptions.AddArgument("--ignore-gpu-blocklis");
            firefoxOptions.AddArgument("--headless");
            firefoxOptions.AcceptInsecureCertificates = true;
            driver = new FirefoxDriver(firefoxOptions);

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

            driver.Url = "https://bank.codes/swift-code-search/";

            driver.GetFullPageScreenshot("test_long_page.png");
        }

        [Test]
        public void Test2()
        {
            driver.Url = "http://info.cern.ch/";

            driver.GetFullPageScreenshot("test_simple_page.png");
        }
    }
}