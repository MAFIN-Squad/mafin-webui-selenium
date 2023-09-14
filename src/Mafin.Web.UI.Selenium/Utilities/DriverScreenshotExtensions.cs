using ImageMagick;
using Mafin.Web.UI.Selenium.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using SkiaSharp;

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
    /// <param name="elementsToHide">Elements to hide on a fullscreen picture.</param>
    public static void TakeFlexibleScreenshot(this IWebDriver driver, ScreenshotType screenshotType = ScreenshotType.DefaultScreen, IWebElement? element = null, string screenshotName = "", string screenshotDirectory = "Screenshots", params By[] elementsLocatorToHide)
    {
        Directory.CreateDirectory(screenshotDirectory);

        switch (screenshotType)
        {
            case ScreenshotType.FullScreen:
                driver.TakeFullScreenshot(Path.Combine(screenshotDirectory, screenshotName), elementsLocatorToHide);
                break;
            case ScreenshotType.SingleElement:
                driver.TakeElementScreenshot(element, Path.Combine(screenshotDirectory, screenshotName));
                break;
            case ScreenshotType.DefaultScreen:
                driver.TakeScreenshot().SaveAsFile(Path.Combine(screenshotDirectory, screenshotName));
                break;
        }
    }

    private static void TakeFullScreenshot(this IWebDriver driver, string screenshotFilePath = "", params By[] elementsLocatorToHide) =>
        driver.TakeFullPageScreenshot(screenshotFilePath, elementsLocatorToHide);

    private static void TakeElementScreenshot(this IWebDriver driver, IWebElement element, string filePath, SKEncodedImageFormat format = SKEncodedImageFormat.Png, int quality = 100)
    {
        using (var image = SKBitmap.Decode(driver.TakeScreenshot().AsByteArray))
        {
            using (var croppedImage = new SKBitmap(element.Size.Width, element.Size.Height))
            using (var canvas = new SKCanvas(croppedImage))
            {
                canvas.DrawBitmap(image, new SKRect(element.Location.X, element.Location.Y, element.Location.X + element.Size.Width, element.Location.Y + element.Size.Height), new SKRect(0, 0, element.Size.Width, element.Size.Height));

                using (var output = File.OpenWrite(filePath))
                {
                    croppedImage.Encode(format, quality).SaveTo(output);
                }
            }
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
