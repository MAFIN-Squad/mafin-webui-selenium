using Mafin.Web.UI.Selenium.Example.Core;
using Mafin.Web.UI.Selenium.Example.Steps;
using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.Example.Pages;

public class SearchPage : BaseEpamPage
{
    public SearchPage(WdmExtended wdm, ActionsSteps actions)
        : base(wdm, actions)
    {
    }

    public override string Path => "search";

    public By SearchResultsLinks => By.ClassName("search-results__title-link");
}
