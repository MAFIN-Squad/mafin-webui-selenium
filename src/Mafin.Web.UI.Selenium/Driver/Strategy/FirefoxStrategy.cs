using Mafin.Web.UI.Selenium.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Mafin.Web.UI.Selenium.Driver.Strategy;

public class FirefoxStrategy(WebConfiguration webConfiguration) : AbstractDriverStrategy(webConfiguration)
{
    protected override IWebDriver GetSpecificDriver() =>
        new FirefoxDriver(GetSpecificDriverService() as FirefoxDriverService, GetSpecificDriverOptions() as FirefoxOptions);

    protected override DriverOptions GetSpecificDriverOptions() => BuildDriverOptions<FirefoxOptions>();

    protected override DriverService GetSpecificDriverService() => BuildDriverService(FirefoxDriverService.CreateDefaultService());
}
