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
    {
        return new ChromeDriver((ChromeDriverService)GetDriverSpecificService(), (ChromeOptions)GetDriverSpecificOptions());
    }

    protected override DriverOptions GetDriverSpecificOptions()
    {
        return BuildDriverOptions<ChromeOptions>();
    }

    protected override DriverService GetDriverSpecificService()
    {
        return BuildDriverService(ChromeDriverService.CreateDefaultService());
    }
}
