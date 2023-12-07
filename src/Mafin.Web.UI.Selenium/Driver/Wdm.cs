using Mafin.Web.UI.Selenium.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Mafin.Web.UI.Selenium.Driver;

public class Wdm(IWebDriver driver, TimeoutsConfig timeoutsConfig)
{
    private Type[] _ignoredExceptions =
    [
        typeof(NoSuchElementException),
        typeof(StaleElementReferenceException),
        typeof(ElementNotInteractableException),
        typeof(AggregateException)
    ];

    public IWebDriver GetDriver() => driver;

    public Actions GetActions() => new(driver);

    public void SetIgnoredExceptions(Type[] ignoredExceptions) => _ignoredExceptions = ignoredExceptions;

    public WebDriverWait GetWebDriverWait()
    {
        WebDriverWait wait = new(new SystemClock(), driver, timeoutsConfig.ExplicitWait, timeoutsConfig.ExplicitWaitPooling);
        wait.IgnoreExceptionTypes(_ignoredExceptions);
        return wait;
    }
}
