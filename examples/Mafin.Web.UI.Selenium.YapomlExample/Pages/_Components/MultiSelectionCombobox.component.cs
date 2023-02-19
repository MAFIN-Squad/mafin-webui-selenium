namespace Mafin.Web.UI.Selenium.YapomlExample.Pages.Components;

partial class MultiSelectionComboboxComponent
{
    public void Expand()
    {
        SelectionPane.When(it => it.IsDisplayed()).Click();
    }

    public void Collapse()
    {
        SelectionPane.Click();
    }

    public void Select(params string[] optionNames)
    {
        Expand();

        Flyout.When(it => it.IsDisplayed());
        Thread.Sleep(1000);

        foreach (var optionName in optionNames)
        {
            Flyout.Option(optionName).Click();
        }

        Collapse();
    }
}
