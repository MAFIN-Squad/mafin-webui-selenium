using System.Text.Json.Serialization;
using Mafin.Web.UI.Selenium.Configuration;

namespace Mafin.Web.UI.Selenium.Models;

[ConfigurationSection("mafin", "selenium")]
public class BrowserConfiguration
{
    [JsonPropertyName("remote")]
    public RemoteConfig RemoteConfig { get; set; }

    [JsonPropertyName("capabilities")]
    public Dictionary<string, object> Capabilities { get; set; }

    [JsonPropertyName("arguments")]
    public List<string> Arguments { get; set; }

    [JsonPropertyName("extensions")]
    public List<string> Extensions { get; set; }

    [JsonPropertyName("preferences")]
    public Dictionary<string, object> Preferences { get; set; }
}
