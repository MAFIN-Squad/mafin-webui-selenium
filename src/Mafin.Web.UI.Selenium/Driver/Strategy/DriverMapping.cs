using Mafin.Web.UI.Selenium.Models;

namespace Mafin.Web.UI.Selenium.Driver.Strategy;

public static class DriverMapping
{
    private static readonly Dictionary<string, Type> DriverMappings = new()
    {
        { "chrome", typeof(ChromeStrategy) }
    };

    public static AbstractDriverStrategy GetDriverStrategy(WebConfiguration webConfiguration)
    {
        var driverType = webConfiguration.DriverType;
        if (!DriverMappings.ContainsKey(driverType))
        {
            throw new KeyNotFoundException($"There is no registered DriverStrategy for the driverType = {driverType}. Available values are: {DriverMappings.Keys}");
        }

        return (AbstractDriverStrategy)Activator.CreateInstance(DriverMappings[driverType], webConfiguration);
    }

}
