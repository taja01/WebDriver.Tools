# WebDriver.Tools

# WebDriver.Tools.ScreenshotExtension
Purpose: I needed a screenshot from the error... but the error never in the screenshot... (long page, pop-up set to top of the page - no auto scroll there..)
As I see only firefox support full page screenshot:
var driver = new FirefoxDriver();
driver.Url = "https://bank.codes/swift-code-search/";
driver.GetFullPageScreenshot();

But I use RemoveWebDriver with FireFox and Chrome.
That's why I've created this.
