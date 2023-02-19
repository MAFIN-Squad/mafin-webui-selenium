namespace Mafin.Web.UI.Selenium.YapomlExample.Pages.Components;

partial class SingleSelectionComboboxComponent
{
    public string SelectedOption => Selected.Text.Trim();

    public void Expand()
    {
        Arrow.Click();
    }

    public SingleSelectionComboboxComponent Select(string optionName)
    {
        Expand();

        Flyout.Options.First(o => o.Text.TrimEnd() == optionName).Click();

        return this;
    }
}
