using CoreUi.Elements;
using CoreUi.Pages;
using CoreUi.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Serilog;

namespace CoreUi.Browser {
    public class Browser : IBrowser {
        private readonly Uri baseUrl;
        private static readonly ILogger logger = Log.ForContext(typeof(Browser));

        private const int Timeout = 500;
        public IPage CurrentPage { get; private set; }

        public IWebDriver WebDriver { get; }

        public Actions BrowserActions => new(WebDriver);

        public Browser(IWebDriver webDriver, Uri baseUrl) {
            WebDriver = webDriver;
            this.baseUrl = baseUrl;
        }

            public TPage GoToPage<TPage>() where TPage : class, IPage, new()
        {
            return GoTo(typeof(TPage)) as TPage;
        }

        private IPage GoTo(Type type) {
            Page page = GetPage(type);
            string fullUrl = baseUrl + page.Uri;
            WebDriver.Url = fullUrl;
            WebDriver.Navigate().GoToUrl(fullUrl);
            return page;
        }

        private Page GetPage(Type pageType) {
            if (Activator.CreateInstance(pageType) is not Page page) {
                throw new NotFoundException($"Type {pageType.FullName} is not assignable from IPage.");
            }

            page.Initialize(this);
            CurrentPage = page;
            return page;
        }

        public bool IsElementPresent(By by) {
            return FindElements<Element>(by).Any();
        }

        public void WaitForElementToBeClickable(By by) {
            WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(Timeout));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            wait.Until(webDriver => {
                IWebElement? element = webDriver.FindElements(by).FirstOrDefault();
                return element != null ? element.Enabled && element.Displayed : false;
            });
        }

        public TElement FindElement<TElement>(By by, ISearchContext searchContext)
            where TElement : class, IElement, new() {
            WaitForElementToAppear(by);
            IWebElement? webElement = searchContext.FindElement(by);
            return AsComponent<TElement>(webElement, by);
        }

        public IEnumerable<TElement> FindElements<TElement>(By by, ISearchContext searchContext)
            where TElement : class, IElement, new() {
            WaitForElementToAppear(by);
            IEnumerable<IWebElement> webElements = searchContext.FindElements(by);
            return webElements.Select(el => AsComponent<TElement>(el, by));
        }

        public TElement FindElement<TElement>(By by) where TElement : class, IElement, new() {
            return FindElement<TElement>(by, WebDriver);
        }

        public IEnumerable<TElement> FindElements<TElement>(By by) where TElement : class, IElement, new() {
            return FindElements<TElement>(by, WebDriver);
        }


        public TPage WaitForSubmit<TPage>() where TPage : class, IPage, new()
        {
            WaitForPageToLoad();
            TPage page = GetCurrentUnsafe<TPage>();
            logger.Information($"Page {typeof(TPage).Name} was loaded after form submit");
            return page;
        }

        public string ExecuteScript(string script, params object[] args)
        {
            try {
                return (string)((IJavaScriptExecutor)WebDriver).ExecuteScript(script);
            }
            catch (InvalidOperationException) {
                throw;
            }
        }

        public void WaitForPageToLoad() {
            Wait.UntilEquals("Complete",
                ((IJavaScriptExecutor)WebDriver).ExecuteScript("return document.readyState").ToString,
                Timeout);
        }

        private TElement AsComponent<TElement>(IWebElement webElement, By searchCriteria) where TElement : class, new() {
            TElement element = (TElement)Activator.CreateInstance(typeof(TElement), true);
            (element as Element).InitializeElement(webElement, this, this as ISearchContext, searchCriteria);
            return element;
        }

        public void WaitForElementToAppear(By by) {
            try {
                WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(Timeout));
                wait.Until(ExpectedConditions.ElementExists(by));
            }
            catch (Exception ex) when (ex is TimeoutException or WebDriverTimeoutException) {
                logger.Error($"Element is not present after {Timeout} wait. By: {by}");
            }
        }
        private TPage GetCurrentUnsafe<TPage>() where TPage : class, IPage, new() {
            TPage page = new TPage();
            page.Initialize(this);
            CurrentPage = page;
            return page;
        }


        public void Dispose() {
            WebDriver.Dispose();
        }
    }
}
