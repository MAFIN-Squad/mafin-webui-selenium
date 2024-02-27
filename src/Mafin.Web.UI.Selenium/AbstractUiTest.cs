using Mafin.Web.UI.Selenium.Configuration;
using Mafin.Web.UI.Selenium.Driver;
using Mafin.Web.UI.Selenium.Driver.Strategy;
using Mafin.Web.UI.Selenium.Models;
using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium;

public abstract class AbstractUiTest
{
    private Lazy<Wdm>? _driver;

    protected Wdm Driver => _driver!.Value;

    protected virtual WebConfiguration WebConfiguration => WebConfigurationProvider.GetWebConfiguration();

    public virtual void SetUpUiTest()
    {
        var strategy = DriverMapping.GetDriverStrategy(WebConfiguration);
        _driver = new(() => new(strategy.GetDriver(), WebConfiguration.Timeouts!));
    }

    public virtual void CleanupUiTest()
    {
        if (_driver?.IsValueCreated ?? false)
        {
            Driver.GetDriver().Quit();
        }

        _driver = null;
    }
}