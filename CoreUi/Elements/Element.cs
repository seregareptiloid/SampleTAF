using System.Text.RegularExpressions;
using CoreUi.Browser;
using CoreUi.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Serilog;

namespace CoreUi.Elements
{
    public class Element : IElement
    {
        public static readonly ILogger Logger = Log.ForContext(typeof(Element));

        protected IBrowser _browser;

        protected ISearchContext _searchContext;

        private By _searchCriteria;

        protected IWebElement _webElement;

        public int Height => _webElement.Size.Height;

        public int Width => _webElement.Size.Width;

        public string Text => _webElement.Text;

        public int TextNumber => int.Parse(Regex.Match(Text, @"\d+").Value);

        public bool Enabled => _webElement.Enabled;

        public void InitializeElement(IWebElement element, IBrowser browser, ISearchContext searchContext,
            By searchCriteria)
        {
            _webElement = element;
            _browser = browser;
            _searchContext = searchContext;
            _searchCriteria = searchCriteria;
        }

        public void Click()
        {
            try
            {
                Wait.For(() => _webElement.Displayed);
                if (!_webElement.Displayed)
                {
                    ScrollTo();
                    HoverMouse();
                    SafeClick();
                }
                else
                {
                    HoverMouse();
                    SafeClick();
                }
            }
            catch (ElementClickInterceptedException)
            {
                Logger.Error($"Element Click Intercepted Exception occurred while clicking {_searchCriteria.Criteria}");
            }

            Logger.Information($"Clicked Element by locator {_searchCriteria.Criteria}");
        }

        private void SafeClick()
        {
            try
            {
                _webElement.Click();
            }
            catch (StaleElementReferenceException)
            {
                _webElement = _browser.WebDriver.FindElement(_searchCriteria);
                _webElement.Click();
            }
            catch (ElementNotVisibleException e)
            {
                Logger.Error($"Element '{_webElement.GetCssValue("class")}' not visible: {e.StackTrace}");
            }
            catch (MoveTargetOutOfBoundsException)
            {
                _webElement.Click();
            }
        }

        public string GetAttribute(string name) {
            return _webElement.GetAttribute(name);
        }

        public void Type(string text, bool clear = true) {
            try {
                if (clear) {
                    _webElement.Clear();
                }

                _webElement.SendKeys(text);
            }
            catch (ElementNotInteractableException) {
                new Actions(_browser.WebDriver).KeyDown(Keys.Control)
                    .SendKeys("a").KeyUp(Keys.Control).SendKeys(Keys.Backspace)
                    .SendKeys(text).Perform();
            }
        }

        public TElement FindElement<TElement>(By by) where TElement : class, IElement, new() {
            return _browser.FindElement<TElement>(by, _webElement);
        }

        public IEnumerable<TElement> FindElements<TElement>(By by) where TElement : class, IElement, new() {
            return _browser.FindElements<TElement>(by, _webElement);
        }

        public void ScrollTo()
        {
            Actions actions = new(_browser.WebDriver);
            actions.MoveToElement(_webElement).Perform();
        }

        public void HoverMouse()
        {
            Actions actions = _browser.BrowserActions;
            try
            {
                actions.MoveToElement(_webElement).Perform();
            }
            catch (StaleElementReferenceException)
            {
                _webElement = _browser.WebDriver.FindElement(_searchCriteria);
                actions.MoveToElement(_webElement).Perform();
            }
        }
    }
}
