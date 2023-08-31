namespace Mafin.Web.UI.Selenium.YapomlExample.Pages.Components;

public partial class CheckboxComponent<TComponent, TConditions>
{
    public bool IsChecked => WrappedElement.Selected;

    public void Check()
    {
        Label.Click();
    }
}
