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
        => new EdgeDriver(GetSpecificDriverService() as EdgeDriverService, GetSpecificDriverOptions() as EdgeOptions);

    protected override DriverOptions GetSpecificDriverOptions() => BuildDriverOptions<EdgeOptions>();

    protected override DriverService GetSpecificDriverService() => BuildDriverService(EdgeDriverService.CreateDefaultService());
}
