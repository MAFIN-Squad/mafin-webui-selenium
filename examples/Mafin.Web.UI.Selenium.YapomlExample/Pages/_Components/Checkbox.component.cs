namespace Mafin.Web.UI.Selenium.YapomlExample.Pages.Components;

public partial class CheckboxComponent<TComponent, TConditions, TCondition>
{
    public bool IsChecked => WrappedElement.Selected;

    public void Check()
    {
        Label.Click();
    }
}
