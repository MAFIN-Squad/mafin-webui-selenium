using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mafin.Web.UI.Selenium.MsTest;

public class UiTest : AbstractUiTest
{
    [TestInitialize]
    public override void SetUpUiTest()
    {
        base.SetUpUiTest();
    }

    [TestCleanup]
    public override void CleanupUiTest()
    {
        base.CleanupUiTest();
    }
}