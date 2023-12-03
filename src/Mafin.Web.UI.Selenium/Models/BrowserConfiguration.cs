using Mafin.Web.UI.Selenium.Configuration;

namespace Mafin.Web.UI.Selenium.Models;

[ConfigurationSection("selenium")]
public class BrowserConfiguration
{
    public RemoteConfig? Remote { get; set; }

    public IDictionary<string, object>? Capabilities { get; set; }

    public IList<string>? Arguments { get; set; }

    public IList<string>? Extensions { get; set; }

    public IDictionary<string, object>? Preferences { get; set; }
}
