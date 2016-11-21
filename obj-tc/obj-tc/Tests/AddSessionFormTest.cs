using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using obj_tc.Extensions;
using obj_tc.Page;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Tests.Xunit;
using Xunit;

namespace obj_tc.Tests
{
    public class AddSessionFormTest : ProjectTestBase
    {
        private readonly string email = BaseConfiguration.Username;
        private readonly string password = BaseConfiguration.Password;
        private DateTime date = DateTime.Now;
        private const string format = "dd.MM.yyyy HH:mm";

        [Fact]
        public void CheckFormValidation_Test() {
            var landingPage = new LandingPage(DriverContext);
            var dashboardPage =
                landingPage.OpenLandingPage().OpenLogInPage().SetEmail(email).SetPassword(password).LogIn();

            var addSessionPage = dashboardPage.Topbar.OpenAddSession();
            var city = "Konin";
            var examDate = date.AddDays(2).ToString(format);
            var level = new List<string> { "Ekspercki" };
            var product = new Dictionary<string, int>
            {
                {"ISTQB Improving the Testing Process / Angielski", 999}
            };
            var expectedProduct = new List<string>
            {
                "ISTQB Improving the Testing Process/Angielski"
            };

            var result = addSessionPage.SaveSessionReturnAddSession();

            result.cityValidationText.Should().Be("Pole City jest wymagane");
            
            addSessionPage.SetCity("Ko");

            result = addSessionPage.SaveSessionReturnAddSession();

            result.cityValidationText.Should().Be("Pole musi być ciągiem o minimalnej długości 3 i maksymalnej długości 50.");

            addSessionPage.SetCity(city);

            result = addSessionPage.SaveSessionReturnAddSession();

            result.dateValidationText.Should().Be("Pole Date jest wymagane");
            result.cityValidationPresent.Should().BeFalse();

            addSessionPage.SetDate(examDate);

            result = addSessionPage.SaveSessionReturnAddSession();

            result.productValidationText.Should().Be("Musisz wybrać co najmniej jeden produkt");
            result.cityValidationPresent.Should().BeFalse();
            result.dateValidationPresent.Should().BeFalse();

            addSessionPage.SelectLevel(level)
                .SelectProduct(product.Select(el => el.Key).ToList());

            var sessionDetailsPage = addSessionPage.SaveSession();

            sessionDetailsPage.Date.Should().Be(examDate.Split(' ')[0]);
            sessionDetailsPage.Time.Should().Be(examDate.Split(' ')[1]);
            sessionDetailsPage.City.Should().Be(city);

            sessionDetailsPage.SwitchToExams();
            sessionDetailsPage.ExamList.ShouldAllBeEquivalentTo(expectedProduct);
                 
        }

        [Theory]
        [InlineData("Bielsko-Biała")]
        [InlineData("Żółwia błoć")]
        [InlineData("ąęźżćśóńł")]
        [InlineData("Kon")]
        [InlineData("asdsbvewghmhffdmnbfdffhjjhbvrerhtjyngbfeghnjbrefgm")]
        public void CheckCityNameExamples_Test(string city) {
            var landingPage = new LandingPage(DriverContext);
            var dashboardPage =
                landingPage.OpenLandingPage().OpenLogInPage().SetEmail(email).SetPassword(password).LogIn();
            var examDate = date.AddDays(2).ToString(format);
            var level = new List<string> { "Ekspercki" };
            var product = new Dictionary<string, int>
            {
                {"ISTQB Improving the Testing Process / Angielski", 999}
            };
            var expectedProduct = new List<string>
            {
                "ISTQB Improving the Testing Process/Angielski"
            };
            var addSessionPage = dashboardPage.Topbar.OpenAddSession();

            addSessionPage.SetCity(city);

            var result = addSessionPage.SaveSessionReturnAddSession();

            result.cityValidationPresent.Should().BeFalse();

            addSessionPage.SetDate(examDate);
            addSessionPage.SelectLevel(level)
                .SelectProduct(product.Select(el => el.Key).ToList());

            var sessionDetailsPage = addSessionPage.SaveSession();

            sessionDetailsPage.City.Should().Be(city);
        }

        [Theory]
        [InlineData("Ko")]
        [InlineData("asdsbvewghmhffdmnbfdffhjjhbvrerhtjyngbfeghnjbrefgm ")]
        [InlineData("asdsbvewghmhffdmnbfdffhjjhbvrerhtjyngbfeghnjbrefgma")]
        [InlineData("    ")]
        public void CheckCityNameLengthPositive_Test(string city) {
            var landingPage = new LandingPage(DriverContext);
            var dashboardPage =
                landingPage.OpenLandingPage().OpenLogInPage().SetEmail(email).SetPassword(password).LogIn();
            var addSessionPage = dashboardPage.Topbar.OpenAddSession();


            addSessionPage.SetCity(city);

            var result = addSessionPage.SaveSessionReturnAddSession();
            if (city.Equals("    "))
            {
                result.cityValidationText.Should().Be("Pole City jest wymagane");
            }
            else
            {
                result.cityValidationText.Should().Be("Pole musi być ciągiem o minimalnej długości 3 i maksymalnej długości 50.");
            }

        }

