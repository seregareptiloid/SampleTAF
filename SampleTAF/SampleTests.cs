using BLibraries.PageObjects;
using FluentAssertions;
using NUnit.Framework;

namespace SampleTAF {
    public class SampleTests : BaseTestUi {
        [Test]
        public void AuthenticationSuccessTest()
        {
            var herokuAppPage = Browser.WaitForSubmit<HerokuAppPage>();
            herokuAppPage.FormAuthenticationLink.Click();
            var formAuthenticationPage = Browser.WaitForSubmit<FormAuthenticationPage>();
            formAuthenticationPage.AuthenticationFormComponent.PerformAuthentication("tomsmith", "SuperSecretPassword!");
            formAuthenticationPage.SuccessMessage.Enabled.Should().BeTrue();
        }

        [Test]
        public void AuthenticationFailureTest()
        {
            var herokuAppPage = Browser.WaitForSubmit<HerokuAppPage>();
            herokuAppPage.FormAuthenticationLink.Click();
            var formAuthenticationPage = Browser.WaitForSubmit<FormAuthenticationPage>();
            formAuthenticationPage.AuthenticationFormComponent.PerformAuthentication("Failed", "SuperSecretPassword!");
            formAuthenticationPage.ErrorMessage.Enabled.Should().BeTrue();
        }

        [Test]
        public void AddElementsTest()
        {
            var herokuAppPage = Browser.WaitForSubmit<HerokuAppPage>();
            herokuAppPage.AddOrRemoveElementLink.Click();
            var addRemoveElementPage = Browser.WaitForSubmit<AddRemoveElementPage>();
            addRemoveElementPage.AddElementButton.Click();
            addRemoveElementPage.ElementsButtons.Count().Should().Be(1);
        }

        [Test]
        public void CheckBoxTest()
        {
            var herokuAppPage = Browser.WaitForSubmit<HerokuAppPage>();
            herokuAppPage.CheckBoxesLink.Click();
            var checkBoxPage = Browser.WaitForSubmit<CheckBoxPageObject>();
            checkBoxPage.ClickCheckboxes();
            checkBoxPage.CheckBoxOne.IsChecked.Should().Be("true");
        }
    }
}
