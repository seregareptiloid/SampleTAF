using CoreUi.Browser;
using OpenQA.Selenium;
using Serilog;

namespace CoreUi.Pages
{
    public abstract class Page : IPage {
        public static ILogger Logger = Log.ForContext(typeof(Page));

        protected IWebDriver WebDriver { get; private set; }

        protected IBrowser Browser { get; private set; }

        public abstract string Uri { get; }

        public abstract Uri BaseUri { get; }

        public void Initialize(IBrowser browser) {
            WebDriver = browser.WebDriver;
            Browser = browser;
        }

    }
}
