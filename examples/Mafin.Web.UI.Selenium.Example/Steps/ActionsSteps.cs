using Mafin.Web.UI.Selenium.Example.Core;
using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.Example.Steps;

public class ActionsSteps : BaseSteps
{
    public ActionsSteps(WdmExtended wdm)
        : base(wdm)
    {
    }

    public ActionsSteps SelectValueInTheList(By byParent, By byList, string value)
    {
        Click(byParent);

        return ClickOnFirstInTheListByText(byList, value);
    }

    public IWebElement Click(By by)
    {
        return Click(GetElement(by));
    }

    public IWebElement Click(IWebElement element)
    {
        MoveToElement(element).Click();
        return element;
    }

    public ActionsSteps ClickOnFirstInTheListByText(By byList, string value)
    {
        Wdm.Wait(d => GetElements(byList).Any());
        var list = GetElements(byList);

        var valueToSelect = list.FirstOrDefault(e => e.Text.Contains(value));

        if (valueToSelect == null)
        {
            throw new Exception($"Unable to find element with value '{value}' in the target '{byList}' list");
        }

        ScrollToElement(valueToSelect).Click();

        return this;
    }

    public IWebElement FindElementInTheListByIndex(By byList, int index)
    {
        var list = Wdm.GetElements(byList);

        if (list.Count < index + 1)
        {
            throw new Exception($"Unable to find element by index='{index}' in the target '{byList}' list");
        }

        return ScrollToElement(list[index]);
    }

    public IWebElement TypeText(By by, string text)
    {
        var element = GetElement(by);
        element.Clear();
        element.SendKeys(text);
        return element;
    }

    public string GetText(By by)
    {
        return GetText(GetElement(by));
    }

    public string GetText(IWebElement element)
    {
        var tagName = element.TagName;

        if (tagName == "input")
        {
            return element.GetAttribute("value");
        }

        return element.Text;
    }

    public bool IsChecked(By by)
    {
        var element = GetElement(by);
        return bool.Parse(element.GetDomProperty("checked"));
    }

    public ActionsSteps SendKeys(string keys)
    {
        Wdm.GetActions().SendKeys(keys).Perform();
        return this;
    }
}
