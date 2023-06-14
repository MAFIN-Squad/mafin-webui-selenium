using Mafin.Web.UI.Selenium.Driver.Strategy;
using Mafin.Web.UI.Selenium.Models;
using OpenQA.Selenium.Safari;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Driver.Strategy
{
    public class SafariStrategyTests : SafariStrategy
    {
        public SafariStrategyTests()
            : base(new WebConfiguration())
        {
        }

        [Fact(Skip = "Safari driver creation has not been implemented yet")]
        public void GetDriver_ShouldReturn_NotNull_SafariDriver()
        {
            var driver = GetSpecificDriver();
            driver.Quit();

            Assert.NotNull(driver);
            Assert.IsAssignableFrom<SafariDriver>(driver);
        }

        [Fact]
        public void GetDriverSpecificConfig_ShouldReturn_NotNull_SafariConfig()
        {
            var driverConfig = GetDriverSpecificConfig();

            Assert.Null(driverConfig);
        }

        [Fact]
        public void GetDriverSpecificOptions_ShouldReturn_NotNull_SafariOptions()
        {
            var driverOptions = GetDriverSpecificOptions();

            Assert.NotNull(driverOptions);
            Assert.IsAssignableFrom<SafariOptions>(driverOptions);
        }
    }
}
