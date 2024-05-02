using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace CoreUi.Browser
{
    public static class BrowserFactory
    {
        private static readonly string[] DefaultOptions =
        {
            "--disable-extensions", "--disable-notifications", "--no-sandbox", "--disable-save-password-bubble",
            "--start-maximized", "--disable-dev-shm-usage"
        };

        public static IBrowser CreateChrome() {
            ChromeOptions options = CreateChromeOptions();
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);

            ChromeDriver driver = new ChromeDriver(options);

            return new Browser(driver);
        }

        private static ChromeOptions CreateChromeOptions() {
            ChromeOptions driverOptions = new ChromeOptions();
            driverOptions.AddArguments(DefaultOptions);
            return driverOptions;
        }
    }
}