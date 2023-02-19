using Mafin.Web.UI.Selenium.Example.Core;
using Mafin.Web.UI.Selenium.Example.Steps;
using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.Example.Pages;

public class CareersPage : BaseEpamPage
{
    public CareersPage(WdmExtended wdm, ActionsSteps actions)
        : base(wdm, actions)
    {
    }

    public override string Path => "careers";

    public By SkillsInput => By.XPath("//select[@name='department']//..//div[contains(@class,'selected-params')]");

    public By SkillsInputList => By.XPath("//select[@name='department']//..//span[@class='checkbox-custom-label']");

    public By FindButton => By.CssSelector("button[type='submit']");

    // search results
    public By SearchResultItem => By.CssSelector("li[class='search-result__item']");

    public By SearchResultItemName => By.CssSelector("a[class='search-result__item-name']");

    public By SearchResultItemViewAndApplyButton => By.CssSelector("a[class='search-result__item-apply']");

    // methods
    public CareersPage SelectSkills(params string[] skills)
    {
        skills.ToList().ForEach(skill =>
        {
            _actions.SelectValueInTheList(SkillsInput, SkillsInputList, skill);
        });
        return this;
    }
}
