using System.Drawing;
using OpenQA.Selenium;
using Yapoml.Selenium.Services.Factory;

namespace Mafin.Web.UI.Selenium.YapomlExample.Pages;

partial class BasePage
{
    public void AcceptCookies()
    {
        var pane = CookiesPane.When(it => it.IsDisplayed().IsAnimated());
        pane.AcceptAll.When(it => it.IsDisplayed()).Click();
        pane.When(it => it.IsNotDisplayed());
    }

    public void Navigate(string menuName)
    {
        Header.Navigation.Items[i => i.Title == menuName].Click();
    }

    public void Navigate(string menuName, string subMenuName)
    {
        Header.Navigation
            .Items[i => i.Title == menuName].Hover()
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
                    TimeSpan.FromMilliseconds(50));

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
                SearchButton.Click()
                    .SearchInput.Type(text, when => when.IsDisplayed().IsEnabled());

                if (usingKeyboard)
                {
                    SearchInput.Type(Keys.Enter);
                }
                else
                {
                    FindButton.Click(when => when.IsEnabled());
                }

                return SpaceOptions.Services.Get<ISpaceFactory>().Create<PagesSpace>(WebDriver, SpaceOptions).SearchPage;
            }
        }
    }
}
