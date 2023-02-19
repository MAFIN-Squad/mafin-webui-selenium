using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.Models;

public class TimeoutsConfig : ITimeouts
{
    public TimeSpan ImplicitWait { get; set; }

    public TimeSpan PageLoad { get; set; }

    public TimeSpan AsynchronousJavaScript { get; set; }

    public TimeSpan CommandTimeout { get; set; }

    public TimeSpan ExplicitWaitTimeout { get; set; }

    public TimeSpan ExplicitWaitPoolingTimeout { get; set; }
}
