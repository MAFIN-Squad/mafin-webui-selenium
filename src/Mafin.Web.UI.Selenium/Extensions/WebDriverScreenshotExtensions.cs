using ImageMagick;
using Mafin.Web.UI.Selenium.Meta;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace Mafin.Web.UI.Selenium.Extensions;

/// <summary>
/// Presents screenshot extension methods for <see cref="IWebDriver"/>.
/// </summary>
public static class WebDriverScreenshotExtensions
{
    /// <summary>
    /// Customizible screenshot method.
    /// </summary>
    /// <param name="driver">Webdriver instance.</param>
    /// <param name="visibleArea">Type of a screenshot.</param>
    /// <param name="element">Element to take.</param>
    /// <param name="screenshotName">Name of a screenshot file.</param>
    /// <param name="screenshotDirectory">Directory to save a screenshot file.</param>
    /// <param name="elementsToHideLocators">Elements to hide on a fullscreen picture.</param>
    public static void TakeFlexibleScreenshot(this IWebDriver driver, VisibleArea visibleArea = VisibleArea.DefaultScreen, IWebElement? element = null, string? screenshotName = null, string? screenshotDirectory = null, params By[] elementsToHideLocators)
    {
        if (string.IsNullOrEmpty(screenshotName))
        {
            screenshotName = $"Screenshot_{DateTime.Now.ToFileTime()}";
        }

        if (string.IsNullOrEmpty(screenshotDirectory))
        {
            screenshotDirectory = "Screenshots";
        }

        Directory.CreateDirectory(screenshotDirectory);
        var screenshotPath = Path.Combine(screenshotDirectory, screenshotName);

        switch (visibleArea)
        {
            case VisibleArea.FullScreen:
                driver?.TakeFullPageScreenshot(screenshotPath, elementsToHideLocators);
                break;
            case VisibleArea.SingleElement:
                driver?.TakeElementScreenshot(element ?? throw new NoSuchElementException(), screenshotPath);
                break;
            case VisibleArea.DefaultScreen:
                driver?.TakeScreenshot().SaveAsFile(screenshotPath);
                break;
            default:
                break;
        }
    }

    private static void TakeElementScreenshot(this IWebDriver driver, IWebElement element, string filePath)
    {
        using MemoryStream screenshotStream = new(driver.TakeScreenshot().AsByteArray);
        using MagickImage image = new(screenshotStream);
        MagickGeometry cropRectangle = new(element.Location.X, element.Location.Y, element.Size.Width, element.Size.Height);

        image.Crop(cropRectangle);
        image.Write(filePath);
    }

    private static void TakeFullPageScreenshot(this IWebDriver driver, string? screenshotFilePath = null, params By[]? elementsToHideLocators)
    {
        if (elementsToHideLocators is null)
        {
            elementsToHideLocators = Array.Empty<By>();
        }

        var windowHeight = driver.ExecuteJavaScript<long>("return window.innerHeight;");
        var totalHeight = driver.ExecuteJavaScript<long>("return document.documentElement.scrollHeight;");
        
        using MagickImageCollection screenshotCollection = new();

        var scrolledHeight = 0L;

        driver.ExecuteJavaScript("return document.body.style.overflow = 'hidden';");

        foreach (var element in elementsToHideLocators)
        {
            driver.ExecuteJavaScript("arguments[0].style.visibility='hidden';", driver.FindElement(element));
        }

        while (scrolledHeight < totalHeight)
        {
            driver.ExecuteJavaScript($"window.scrollTo(0, {scrolledHeight});");

            screenshotCollection.Add(new MagickImage(driver.TakeScreenshot().AsByteArray));

            scrolledHeight += windowHeight;
        }

        using var vertical = screenshotCollection.AppendVertically();
        vertical.Write(screenshotFilePath);
    }
}
