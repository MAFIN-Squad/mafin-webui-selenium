using Mafin.Web.UI.Selenium.Meta;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using SkiaSharp;

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
    /// <param name="imageFormat">Image format.</param>
    /// <param name="elementsToHideLocators">Elements to hide on a screenshot (only for <see cref="ScreenshotType.FullScreen"/>).</param>
    /// <exception cref="ArgumentNullException"><paramref name="element"/> is null when <see cref="ScreenshotType.SingleElement"/>.</exception>
    public static void TakeFlexibleScreenshot(this IWebDriver driver, ScreenshotType screenshotType = default, string? screenshotDirectory = null, string? screenshotName = null, IWebElement? element = null, SKEncodedImageFormat imageFormat = SKEncodedImageFormat.Png, params By[] elementsToHideLocators)
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
                driver?.TakeFullPageScreenshot(screenshotPath, imageFormat, elementsToHideLocators);
                break;
            case ScreenshotType.SingleElement:
                driver?.TakeElementScreenshot(element ?? throw new ArgumentNullException(nameof(element)), screenshotPath, imageFormat);
                break;
            case ScreenshotType.ViewPort:
                driver?.TakeScreenshot().SaveAsFile(screenshotPath);
                break;
            default:
                break;
        }
    }

    private static void TakeElementScreenshot(this IWebDriver driver, IWebElement element, string filePath, SKEncodedImageFormat imageFormat)
    {
        using var screenshot = SKBitmap.Decode(driver.TakeScreenshot().AsByteArray);

        var info = new SKImageInfo(element.Size.Width, element.Size.Height);

        using var surface = SKSurface.Create(info);
        surface.Canvas.DrawBitmap(screenshot, -element.Location.X, -element.Location.Y);

        using var fileStream = File.OpenWrite(filePath);
        surface.Snapshot().Encode(imageFormat, default).SaveTo(fileStream);
    }

    private static void TakeFullPageScreenshot(this IWebDriver driver, string filePath, SKEncodedImageFormat imageFormat, By[]? elementsToHideLocators = null)
    {
        elementsToHideLocators ??= [];

        var windowHeight = driver.ExecuteJavaScript<long>("return window.innerHeight;");
        var totalHeight = driver.ExecuteJavaScript<long>("return document.documentElement.scrollHeight;");

        driver.ExecuteJavaScript("document.body.style.overflow = 'hidden';");

        foreach (var element in elementsToHideLocators)
        {
            driver.ExecuteJavaScript("arguments[0].style.visibility = 'hidden';", driver.FindElement(element));
        }

        var scrolledHeight = 0L;
        List<SKBitmap> screenshotCollection = [];

        while (scrolledHeight < totalHeight)
        {
            driver.ExecuteJavaScript($"window.scrollTo(0, {scrolledHeight});");

            screenshotCollection.Add(SKBitmap.Decode(driver.TakeScreenshot().AsByteArray));

            scrolledHeight += windowHeight;
        }

        using var stream = File.OpenWrite(filePath);
        AppendVertically(screenshotCollection).Encode(stream, imageFormat, default);
    }

    private static SKBitmap AppendVertically(List<SKBitmap> images)
    {
        var totalHeight = images.Sum(image => image.Height);
        var maxWidth = images.Max(image => image.Width);

        var finalBitmap = new SKBitmap(maxWidth, totalHeight);
        using var canvas = new SKCanvas(finalBitmap);

        var currentY = 0;

        foreach (var image in images)
        {
            canvas.DrawBitmap(image, new SKRect(0, currentY, image.Width, currentY + image.Height));
            currentY += image.Height;
        }

        return finalBitmap;
    }
}
