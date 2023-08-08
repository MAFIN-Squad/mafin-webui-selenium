using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Mafin.Web.UI.Selenium.Enums;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using WDSE;
using WDSE.Decorators;
using WDSE.ScreenshotMaker;

namespace Mafin.Web.UI.Selenium.Utilities
{
    public static class ScreenshotUtility
    {
        public static void TakeFlexibleScreenshot(this IWebDriver driver, ScreenshotType screenshotType = ScreenshotType.DefaultScreen, IWebElement? element = null, string screenshotName = "", string screenshotDirectory = "Screenshots", bool isAttached = false, params By[] elementsToHide)
        {
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
                    driver.TakeScreenshot().SaveAsFile(Path.Combine(screenshotDirectory, screenshotName));
                    break;
            }

            if (isAttached)
            {
                TestContext.AddTestAttachment(Path.Combine(screenshotDirectory, screenshotName));
            }
        }

        private static void TakeElementScreenshot(this IWebDriver driver, IWebElement element, string filePath)
        {
            var screenshot = driver.TakeScreenshot();
            var image = new Bitmap(new MemoryStream(screenshot.AsByteArray));
            image.Clone(new Rectangle(element.Location, element.Size), image.PixelFormat).Save(filePath);
        }
    }
}
