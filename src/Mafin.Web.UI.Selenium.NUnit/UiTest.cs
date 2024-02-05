using NUnit.Framework;

namespace Mafin.Web.UI.Selenium.NUnit;

public class UiTest : AbstractUiTest
{
    [SetUp]
    public override void SetUpUiTest()
    {
        base.SetUpUiTest();
    }

    [TearDown]
    public override void CleanupUiTest()
    {
        base.CleanupUiTest();
    }
}