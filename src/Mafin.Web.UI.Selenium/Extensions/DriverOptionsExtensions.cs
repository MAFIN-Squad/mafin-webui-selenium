using OpenQA.Selenium;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;

namespace Mafin.Web.UI.Selenium.Extensions;

/// <summary>
/// Provides extension methods for <see cref="DriverOptions"/>.
/// </summary>
internal static class DriverOptionsExtensions
{
    /// <summary>
    /// Adds driver arguments to the <paramref name="options"/>.
    /// </summary>
    /// <param name="options"><see cref="DriverOptions"/>.</param>
    /// <param name="arguments">Collection of driver arguments.</param>
    /// <typeparam name="T">Object type inherited from <see cref="DriverOptions"/>.</typeparam>
    public static void AddArguments<T>(this T options, IEnumerable<string> arguments)
        where T : DriverOptions
    {
        switch (options)
        {
            case FirefoxOptions firefoxOptions:
                firefoxOptions.AddArguments(arguments);
                break;
            case ChromiumOptions chromiumOptions:
                chromiumOptions.AddArguments(arguments);
                break;
            case SafariOptions safariOptions:
                safariOptions.AddAdditionalOption("args", arguments.ToList());
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(options), options?.GetType().FullName, "Adding an argument is not supported for this browser");
        }
    }

    /// <summary>
    /// Adds an extension to the <paramref name="options"/> located at <paramref name="path"/>.
    /// </summary>
    /// <param name="options"><see cref="DriverOptions"/>.</param>
    /// <param name="path">Path to the browser extension.</param>
    /// <typeparam name="T">Object type inherited from <see cref="DriverOptions"/>.</typeparam>
    public static void AddExtension<T>(this T options, string path)
        where T : DriverOptions
    {
        switch (options)
        {
            case ChromiumOptions chromiumOptions:
                chromiumOptions.AddExtension(path);
                break;
            case FirefoxOptions firefoxOptions:
                firefoxOptions.Profile ??= new FirefoxProfile();
                firefoxOptions.Profile.AddExtension(path);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(options), options?.GetType().FullName, "Adding an extension is not supported for this browser");
        }
    }

    /// <summary>
    /// Adds a user profile preference to the <paramref name="options"/>.
    /// </summary>
    /// <param name="options"><see cref="DriverOptions"/>.</param>
    /// <param name="preferenceName">Preference name.</param>
    /// <param name="preferenceValue">Preference value.</param>
    /// <typeparam name="T">Object type inherited from <see cref="DriverOptions"/>.</typeparam>
    public static void AddPreference<T>(this T options, string preferenceName, object preferenceValue)
        where T : DriverOptions
    {
        switch (options)
        {
            case ChromiumOptions chromiumOptions:
                chromiumOptions.AddUserProfilePreference(preferenceName, preferenceValue);
                break;
            case FirefoxOptions firefoxOptions:
                firefoxOptions.SetPreference(preferenceName, preferenceValue.ToString());
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(options), options?.GetType().FullName, "Adding a profile preference is not supported for this browser");
        }
    }
}
