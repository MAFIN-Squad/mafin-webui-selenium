using Mafin.Web.UI.Selenium.Example.Core;
using Mafin.Web.UI.Selenium.Example.Steps;
using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.Example.Pages;

public class ServicesPage : BaseEpamPage
{
    public ServicesPage(WdmExtended wdm, ActionsSteps actions)
        : base(wdm, actions)
    {
    }

    public By ContactUsButton => By.CssSelector("div[class='button'] a[href*='contact']");

    public override string Path => "services";
}
