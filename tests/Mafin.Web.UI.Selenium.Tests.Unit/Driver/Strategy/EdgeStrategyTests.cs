using Mafin.Web.UI.Selenium.Driver.Strategy;
using Mafin.Web.UI.Selenium.Models;
using OpenQA.Selenium.Edge;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Driver.Strategy;

public class EdgeStrategyTests : EdgeStrategy
{
    public EdgeStrategyTests()
        : base(new WebConfiguration())
    {
    }

    [Fact]
    public void GetSpecificDriverOptions_WhenCalled_ShouldReturnEdgeOptionsInstance()
    {
        var driverOptions = GetSpecificDriverOptions();

        driverOptions.Should().NotBeNull();
        driverOptions.Should().BeAssignableTo<EdgeOptions>();
    }

    [Fact]
    public void GetSpecificDriverService_WhenCalled_ShouldReturnEdgeServiceInstance()
    {
        var driverConfig = GetSpecificDriverService();

        driverConfig.Should().NotBeNull();
        driverConfig.Should().BeAssignableTo<EdgeDriverService>();
    }
}
