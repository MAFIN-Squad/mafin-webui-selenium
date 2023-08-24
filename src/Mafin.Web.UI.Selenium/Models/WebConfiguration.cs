using System.Text.Json.Serialization;
using Mafin.Web.UI.Selenium.Configuration;
using Mafin.Web.UI.Selenium.Meta;

namespace Mafin.Web.UI.Selenium.Models;

[ConfigurationSection("selenium")]
public class WebConfiguration
{
    public string Browser { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public RunType RunType { get; set; }

    public bool IsLatestLocal { get; set; }

    public TimeoutsConfig Timeouts { get; set; }

    public BrowserConfiguration BrowserConfiguration { get; set; }
}
