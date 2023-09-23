namespace Mafin.Web.UI.Selenium.Meta;

/// <summary>
/// Represents type of screenshot by the visible area.
/// </summary>
public enum ScreenshotType
{
    /// <summary>
    /// Screenshot of screen's visible area.
    /// </summary>
    ViewPort,

    /// <summary>
    /// Screenshot of full screen page.
    /// </summary>
    FullScreen,

    /// <summary>
    /// Screenshot of screen's single element.
    /// </summary>
    SingleElement
}
