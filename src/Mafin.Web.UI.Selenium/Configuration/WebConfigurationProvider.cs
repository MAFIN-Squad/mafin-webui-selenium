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
    private const string CommonConfigFileName = "Mafin.Configuration.json";
    private const string BrowserConfigFileNamePattern = "Mafin.Configuration.{0}.json";

    /// <summary>
    /// Gets <see cref='WebConfiguration'/> values from Mafin.Configuration.json.
    /// </summary>
    /// <returns><see cref='WebConfiguration'/> instance.</returns>
    /// <exception cref="FileNotFoundException">Configuration file is not created.</exception>
    public static WebConfiguration GetWebConfiguration()
    {
        VerifyFileExists(CommonConfigFileName);
        var result = File.ReadAllText(CommonConfigFileName).ParseObject<WebConfiguration>();

        if (result.BrowserConfiguration is null)
        {
            var browser = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(result.Browser);
            var browserConfigurationFileName = string.Format(CultureInfo.CurrentCulture, BrowserConfigFileNamePattern, browser);

            VerifyFileExists(browserConfigurationFileName);
            result.BrowserConfiguration = File.ReadAllText(browserConfigurationFileName).ParseObject<BrowserConfiguration>();
        }

        return result;
    }

    private static void VerifyFileExists(string fileName)
    {
        if (!File.Exists(fileName))
        {
            throw new FileNotFoundException($"Unable to load configuration for Mafin.Web.UI.Selenium module because {fileName} file does not exist.");
        }
    }

    private static T ParseObject<T>(this string unparsedJson)
    {
        var pathParts = typeof(T).GetCustomAttributes<ConfigurationSectionAttribute>().FirstOrDefault()?.Path;
        var section = pathParts is null || !pathParts.Any()
            ? unparsedJson
            : GetSection(unparsedJson, pathParts);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<T>(section, options);
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

    private static string? GetSection(string element, string key)
    {
        var node = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(element);
        return node?[key].GetRawText();
    }
}
