namespace Mafin.Web.UI.Selenium.YapomlExample.Pages.Components;

partial class CheckboxComponent<TComponent, TConditions>
{
    public void Check()
    {
        Label.Click();
    }

    public bool IsChecked => WrappedElement.Selected;
}
