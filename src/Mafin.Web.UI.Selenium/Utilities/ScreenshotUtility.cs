using System.Drawing;
using Mafin.Web.UI.Selenium.Enums;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using WDSE;
using WDSE.Decorators;
using WDSE.ScreenshotMaker;

namespace Mafin.Web.UI.Selenium.Utilities
{
    public static class ScreenshotUtility
    {
        public static void TakeFlexibleScreenshot(this IWebDriver driver, ScreenshotType screenshotType = ScreenshotType.DefaultScreen, IWebElement? element = null, string screenshotName = "", string screenshotsFolder = "Screenshots", params By[] elementsToHide)
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var screenshotDirectory = Path.Combine(Environment.CurrentDirectory, screenshotsFolder);

                if (!Directory.Exists(screenshotDirectory))
                {
                    Directory.CreateDirectory(screenshotDirectory);
                }

                switch (screenshotType)
                {
                    case ScreenshotType.FullScreen:
                        driver.TakeScreenshot(new VerticalCombineDecorator(elementsToHide == null ? new ScreenshotMaker() : new ScreenshotMaker().SetElementsToHide(elementsToHide))).ToMagickImage().Write(Path.Combine(screenshotDirectory, screenshotName));
                        break;
                    case ScreenshotType.SingleElement:
                        driver.TakeElementScreenshot(element, Path.Combine(screenshotDirectory, screenshotName));
                        break;
                    case ScreenshotType.DefaultScreen:
                        ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(Path.Combine(screenshotDirectory, screenshotName));
                        break;
                }

                TestContext.AddTestAttachment(Path.Combine(screenshotDirectory, screenshotName));
            }
        }

        private static void TakeElementScreenshot(this IWebDriver driver, IWebElement element, string filePath)
        {
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            var image = Image.FromStream(new MemoryStream(screenshot.AsByteArray)) as Bitmap;
            image.Clone(new Rectangle(element.Location, element.Size), image.PixelFormat).Save(filePath);
        }
    }
}
