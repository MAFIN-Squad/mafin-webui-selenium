using AutoFixture;
using Mafin.Web.UI.Selenium.Extensions;
using NSubstitute;
using OpenQA.Selenium;
using SkiaSharp;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Extensions;

public sealed class WebDriverScreenshotExtensionsTests : IDisposable
{
    private readonly Fixture _fixture = new();
    private readonly string _screenshotDirectory;

    public WebDriverScreenshotExtensionsTests()
    {
        _screenshotDirectory = _fixture.Create<string>();
    }

    public void Dispose() => Directory.Delete(_screenshotDirectory, true);

    [Theory]
    [InlineData(SKEncodedImageFormat.Png)]
    [InlineData(SKEncodedImageFormat.Jpeg)]
    public void TakeViewPortScreenshot_WhenDifferentExtensions_ShouldSaveWithCorrespondingExtension(SKEncodedImageFormat extension)
    {
        var screenshotName = $"{_fixture.Create<string>()}.{extension}";
        var elementScreenshotPath = Path.Combine(_screenshotDirectory, screenshotName);
        By[] elementsToHide = [];

        var driverMock = Substitute.For<IWebDriver, ITakesScreenshot>();
        _ = ((ITakesScreenshot)driverMock).GetScreenshot().Returns(new Screenshot(string.Empty));

        driverMock.TakeViewPortScreenshot(_screenshotDirectory, screenshotName, extension, elementsToHide);

        File.Exists(elementScreenshotPath).Should().BeTrue();
    }
}
