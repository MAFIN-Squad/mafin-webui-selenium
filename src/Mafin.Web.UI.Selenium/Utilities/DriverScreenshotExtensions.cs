using ImageMagick;
using Mafin.Web.UI.Selenium.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

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
    /// <param name="elementsLocatorToHide">Elements to hide on a fullscreen picture.</param>
    public static void TakeFlexibleScreenshot(this IWebDriver driver, ScreenshotType screenshotType = ScreenshotType.DefaultScreen, IWebElement? element = null, string screenshotName = "", string screenshotDirectory = "Screenshots", params By[] elementsLocatorToHide)
    {
        Directory.CreateDirectory(screenshotDirectory);

        switch (screenshotType)
        {
            case ScreenshotType.FullScreen:
                driver.TakeFullPageScreenshot(Path.Combine(screenshotDirectory, screenshotName), elementsLocatorToHide);
                break;
            case ScreenshotType.SingleElement:
                driver.TakeElementScreenshot(element, Path.Combine(screenshotDirectory, screenshotName));
                break;
            case ScreenshotType.DefaultScreen:
                driver.TakeScreenshot().SaveAsFile(Path.Combine(screenshotDirectory, screenshotName));
                break;
        }
    }

    private static void TakeElementScreenshot(this IWebDriver driver, IWebElement element, string filePath)
    {

        using (var screenshotStream = new MemoryStream(driver.TakeScreenshot().AsByteArray))
        using (var image = new MagickImage(screenshotStream))
        {
            var cropRectangle = new MagickGeometry(element.Location.X, element.Location.Y, element.Size.Width, element.Size.Height);

            image.Crop(cropRectangle);
            image.Write(filePath);
        }
    }

    private static void TakeFullPageScreenshot(this IWebDriver driver, string screenshotFilePath = "", params By[] elementsLocatorToHide)
    {
        long windowHeight = (long)((IJavaScriptExecutor)driver).ExecuteScript("return window.innerHeight;");
        long totalHeight = (long)((IJavaScriptExecutor)driver).ExecuteScript("return document.documentElement.scrollHeight;");

        var screenshotCollection = new MagickImageCollection();

        long scrolledHeight = 0;

        ((IJavaScriptExecutor)driver).ExecuteScript("return document.body.style.overflow = 'hidden';");

        foreach (var element in elementsLocatorToHide)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.visibility='hidden';", driver.FindElement(element));
        }

        while (scrolledHeight < totalHeight)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript($"window.scrollTo(0, {scrolledHeight});");

            screenshotCollection.Add(new MagickImage(driver.TakeScreenshot().AsByteArray));

            scrolledHeight += windowHeight;
        }

        using (var vertical = screenshotCollection.AppendVertically())
        {
            vertical.Write(screenshotFilePath);
        }
    }
}
