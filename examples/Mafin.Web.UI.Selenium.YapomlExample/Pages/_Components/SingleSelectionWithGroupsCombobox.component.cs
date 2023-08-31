namespace Mafin.Web.UI.Selenium.YapomlExample.Pages.Components;

public partial class SingleSelectionWithGroupsComboboxComponent<TComponent, TConditions>
{
    public void Expand()
    {
        Arrow.Click();
    }

    public TComponent Select(string groupName, string optionName)
    {
        Expand();

        Flyout
            .Groups[g => g.Name == groupName].Click()
            .Options[o => o.Text.TrimEnd() == optionName]
            .Click();

        return component;
    }
}
