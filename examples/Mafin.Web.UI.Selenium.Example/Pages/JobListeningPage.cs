using Mafin.Web.UI.Selenium.Example.Core;
using Mafin.Web.UI.Selenium.Example.Steps;
using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.Example.Pages;

public class JobListeningPage : BaseEpamPage
{
    public JobListeningPage(WdmExtended wdm, ActionsSteps actions)
        : base(wdm, actions)
    {
    }

    public override string Path => "careers/job-listings";

    public By ApplyForJobTitle => By.CssSelector("div[class='form-component__description'] div");
}
