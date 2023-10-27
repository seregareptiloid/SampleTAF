using BLibraries.PageObjects;
using CoreUi;
using NUnit.Framework;

namespace SampleTAF {
    public class BaseTestUi : UiFixtureBase {
        [SetUp]
        public void SetupUiTest()
        {
            Browser.GoToPage<HerokuAppPage>();
        }
    }
}
