using Mafin.Web.UI.Selenium.Example.Core;
using Mafin.Web.UI.Selenium.Example.Meta;
using Mafin.Web.UI.Selenium.Example.Steps;
using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.Example.Pages;

public class NavigationBar : BaseEpamPage
{
    private const string BaseNavigationCssLocator = "ul[class='top-navigation__row']";
    private readonly Dictionary<NavigationMenu, By> _navigationMenuMapping;

    public NavigationBar(WdmExtended wdm, ActionsSteps actions)
        : base(wdm, actions)
    {
        _navigationMenuMapping = new Dictionary<NavigationMenu, By>
        {
            { NavigationMenu.Services, Services },
            { NavigationMenu.HowWeDoIt, HowWeDoIt },
            { NavigationMenu.OurWork, OurWork },
            { NavigationMenu.Insights, Insights },
            { NavigationMenu.About, About },
            { NavigationMenu.Careers, Careers },
            { NavigationMenu.ContactUs, ContactUs }
        };
    }

    public By Services => By.CssSelector($"{BaseNavigationCssLocator} a[href='/services']");

    public By HowWeDoIt => By.CssSelector($"{BaseNavigationCssLocator} a[href='/how-we-do-it']");

    public By OurWork => By.CssSelector($"{BaseNavigationCssLocator} a[href='/our-work']");

    public By Insights => By.CssSelector($"{BaseNavigationCssLocator} a[href='/insights']");

    public By About => By.CssSelector($"{BaseNavigationCssLocator} a[href='/about']");

    public By Careers => By.CssSelector($"{BaseNavigationCssLocator} a[href='/careers']");

    public By ContactUs => By.CssSelector("a[data-gtm-category='header-contact-cta']");

    public By SearchIcon => By.CssSelector("button[class*='header-search__button header__icon']");

    public By SearchInput => By.Id("new_form_search");

    public By FindButton => By.CssSelector(".header-search__submit");

    public NavigationBar NavigateToMenuItemByClick(NavigationMenu menu)
    {
        _actions.MoveToElement(_navigationMenuMapping[menu]).Click();
        return this;
    }
}
