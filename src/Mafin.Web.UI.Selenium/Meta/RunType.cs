namespace Mafin.Web.UI.Selenium.Meta;

/// <summary>
/// Represents type of test execution by location of executor.
/// </summary>
public enum RunType
{
    /// <summary>
    /// Local execution.
    /// </summary>
    Local,

    /// <summary>
    /// Execution via a remote executor, e.g. Selenium Grid.
    /// </summary>
    Remote
}
