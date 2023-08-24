using System.Globalization;
using System.Reflection;
using System.Text.Json;
using Mafin.Web.UI.Selenium.Models;

namespace Mafin.Web.UI.Selenium.Configuration;

/// <summary>
/// Represents <see cref='WebConfiguration'/> provider.
/// </summary>
public static class WebConfigurationProvider
{
    private const string ConfigurationFileName = "Mafin.Configuration.json";
    private const string BrowserConfigurationFileNamePattern = "Mafin.Configuration.{0}.json";

    /// <summary>
    /// Gets <see cref='WebConfiguration'/> from Mafin.Configuration.json.
    /// </summary>
    /// <returns> <see cref='WebConfiguration'/>. </returns>
    public static WebConfiguration GetWebConfiguration()
    {
        if (!File.Exists(ConfigurationFileName))
        {
            throw new FileNotFoundException($"Unable to load configuration for Mafin.Web.UI.Selenium module because {ConfigurationFileName} file does not exist.");
        }

        var result = File.ReadAllText(ConfigurationFileName).ParseObject<WebConfiguration>();

        if (result.BrowserConfiguration is null)
        {
            var browser = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(result.Browser);
            var browserConfigurationFileName = string.Format(CultureInfo.CurrentCulture, BrowserConfigurationFileNamePattern, browser);

            if (!File.Exists(ConfigurationFileName))
            {
                throw new FileNotFoundException($"Unable to load configuration for Mafin.Web.UI.Selenium module because {browserConfigurationFileName} file does not exist.");
            }

            result.BrowserConfiguration = File.ReadAllText(browserConfigurationFileName).ParseObject<BrowserConfiguration>();
        }

        return result;
    }

    private static T ParseObject<T>(this string unparsedJson)
    {
        var pathParts = typeof(T).GetCustomAttributes<ConfigurationSectionAttribute>().FirstOrDefault()?.Path;
        var section = pathParts is null || !pathParts.Any() ? unparsedJson : GetSection(unparsedJson, pathParts);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var result = JsonSerializer.Deserialize<T>(section, options);

        return result;
    }

    private static string GetSection(string element, string[] pathParts)
    {
        var result = element;

        foreach (var part in pathParts)
        {
            result = GetSection(result, part);
        }

        return result;
    }

    private static string GetSection(string element, string key)
    {
        var node = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(element);
        return node[key].GetRawText();
    }
}
