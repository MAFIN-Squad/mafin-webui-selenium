using FluentAssertions;
using Mafin.Web.UI.Selenium.Driver.Strategy;
using Mafin.Web.UI.Selenium.Models;
using Mafin.Web.UI.Selenium.Tests.Unit.Stubs;
using Moq;
using OpenQA.Selenium.Safari;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Driver.Strategy
{
    public class SafariStrategyTests : SafariStrategy
    {
        private readonly Mock<SafariStrategyTests> _strategy;

        public SafariStrategyTests()
            : base(new WebConfiguration())
        {
            _strategy = new Mock<SafariStrategyTests>() { CallBase = true };
        }

        [Fact]
        public void GetDriver_ShouldReturn_NotNull_SafariDriver()
        {
            _strategy.Setup(x => x.GetSpecificDriver()).Returns(new StubDriver());
            var driver = _strategy.Object.GetSpecificDriver();

            driver.Should().NotBeNull();
            driver.Should().BeAssignableTo<StubDriver>();
        }

        [Fact]
        public void GetDriverSpecificConfig_ShouldReturn_NotNull_SafariConfig()
        {
            var driverConfig = GetDriverSpecificConfig();

            driverConfig.Should().BeNull();
        }

        [Fact]
        public void GetDriverSpecificOptions_ShouldReturn_NotNull_SafariOptions()
        {
            var driverOptions = GetDriverSpecificOptions();

            driverOptions.Should().NotBeNull();
            driverOptions.Should().BeAssignableTo<SafariOptions>();
        }
    }
}
