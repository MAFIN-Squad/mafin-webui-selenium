using Mafin.Web.UI.Selenium.Driver.Strategy;
using Mafin.Web.UI.Selenium.Models;
using OpenQA.Selenium.Firefox;
using WebDriverManager.DriverConfigs.Impl;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Driver.Strategy;

public class FirefoxStrategyTests : FirefoxStrategy
{
    public FirefoxStrategyTests()
        : base(new WebConfiguration())
    {
    }

    [Fact]
    public void GetDriverSpecificConfig_WhenCalled_ShouldReturnFirefoxConfigInstance()
    {
        var driverConfig = GetDriverSpecificConfig();

        driverConfig.Should().NotBeNull();
        driverConfig.Should().BeAssignableTo<FirefoxConfig>();
    }

    [Fact]
    public void GetDriverSpecificOptions_WhenCalled_ShouldReturnFirefoxOptionsInstance()
    {
        var driverOptions = GetDriverSpecificOptions();

        driverOptions.Should().NotBeNull();
        driverOptions.Should().BeAssignableTo<FirefoxOptions>();
    }
}
