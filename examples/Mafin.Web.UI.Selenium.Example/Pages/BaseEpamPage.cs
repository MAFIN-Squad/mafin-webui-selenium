using Mafin.Web.UI.Selenium.Example.Core;
using Mafin.Web.UI.Selenium.Example.Steps;
using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.Example.Pages;

public class BaseEpamPage : BaseWebPage
{
    protected readonly ActionsSteps _actions;

    public BaseEpamPage(WdmExtended wdm, ActionsSteps actions)
        : base(wdm)
    {
        _actions = actions;
    }

    public override Uri SiteUrl => new("https://www.epam.com");

    public By PrivacyDialog => By.CssSelector("div[aria-label='Privacy']");

    public By AcceptAll => By.Id("onetrust-accept-btn-handler");

    public BaseEpamPage AcceptCookiesIfPresent()
    {
        if (_actions.IsElementPresent(PrivacyDialog) && Wdm.GetElement(PrivacyDialog, isIntercept: false).Displayed)
        {
            try
            {
                Wdm.GetElement(AcceptAll, isIntercept: false).Click();
            }
            catch
            {
                // Element is not interactable exception might happen because the dialog is appearing slowly. It can be fixed by finding appropriate wait.
            }
        }

        return this;
    }
}
