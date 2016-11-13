using FluentAssertions;
using obj_tc.Page;
using Objectivity.Test.Automation.Tests.Xunit;
using Xunit;

namespace obj_tc.Tests
{
    public class LogInTests : ProjectTestBase
    {
        [Fact]
        public void LogInTest()
        {
            var page = new LandingPage(DriverContext);

            var signInPage = page.OpenLandingPage().OpenLogInPage();

            var dashboardPage = signInPage
                .SetEmail("objectivity1@pgs-soft.com")
                .SetPassword("FrhHHLQLj9")
                .LogIn();

            dashboardPage.UserName.Should().Be("objectivity1@pgs-soft.com");
        }
    }
}
