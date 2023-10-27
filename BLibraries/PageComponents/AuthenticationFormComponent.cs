using CoreUi.Elements;
using OpenQA.Selenium;

namespace BLibraries.PageComponents {
    public class AuthenticationFormComponent : Element
    {
        public IElement UsernameInput => FindElement<Element>(By.Id("username"));

        public IElement PasswordInput => FindElement<Element>(By.Id("password"));

        public IElement LoginButton => FindElement<Element>(By.XPath(".//button[@type='submit']"));

        public void PerformAuthentication(string username, string password)
        {
            UsernameInput.Type(username);
            PasswordInput.Type(password);
            LoginButton.Click();
        }
    }
}
