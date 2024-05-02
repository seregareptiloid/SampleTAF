using BLibraries.PageComponents;
using CoreUi.Elements;
using CoreUi.Pages;
using OpenQA.Selenium;

namespace BLibraries.PageObjects {
    public class FormAuthenticationPage : Page
    {
        public AuthenticationFormComponent AuthenticationFormComponent =>
            Browser.FindElement<AuthenticationFormComponent>(By.Id("login"));

        public IElement SuccessMessage => Browser.FindElement<Element>(By.CssSelector("[class='flash success']"));

        public IElement ErrorMessage => Browser.FindElement<Element>(By.CssSelector("[class='flash error']"));

        public IElement LogoutButton => Browser.FindElement<Element>(By.XPath("//i[contains(text(), 'Logout')]"));

        public override string Uri => "login";

        public override Uri BaseUri => new("https://the-internet.herokuapp.com/");
    }
}
