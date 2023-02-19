using Mafin.Web.UI.Selenium.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs;
using WebDriverManager.DriverConfigs.Impl;

namespace Mafin.Web.UI.Selenium.Driver.Strategy;

public class ChromeStrategy : AbstractDriverStrategy
{
    public ChromeStrategy(WebConfiguration webConfiguration)
        : base(webConfiguration)
    {
    }

    protected override IWebDriver GetSpecificDriver()
    {
        ChromeOptions chromeOptions = new();

        // Use full screen mode
        chromeOptions.AddArgument("--start-maximized");

        // Use same language on all environments
        chromeOptions.AddArgument("--lang=en");

        // Hide "Chrome is being controlled by automated test software." bar
        chromeOptions.AddExcludedArgument("enable-automation");

        // Disable password save dialog
        chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
        chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);

        return new ChromeDriver(chromeOptions);
    }

    protected override IDriverConfig GetDriverSpecificConfig()
    {
        return new ChromeConfig();
    }

    protected override DriverOptions GetDriverSpecificOptions()
    {
        return new ChromeOptions();
    }
}
