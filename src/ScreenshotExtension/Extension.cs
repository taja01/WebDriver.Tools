using OpenQA.Selenium;

namespace ScreenshotExtension
{
    public static class Extension
    {
        /// <summary>
        /// Create a full screenshot from the page
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        /// <param name="fileName">File name. Simple name or with path.</param>
        /// <remarks>Only PNG image file supported!</remarks>
        public static void GetFullPageScreenshot(this IWebDriver driver, string fileName)
        {
            new Page(driver).GetFullScreenShot(fileName);
        }
    }
}
