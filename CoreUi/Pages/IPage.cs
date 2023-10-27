using CoreUi.Browser;
using Serilog;

namespace CoreUi.Pages {
    public interface IPage {
        public static ILogger Logger;

        public string Uri { get; }

        public void Initialize(IBrowser browser);
    }
}
