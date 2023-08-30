using Mafin.Web.UI.Selenium.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace Mafin.Web.UI.Selenium.Driver.Strategy;

public class EdgeStrategy : AbstractDriverStrategy
{
    public EdgeStrategy(WebConfiguration webConfiguration)
        : base(webConfiguration)
    {
    }

    protected override IWebDriver GetSpecificDriver()
    {
        return new EdgeDriver((EdgeDriverService)GetDriverSpecificService(), (EdgeOptions)GetDriverSpecificOptions());
    }

    protected override DriverOptions GetDriverSpecificOptions()
    {
        return BuildDriverOptions<EdgeOptions>();
    }

    protected override DriverService GetDriverSpecificService()
    {
        return BuildDriverService(EdgeDriverService.CreateDefaultService());
    }
}
