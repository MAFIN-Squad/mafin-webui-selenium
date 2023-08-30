using Mafin.Web.UI.Selenium.Driver.Strategy;
using Mafin.Web.UI.Selenium.Models;
using OpenQA.Selenium.Firefox;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Driver.Strategy;

public class FirefoxStrategyTests : FirefoxStrategy
{
    public FirefoxStrategyTests()
        : base(new WebConfiguration())
    {
    }

    [Fact]
    public void GetDriverSpecificOptions_WhenCalled_ShouldReturnFirefoxOptionsInstance()
    {
        var driverOptions = GetDriverSpecificOptions();

        driverOptions.Should().NotBeNull();
        driverOptions.Should().BeAssignableTo<FirefoxOptions>();
    }

    [Fact]
    public void GetDriverSpecificService_WhenCalled_ShouldReturnFirefoxServiceInstance()
    {
        var driverConfig = GetDriverSpecificService();

        driverConfig.Should().NotBeNull();
        driverConfig.Should().BeAssignableTo<FirefoxDriverService>();
    }
}
