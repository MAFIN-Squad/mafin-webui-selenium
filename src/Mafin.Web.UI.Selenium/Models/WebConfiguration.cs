using Mafin.Web.UI.Selenium.Meta;

namespace Mafin.Web.UI.Selenium.Models;

public class WebConfiguration
{
    public string DriverType { get; set; }

    public RunType RunType { get; set; }

    public bool IsLatestLocal { get; set; }

    public RemoteConfig RemoteConfig { get; set; }

    public TimeoutsConfig TimeoutsConfig { get; set; }
}
