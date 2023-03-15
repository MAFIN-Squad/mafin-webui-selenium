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
        var driver = _webConfiguration.RunType == RunType.Local ?
            GetLocalDriver() :
            GetRemoteDriver();

        return driver;
    }

    public virtual IWebDriver GetLocalDriver()
    {
        var isLatestLocal = _webConfiguration.IsLatestLocal;
        var version = isLatestLocal ? VersionResolveStrategy.Latest : VersionResolveStrategy.MatchingBrowser;

        var driverConfig = GetDriverSpecificConfig();
        if (driverConfig is not null)
        {
            new DriverManager().SetUpDriver(driverConfig, version);
        }

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

    protected DriverOptions BuildDriverOptions<T>()
        where T : DriverOptions, new()
    {
        var driverOptions = new T();

        if (_webConfiguration.Capabilities is not null && _webConfiguration.Capabilities.Any())
        {
            foreach (var capability in _webConfiguration.Capabilities)
            {
                driverOptions.AddAdditionalOption(capability.Key, capability.Value);
            }
        }

        if (_webConfiguration.Arguments is not null && _webConfiguration.Arguments.Any())
        {
            driverOptions.AddArguments(_webConfiguration.Arguments);
        }

        if (_webConfiguration.Extensions is not null && _webConfiguration.Extensions.Any())
        {
            foreach (var extension in _webConfiguration.Extensions)
            {
                driverOptions.AddExtension(extension);
            }
        }

        if (_webConfiguration.Preferences is not null && _webConfiguration.Preferences.Any())
        {
            foreach (var preference in _webConfiguration.Preferences)
            {
                driverOptions.AddPreference(preference.Key, preference.Value);
            }
        }

        return driverOptions;
    }

    private DriverOptions GetRemoteOptions()
    {
        var driverOptions = GetDriverSpecificOptions();

        var browserVersion = _webConfiguration.RemoteConfig.BrowserVersion;

        driverOptions.BrowserVersion = browserVersion;

        return driverOptions;
    }
}
