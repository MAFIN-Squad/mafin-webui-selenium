namespace Mafin.Web.UI.Selenium.YapomlExample.Pages.Components;

partial class SingleSelectionWithGroupsComboboxComponent<TComponent, TConditions>
{
    public void Expand()
    {
        Arrow.Click();
    }

    public TComponent Select(string groupName, string optionName)
    {
        Expand();

        Flyout.Groups.
            First(g => g.Name.Text == groupName).Click()
            .Options.First(o => o.Text.TrimEnd() == optionName)
            .Click();

        return component;
    }
}
