namespace Mafin.Web.UI.Selenium.YapomlExample.Pages.Components;

partial class SingleSelectionWithGroupsComboboxComponent
{
    public void Expand()
    {
        Arrow.Click();
    }

    public SingleSelectionWithGroupsComboboxComponent Select(string groupName, string optionName)
    {
        Expand();

        Flyout.Groups.
            First(g => g.Name.Text == groupName).Click()
            .Options.First(o => o.Text.TrimEnd() == optionName)
            .Click();

        return this;
    }
}
