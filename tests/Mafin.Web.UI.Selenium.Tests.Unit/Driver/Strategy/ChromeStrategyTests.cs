using Mafin.Web.UI.Selenium.Driver.Strategy;
using Mafin.Web.UI.Selenium.Models;
using OpenQA.Selenium.Chrome;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Driver.Strategy;

public class ChromeStrategyTests() : ChromeStrategy(new WebConfiguration())
{
    [Fact]
    public void GetSpecificDriverOptions_WhenCalled_ShouldReturnChromeOptionsInstance()
    {
        var driverOptions = GetSpecificDriverOptions();

        driverOptions.Should().NotBeNull();
        driverOptions.Should().BeAssignableTo<ChromeOptions>();
    }

    [Fact]
    public void GetSpecificDriverService_WhenCalled_ShouldReturnChromeServiceInstance()
    {
        var driverConfig = GetSpecificDriverService();

        driverConfig.Should().NotBeNull();
        driverConfig.Should().BeAssignableTo<ChromeDriverService>();
    }
}
