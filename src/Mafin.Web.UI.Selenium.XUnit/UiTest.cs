namespace Mafin.Web.UI.Selenium.XUnit;

public class UiTest : AbstractUiTest, IDisposable
{

    public UiTest()
    {
        base.SetUpUiTest();
    }

    public virtual void Dispose()
    {
        base.CleanupUiTest();
    }
}