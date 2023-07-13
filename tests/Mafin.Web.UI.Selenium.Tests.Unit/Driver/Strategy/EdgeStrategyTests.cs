using FluentAssertions;
using Mafin.Web.UI.Selenium.Driver.Strategy;
using Mafin.Web.UI.Selenium.Models;
using Mafin.Web.UI.Selenium.Tests.Unit.TestDoubles;
using Moq;
using OpenQA.Selenium.Edge;
using WebDriverManager.DriverConfigs.Impl;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Driver.Strategy
{
    public class EdgeStrategyTests : EdgeStrategy
    {
        private readonly Mock<EdgeStrategyTests> _strategy;

        public EdgeStrategyTests()
            : base(new WebConfiguration())
        {
            _strategy = new Mock<EdgeStrategyTests>() { CallBase = true };
        }

        [Fact]
        public void GetDriver_ShouldReturn_NotNull_EdgeDriver()
        {
            _strategy.Setup(x => x.GetSpecificDriver()).Returns(new DummyDriver());
            var driver = _strategy.Object.GetSpecificDriver();

            driver.Should().NotBeNull();
            driver.Should().BeAssignableTo<DummyDriver>();
        }

        [Fact]
        public void GetDriverSpecificConfig_ShouldReturn_NotNull_EdgeConfig()
        {
            var driverConfig = GetDriverSpecificConfig();

            driverConfig.Should().NotBeNull();
            driverConfig.Should().BeAssignableTo<EdgeConfig>();
        }

        [Fact]
        public void GetDriverSpecificOptions_ShouldReturn_NotNull_EdgeOptions()
        {
            var driverOptions = GetDriverSpecificOptions();

            driverOptions.Should().NotBeNull();
            driverOptions.Should().BeAssignableTo<EdgeOptions>();
        }
    }
}
