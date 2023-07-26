using System.Text.Json.Serialization;
using Mafin.Web.UI.Selenium.Configuration;
using Mafin.Web.UI.Selenium.Meta;

namespace Mafin.Web.UI.Selenium.Models;

[ConfigurationSection("mafin", "selenium")]
public class WebConfiguration
{
    [JsonPropertyName("browser")]
    public string DriverType { get; set; }

    [JsonPropertyName("runType")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public RunType RunType { get; set; }

    public bool IsLatestLocal { get; set; }

    [JsonPropertyName("timeouts")]
    public TimeoutsConfig TimeoutsConfig { get; set; }

    [JsonPropertyName("browserConfiguration")]
    public BrowserConfiguration BrowserConfiguration { get; set; }
}
