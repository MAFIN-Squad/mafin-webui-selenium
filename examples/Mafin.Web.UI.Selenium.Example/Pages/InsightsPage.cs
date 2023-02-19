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

    public By IndustriesDropDown => By.XPath("//select[contains(@class,'select--industries')]//..//div[contains(@class,'selected-params')]");

    public By IndustriesDropDownList => By.XPath("//select[contains(@class,'select--industries')]//..//span[@class='checkbox-custom-label']");

    public By ContentTypesDropDown => By.XPath("//select[contains(@class,'select--content-types')]//..//div[contains(@class,'selected-params')]");

    public By ContentTypesDropDownList => By.XPath("//select[contains(@class,'select--content-types')]//..//span[@class='checkbox-custom-label']");

    // search results
    public By IndustriesItem => By.CssSelector("li[class*='detail-pages-list__item']");

    public By ErrorMessage => By.CssSelector(".detail-pages-filter__error-message");

    // methods
    public InsightsPage SelectIndustries(params string[] industries)
    {
        industries.ToList().ForEach(industry =>
        {
            _actions.SelectValueInTheList(IndustriesDropDown, IndustriesDropDownList, industry);
        });
        return this;
    }

    public InsightsPage SelectContentTypes(params string[] contentTypes)
    {
        contentTypes.ToList().ForEach(contentType =>
        {
            _actions.SelectValueInTheList(ContentTypesDropDown, ContentTypesDropDownList, contentType);
        });
        return this;
    }
}
