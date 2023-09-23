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
    public void GetSpecificDriverOptions_WhenCalled_ShouldReturnSafariOptionsInstance()
    {
        var driverOptions = GetSpecificDriverOptions();

        driverOptions.Should().NotBeNull();
        driverOptions.Should().BeAssignableTo<SafariOptions>();
    }

    [Fact]
    public void GetSpecificDriverService_WhenCalled_ShouldReturnSafariServiceInstance()
    {
        var driverConfig = GetSpecificDriverService();

        driverConfig.Should().NotBeNull();
        driverConfig.Should().BeAssignableTo<SafariDriverService>();
    }
}
