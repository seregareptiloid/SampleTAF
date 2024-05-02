using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLibraries.PageObjects;
using FluentAssertions;
using NUnit.Framework;

namespace SampleTAF {
    public class AccessibilityTest : BaseTestUi {
        [Test]
        public void AuthenticationSuccessTest() {
            var badAccessibilityExample = Browser.WaitForSubmit<BadAccessibilityExample>();
            badAccessibilityExample.WaitForPageToLoad();
        }
    }
}
