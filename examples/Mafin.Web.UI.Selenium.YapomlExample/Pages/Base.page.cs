using System.Drawing;
using OpenQA.Selenium;
using Yapoml.Selenium.Services.Factory;
using Yapoml.Selenium.Services.Locator;

namespace Mafin.Web.UI.Selenium.YapomlExample.Pages;

partial class BasePage
{
    public void AcceptCookies()
    {
        var pane = CookiesPane.When(it => it.IsDisplayed().IsAnimated());
        pane.AcceptAll.When(it => it.IsDisplayed()).Click();
        pane.When(it => it.IsDisappeared());
    }

    public void Navigate(string menuName)
    {
        Header.Navigation.Items
           .First(i => i.Title.Text == menuName)
           .Click();
    }

    public void Navigate(string menuName, string subMenuName)
    {
        Header.Navigation.Items.First(i => i.Title.Text == menuName)
           .Hover()
           .Flyout.SubItem(subMenuName)
           .Click();
    }

    partial class CookiesPaneComponent
    {
        partial class Conditions
        {
            public Conditions IsAnimated()
            {
                Point previousLocation = default;
                Size previousSize = default;

                Yapoml.Selenium.Services.Waiter.Until<bool?>(
                    () =>
                    {
                        var location = this.ElementHandler.Locate().Location;
                        var size = this.ElementHandler.Locate().Size;

                        if (location != previousLocation && size != previousSize)
                        {
                            previousLocation = location;
                            previousSize = size;
                            return null;
                        }
                        else
                        {
                            return true;
                        }
                    },
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromMilliseconds(0));

                return this;
            }

            public Conditions IsDisappeared()
            {
                Yapoml.Selenium.Services.Waiter.Until<bool?>(
                    () =>
                    {
                        try
                        {
                            if (ElementHandler.Locate().Displayed)
                            {
                                return null;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        catch (StaleElementReferenceException)
                        {
                            return true;
                        }
                    },
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromMilliseconds(0));

                return this;
            }
        }
    }

    partial class HeaderComponent
    {
        partial class SearchComponent
        {
            public SearchPage Search(string text, bool usingKeyboard = false)
            {
                SearchButton.Click();

                SearchInput.SendKeys(text);

                if (usingKeyboard)
                {
                    SearchInput.SendKeys(Keys.Enter);
                }
                else
                {
                    FindButton.Click();
                }

                return SpaceOptions.Services.Get<IPageFactory>().Create<SearchPage>(WebDriver, new ElementHandlerRepository(), SpaceOptions);
            }
        }
    }
}
