using System.Text.Json.Serialization;

namespace Mafin.Web.UI.Selenium.Models;

public class RemoteConfig
{
    public string BrowserVersion { get; set; }

    public Uri Url { get; set; }
}
