using ImageMagick;
using Mafin.Web.UI.Selenium.Meta;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace Mafin.Web.UI.Selenium.Extensions;

/// <summary>
/// Provides screenshot extension methods for <see cref="IWebDriver"/>.
/// </summary>
public static class WebDriverScreenshotExtensions
{
    /// <summary>
    /// Takes a customized area screenshot.
    /// </summary>
    /// <param name="driver">WebDriver instance.</param>
    /// <param name="screenshotType">Type of a screenshot to take.</param>
    /// <param name="screenshotDirectory">Directory to save a screenshot file.</param>
    /// <param name="screenshotName">Name for a screenshot file.</param>
    /// <param name="element">Element to make screenshot of (only for <see cref="ScreenshotType.SingleElement"/>).</param>
    /// <param name="elementsToHideLocators">Elements to hide on a screenshot (only for <see cref="ScreenshotType.FullScreen"/>).</param>
    /// <exception cref="ArgumentNullException"><paramref name="element"/> is null when <see cref="ScreenshotType.SingleElement"/>.</exception>
    public static void TakeFlexibleScreenshot(this IWebDriver driver, ScreenshotType screenshotType = default, string? screenshotDirectory = null, string? screenshotName = null, IWebElement? element = null, params By[] elementsToHideLocators)
    {
        if (string.IsNullOrWhiteSpace(screenshotDirectory))
        {
            screenshotDirectory = "Screenshots";
        }

        if (string.IsNullOrWhiteSpace(screenshotName))
        {
            screenshotName = $"Screenshot_{DateTime.UtcNow.ToFileTime()}.png";
        }

        Directory.CreateDirectory(screenshotDirectory);
        var screenshotPath = Path.Combine(screenshotDirectory, screenshotName);

        switch (screenshotType)
        {
            case ScreenshotType.FullScreen:
                driver?.TakeFullPageScreenshot(screenshotPath, elementsToHideLocators);
                break;
            case ScreenshotType.SingleElement:
                driver?.TakeElementScreenshot(element ?? throw new ArgumentNullException(nameof(element)), screenshotPath);
                break;
            case ScreenshotType.ViewPort:
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

    private static void TakeFullPageScreenshot(this IWebDriver driver, string filePath, By[]? elementsToHideLocators)
    {
        elementsToHideLocators ??= Array.Empty<By>();

        var windowHeight = driver.ExecuteJavaScript<long>("return window.innerHeight;");
        var totalHeight = driver.ExecuteJavaScript<long>("return document.documentElement.scrollHeight;");

        driver.ExecuteJavaScript("document.body.style.overflow = 'hidden';");

        foreach (var element in elementsToHideLocators)
        {
            driver.ExecuteJavaScript("arguments[0].style.visibility = 'hidden';", driver.FindElement(element));
        }

        var scrolledHeight = 0L;
        using MagickImageCollection screenshotCollection = new();

        while (scrolledHeight < totalHeight)
        {
            driver.ExecuteJavaScript($"window.scrollTo(0, {scrolledHeight});");

            screenshotCollection.Add(new MagickImage(driver.TakeScreenshot().AsByteArray));

            scrolledHeight += windowHeight;
        }

        using var resultingImage = screenshotCollection.AppendVertically();
        resultingImage.Write(filePath);
    }
}
