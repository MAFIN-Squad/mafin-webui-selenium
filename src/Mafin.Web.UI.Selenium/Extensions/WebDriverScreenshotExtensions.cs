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
    /// Takes a screenshot of the current screen visible area.
    /// </summary>
    /// <param name="driver">WebDriver instance.</param>
    /// <param name="screenshotDirectory">Directory to save a screenshot file.</param>
    /// <param name="screenshotName">Screenshot file name.</param>
    /// <param name="imageFormat">Image format.</param>
    /// <param name="elementsToHide">Locators of elements to hide on a screenshot.</param>
    public static void TakeViewPortScreenshot(this IWebDriver driver, string? screenshotDirectory = null, string? screenshotName = null, SKEncodedImageFormat imageFormat = SKEncodedImageFormat.Png, params By[] elementsToHide)
    {
        driver.HideElements(elementsToHide);
        driver.TakeScreenshot().SaveAsFile(GetScreenshotPath(screenshotDirectory, screenshotName, imageFormat));
    }

    /// <summary>
    /// Takes a screenshot of a single element on a page.
    /// </summary>
    /// <param name="driver">WebDriver instance.</param>
    /// <param name="element">Element to make screenshot of.</param>
    /// <param name="screenshotDirectory">Directory to save a screenshot file.</param>
    /// <param name="screenshotName">Screenshot file name.</param>
    /// <param name="imageFormat">Image format.</param>
    /// <exception cref="ArgumentNullException"><paramref name="element"/> is null.</exception>
    public static void TakeElementScreenshot(this IWebDriver driver, IWebElement element, string? screenshotDirectory = null, string? screenshotName = null, SKEncodedImageFormat imageFormat = SKEncodedImageFormat.Png)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element));
        }

        using var bitmap = SKBitmap.Decode(driver.TakeScreenshot().AsByteArray);

        var info = new SKImageInfo(element.Size.Width, element.Size.Height);

        using var surface = SKSurface.Create(info);
        surface.Canvas.DrawBitmap(bitmap, -element.Location.X, -element.Location.Y);

        using var fileStream = File.OpenWrite(GetScreenshotPath(screenshotDirectory, screenshotName, imageFormat));
        surface.Snapshot().Encode(imageFormat, default).SaveTo(fileStream);
    }

    /// <summary>
    /// Takes a screenshot of the full web page.
    /// </summary>
    /// <param name="driver">WebDriver instance.</param>
    /// <param name="screenshotDirectory">Directory to save a screenshot file.</param>
    /// <param name="screenshotName">Screenshot file name.</param>
    /// <param name="imageFormat">Image format.</param>
    /// <param name="elementsToHide">Locators of elements to hide on a screenshot.</param>
    public static void TakeFullPageScreenshot(this IWebDriver driver, string? screenshotDirectory = null, string? screenshotName = null, SKEncodedImageFormat imageFormat = SKEncodedImageFormat.Png, params By[] elementsToHide)
    {
        var windowHeight = driver.ExecuteJavaScript<long>("return window.innerHeight;");
        var totalHeight = driver.ExecuteJavaScript<long>("return document.documentElement.scrollHeight;");

        driver.ExecuteJavaScript("document.body.style.overflow = 'hidden';");
        driver.HideElements(elementsToHide);

        var scrolledHeight = 0L;
        List<SKBitmap> screenshotCollection = [];

        while (scrolledHeight < totalHeight)
        {
            driver.ExecuteJavaScript($"window.scrollTo(0, {scrolledHeight});");

            screenshotCollection.Add(SKBitmap.Decode(driver.TakeScreenshot().AsByteArray));

            scrolledHeight += windowHeight;
        }

        using var stream = File.OpenWrite(GetScreenshotPath(screenshotDirectory, screenshotName, imageFormat));
        using var bitmap = AppendVertically(screenshotCollection);
        _ = bitmap.Encode(stream, imageFormat, default);
    }

    private static void HideElements(this IWebDriver driver, By[] elementsToHide)
    {
        elementsToHide ??= [];
        foreach (var element in elementsToHide)
        {
            driver.ExecuteJavaScript("arguments[0].style.visibility = 'hidden';", driver.FindElement(element));
        }
    }

    private static string GetScreenshotPath(string? screenshotDirectory, string? screenshotName, SKEncodedImageFormat imageFormat)
    {
        if (string.IsNullOrWhiteSpace(screenshotDirectory))
        {
            screenshotDirectory = "Screenshots";
        }

        if (string.IsNullOrWhiteSpace(screenshotName))
        {
            screenshotName = $"Screenshot_{DateTime.UtcNow.ToFileTime()}.{imageFormat}";
        }

        Directory.CreateDirectory(screenshotDirectory);

        return Path.Combine(screenshotDirectory, screenshotName);
    }

    private static SKBitmap AppendVertically(List<SKBitmap> images)
    {
        var totalHeight = images.Sum(i => i.Height);
        var maxWidth = images.Max(i => i.Width);

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
