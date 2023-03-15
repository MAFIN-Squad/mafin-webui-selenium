using Mafin.Web.UI.Selenium.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using WebDriverManager.DriverConfigs;
using WebDriverManager.DriverConfigs.Impl;

namespace Mafin.Web.UI.Selenium.Driver.Strategy;

public class FirefoxStrategy : AbstractDriverStrategy
{
    public FirefoxStrategy(WebConfiguration webConfiguration)
        : base(webConfiguration)
    {
    }

    protected override IWebDriver GetSpecificDriver()
    {
        return new FirefoxDriver((FirefoxOptions)GetDriverSpecificOptions());
    }

    protected override IDriverConfig GetDriverSpecificConfig()
    {
        return new FirefoxConfig();
    }

    protected override DriverOptions GetDriverSpecificOptions()
    {
        return BuildDriverOptions<FirefoxOptions>();
    }
}
