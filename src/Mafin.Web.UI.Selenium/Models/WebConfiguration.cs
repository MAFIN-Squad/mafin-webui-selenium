using Mafin.Web.UI.Selenium.Meta;
using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.Models;

public class WebConfiguration
{
    public string DriverType { get; set; }

    public RunType RunType { get; set; }

    public bool IsLatestLocal { get; set; }

    public RemoteConfig RemoteConfig { get; set; }

    public TimeoutsConfig TimeoutsConfig { get; set; }

    public Dictionary<string, object> Capabilities { get; set; }

    public List<string> Arguments { get; set; }

    public List<string> Extensions { get; set; }

    public Dictionary<string, object> Preferences { get; set; }
}
