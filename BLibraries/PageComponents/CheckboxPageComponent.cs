using CoreUi.Elements;

namespace BLibraries.PageComponents
{
    public class CheckboxPageComponent : Element
    {
        public string IsChecked => this.GetAttribute("checked");

        public void Check()
        {
            if (IsChecked == "false")
            {
                this.Click();
            }
        }

        public void Uncheck()
        {
            if (IsChecked == "true")
            {
                this.Click();
            }
        }
    }
}
