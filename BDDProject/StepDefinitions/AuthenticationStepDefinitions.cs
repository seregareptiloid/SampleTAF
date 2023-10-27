using BLibraries.PageObjects;
using CoreUi.Browser;
using SampleTAF;

namespace BDDProject.StepDefinitions {
    [Binding]
    public sealed class AuthenticationStepDefinitions : BaseTestUi
    {
        public FormAuthenticationPage FormAuthenticationPage;

        [Given(@"\[Authentication page is opened]")]
        public void AuthenticationPageIsOpened() {
            Browser = BrowserFactory.CreateChrome();
            Browser.GoToPage<HerokuAppPage>();
            var herokuAppPage = Browser.WaitForSubmit<HerokuAppPage>();
            herokuAppPage.FormAuthenticationLink.Click();
        }

        [When(@"\[Users logins using bad credentials]")]
        public void UsersKLoginsUsingBadCredentials() {
            FormAuthenticationPage = Browser.WaitForSubmit<FormAuthenticationPage>();
            FormAuthenticationPage.AuthenticationFormComponent.PerformAuthentication("Failed", "SuperSecretPassword!");
        }

        [Then(@"\[the result should be failed]")]
        public void ThenTheResultShouldBe() {
            FormAuthenticationPage.ErrorMessage.Enabled.Should().BeTrue();
        }
    }
}