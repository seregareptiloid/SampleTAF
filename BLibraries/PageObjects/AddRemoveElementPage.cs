using CoreUi.Elements;
using CoreUi.Pages;
using OpenQA.Selenium;

namespace BLibraries.PageObjects {
    public class AddRemoveElementPage : Page
    {
        public IElement AddElementButton => Browser.FindElement<Element>(By.XPath("//button[text()= 'Add Element']"));

        public IEnumerable<Element> ElementsButtons =>
            Browser.FindElements<Element>(By.XPath("//button[text()= 'Add Element']"));
        
        public override string Uri => "add_remove_elements";

        public override Uri BaseUri => new("https://the-internet.herokuapp.com/");
    }
}
