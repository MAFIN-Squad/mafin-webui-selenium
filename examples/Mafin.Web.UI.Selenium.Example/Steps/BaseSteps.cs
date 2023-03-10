using Mafin.Web.UI.Selenium.Example.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace Mafin.Web.UI.Selenium.Example.Steps;

public class BaseSteps
{
    protected WdmExtended Wdm { get; }

    public BaseSteps(WdmExtended wdm)
    {
        Wdm = wdm;
    }

    public void OpenPage(BaseWebPage webPage)
    {
        webPage.OpenPage();
    }

    public IWebElement MoveToElement(By by)
    {
        var element = GetElement(by);
        return MoveToElement(element);
    }

    public IWebElement MoveToElement(IWebElement element)
    {
        Wdm.GetActions().MoveToElement(element).Perform();
        return element;
    }

    public IWebElement ScrollToElement(By by)
    {
        return ScrollToElement(GetElement(by));
    }

    public IWebElement ScrollToElement(IWebElement element)
    {
        Wdm.GetActions().ScrollToElement(element).Perform();
        return element;
    }

    public IWebElement ScrollIntoView(By by)
    {
        return ScrollIntoView(GetElement(by));
    }

    public IWebElement ScrollIntoView(IWebElement element)
    {
        Wdm.GetDriver().ExecuteJavaScript("arguments[0].scrollIntoView(true);", element);
        return element;
    }

    public bool IsElementPresent(By elementLocator)
    {
        try
        {
            Wdm.GetDriver().FindElement(elementLocator);
            return true;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }

    public IWebElement GetElement(By by)
    {
        return Wdm.GetElement(by);
    }

    public IWebElement GetElement(IWebElement element, By by)
    {
        return Wdm.GetElement(element, by);
    }

    public List<IWebElement> GetElements(By by)
    {
        return Wdm.GetElements(by);
    }

    public List<IWebElement> GetElements(IWebElement element, By by)
    {
        return Wdm.GetElements(element, by);
    }
}
