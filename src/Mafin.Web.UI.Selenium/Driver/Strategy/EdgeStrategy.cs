using Mafin.Web.UI.Selenium.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using WebDriverManager.DriverConfigs;
using WebDriverManager.DriverConfigs.Impl;

namespace Mafin.Web.UI.Selenium.Driver.Strategy;

public class EdgeStrategy : AbstractDriverStrategy
{
    public EdgeStrategy(WebConfiguration webConfiguration)
        : base(webConfiguration)
    {
    }

    protected override IWebDriver GetSpecificDriver()
    {
        return new EdgeDriver((EdgeOptions)GetDriverSpecificOptions());
    }

    protected override IDriverConfig GetDriverSpecificConfig()
    {
        return new EdgeConfig();
    }

    protected override DriverOptions GetDriverSpecificOptions()
    {
        return BuildDriverOptions<EdgeOptions>();
    }
}
