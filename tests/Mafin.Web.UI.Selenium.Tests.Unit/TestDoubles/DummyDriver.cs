using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Mafin.Web.UI.Selenium.Tests.Unit.TestDoubles;

public sealed class DummyDriver : IWebDriver, IActionExecutor
{
    public string? Url { get; set; }

    public string? Title { get; }

    public string? PageSource { get; }

    public string? CurrentWindowHandle { get; }

    public ReadOnlyCollection<string>? WindowHandles { get; }

    public bool IsActionExecutor { get; }

    public void Close() => throw new NotImplementedException();

    public IWebElement FindElement(By by) => throw new NotImplementedException();

    public ReadOnlyCollection<IWebElement> FindElements(By by) => throw new NotImplementedException();

    public IOptions Manage() => throw new NotImplementedException();

    public INavigation Navigate() => throw new NotImplementedException();

    public void PerformActions(IList<ActionSequence> actionSequenceList) => throw new NotImplementedException();

    public void Quit() => throw new NotImplementedException();

    public void ResetInputState() => throw new NotImplementedException();

    public ITargetLocator SwitchTo() => throw new NotImplementedException();

    public void Dispose()
    {
        // Intentionally empty
    }
}
