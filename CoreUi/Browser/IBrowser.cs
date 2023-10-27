using CoreUi.Elements;
using CoreUi.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace CoreUi.Browser {
    public interface IBrowser : IDisposable {
        IWebDriver WebDriver { get; }

        public Actions BrowserActions { get; }

        public TPage GoToPage<TPage>() where TPage : class, IPage, new();

        public bool IsElementPresent(By by);

        public void WaitForElementToBeClickable(By by);

        public TElement FindElement<TElement>(By by) where TElement : class, IElement, new();

        public TElement FindElement<TElement>(By by, ISearchContext searchContext)
            where TElement : class, IElement, new();

        public IEnumerable<TElement> FindElements<TElement>(By by) where TElement : class, IElement, new();

        public IEnumerable<TElement> FindElements<TElement>(By by, ISearchContext searchContext)
            where TElement : class, IElement, new();

        public TPage WaitForSubmit<TPage>() where TPage : class, IPage, new();

        public string ExecuteScript(string script, params object[] args);

        public void WaitForPageToLoad();

        public void Dispose();
    }
}
