using CoreUi.Browser;
using OpenQA.Selenium;

namespace CoreUi.Elements {
    public interface IElement {
        int Height { get; }

        int Width { get; }

        string Text { get; }

        public int TextNumber { get; }

        bool Enabled { get; }

        void Click();

        void InitializeElement(IWebElement element, IBrowser browser, ISearchContext searchContext, By searchCriteria);

        void ScrollTo();

        public void HoverMouse();

        public string GetAttribute(string name);

        public TElement FindElement<TElement>(By by) where TElement : class, IElement, new();

        public IEnumerable<TElement> FindElements<TElement>(By by) where TElement : class, IElement, new();

        void Type(string text, bool clear = true);
    }
}
