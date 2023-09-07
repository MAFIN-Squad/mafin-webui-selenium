using System.Globalization;
using Mafin.Web.UI.Selenium.Example.Core;
using Mafin.Web.UI.Selenium.Example.Steps;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace Mafin.Web.UI.Selenium.Example.Pages;

public class ClientWorkPage : BaseEpamPage
{
    private const int MaxScrollCount = 30;
    private const string BaseTabsNavigationLocator = "div[class='tabs-23__item js-tabs-item active']";

    public ClientWorkPage(WdmExtended wdm, ActionsSteps actions)
        : base(wdm, actions)
    {
    }

    public override string Path => "services/client-work";

    public By OfficesTabs => By.CssSelector(".tabs-23__title");

    public By ActiveOffices => By.CssSelector("div[class*='locations-viewer-23__carousel'] div[class='owl-item active'] button");

    public By NavigationItems => By.CssSelector($"{BaseTabsNavigationLocator} .owl-nav");

    public By NextItem => By.CssSelector($"{BaseTabsNavigationLocator} .owl-next");

    public By ActiveCountryParent => By.CssSelector("div[class='locations-viewer-23__country-details active']");

    public By ActiveCountryOfficesName => By.CssSelector("li[class='locations-viewer-23__office'] h5");

    // methods
    public ClientWorkPage SelectTab(string tab)
    {
        _actions.ClickOnFirstInTheListByText(OfficesTabs, tab);

        return this;
    }

    public ClientWorkPage SelectOffice(string office)
    {
        var isFound = false;

        for (var i = 0; i < MaxScrollCount; i++)
        {
            var item = _actions.GetElements(ActiveOffices).Find(w => _actions.GetText(w).Equals(office, StringComparison.Ordinal));
            if (item != null)
            {
                isFound = true;
                _actions.Click(item);
                break;
            }

            Wdm.GetDriver()
                .ExecuteJavaScript(
                "arguments[0].setAttribute('class', arguments[1])",
                _actions.GetElement(NavigationItems),
                "owl-nav");
            _actions.Click(NextItem);
        }

        if (!isFound)
        {
            throw new Exception($"No item found office='{office}' after '{MaxScrollCount}' scrolls");
        }

        return this;
    }

    public string GetActiveOfficeCountryName()
    {
        return _actions.GetElement(ActiveCountryParent).GetAttribute("data-country").ToUpper(CultureInfo.InvariantCulture);
    }

    public List<IWebElement> GetActiveOfficeNames()
    {
        var parent = _actions.GetElement(ActiveCountryParent);
        return _actions.GetElements(parent, ActiveCountryOfficesName);
    }
}
