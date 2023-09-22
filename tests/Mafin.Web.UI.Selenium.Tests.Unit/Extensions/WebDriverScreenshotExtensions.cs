using Mafin.Web.UI.Selenium.Meta;
using Mafin.Web.UI.Selenium.Extensions;
using Moq;
using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Extensions;

public class WebDriverScreenshotExtensions : IDisposable
{
    private const string ScreenshotsFolderName = "Screenshots";
    private const string ScreenshotsFileName = "testscreenshot";

    public void Dispose() => Directory.Delete(ScreenshotsFolderName, true);

    [Theory]
    [InlineData("png")]
    [InlineData("jpg")]
    public void TakeFlexibleScreenshot_WhenTakeScreensotWithDifferentExtensions_ShouldSaveWithCorrespondingExtension(string extension)
    {
        var driverMock = new Mock<IWebDriver>();

        var elementScreenshotPath = Path.Combine(ScreenshotsFolderName, $"{ScreenshotsFileName}.{extension}");

        var elementsToHide = new List<By>();
        var elementMock = new Mock<IWebElement>();

        var takesScreenshotDriverMock = driverMock.As<ITakesScreenshot>();
        takesScreenshotDriverMock.Setup(d => d.GetScreenshot()).Returns(new Screenshot(string.Empty));

        driverMock.Object.TakeFlexibleScreenshot(VisibleArea.DefaultScreen, elementMock.Object, $"{ScreenshotsFileName}.{extension}", ScreenshotsFolderName, elementsToHide.ToArray());

        File.Exists(elementScreenshotPath).Should().BeTrue();
    }
}
