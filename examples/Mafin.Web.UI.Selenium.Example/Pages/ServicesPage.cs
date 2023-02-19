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

    public By ContactUsButton => By.CssSelector("a[aria-label='button for accessibility']");

    public override string Path => "services";
}
