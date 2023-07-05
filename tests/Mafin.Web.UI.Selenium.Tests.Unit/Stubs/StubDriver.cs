using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Stubs
{
    public class StubDriver : IWebDriver, IActionExecutor
    {
        public string Url { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Title => throw new NotImplementedException();

        public string PageSource => throw new NotImplementedException();

        public string CurrentWindowHandle => throw new NotImplementedException();

        public ReadOnlyCollection<string> WindowHandles => throw new NotImplementedException();

        public bool IsActionExecutor => throw new NotImplementedException();

        public void Close() => throw new NotImplementedException();
        public void Dispose() => throw new NotImplementedException();
        public IWebElement FindElement(By by) => throw new NotImplementedException();
        public ReadOnlyCollection<IWebElement> FindElements(By by) => throw new NotImplementedException();
        public IOptions Manage() => throw new NotImplementedException();
        public INavigation Navigate() => throw new NotImplementedException();
        public void PerformActions(IList<ActionSequence> actionSequenceList) => throw new NotImplementedException();
        public void Quit() => throw new NotImplementedException();
        public void ResetInputState() => throw new NotImplementedException();
        public ITargetLocator SwitchTo() => throw new NotImplementedException();
    }
}
