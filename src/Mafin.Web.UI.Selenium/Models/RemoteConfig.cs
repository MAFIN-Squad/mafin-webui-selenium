using System.Text.Json.Serialization;

namespace Mafin.Web.UI.Selenium.Models;

public class RemoteConfig
{
    [JsonPropertyName("browserVersion")]
    public string BrowserVersion { get; set; }

    [JsonPropertyName("url")]
    public Uri Url { get; set; }
}
