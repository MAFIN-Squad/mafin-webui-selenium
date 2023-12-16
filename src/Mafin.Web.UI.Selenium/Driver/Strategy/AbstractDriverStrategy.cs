using Mafin.Web.UI.Selenium.Extensions;
using Mafin.Web.UI.Selenium.Meta;
using Mafin.Web.UI.Selenium.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Mafin.Web.UI.Selenium.Driver.Strategy;

public abstract class AbstractDriverStrategy
{
    private readonly WebConfiguration _webConfiguration;
    private readonly BrowserConfiguration _browserConfiguration;

    protected AbstractDriverStrategy(WebConfiguration webConfiguration)
    {
        _webConfiguration = webConfiguration;
        _browserConfiguration = webConfiguration?.BrowserConfiguration ?? new();
    }

    public TimeoutsConfig Timeouts => _webConfiguration.Timeouts ?? new();

    public virtual IWebDriver GetDriver() => _webConfiguration.RunType == RunType.Local
        ? GetLocalDriver()
        : GetRemoteDriver();

    public virtual IWebDriver GetLocalDriver() => ConfigureDriverTimeouts(GetSpecificDriver());

    public virtual IWebDriver GetRemoteDriver() =>
        ConfigureDriverTimeouts(new RemoteWebDriver(_browserConfiguration.Remote?.Url, GetRemoteOptions().ToCapabilities(), Timeouts.CommandTimeout));

    public virtual IWebDriver ConfigureDriverTimeouts(IWebDriver driver)
    {
        driver.Manage().Timeouts().ImplicitWait = Timeouts.ImplicitWait;
        driver.Manage().Timeouts().PageLoad = Timeouts.PageLoad;
        driver.Manage().Timeouts().AsynchronousJavaScript = Timeouts.AsynchronousJavaScript;
        return driver;
    }

    protected DriverOptions BuildDriverOptions<T>()
        where T : DriverOptions, new()
    {
        T driverOptions = new();

        if (_browserConfiguration.Capabilities is IDictionary<string, object> capabilities && capabilities.Any())
        {
            foreach (var capability in capabilities)
            {
                driverOptions.AddAdditionalOption(capability.Key, capability.Value);
            }
        }

        if (_browserConfiguration.Arguments is IList<string> arguments && arguments.Any())
        {
            driverOptions.AddArguments(arguments);
        }

        if (_browserConfiguration.Extensions is IList<string> extensions && extensions.Any())
        {
            foreach (var extension in extensions)
            {
                driverOptions.AddExtension(extension);
            }
        }

        if (_browserConfiguration.Preferences is IDictionary<string, object> preferences && preferences.Any())
        {
            foreach (var preference in preferences)
            {
                driverOptions.AddPreference(preference.Key, preference.Value);
            }
        }

        return driverOptions;
    }

    /// <summary>
    /// Configures path and port for local driver.<br/>
    /// <a href="https://www.selenium.dev/documentation/webdriver/drivers/service/">More info</a>.
    /// </summary>
    /// <typeparam name="T">Specific <see cref="DriverService"/>.</typeparam>
    /// <param name="defaultService">Service where configuration is applied.</param>
    /// <returns>Configured driver service object.</returns>
    /// <exception cref="ArgumentNullException">Default service based on driver type is not provided.</exception>
    protected DriverService BuildDriverService<T>(T defaultService)
        where T : DriverService
    {
        var service = defaultService
            ?? throw new ArgumentNullException(nameof(defaultService), "Default service should be created for a browser type");

        if (_webConfiguration.LocalDriverPath is string path)
        {
            service.DriverServicePath = path;
        }

        if (_webConfiguration.LocalDriverPort is int port)
        {
            service.Port = port;
        }

        return service;
    }

    protected abstract IWebDriver GetSpecificDriver();

    protected abstract DriverOptions GetSpecificDriverOptions();

    protected abstract DriverService GetSpecificDriverService();

    private DriverOptions GetRemoteOptions()
    {
        var driverOptions = GetSpecificDriverOptions();
        driverOptions.BrowserVersion = _browserConfiguration.Remote?.BrowserVersion;
        return driverOptions;
    }
}
