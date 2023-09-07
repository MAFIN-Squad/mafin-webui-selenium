using System.Drawing;
using Mafin.Web.UI.Selenium.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using WDSE;
using WDSE.Decorators;
using WDSE.ScreenshotMaker;

namespace Mafin.Web.UI.Selenium.Utilities;

/// <summary>
/// Presents screenshot extension methods for <see cref="IWebDriver"/>.
/// </summary>
public static class DriverScreenshotExtensions
{
    /// <summary>
    /// Customizible screenshot method.
    /// </summary>
    /// <param name="driver">Webdriver instance.</param>
    /// <param name="screenshotType">Type of a screenshot.</param>
    /// <param name="element">Element to take.</param>
    /// <param name="screenshotName">Name of a screenshot file.</param>
    /// <param name="screenshotDirectory">Directory to save a screenshot file.</param>
    /// <param name="isAttached">If true - screenshot is atteched to the test result, false otherwise.</param>
    /// <param name="elementsToHide">Elements to hide on a fullscreen picture.</param>
    public static void TakeFlexibleScreenshot(this IWebDriver driver, ScreenshotType screenshotType = ScreenshotType.DefaultScreen, IWebElement? element = null, string screenshotName = "", string screenshotDirectory = "Screenshots", params By[] elementsToHide)
    {
        Directory.CreateDirectory(screenshotDirectory);

        switch (screenshotType)
        {
            case ScreenshotType.FullScreen:
                driver.TakeFullScreenshot(Path.Combine(screenshotDirectory, screenshotName), elementsToHide);
                break;
            case ScreenshotType.SingleElement:
                driver.TakeElementScreenshot(element, Path.Combine(screenshotDirectory, screenshotName));
                break;
            case ScreenshotType.DefaultScreen:
                driver.TakeScreenshot().SaveAsFile(Path.Combine(screenshotDirectory, screenshotName));
                break;
        }
    }

    private static void TakeFullScreenshot(this IWebDriver driver, string screenshotFilePath = "", params By[] elementsToHide) =>
        driver.TakeScreenshot(new VerticalCombineDecorator(elementsToHide == null ? new ScreenshotMaker() :
            new ScreenshotMaker().SetElementsToHide(elementsToHide))).ToMagickImage().Write(screenshotFilePath);

    private static void TakeElementScreenshot(this IWebDriver driver, IWebElement element, string filePath)
    {
        var screenshot = driver.TakeScreenshot();
        var image = new Bitmap(new MemoryStream(screenshot.AsByteArray));
        image.Clone(new Rectangle(element.Location, element.Size), image.PixelFormat).Save(filePath);
    }
}
