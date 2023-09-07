namespace Mafin.Web.UI.Selenium.YapomlExample.Pages.Components;

public partial class SingleSelectionComboboxComponent<TComponent, TConditions>
{
    public string SelectedOption => Selected.Text.Trim();

    public void Expand()
    {
        Arrow.Click();
    }

    public TComponent Select(string optionName)
    {
        Expand();

        Flyout.Options[o => o.Text.TrimEnd() == optionName].Click();

        return component;
    }
}
