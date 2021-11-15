using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace ScreenshotExtension
{
    public class Page
    {
        private IWebDriver _driver;
        public Page(IWebDriver driver)
        {
            _driver = driver;
        }

        public int Width
        {
            get
            {
                var rawValue = _driver.ExecuteJavaScript<object>("return document.body.scrollWidth").ToString();
                return int.Parse(rawValue);
            }
        }

        public int Height
        {
            get
            {
                var rawValue = _driver.ExecuteJavaScript<object>("return document.body.scrollHeight").ToString();
                return int.Parse(rawValue);
            }
        }

        public int ScrollBarX
        {
            get
            {
                var rawValue = _driver.ExecuteJavaScript<object>($"return window.scrollX").ToString();
                return int.Parse(rawValue);
            }
        }

        public int ScrollBarY
        {
            get
            {
                var rawValue = _driver.ExecuteJavaScript<object>($"return window.scrollY").ToString();
                return (int)Convert.ToDouble(rawValue);
            }
        }

        public void ScrollTo(int x, int y)
        {
            _driver.ExecuteJavaScript($"return window.scrollTo({x}, {y});");
        }

        public void ScrollTo(int y)
        {
            _driver.ExecuteJavaScript($"return window.scrollTo(0, {y});");
        }

        public int ViewWidth
        {
            get
            {
                var rawValue = _driver.ExecuteJavaScript<object>($"return window.innerWidth").ToString();
                return int.Parse(rawValue);
            }
        }

        public int ViewHeight
        {
            get
            {
                var rawValue = _driver.ExecuteJavaScript<object>($"return window.innerHeight").ToString();
                return int.Parse(rawValue);
            }
        }

        public void GetFullScreenShot(string fileName)
        {
            //Ideal world would be if IWebDriver has GetFullPageScreenshot method...
            if (_driver is FirefoxDriver firefoxDriver)
            {
                var screenshot = firefoxDriver.GetFullPageScreenshot();
                screenshot.SaveAsFile(fileName);
            }
            else
            {

                ScrollTo(0, 0);
                var pageHeight = Height;
                var viewWidth = ViewWidth;
                var windowHeight = ViewHeight;

                if (windowHeight < pageHeight)
                {
                    var imageList = GetScreenShots(pageHeight, windowHeight);

                    var bitmap = new Bitmap(viewWidth, pageHeight);

                    var current = 0;
                    using (var g = Graphics.FromImage(bitmap))
                    {
                        for (int i = 0; i < imageList.Count; i++)
                        {
                            var currentImage = ConvertToImage(imageList[i].AsByteArray);

                            if (i == imageList.Count - 1)
                            {
                                current -= windowHeight - (pageHeight - ((pageHeight / windowHeight) * windowHeight));
                                g.DrawImage(currentImage, 0, current);
                            }
                            else
                            {
                                g.DrawImage(currentImage, 0, current);
                                current += windowHeight;
                            }
                        }
                    }

                    bitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);

                }
                else
                {
                    CreateScreenShot().SaveAsFile(fileName, ScreenshotImageFormat.Png);
                }
            }
        }

        private IList<Screenshot> GetScreenShots(int pageHeight, int windowHeight)
        {
            var fullJump = pageHeight / windowHeight;
            var screenshotList = new List<Screenshot>();
            for (var i = 0; i < fullJump; i++)
            {
                screenshotList.Add(CreateScreenShot());
                ScrollTo(ScrollBarY + windowHeight);
            }

            screenshotList.Add(CreateScreenShot());

            return screenshotList;
        }

        private Screenshot CreateScreenShot()
        {
            var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();

#if DEBUG
            screenshot.SaveAsFile($"{System.Threading.Thread.CurrentThread.ManagedThreadId + 10}_{DateTime.Now.ToFileTime()}.png");
#endif
            return screenshot;
        }

        private Image ConvertToImage(byte[] byteArrayIn)
        {
            using var ms = new MemoryStream(byteArrayIn);
            return Image.FromStream(ms);
        }
    }
}