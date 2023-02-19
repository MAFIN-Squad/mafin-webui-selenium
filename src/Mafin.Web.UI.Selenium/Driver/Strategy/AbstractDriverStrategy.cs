using Mafin.Web.UI.Selenium.Meta;
using Mafin.Web.UI.Selenium.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using WebDriverManager;
using WebDriverManager.DriverConfigs;
using WebDriverManager.Helpers;

namespace Mafin.Web.UI.Selenium.Driver.Strategy;

public abstract class AbstractDriverStrategy
{
    private readonly WebConfiguration _webConfiguration;

    protected AbstractDriverStrategy(WebConfiguration webConfiguration)
    {
        _webConfiguration = webConfiguration;
    }

    protected abstract IWebDriver GetSpecificDriver();

    protected abstract DriverOptions GetDriverSpecificOptions();

    protected abstract IDriverConfig GetDriverSpecificConfig();

    public virtual IWebDriver GetDriver()
    {
        return _webConfiguration.RunType == RunType.Local ?
            GetLocalDriver() :
            GetRemoteDriver();
    }

    public virtual IWebDriver GetLocalDriver()
    {
        var isLatestLocal = _webConfiguration.IsLatestLocal;
        var version = isLatestLocal ? VersionResolveStrategy.Latest : VersionResolveStrategy.MatchingBrowser;
        new DriverManager().SetUpDriver(GetDriverSpecificConfig(), version);
        return SetTimeouts(GetSpecificDriver());
    }

    public virtual IWebDriver GetRemoteDriver()
    {
        var url = _webConfiguration.RemoteConfig.Url;
        var commandTimeout = _webConfiguration.TimeoutsConfig.CommandTimeout;
        return SetTimeouts(new RemoteWebDriver(url, GetRemoteOptions().ToCapabilities(), commandTimeout));
    }

    public virtual IWebDriver SetTimeouts(IWebDriver driver)
    {
        driver.Manage().Timeouts().ImplicitWait = _webConfiguration.TimeoutsConfig.ImplicitWait;
        driver.Manage().Timeouts().PageLoad = _webConfiguration.TimeoutsConfig.PageLoad;
        driver.Manage().Timeouts().AsynchronousJavaScript = _webConfiguration.TimeoutsConfig.AsynchronousJavaScript;
        return driver;
    }

    public TimeoutsConfig GetTimeouts() => _webConfiguration.TimeoutsConfig;

    private DriverOptions GetRemoteOptions()
    {
        var driverOptions = GetDriverSpecificOptions();

        var browserVersion = _webConfiguration.RemoteConfig.BrowserVersion;

        driverOptions.BrowserVersion = browserVersion;

        return driverOptions;
    }
}
