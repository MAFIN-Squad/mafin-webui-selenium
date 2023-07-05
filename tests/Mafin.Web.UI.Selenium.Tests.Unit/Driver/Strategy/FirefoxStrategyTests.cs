using Mafin.Web.UI.Selenium.Driver.Strategy;
using Mafin.Web.UI.Selenium.Models;
using Mafin.Web.UI.Selenium.Tests.Unit.Stubs;
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
            _strategy.Setup(x => x.GetSpecificDriver()).Returns(new StubDriver());
            var driver = _strategy.Object.GetSpecificDriver();

            Assert.NotNull(driver);
            Assert.IsAssignableFrom<StubDriver>(driver);
        }

        [Fact]
        public void GetDriverSpecificConfig_ShouldReturn_NotNull_FirefoxConfig()
        {
            var driverConfig = GetDriverSpecificConfig();

            Assert.NotNull(driverConfig);
            Assert.IsAssignableFrom<FirefoxConfig>(driverConfig);
        }

        [Fact]
        public void GetDriverSpecificOptions_ShouldReturn_NotNull_FirefoxOptions()
        {
            var driverOptions = GetDriverSpecificOptions();

            Assert.NotNull(driverOptions);
            Assert.IsAssignableFrom<FirefoxOptions>(driverOptions);
        }
    }
}
