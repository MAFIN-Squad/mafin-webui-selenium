namespace Mafin.Web.UI.Selenium.Configuration;

/// <summary>
/// Represents an attribute that contains the path to the section with the configuration.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
internal sealed class ConfigurationSectionAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigurationSectionAttribute"/> class.
    /// </summary>
    /// <param name="path"> path to the section with the configuration. </param>
    public ConfigurationSectionAttribute(params string[] path)
    {
        Path = path;
    }

    /// <summary>
    /// Gets path to the section with the configuration.
    /// </summary>
    public string[] Path { get; }
}
