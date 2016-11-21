using FluentAssertions;
using obj_tc.Page;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Tests.Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace obj_tc.Tests
{
    public class EditExamTest : ProjectTestBase
    {
        private readonly string email = BaseConfiguration.Username;
        private readonly string password = BaseConfiguration.Password;
        private DateTime date = DateTime.Now;
        private const string format = "dd.MM.yyyy HH:mm";

        [Fact]
        public void EditExamSessionInOrderToChangeMaximumNumberOfParticipantsPerExamSession_Test()
        {
            var landingPage = new LandingPage(DriverContext);

            // Data
            var sessionDate = date.AddDays(1).ToString(format);
            var sessionCity = GetTimeStamp();
            var level = new List<string>
            {
                "Podstawowy"
            };
            var product = new Dictionary<string, int>
            {
                {"ISTQB Foundation Level / Polski, Angielski", 999}
            };
            var expectedProduct = new List<string>
            {
                "ISTQB Foundation Level/Polski, Angielski"
            };

            var addSessionPage =
                landingPage.OpenLandingPage().OpenLogInPage().SetEmail(email).SetPassword(password).LogIn().Topbar.OpenAddSession();

            // Create session
            addSessionPage.SetDate(sessionDate)
                .SetCity(sessionCity.ToString())
                .SetSpacePerSession(999.ToString())
                .SelectLevel(level)
                .SelectProduct(product.Select(el => el.Key).ToList());

            var sessionDetailsPage = addSessionPage.SaveSession();

            // Check session details
            sessionDetailsPage.Space.Should().Be(999);
            sessionDetailsPage.SwitchToExams();
            sessionDetailsPage.ExamList.ShouldAllBeEquivalentTo(expectedProduct);
            sessionDetailsPage.ExamsSpaceBasic.Should().Be(999);

            // Check if session present on dashboard
            sessionDetailsPage.EditSession();

            addSessionPage.SetSpacePerSession(200.ToString()).SaveSession();

            sessionDetailsPage.Space.Should().Be(200);
            sessionDetailsPage.SwitchToExams();
            sessionDetailsPage.ExamList.ShouldAllBeEquivalentTo(expectedProduct);
            sessionDetailsPage.ExamsSpaceBasic.Should().Be(200);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1000)]
        public void EditExamSessionInOrderToChangeMaximumNumberOfParticipantsPerExamSessionNegative_Test(int param)
        {
            var landingPage = new LandingPage(DriverContext);

            // Data
            var sessionDate = date.AddDays(1).ToString(format);
            var sessionCity = GetTimeStamp();
            var level = new List<string>
            {
                "Podstawowy"
            };
            var product = new Dictionary<string, int>
            {
                {"ISTQB Foundation Level / Polski, Angielski", 999}
            };
            var expectedProduct = new List<string>
            {
                "ISTQB Foundation Level/Polski, Angielski"
            };

            var addSessionPage =
                landingPage.OpenLandingPage().OpenLogInPage().SetEmail(email).SetPassword(password).LogIn().Topbar.OpenAddSession();

            // Create session
            addSessionPage.SetDate(sessionDate)
                .SetCity(sessionCity.ToString())
                .SetSpacePerSession(999.ToString())
                .SelectLevel(level)
                .SelectProduct(product.Select(el => el.Key).ToList());

            var sessionDetailsPage = addSessionPage.SaveSession();

            // Check session details
            sessionDetailsPage.Space.Should().Be(999);
            sessionDetailsPage.SwitchToExams();
            sessionDetailsPage.ExamList.ShouldAllBeEquivalentTo(expectedProduct);
            sessionDetailsPage.ExamsSpaceBasic.Should().Be(999);

            // Check if session present on dashboard
            sessionDetailsPage.EditSession();

            addSessionPage.SetSpacePerSession(param.ToString()).SaveSessionReturnAddSession();

            addSessionPage.spacePerSessionValidationPresent.Should().BeTrue();
            addSessionPage.spacePerSessionValidationText.Should().Be("Wymagana jest liczba całkowita z zakresu 0-999");
        }

        private int GetTimeStamp()
        {
            return (int)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
