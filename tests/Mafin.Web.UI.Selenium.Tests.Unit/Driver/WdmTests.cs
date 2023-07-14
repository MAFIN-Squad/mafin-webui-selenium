using Mafin.Web.UI.Selenium.Driver;
using Mafin.Web.UI.Selenium.Models;
using Mafin.Web.UI.Selenium.Tests.Unit.TestDoubles;
using OpenQA.Selenium.Support.UI;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Driver;

public class WdmTests
{
    private readonly Wdm _wdm;

    public WdmTests()
    {
        DummyDriver driver = new();
        TimeoutsConfig timeoutsConfig = new();
        _wdm = new Wdm(driver, timeoutsConfig);
    }

    [Fact]
    public void GetDriver_WhenCalled_ShouldReturnDriverInstance()
    {
        var driver = _wdm.GetDriver();

        driver.Should().NotBeNull();
        driver.Should().BeAssignableTo<DummyDriver>();
    }

    [Fact]
    public void GetActions_WhenCalled_ShouldReturnActionsInstance()
    {
        var actions = _wdm.GetActions();

        actions.Should().NotBeNull();
        actions.Should().BeAssignableTo<OpenQA.Selenium.Interactions.Actions>();
    }

    [Fact]
    public void GetWebDriverWait_WhenCalled_ShouldReturnWebDriverWaitInstance()
    {
        var webDriverWait = _wdm.GetWebDriverWait();

        webDriverWait.Should().NotBeNull();
        webDriverWait.Should().BeAssignableTo<WebDriverWait>();
    }
}
