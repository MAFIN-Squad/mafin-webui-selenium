using Mafin.Web.UI.Selenium.Example.Core;
using Mafin.Web.UI.Selenium.Example.Steps;
using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.Example.Pages;

public class LeadershipPage : BaseEpamPage
{
    public LeadershipPage(WdmExtended wdm, ActionsSteps actions)
        : base(wdm, actions)
    {
    }

    public override string Path => "about/who-we-are/leadership";

    public By Names => By.CssSelector("h3[class*='leadership-viewer-ui']");
}
