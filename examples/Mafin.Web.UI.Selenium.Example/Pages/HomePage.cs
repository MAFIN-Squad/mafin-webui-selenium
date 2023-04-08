using Mafin.Web.UI.Selenium.Example.Core;
using Mafin.Web.UI.Selenium.Example.Steps;
using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.Example.Pages;

public class HomePage : BaseEpamPage
{
    public HomePage(WdmExtended wdm, ActionsSteps actions) : base(wdm, actions)
    {
    }

    public By ExploreOurClientWork => By.XPath("//div[@class=\"owl-item active\"]//a[contains(@class, 'single-slide__cta-link') and contains(., 'Our Client Work')]");
}

