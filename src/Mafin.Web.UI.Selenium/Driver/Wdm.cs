using System.Diagnostics.CodeAnalysis;
using Mafin.Web.UI.Selenium.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Mafin.Web.UI.Selenium.Driver;

public class Wdm
{
    private readonly IWebDriver _driver;
    private readonly TimeoutsConfig _timeoutsConfig;
    private Type[] _ignoredExceptions =
    [
        typeof(NoSuchElementException),
        typeof(StaleElementReferenceException),
        typeof(ElementNotInteractableException),
        typeof(AggregateException)
    ];

    public Wdm(IWebDriver driver, TimeoutsConfig timeoutsConfig)
    {
        _driver = driver;
        _timeoutsConfig = timeoutsConfig;
    }

    public IWebDriver GetDriver()
    {
        return _driver;
    }

    public Actions GetActions()
    {
        return new Actions(GetDriver());
    }

    public void SetIgnoredExceptions([NotNull] Type[] ignoredExceptions) => _ignoredExceptions = ignoredExceptions;

    public WebDriverWait GetWebDriverWait()
    {
        WebDriverWait wait = new(new SystemClock(), _driver, _timeoutsConfig.ExplicitWait, _timeoutsConfig.ExplicitWaitPooling);
        wait.IgnoreExceptionTypes(_ignoredExceptions);
        return wait;
    }
}
