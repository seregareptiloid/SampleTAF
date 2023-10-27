using CoreUi.Browser;
using NUnit.Framework;
using Serilog;

namespace CoreUi {
    public class UiFixtureBase {
        public IBrowser Browser;
        public ILogger Logger = Log.ForContext(typeof(UiFixtureBase));

        [SetUp]
        public void BrowserInit() {
            Browser = BrowserFactory.CreateChrome();
            Logger.Information("browser was created");
        }

        [TearDown]
        public void Cleanup() {
            Browser.Dispose();
            Logger.Information("browser was disposed");
        }
    }
}