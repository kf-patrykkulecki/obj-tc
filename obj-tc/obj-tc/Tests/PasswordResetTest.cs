using FluentAssertions;
using obj_tc.Page;
using Objectivity.Test.Automation.Tests.Xunit;
using Xunit;

namespace obj_tc.Tests
{
    public class PasswordResetTest : ProjectTestBase
    {
        [Fact]
        public void PasswordReset_Test()
        {
            const string email = "objectivity1@pgs-soft.com";
            var landingPage = new LandingPage(DriverContext);

            var forgotPasswordPage = landingPage.OpenLandingPage().OpenLogInPage().ForgotPassword();

            var forgotPasswordConfirmationPage =
                forgotPasswordPage.SetEmail(email).ResetPassword();

            forgotPasswordConfirmationPage.ConfirmationMessage.Should().Be(email);
        }
    }
}
