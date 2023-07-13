using FluentAssertions;
using Mafin.Web.UI.Selenium.Driver;
using Mafin.Web.UI.Selenium.Models;
using Mafin.Web.UI.Selenium.Tests.Unit.TestDoubles;
using OpenQA.Selenium.Support.UI;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Driver
{
    public class WdmTests
    {
        private readonly Wdm _wdm;

        public WdmTests()
        {
            var driver = new DummyDriver();
            var timeoutsConfig = new TimeoutsConfig();
            _wdm = new Wdm(driver, timeoutsConfig);
        }

        [Fact]
        public void GetDriver_ShouldReturn_NotNull_ChromeDriver()
        {
            var driver = _wdm.GetDriver();

            driver.Should().NotBeNull();
            driver.Should().BeAssignableTo<DummyDriver>();
        }

        [Fact]
        public void GetActions_ShouldReturn_NotNull_ActionsType()
        {
            var actions = _wdm.GetActions();

            actions.Should().NotBeNull();
            actions.Should().BeAssignableTo<OpenQA.Selenium.Interactions.Actions>();
        }

        [Fact]
        public void GetWebDriverWait_ShouldReturn_NotNull_WebDriverWaitType()
        {
            var webDriverWait = _wdm.GetWebDriverWait();

            webDriverWait.Should().NotBeNull();
            webDriverWait.Should().BeAssignableTo<WebDriverWait>();
        }
    }
}
