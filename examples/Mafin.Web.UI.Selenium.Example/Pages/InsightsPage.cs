using Mafin.Web.UI.Selenium.Example.Core;
using Mafin.Web.UI.Selenium.Example.Steps;
using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.Example.Pages;

public class InsightsPage : BaseEpamPage
{
    public InsightsPage(WdmExtended wdm, ActionsSteps actions)
        : base(wdm, actions)
    {
    }

    public override string Path => "insights";

    public By IndustriesDropDown => By.XPath("//span[contains(.,'Industry')]");

    public By IndustriesDropDownList => By.XPath("//li[contains(@class,'filter-list')]");

    public By ApplyButton => By.CssSelector("button[class*='filter-list-btn-apply']");

    // search results
    public By IndustriesList => By.CssSelector("ul[class*='detail-pages-list-23__tag-list']");

    public By IndustriesItem => By.CssSelector("li[class*='detail-pages-list-23__tag']");

    // methods
    public InsightsPage SelectIndustries(params string[] industries)
    {
        industries.ToList().ForEach(industry =>
        {
            _actions.SelectValueInTheList(IndustriesDropDown, IndustriesDropDownList, industry);
        });
        return this;
    }
}
