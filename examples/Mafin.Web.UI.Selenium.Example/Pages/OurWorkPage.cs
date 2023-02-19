using Mafin.Web.UI.Selenium.Example.Core;
using Mafin.Web.UI.Selenium.Example.Steps;
using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.Example.Pages;

public class OurWorkPage : BaseEpamPage
{
    private const int MaxScrollCount = 30;

    public OurWorkPage(WdmExtended wdm, ActionsSteps actions)
        : base(wdm, actions)
    {
    }

    public override string Path => "our-work";

    public By OfficesTabs => By.CssSelector(".tabs__title");

    public By ActiveOffices => By.CssSelector("div[class='owl-item active'] button");

    public By NextItem => By.CssSelector("div[class='tabs__item js-tabs-item active'] .owl-next");

    public By ActiveCountryParent => By.CssSelector("div[class='locations-viewer__country-details active']");

    public By ActiveCountryName => By.CssSelector(".locations-viewer__country-name");

    public By ActiveCountryOfficesName => By.CssSelector(".locations-viewer__office-name");

    // methods
    public OurWorkPage SelectTab(string tab)
    {
        _actions.ClickOnFirstInTheListByText(OfficesTabs, tab);

        return this;
    }

    public OurWorkPage SelectOffice(string office)
    {
        var isFound = false;
        for (var i = 0; i < MaxScrollCount; i++)
        {
            var item = _actions.GetElements(ActiveOffices)
                .FirstOrDefault(w => _actions.GetText(w).Equals(office));
            if (item != null)
            {
                isFound = true;
                _actions.Click(item);
                break;
            }

            _actions.Click(NextItem);
        }

        if (!isFound)
        {
            throw new Exception($"No item found office='{office}' after '{MaxScrollCount}' scrolls");
        }

        return this;
    }

    public IWebElement GetActiveOffice()
    {
        var parent = _actions.GetElement(ActiveCountryParent);
        return _actions.GetElement(parent, ActiveCountryName);
    }

    public List<IWebElement> GetActiveOfficeNames()
    {
        var parent = _actions.GetElement(ActiveCountryParent);
        return _actions.GetElements(parent, ActiveCountryOfficesName);
    }
}
