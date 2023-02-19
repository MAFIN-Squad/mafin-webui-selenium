using System.Diagnostics.CodeAnalysis;
using Mafin.Web.UI.Selenium.Driver;
using Mafin.Web.UI.Selenium.Models;
using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.Example.Core;

public class WdmExtended : Wdm
{
    private List<Func<bool>> _waitConditions = new();
    private List<Action> _actionsToIntercept = new();

    public WdmExtended(IWebDriver driver, TimeoutsConfig timeoutsConfig)
        : base(driver, timeoutsConfig)
    {
    }

    public void SetCommonConditionsToWait([NotNull] List<Func<bool>> waitConditions) => _waitConditions = waitConditions;

    public void SetActionsToIntercept([NotNull] List<Action> actionsToIntercept) => _actionsToIntercept = actionsToIntercept;

    public IWebElement GetElement(By by, Func<IWebDriver, bool>? before = null, Func<IWebDriver, bool>? after = null, bool isIntercept = true)
    {
        return CustomFindElement(d => d.FindElement(by), before, after, isIntercept);
    }

    public List<IWebElement> GetElements(By by, Func<IWebDriver, bool>? before = null, Func<IWebDriver, bool>? after = null, bool isIntercept = true)
    {
        return CustomFindElement(d => d.FindElements(by).ToList(), before, after, isIntercept);
    }

    public IWebElement GetElement(IWebElement webElement, By by, Func<IWebDriver, bool>? before = null, Func<IWebDriver, bool>? after = null, bool isIntercept = true)
    {
        return CustomFindElement(d => webElement.FindElement(by), before, after, isIntercept);
    }

    public List<IWebElement> GetElements(IWebElement webElement, By by, Func<IWebDriver, bool>? before = null, Func<IWebDriver, bool>? after = null, bool isIntercept = true)
    {
        return CustomFindElement(d => webElement.FindElements(by).ToList(), before, after, isIntercept);
    }

    public TResult Wait<TResult>(Func<IWebDriver, TResult> condition, bool shouldIntercept = true)
    {
        var wait = GetWebDriverWait();

        wait.Until(d => ApplyWait());
        var result = wait.Until(condition);
        wait.Until(d => ApplyWait());

        if (shouldIntercept)
        {
            Intercept();
        }

        return result;
    }

    private TResult CustomFindElement<TResult>(Func<IWebDriver, TResult> condition, Func<IWebDriver, bool>? before = null, Func<IWebDriver, bool>? after = null, bool isIntercept = true)
    {
        if (before != null)
        {
            Wait(before, isIntercept);
        }

        var result = Wait(condition, isIntercept);

        if (after != null)
        {
            Wait(after, isIntercept);
        }

        return result;
    }

    private bool ApplyWait()
    {
        return _waitConditions.All(c => c.Invoke());
    }

    private void Intercept() => _actionsToIntercept.ForEach(c => c.Invoke());
}
