using Mafin.Web.UI.Selenium.Enums;
using Mafin.Web.UI.Selenium.Utilities;
using Moq;
using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Utilities;

public class DriverScreenshotExtensionsTests : IDisposable
{
    private const string ScreenshotsFolder = "Screenshots";
    private const string ScreenshotFileName = "testScreenshot";

    public void Dispose() => Directory.Delete(ScreenshotsFolder, true);

    [Theory]
    [InlineData("png")]
    [InlineData("jpg")]
    public void TakeFlexibleScreenshot_WhenTakeScreensotWithDifferentExtensions_ShouldSaveWithCorrespondingExtension(string extension)
    {
        // Arrange
        var fileName = $"{ScreenshotFileName}.{extension}";
        var elementScreenshotPath = Path.Combine(ScreenshotsFolder, fileName);
        List<By> elementsToHide = new();

        Mock<IWebElement> elementMock = new();
        Mock<IWebDriver> driverMock = new();

        var takesScreenshotDriverMock = driverMock.As<ITakesScreenshot>();
        takesScreenshotDriverMock.Setup(d => d.GetScreenshot()).Returns(new Screenshot(string.Empty));

        // Act
        driverMock.Object.TakeFlexibleScreenshot(ScreenshotType.DefaultScreen, elementMock.Object, fileName, ScreenshotsFolder, elementsToHide.ToArray());

        // Assert
        File.Exists(elementScreenshotPath).Should().BeTrue();
    }

    [Fact]
    public void TakeFlexibleScreenshot_WhenTakeDefaultScreenshot_ShouldSaveScreenshot()
    {
        // Arrange
        const string fileName = $"{ScreenshotFileName}.png";
        var elementScreenshotPath = Path.Combine(ScreenshotsFolder, fileName);
        List<By> elementsToHide = new();

        Mock<IWebElement> elementMock = new();
        Mock<IWebDriver> driverMock = new();

        var takesScreenshotDriverMock = driverMock.As<ITakesScreenshot>();
        takesScreenshotDriverMock.Setup(d => d.GetScreenshot()).Returns(new Screenshot(string.Empty));

        // Act
        driverMock.Object.TakeFlexibleScreenshot(ScreenshotType.DefaultScreen, elementMock.Object, fileName, ScreenshotsFolder, elementsToHide.ToArray());

        // Assert
        File.Exists(elementScreenshotPath).Should().BeTrue();
    }

    [Fact]
    public void TakeFlexibleScreenshot_WhenTakeScreenshot_ShouldCreateFolder()
    {
        // Arrange
        List<By> elementsToHide = new();

        Mock<IWebDriver> driverMock = new();
        Mock<IWebElement> elementMock = new();

        var takesScreenshotDriverMock = driverMock.As<ITakesScreenshot>();
        takesScreenshotDriverMock.Setup(d => d.GetScreenshot()).Returns(new Screenshot(string.Empty));

        // Act
        driverMock.Object.TakeFlexibleScreenshot(ScreenshotType.DefaultScreen, elementMock.Object, $"{ScreenshotFileName}.png", ScreenshotsFolder, elementsToHide.ToArray());

        // Assert
        Directory.Exists(ScreenshotsFolder).Should().BeTrue();
    }
}
