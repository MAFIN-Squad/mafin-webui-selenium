using Mafin.Web.UI.Selenium.Driver.Strategy;
using Mafin.Web.UI.Selenium.Models;
using OpenQA.Selenium.Safari;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Driver.Strategy;

public class SafariStrategyTests : SafariStrategy
{
    public SafariStrategyTests()
        : base(new WebConfiguration())
    {
    }

    [Fact]
    public void GetDriverSpecificConfig_WhenCalled_ShouldReturnNull()
    {
        var driverConfig = GetDriverSpecificConfig();

        driverConfig.Should().BeNull();
    }

    [Fact]
    public void GetDriverSpecificOptions_WhenCalled_ShouldReturnSafariOptionsInstance()
    {
        var driverOptions = GetDriverSpecificOptions();

        driverOptions.Should().NotBeNull();
        driverOptions.Should().BeAssignableTo<SafariOptions>();
    }
}
