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
    public void GetDriverSpecificOptions_WhenCalled_ShouldReturnEdgeOptionsInstance()
    {
        var driverOptions = GetDriverSpecificOptions();

        driverOptions.Should().NotBeNull();
        driverOptions.Should().BeAssignableTo<EdgeOptions>();
    }
}
