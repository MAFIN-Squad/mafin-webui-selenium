namespace Mafin.Web.UI.Selenium.Configuration;

/// <summary>
/// Represents an attribute containing the path to a section with configuration.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
internal sealed class ConfigurationSectionAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigurationSectionAttribute"/> class.
    /// </summary>
    /// <param name="path">Path to the section with the configuration.</param>
    public ConfigurationSectionAttribute(params string[] path)
    {
        Path = path;
    }

    /// <summary>
    /// Gets path to the section with the configuration.
    /// </summary>
    public string[] Path { get; }
}
