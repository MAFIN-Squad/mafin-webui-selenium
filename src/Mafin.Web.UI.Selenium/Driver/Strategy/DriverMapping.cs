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

    public static AbstractDriverStrategy GetDriverStrategy(WebConfiguration webConfiguration)
    {
        var driverType = webConfiguration.Browser;

        if (string.IsNullOrWhiteSpace(driverType))
        {
            throw new ArgumentNullException($"Driver type is not defined, set {nameof(webConfiguration.Browser)} in {nameof(webConfiguration)}. Available values are: [{string.Join("], [", DriverMappings.Keys)}]");
        }

        if (!DriverMappings.TryGetValue(driverType, out var value))
        {
            throw new KeyNotFoundException($"There is no registered DriverStrategy for the {nameof(driverType)} = {driverType}. Available values are: [{string.Join("], [", DriverMappings.Keys)}]");
        }

        return Activator.CreateInstance(value, webConfiguration) as AbstractDriverStrategy;
    }
}
