using Mafin.Web.UI.Selenium.Driver;
using Mafin.Web.UI.Selenium.Models;
using Mafin.Web.UI.Selenium.Tests.Unit.Stubs;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Driver
{
    public class WdmTests
    {
        private readonly Wdm _wdm;

        public WdmTests()
        {
            var driver = new StubDriver();
            var timeoutsConfig = new TimeoutsConfig();
            _wdm = new Wdm(driver, timeoutsConfig);
        }

        [Fact]
        public void GetDriver_ShouldReturn_NotNull_ChromeDriver()
        {
            var driver = _wdm.GetDriver();

            Assert.NotNull(driver);
            Assert.IsAssignableFrom<StubDriver>(driver);
        }

        [Fact]
        public void GetActions_ShouldReturn_NotNull_ActionsType()
        {
            var actions = _wdm.GetActions();

            Assert.NotNull(actions);
            Assert.IsAssignableFrom<Actions>(actions);
        }

        [Fact]
        public void GetWebDriverWait_ShouldReturn_NotNull_WebDriverWaitType()
        {
            var webDriverWait = _wdm.GetWebDriverWait();

            Assert.NotNull(webDriverWait);
            Assert.IsAssignableFrom<WebDriverWait>(webDriverWait);
        }
    }
}
