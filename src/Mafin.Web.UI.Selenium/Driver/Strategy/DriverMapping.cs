using System.Diagnostics.CodeAnalysis;
using Mafin.Web.UI.Selenium.Models;

namespace Mafin.Web.UI.Selenium.Driver.Strategy;

public static class DriverMapping
{
    private static readonly Dictionary<string, Type> DriverMappings = new(StringComparer.OrdinalIgnoreCase)
    {
        { "chrome", typeof(ChromeStrategy) },
        { "edge", typeof(EdgeStrategy) },
        { "safari", typeof(SafariStrategy) },
        { "firefox", typeof(FirefoxStrategy) }
    };

    public static AbstractDriverStrategy GetDriverStrategy([NotNull] WebConfiguration webConfiguration)
    {
        var driverType = webConfiguration.Browser;

        if (string.IsNullOrEmpty(driverType))
        {
            throw new ArgumentNullException($"Driver type is not defined, set {nameof(webConfiguration.Browser)} in {nameof(webConfiguration)}. Available values are: [{string.Join("], [", DriverMappings.Keys)}]");
        }

        if (!DriverMappings.ContainsKey(driverType))
        {
            throw new KeyNotFoundException($"There is no registered DriverStrategy for the {nameof(driverType)} = {driverType}. Available values are: [{string.Join("], [", DriverMappings.Keys)}]");
        }

        return (AbstractDriverStrategy)Activator.CreateInstance(DriverMappings[driverType], webConfiguration)!;
    }
}
