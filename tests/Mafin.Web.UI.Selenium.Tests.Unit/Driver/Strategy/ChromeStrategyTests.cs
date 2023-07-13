using FluentAssertions;
using Mafin.Web.UI.Selenium.Driver.Strategy;
using Mafin.Web.UI.Selenium.Models;
using Mafin.Web.UI.Selenium.Tests.Unit.TestDoubles;
using Moq;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Driver.Strategy
{
    public class ChromeStrategyTests : ChromeStrategy
    {
        private readonly Mock<ChromeStrategyTests> _strategy;

        public ChromeStrategyTests()
            : base(new WebConfiguration())
        {
            _strategy = new Mock<ChromeStrategyTests>() { CallBase = true };
        }

        [Fact]
        public void GetDriver_ShouldReturn_NotNull_ChromeDriver()
        {
            _strategy.Setup(x => x.GetSpecificDriver()).Returns(new DummyDriver());
            var driver = _strategy.Object.GetSpecificDriver();

            driver.Should().NotBeNull();
            driver.Should().BeAssignableTo<DummyDriver>();
        }

        [Fact]
        public void GetDriverSpecificConfig_ShouldReturn_NotNull_ChromeConfig()
        {
            var driverConfig = GetDriverSpecificConfig();

            driverConfig.Should().NotBeNull();
            driverConfig.Should().BeAssignableTo<ChromeConfig>();
        }

        [Fact]
        public void GetDriverSpecificOptions_ShouldReturn_NotNull_ChromeOptions()
        {
            var driverOptions = GetDriverSpecificOptions();

            driverOptions.Should().NotBeNull();
            driverOptions.Should().BeAssignableTo<ChromeOptions>();
        }
    }
}
