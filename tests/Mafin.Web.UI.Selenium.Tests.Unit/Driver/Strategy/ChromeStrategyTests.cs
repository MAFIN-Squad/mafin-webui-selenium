using Mafin.Web.UI.Selenium.Driver.Strategy;
using Mafin.Web.UI.Selenium.Models;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Driver.Strategy;

public class ChromeStrategyTests : ChromeStrategy
{
    public ChromeStrategyTests()
        : base(new WebConfiguration())
    {
    }

    [Fact]
    public void GetDriverSpecificConfig_WhenCalled_ShouldReturnChromeConfigInstance()
    {
        var driverConfig = GetDriverSpecificConfig();

        driverConfig.Should().NotBeNull();
        driverConfig.Should().BeAssignableTo<ChromeConfig>();
    }

    [Fact]
    public void GetDriverSpecificOptions_WhenCalled_ShouldReturnChromeOptionsInstance()
    {
        var driverOptions = GetDriverSpecificOptions();

        driverOptions.Should().NotBeNull();
        driverOptions.Should().BeAssignableTo<ChromeOptions>();
    }
}
