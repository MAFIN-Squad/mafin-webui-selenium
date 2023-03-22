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
        return new ChromeDriver((ChromeOptions)GetDriverSpecificOptions());
    }

    protected override IDriverConfig GetDriverSpecificConfig()
    {
        return new ChromeConfig();
    }

    protected override DriverOptions GetDriverSpecificOptions()
    {
        return BuildDriverOptions<ChromeOptions>();
    }
}
