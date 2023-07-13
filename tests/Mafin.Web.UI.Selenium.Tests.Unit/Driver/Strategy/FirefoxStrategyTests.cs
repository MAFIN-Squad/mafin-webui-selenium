using FluentAssertions;
using Mafin.Web.UI.Selenium.Driver.Strategy;
using Mafin.Web.UI.Selenium.Models;
using Mafin.Web.UI.Selenium.Tests.Unit.TestDoubles;
using Moq;
using OpenQA.Selenium.Firefox;
using WebDriverManager.DriverConfigs.Impl;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Driver.Strategy
{
    public class FirefoxStrategyTests : FirefoxStrategy
    {
        private readonly Mock<FirefoxStrategyTests> _strategy;

        public FirefoxStrategyTests()
            : base(new WebConfiguration())
        {
            _strategy = new Mock<FirefoxStrategyTests>() { CallBase = true };
        }

        [Fact]
        public void GetDriver_ShouldReturn_NotNull_FirefoxDriver()
        {
            _strategy.Setup(x => x.GetSpecificDriver()).Returns(new DummyDriver());
            var driver = _strategy.Object.GetSpecificDriver();

            driver.Should().NotBeNull();
            driver.Should().BeAssignableTo<DummyDriver>();
        }

        [Fact]
        public void GetDriverSpecificConfig_ShouldReturn_NotNull_FirefoxConfig()
        {
            var driverConfig = GetDriverSpecificConfig();

            driverConfig.Should().NotBeNull();
            driverConfig.Should().BeAssignableTo<FirefoxConfig>();
        }

        [Fact]
        public void GetDriverSpecificOptions_ShouldReturn_NotNull_FirefoxOptions()
        {
            var driverOptions = GetDriverSpecificOptions();

            driverOptions.Should().NotBeNull();
            driverOptions.Should().BeAssignableTo<FirefoxOptions>();
        }
    }
}
