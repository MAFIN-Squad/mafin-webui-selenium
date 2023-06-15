using Mafin.Web.UI.Selenium.Driver.Strategy;
using Mafin.Web.UI.Selenium.Models;
using OpenQA.Selenium.Edge;
using WebDriverManager.DriverConfigs.Impl;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Driver.Strategy
{
    public class EdgeStrategyTests : EdgeStrategy
    {
        public EdgeStrategyTests()
            : base(new WebConfiguration())
        {
        }

        [Fact(Skip = "Driver can not be created on the test runner")]
        public void GetDriver_ShouldReturn_NotNull_EdgeDriver()
        {
            var driver = GetSpecificDriver();
            driver.Quit();

            Assert.NotNull(driver);
            Assert.IsAssignableFrom<EdgeDriver>(driver);
        }

        [Fact]
        public void GetDriverSpecificConfig_ShouldReturn_NotNull_EdgeConfig()
        {
            var driverConfig = GetDriverSpecificConfig();

            Assert.NotNull(driverConfig);
            Assert.IsAssignableFrom<EdgeConfig>(driverConfig);
        }

        [Fact]
        public void GetDriverSpecificOptions_ShouldReturn_NotNull_EdgeOptions()
        {
            var driverOptions = GetDriverSpecificOptions();

            Assert.NotNull(driverOptions);
            Assert.IsAssignableFrom<EdgeOptions>(driverOptions);
        }
    }
}
