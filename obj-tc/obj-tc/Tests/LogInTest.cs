using FluentAssertions;
using obj_tc.Page;
using Objectivity.Test.Automation.Tests.Xunit;
using Xunit;

namespace obj_tc.Tests
{
    public class LogInTest : ProjectTestBase
    {
        [Fact]
        public void LogIn_Test()
        {
            const string email = "objectivity1@pgs-soft.com";
            const string password = "FrhHHLQLj9";
            var page = new LandingPage(DriverContext);

            var signInPage = page.OpenLandingPage().OpenLogInPage();

            var dashboardPage = signInPage
                .SetEmail(email)
                .SetPassword(password)
                .LogIn();

            dashboardPage.UserName.Should().Be(email);
        }
    }
}
