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
    {
        return new SafariDriver((SafariOptions)GetDriverSpecificOptions());
    }

    protected override DriverOptions GetDriverSpecificOptions()
    {
        return BuildDriverOptions<SafariOptions>();
    }

    protected override DriverService GetDriverSpecificService() => throw new NotImplementedException();
}
