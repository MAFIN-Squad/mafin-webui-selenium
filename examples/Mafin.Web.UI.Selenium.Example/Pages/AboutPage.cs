using Mafin.Web.UI.Selenium.Example.Core;
using Mafin.Web.UI.Selenium.Example.Steps;
using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.Example.Pages;

public class AboutPage : BaseEpamPage
{
    public AboutPage(WdmExtended wdm, ActionsSteps actions)
        : base(wdm, actions)
    {
    }

    public override string Path => "about";

    public By SeeAllLeaderShipLink => By.CssSelector("div[class*=button-in-columns] [href*='who-we-are/leadership']");
}
