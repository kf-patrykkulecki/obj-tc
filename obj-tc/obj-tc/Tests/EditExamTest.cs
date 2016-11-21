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

        [Fact]
        public void EditExamSessionInOrderToChangesPerExamSessionToPerType_Test()
        {
            var landingPage = new LandingPage(DriverContext);

            // Data
            var sessionDate = date.AddDays(1).ToString(format);
            var sessionCity = GetTimeStamp();
            var level = new List<string>
            {
                "Podstawowy",
                "Ekspercki"
            };
            var product = new Dictionary<string, int>
            {
                {"ISTQB Foundation Level / Polski, Angielski", 999},
                {"ISTQB Improving the Testing Process / Angielski", 999 }
            };
            var expectedProduct = new List<string>
            {
                "ISTQB Foundation Level/Polski, Angielski",
                "ISTQB Improving the Testing Process/Angielski"
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

            addSessionPage.SelectSpacePerProduct();

            foreach (var prod in product)
            {
                addSessionPage.SetProductSpace(prod.Key, prod.Value);
            }

            addSessionPage.SaveSession();

            sessionDetailsPage.Space.Should().Be(product.Select(el => el.Value).Sum());
            sessionDetailsPage.SwitchToExams();
            sessionDetailsPage.ExamList.ShouldAllBeEquivalentTo(expectedProduct);
            sessionDetailsPage.ExamsSpaceBasic.Should().Be(999);
            sessionDetailsPage.ExamsSpaceExpert.Should().Be(999);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1000)]
        public void EditExamSessionInOrderToChangesPerExamSessionToPerTypeNegative_Test(int param)
        {
            var landingPage = new LandingPage(DriverContext);

            // Data
            var sessionDate = date.AddDays(1).ToString(format);
            var sessionCity = GetTimeStamp();
            var level = new List<string>
            {
                "Podstawowy",
                "Ekspercki"
            };
            var product = new Dictionary<string, int>
            {
                {"ISTQB Foundation Level / Polski, Angielski", 999},
                {"ISTQB Improving the Testing Process / Angielski", 999 }
            };
            var expectedProduct = new List<string>
            {
                "ISTQB Foundation Level/Polski, Angielski",
                "ISTQB Improving the Testing Process/Angielski"
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

            addSessionPage.SelectSpacePerProduct();

            foreach (var prod in product)
            {
                addSessionPage.SetProductSpace(prod.Key, param);
            }

            addSessionPage.spacePerProductValidationPresent.Should().BeTrue();
            addSessionPage.spacePerProductValidationText.First().Should().Be("Wymagana jest liczba całkowita z zakresu 0-999");
            addSessionPage.spacePerProductValidationText.Last().Should().Be("Wymagana jest liczba całkowita z zakresu 0-999");
        }

        [Fact]
        public void EditExamSessionInOrderToChangesPerExamTypeToPerSession_Test()
        {
            var landingPage = new LandingPage(DriverContext);

            // Data
            var sessionDate = date.AddDays(1).ToString(format);
            var sessionCity = GetTimeStamp();
            var level = new List<string>
            {
                "Podstawowy",
                "Ekspercki"
            };
            var product = new Dictionary<string, int>
            {
                {"ISTQB Foundation Level / Polski, Angielski", 999},
                {"ISTQB Improving the Testing Process / Angielski", 999 }
            };
            var expectedProduct = new List<string>
            {
                "ISTQB Foundation Level/Polski, Angielski",
                "ISTQB Improving the Testing Process/Angielski"
            };

            var addSessionPage =
                landingPage.OpenLandingPage().OpenLogInPage().SetEmail(email).SetPassword(password).LogIn().Topbar.OpenAddSession();

            // Create session
            addSessionPage.SetDate(sessionDate)
                .SetCity(sessionCity.ToString())
                .SelectSpacePerProduct()
                .SelectLevel(level)
                .SelectProduct(product.Select(el => el.Key).ToList());

            foreach (var prod in product)
            {
                addSessionPage.SetProductSpace(prod.Key, prod.Value);
            }

            var sessionDetailsPage = addSessionPage.SaveSession();

            // Check session details
            sessionDetailsPage.Space.Should().Be(product.Select(el => el.Value).Sum());
            sessionDetailsPage.SwitchToExams();
            sessionDetailsPage.ExamList.ShouldAllBeEquivalentTo(expectedProduct);
            sessionDetailsPage.ExamsSpaceBasic.Should().Be(999);
            sessionDetailsPage.ExamsSpaceExpert.Should().Be(999);

            // Check if session present on dashboard
            sessionDetailsPage.EditSession();

            addSessionPage.SelectSpacePerSession().SetSpacePerSession(100.ToString()).SaveSession();

            sessionDetailsPage.Space.Should().Be(100);
            sessionDetailsPage.SwitchToExams();
            sessionDetailsPage.ExamList.ShouldAllBeEquivalentTo(expectedProduct);
            sessionDetailsPage.ExamsSpaceBasic.Should().Be(100);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1000)]
        public void EditExamSessionInOrderToChangesPerExamTypeToPerSessionNegative_Test(int param)
        {
            var landingPage = new LandingPage(DriverContext);

            // Data
            var sessionDate = date.AddDays(1).ToString(format);
            var sessionCity = GetTimeStamp();
            var level = new List<string>
            {
                "Podstawowy",
                "Ekspercki"
            };
            var product = new Dictionary<string, int>
            {
                {"ISTQB Foundation Level / Polski, Angielski", 999},
                {"ISTQB Improving the Testing Process / Angielski", 999 }
            };
            var expectedProduct = new List<string>
            {
                "ISTQB Foundation Level/Polski, Angielski",
                "ISTQB Improving the Testing Process/Angielski"
            };

            var addSessionPage =
                landingPage.OpenLandingPage().OpenLogInPage().SetEmail(email).SetPassword(password).LogIn().Topbar.OpenAddSession();

            // Create session
            addSessionPage.SetDate(sessionDate)
                .SetCity(sessionCity.ToString())
                .SelectSpacePerProduct()
                .SelectLevel(level)
                .SelectProduct(product.Select(el => el.Key).ToList());

            foreach (var prod in product)
            {
                addSessionPage.SetProductSpace(prod.Key, prod.Value);
            }

            var sessionDetailsPage = addSessionPage.SaveSession();

            // Check session details
            sessionDetailsPage.Space.Should().Be(product.Select(el => el.Value).Sum());
            sessionDetailsPage.SwitchToExams();
            sessionDetailsPage.ExamList.ShouldAllBeEquivalentTo(expectedProduct);
            sessionDetailsPage.ExamsSpaceBasic.Should().Be(999);
            sessionDetailsPage.ExamsSpaceExpert.Should().Be(999);

            // Check if session present on dashboard
            sessionDetailsPage.EditSession();

            addSessionPage.SelectSpacePerSession().SetSpacePerSession(param.ToString()).SaveSessionReturnAddSession();
            addSessionPage.spacePerSessionValidationPresent.Should().BeTrue();
            addSessionPage.spacePerSessionValidationText.Should().Be("Wymagana jest liczba całkowita z zakresu 0-999");
        }

        private int GetTimeStamp()
        {
            return (int)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
