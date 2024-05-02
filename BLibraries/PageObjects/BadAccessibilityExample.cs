using CoreUi.Elements;
using CoreUi.Pages;
using CoreUi.Utils;
using OpenQA.Selenium;

namespace BLibraries.PageObjects {
    public class BadAccessibilityExample : Page {
        public IElement EmailInput => Browser.FindElement<Element>(By.Id("formBasicText"));

        public override string Uri => "sandbox-automation-testing";

        public override Uri BaseUri => new("https://thefreerangetester.github.io");

        public void WaitForPageToLoad()
        {
            Browser.WaitForPageToLoad();
        }
    }
}
