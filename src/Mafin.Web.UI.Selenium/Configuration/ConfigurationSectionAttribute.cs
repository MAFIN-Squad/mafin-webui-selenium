namespace Mafin.Web.UI.Selenium.Configuration;

/// <summary>
/// Represents an attribute containing the path to a section with configuration.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ConfigurationSectionAttribute"/> class.
/// </remarks>
/// <param name="path">Path to the section with the configuration.</param>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
internal sealed class ConfigurationSectionAttribute(params string[] path) : Attribute
{
    /// <summary>
    /// Gets path to the section with the configuration.
    /// </summary>
    public string[] Path { get; } = path;
}
