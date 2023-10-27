using CoreUi.Elements;
using CoreUi.Pages;
using OpenQA.Selenium;

namespace BLibraries.PageObjects {
    public class HerokuAppPage : Page
    {
        public IElement FormAuthenticationLink =>
            Browser.FindElement<Element>(By.XPath("//a[text()='Form Authentication']"));
        
        public IElement AddOrRemoveElementLink => Browser.FindElement<Element>(By.XPath("//a[text()='Add/Remove Elements']"));

        public IElement CheckBoxesLink => Browser.FindElement<Element>(By.XPath("//a[text()='Checkboxes']"));


        public override string Uri => string.Empty;
    }
}
