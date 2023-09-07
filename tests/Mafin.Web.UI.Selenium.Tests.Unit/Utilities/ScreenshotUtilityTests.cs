using Mafin.Web.UI.Selenium.Enums;
using Mafin.Web.UI.Selenium.Utilities;
using Moq;
using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Utilities
{
    public class ScreenshotUtilityTests : IDisposable
    {
        private const string ScreenshotsFolderName = "Screenshots";
        private const string ScreenshotsFileName = "testscreenshot";

        public void Dispose() => Directory.Delete(ScreenshotsFolderName, true);

        [Theory]
        [InlineData("png")]
        [InlineData("jpg")]
        public void TakeFlexibleScreenshot_WhenTakeScreensotWithDifferentExtensions_ShouldSaveWithCorrespondingExtension(string extension)
        {
            // Arrange
            var driverMock = new Mock<IWebDriver>();

            var elementScreenshotPath = Path.Combine(ScreenshotsFolderName, $"{ScreenshotsFileName}.{extension}");

            var elementsToHide = new List<By>();
            var elementMock = new Mock<IWebElement>();

            var takesScreenshotDriverMock = driverMock.As<ITakesScreenshot>();
            takesScreenshotDriverMock.Setup(d => d.GetScreenshot()).Returns(new Screenshot(string.Empty));

            // Act
            driverMock.Object.TakeFlexibleScreenshot(ScreenshotType.DefaultScreen, elementMock.Object, $"{ScreenshotsFileName}.{extension}", ScreenshotsFolderName, elementsToHide.ToArray());

            // Assert
            File.Exists(elementScreenshotPath).Should().BeTrue();
        }

        [Fact]
        public void TakeFlexibleScreenshot_WhenTakeDefaultScreenshot_ShouldSaveScreenshot()
        {
            // Arrange
            var driverMock = new Mock<IWebDriver>();

            var pictureName = $"{ScreenshotsFileName}.png";
            var elementScreenshotPath = Path.Combine(ScreenshotsFolderName, pictureName);

            var elementsToHide = new List<By>();
            var elementMock = new Mock<IWebElement>();

            var takesScreenshotDriverMock = driverMock.As<ITakesScreenshot>();
            takesScreenshotDriverMock.Setup(d => d.GetScreenshot()).Returns(new Screenshot(string.Empty));

            // Act
            driverMock.Object.TakeFlexibleScreenshot(ScreenshotType.DefaultScreen, elementMock.Object, pictureName, ScreenshotsFolderName, elementsToHide.ToArray());

            // Assert
            File.Exists(elementScreenshotPath).Should().BeTrue();
        }

        [Fact]
        public void TakeFlexibleScreenshot_WhenTakeScreenshot_ShouldCreateFolder()
        {
            // Arrange
            var driverMock = new Mock<IWebDriver>();

            var elementsToHide = new List<By>();
            var elementMock = new Mock<IWebElement>();

            var takesScreenshotDriverMock = driverMock.As<ITakesScreenshot>();
            takesScreenshotDriverMock.Setup(d => d.GetScreenshot()).Returns(new Screenshot(string.Empty));

            // Act
            driverMock.Object.TakeFlexibleScreenshot(ScreenshotType.DefaultScreen, elementMock.Object, $"{ScreenshotsFileName}.png", ScreenshotsFolderName, elementsToHide.ToArray());

            // Assert
            File.Exists(ScreenshotsFileName).Should().BeTrue();
        }
    }
}