        [Theory]
        [InlineData("-1")]
        [InlineData("30.12 12:55")]
        [InlineData("30.12.0000 12:55")]
        [InlineData("32.11.2016 12:55")]
        [InlineData("19.13.2016 12:55")]
        [InlineData("00.00.2016 12:55")]
        [InlineData("30.12.2016 25:00")]
        [InlineData("30.12.2016 21:60")]
        [InlineData("29.02.2017 01:00")]
        public void CheckNegativeDates_Test(string testDate) {
            var landingPage = new LandingPage(DriverContext);
            var dashboardPage =
                landingPage.OpenLandingPage().OpenLogInPage().SetEmail(email).SetPassword(password).LogIn();
            var addSessionPage = dashboardPage.Topbar.OpenAddSession();
            var city = "Parowkowo";
            var examDate = date.AddDays(2).ToString(format);
            var level = new List<string> { "Ekspercki" };
            var product = new Dictionary<string, int>
            {
                {"ISTQB Improving the Testing Process / Angielski", 999}
            };
            var expectedProduct = new List<string>
            {
                "ISTQB Improving the Testing Process/Angielski"
            };

            addSessionPage.SetCity(city);
            addSessionPage.SetDate(examDate);
            addSessionPage.SetDate(testDate);

            var result = addSessionPage.SaveSessionReturnAddSessionWithAlert();

            result.dateValidationPresent.Should().BeTrue();
            result.dateValidationText.Should().Be("Pole Date jest wymagane");
        }

        [Theory]
        [InlineData("29.02.2020 01:00")]
        [InlineData("30.12.2016 00:00")]
        public void CheckLeapYear_Test(string inputDate) {
            var landingPage = new LandingPage(DriverContext);
            var dashboardPage =
                landingPage.OpenLandingPage().OpenLogInPage().SetEmail(email).SetPassword(password).LogIn();
            var addSessionPage = dashboardPage.Topbar.OpenAddSession();
            string city =Convert.ToString( GetTimeStamp());
            var testDate = inputDate;
            var level = new List<string> { "Ekspercki" };
            var product = new Dictionary<string, int>
            {
                {"ISTQB Improving the Testing Process / Angielski", 999}
            };
            var expectedProduct = new List<string>
            {
                "ISTQB Improving the Testing Process/Angielski"
            };

            addSessionPage.SetCity(city);
            addSessionPage.SetDate(testDate);

            addSessionPage.SelectLevel(level)
              .SelectProduct(product.Select(el => el.Key).ToList());

            var sessionDetailsPage = addSessionPage.SaveSessionAndAcceptAlert();
            sessionDetailsPage.Date.Should().Be(testDate.Split(' ')[0]);
            sessionDetailsPage.Time.Should().Be(testDate.Split(' ')[1]);
        }

        [Theory]
        [InlineData("-1")]
        public void CheckSpaceNegativeCondition_Test(string space) {
            var landingPage = new LandingPage(DriverContext);
            var dashboardPage =
                landingPage.OpenLandingPage().OpenLogInPage().SetEmail(email).SetPassword(password).LogIn();
            var addSessionPage = dashboardPage.Topbar.OpenAddSession();
            string city = Convert.ToString(GetTimeStamp());
            var testDate = date.AddDays(2).ToString(format);
            var testSpace = space;
            var level = new List<string> { "Ekspercki" };
            var product = new Dictionary<string, int>
            {
                {"ISTQB Improving the Testing Process / Angielski", 999}
            };
            var expectedProduct = new List<string>
            {
                "ISTQB Improving the Testing Process/Angielski"
            };

            addSessionPage.SetCity(city);
            addSessionPage.SetDate(testDate);

            addSessionPage.SelectLevel(level)
              .SelectProduct(product.Select(el => el.Key).ToList());
            addSessionPage.SetSpacePerSession(testSpace);

            var result = addSessionPage.SaveSessionReturnAddSession();

            result.spaceValidationPresent.Should().BeTrue();
        }

        [Fact]
        public void CheckSpacePerSessionVisibleByDefault_Test()
        {
            var landingPage = new LandingPage(DriverContext);
            var dashboardPage =
                landingPage.OpenLandingPage().OpenLogInPage().SetEmail(email).SetPassword(password).LogIn();
            var addSessionPage = dashboardPage.Topbar.OpenAddSession();

            addSessionPage.spacePerSessionInputPresent.Should().BeTrue();
        }

        private int GetTimeStamp()
        {
            return (int)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
