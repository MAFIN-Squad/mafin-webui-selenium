using Mafin.Web.UI.Selenium.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Safari;

namespace Mafin.Web.UI.Selenium.Driver.Strategy;

public class SafariStrategy : AbstractDriverStrategy
{
    public SafariStrategy(WebConfiguration webConfiguration)
        : base(webConfiguration)
    {
    }

    protected override IWebDriver GetSpecificDriver()
        => new SafariDriver(GetSpecificDriverService() as SafariDriverService, GetSpecificDriverOptions() as SafariOptions);

    protected override DriverOptions GetSpecificDriverOptions() => BuildDriverOptions<SafariOptions>();

    protected override DriverService GetSpecificDriverService() => BuildDriverService(SafariDriverService.CreateDefaultService());
}
