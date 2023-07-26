using System.Text.Json.Serialization;
using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.Models;

public class TimeoutsConfig : ITimeouts
{
    [JsonPropertyName("implicit")]
    public TimeSpan ImplicitWait { get; set; }

    [JsonPropertyName("pageLoad")]
    public TimeSpan PageLoad { get; set; }

    [JsonPropertyName("asynchronousJavaScript")]
    public TimeSpan AsynchronousJavaScript { get; set; }

    [JsonPropertyName("commandTimeout")]
    public TimeSpan CommandTimeout { get; set; }

    [JsonPropertyName("explicit")]
    public TimeSpan ExplicitWaitTimeout { get; set; }

    [JsonPropertyName("explicitWaitPooling")]
    public TimeSpan ExplicitWaitPoolingTimeout { get; set; }
}
