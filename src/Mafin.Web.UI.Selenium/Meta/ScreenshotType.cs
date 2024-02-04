namespace Mafin.Web.UI.Selenium.Meta;

/// <summary>
/// Represents type of screenshot by the area.
/// </summary>
public enum ScreenshotType
{
    /// <summary>
    /// Screenshot of a screen's visible area.
    /// </summary>
    ViewPort,

    /// <summary>
    /// Screenshot of a full page.
    /// </summary>
    FullScreen,

    /// <summary>
    /// Screenshot of a pages' single element.
    /// </summary>
    SingleElement
}
