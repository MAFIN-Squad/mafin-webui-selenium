using Mafin.Web.UI.Selenium.Example.Core;
using Mafin.Web.UI.Selenium.Example.Meta;
using Mafin.Web.UI.Selenium.Example.Steps;
using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.Example.Pages;

public class NavigationBar : BaseEpamPage
{
    private const string BaseNavigationCssLocator = "a.top-navigation__item-link";
    private readonly Dictionary<NavigationMenu, By> _navigationMenuMapping;

    public NavigationBar(WdmExtended wdm, ActionsSteps actions)
        : base(wdm, actions)
    {
        _navigationMenuMapping = new Dictionary<NavigationMenu, By>
        {
            { NavigationMenu.Services, Services },
            { NavigationMenu.Insights, Insights },
            { NavigationMenu.About, About },
            { NavigationMenu.Careers, Careers },
            { NavigationMenu.ContactUs, ContactUs }
        };
    }

    public By Services => By.CssSelector($"{BaseNavigationCssLocator}[href='/services']");

    public By Insights => By.CssSelector($"{BaseNavigationCssLocator}[href='/insights']");

    public By About => By.CssSelector($"{BaseNavigationCssLocator}[href='/about']");

    public By Careers => By.CssSelector($"{BaseNavigationCssLocator}[href='/careers']");

    public By ContactUs => By.CssSelector("div.header__content > a[data-gtm-category='header-contact-cta']");

    public By SearchIcon => By.CssSelector("button[class*='header-search__button header__icon']");

    public By SearchInput => By.CssSelector(".search-results__input-holder input");

    public By FindButton => By.CssSelector(".header-search__submit");

    public NavigationBar NavigateToMenuItemByClick(NavigationMenu menu)
    {
        _actions.MoveToElement(_navigationMenuMapping[menu]).Click();
        return this;
    }
}
