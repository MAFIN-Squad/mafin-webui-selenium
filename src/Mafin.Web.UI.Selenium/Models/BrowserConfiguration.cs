using System.Text.Json.Serialization;
using Mafin.Web.UI.Selenium.Configuration;

namespace Mafin.Web.UI.Selenium.Models;

[ConfigurationSection("selenium")]
public class BrowserConfiguration
{
    public RemoteConfig Remote { get; set; }

    public Dictionary<string, object> Capabilities { get; set; }

    public List<string> Arguments { get; set; }

    public List<string> Extensions { get; set; }

    public Dictionary<string, object> Preferences { get; set; }
}
