using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mafin.Web.UI.Selenium.Driver.Strategy;
using Mafin.Web.UI.Selenium.Models;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using WebDriverManager.DriverConfigs.Impl;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Driver.Strategy
{
    public class FirefoxStrategyTests : FirefoxStrategy
    {
        public FirefoxStrategyTests()
            : base(new WebConfiguration())
        {
        }

        [Fact]
        public void GetDriver_ShouldReturn_NotNull_FirefoxDriver()
        {
            var driver = GetSpecificDriver();
            driver.Quit();

            Assert.NotNull(driver);
            Assert.IsAssignableFrom<FirefoxDriver>(driver);
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
