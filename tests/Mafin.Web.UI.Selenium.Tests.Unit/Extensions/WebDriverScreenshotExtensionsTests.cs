using AutoFixture;
using Mafin.Web.UI.Selenium.Extensions;
using Mafin.Web.UI.Selenium.Meta;
using Moq;
using OpenQA.Selenium;

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

    // TODO: revisit this test, seems we don't have any functionality related to extensions.
    [Theory]
    [InlineData("png")]
    [InlineData("jpg")]
    public void TakeFlexibleScreenshot_WhenDifferentExtensions_ShouldSaveWithCorrespondingExtension(string extension)
    {
        var screenshotName = $"{_fixture.Create<string>()}.{extension}";
        var elementScreenshotPath = Path.Combine(_screenshotDirectory, screenshotName);
        var elementsToHide = Array.Empty<By>();

        Mock<IWebDriver> driverMock = new();
        Mock<IWebElement> elementMock = new();

        var takesScreenshotDriverMock = driverMock.As<ITakesScreenshot>();
        takesScreenshotDriverMock.Setup(d => d.GetScreenshot()).Returns(new Screenshot(string.Empty));

        driverMock.Object.TakeFlexibleScreenshot(ScreenshotType.ViewPort, _screenshotDirectory, screenshotName, elementMock.Object, elementsToHide);

        File.Exists(elementScreenshotPath).Should().BeTrue();
    }
}