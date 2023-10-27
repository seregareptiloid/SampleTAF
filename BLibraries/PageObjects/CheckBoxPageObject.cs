using BLibraries.PageComponents;
using CoreUi.Elements;
using CoreUi.Pages;
using OpenQA.Selenium;

namespace BLibraries.PageObjects {
    public class CheckBoxPageObject : Page
    {
        public override string Uri => "checkboxes";

        public CheckboxPageComponent CheckBoxOne => Browser.FindElement<CheckboxPageComponent>(By.XPath("//input[1]"));

        public CheckboxPageComponent CheckBoxTwo => Browser.FindElement<CheckboxPageComponent>(By.XPath("//input[2]"));

        public void ClickCheckboxes()
        {
            CheckBoxOne.Check();
            CheckBoxTwo.Uncheck();
        }
    }
}
