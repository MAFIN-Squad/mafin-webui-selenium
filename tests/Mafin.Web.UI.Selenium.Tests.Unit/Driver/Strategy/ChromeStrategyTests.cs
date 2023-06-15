using Mafin.Web.UI.Selenium.Driver.Strategy;
using Mafin.Web.UI.Selenium.Models;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Driver.Strategy
{
    public class ChromeStrategyTests : ChromeStrategy
    {
        public ChromeStrategyTests()
            : base(new WebConfiguration())
        {
        }

        [Fact(Skip = "Driver can not be created on the test runner")]
        public void GetDriver_ShouldReturn_NotNull_ChromeDriver()
        {
            var driver = GetSpecificDriver();
            driver.Quit();

            Assert.NotNull(driver);
            Assert.IsAssignableFrom<ChromeDriver>(driver);
        }

        [Fact]
        public void GetDriverSpecificConfig_ShouldReturn_NotNull_ChromeConfig()
        {
            var driverConfig = GetDriverSpecificConfig();

            Assert.NotNull(driverConfig);
            Assert.IsAssignableFrom<ChromeConfig>(driverConfig);
        }

        [Fact]
        public void GetDriverSpecificOptions_ShouldReturn_NotNull_ChromeOptions()
        {
            var driverOptions = GetDriverSpecificOptions();

            Assert.NotNull(driverOptions);
            Assert.IsAssignableFrom<ChromeOptions>(driverOptions);
        }
    }
}
