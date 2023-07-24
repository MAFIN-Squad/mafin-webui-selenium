using Mafin.Web.UI.Selenium.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

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

    protected override DriverOptions GetDriverSpecificOptions()
    {
        return BuildDriverOptions<FirefoxOptions>();
    }
}
