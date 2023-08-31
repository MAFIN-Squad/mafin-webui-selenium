using Mafin.Web.UI.Selenium.Meta;
using Mafin.Web.UI.Selenium.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Mafin.Web.UI.Selenium.Driver.Strategy;

public abstract class AbstractDriverStrategy
{
    private readonly WebConfiguration _webConfiguration;

    protected AbstractDriverStrategy(WebConfiguration webConfiguration)
    {
        _webConfiguration = webConfiguration;
    }

    public virtual IWebDriver GetDriver()
    {
        var driver = _webConfiguration.RunType == RunType.Local ?
            GetLocalDriver() :
            GetRemoteDriver();

        return driver;
    }

    public virtual IWebDriver GetLocalDriver()
    {
        return SetTimeouts(GetSpecificDriver());
    }

    public virtual IWebDriver GetRemoteDriver()
    {
        var url = _webConfiguration.BrowserConfiguration.Remote.Url;
        var commandTimeout = _webConfiguration.Timeouts.CommandTimeout;
        return SetTimeouts(new RemoteWebDriver(url, GetRemoteOptions().ToCapabilities(), commandTimeout));
    }

    public virtual IWebDriver SetTimeouts(IWebDriver driver)
    {
        driver.Manage().Timeouts().ImplicitWait = _webConfiguration.Timeouts.ImplicitWait;
        driver.Manage().Timeouts().PageLoad = _webConfiguration.Timeouts.PageLoad;
        driver.Manage().Timeouts().AsynchronousJavaScript = _webConfiguration.Timeouts.AsynchronousJavaScript;
        return driver;
    }

    public TimeoutsConfig GetTimeouts() => _webConfiguration.Timeouts;

    protected DriverOptions BuildDriverOptions<T>()
        where T : DriverOptions, new()
    {
        var driverOptions = new T();

        if (_webConfiguration?.BrowserConfiguration?.Capabilities is not null && _webConfiguration.BrowserConfiguration.Capabilities.Any())
        {
            foreach (var capability in _webConfiguration.BrowserConfiguration.Capabilities)
            {
                driverOptions.AddAdditionalOption(capability.Key, capability.Value);
            }
        }

        if (_webConfiguration?.BrowserConfiguration?.Arguments is not null && _webConfiguration.BrowserConfiguration.Arguments.Any())
        {
            driverOptions.AddArguments(_webConfiguration.BrowserConfiguration.Arguments);
        }

        if (_webConfiguration?.BrowserConfiguration?.Extensions is not null && _webConfiguration.BrowserConfiguration.Extensions.Any())
        {
            foreach (var extension in _webConfiguration.BrowserConfiguration.Extensions)
            {
                driverOptions.AddExtension(extension);
            }
        }

        if (_webConfiguration?.BrowserConfiguration?.Preferences is not null && _webConfiguration.BrowserConfiguration.Preferences.Any())
        {
            foreach (var preference in _webConfiguration.BrowserConfiguration.Preferences)
            {
                driverOptions.AddPreference(preference.Key, preference.Value);
            }
        }

        return driverOptions;
    }

    /// <summary>
    /// Configure path and port for local driver.<br/>
    /// <a href="https://www.selenium.dev/documentation/webdriver/drivers/service/">More info</a>.
    /// </summary>
    /// <typeparam name="T">Specific Driver Service.</typeparam>
    /// <param name="defaultService">Service where configuration is applied.</param>
    /// <returns>Configured driver service object.</returns>
    /// <exception cref="ArgumentNullException">Default service based on driver type shoul be provided.</exception>
    protected DriverService BuildDriverService<T>(T defaultService)
        where T : DriverService
    {
        if (defaultService is null)
        {
            throw new ArgumentNullException(nameof(defaultService), "Default service should be created for browser type");
        }

        var driverService = defaultService;

        if (_webConfiguration.LocalDriverPath is not null)
        {
            driverService.DriverServicePath = _webConfiguration.LocalDriverPath;
        }

        if (_webConfiguration.LocalDriverPort is not null)
        {
            driverService.Port = (int)_webConfiguration.LocalDriverPort;
        }

        return driverService;
    }
    
    protected abstract IWebDriver GetSpecificDriver();

    protected abstract DriverOptions GetDriverSpecificOptions();

    protected abstract DriverService GetDriverSpecificService();

    private DriverOptions GetRemoteOptions()
    {
        var driverOptions = GetDriverSpecificOptions();

        var browserVersion = _webConfiguration.BrowserConfiguration.Remote.BrowserVersion;

        driverOptions.BrowserVersion = browserVersion;

        return driverOptions;
    }
}
