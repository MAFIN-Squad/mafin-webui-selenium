using Mafin.Web.UI.Selenium.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Mafin.Web.UI.Selenium.Driver.Strategy;

public class ChromeStrategy : AbstractDriverStrategy
{
    public ChromeStrategy(WebConfiguration webConfiguration)
        : base(webConfiguration)
    {
    }

    protected override IWebDriver GetSpecificDriver()
        => new ChromeDriver(GetSpecificDriverService() as ChromeDriverService, GetSpecificDriverOptions() as ChromeOptions);

    protected override DriverOptions GetSpecificDriverOptions() => BuildDriverOptions<ChromeOptions>();

    protected override DriverService GetSpecificDriverService() => BuildDriverService(ChromeDriverService.CreateDefaultService());
}
