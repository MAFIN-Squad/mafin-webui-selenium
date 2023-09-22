namespace Mafin.Web.UI.Selenium.Meta;

/// <summary>
/// Presents visible area types for as a type of a screenshot to take.
/// </summary>
public enum VisibleArea
{
    /// <summary>
    /// Type of a screenshot for a view port.
    /// </summary>
    DefaultScreen,

    /// <summary>
    /// Type of a screenshot for a full page (from the top to the bottom).
    /// </summary>
    FullScreen,

    /// <summary>
    /// Type of a screenshot for a single element.
    /// </summary>
    SingleElement
}
