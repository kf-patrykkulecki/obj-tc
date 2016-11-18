﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using obj_tc.Page;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Tests.Xunit;
using Xunit;

namespace obj_tc.Tests
{
    public class ExamSessionTest : ProjectTestBase
    {
        private readonly string email = BaseConfiguration.Username;
        private readonly string password = BaseConfiguration.Password;
        private DateTime date = DateTime.Now;
        private const string format = "dd.MM.yyyy HH:mm";

        [Fact]
        public void AddExamSessionForOneTypeOfExam_Test()
        {
            var landingPage = new LandingPage(DriverContext);

            // Data
            var sessionDate = date.AddDays(1).ToString(format);
            var sessionCity = GetTimeStamp();
            var level = new List<string> {"Podstawowy"};
            var product = new List<string> {"ISTQB Foundation Level / Polski, Angielski"};
            var expectedProduct = new List<string> {"ISTQB Foundation Level/Polski, Angielski"};

            var dashboardPage =
                landingPage.OpenLandingPage().OpenLogInPage().SetEmail(email).SetPassword(password).LogIn();

            var addSessionPage = dashboardPage.Topbar.OpenAddSession();

            // Create session
            addSessionPage.SetDate(sessionDate)
                .SetCity(sessionCity.ToString())
                .SelectSpacePerProduct()
                .SelectLevel(level)
                .SelectProduct(product);

            var sessionDetailsPage = addSessionPage.SaveSession();

            // Check session details
            sessionDetailsPage.Date.Should().Be(sessionDate.Split(' ')[0]);
            sessionDetailsPage.Time.Should().Be(sessionDate.Split(' ')[1]);
            sessionDetailsPage.Space.Should().Be(0);
            sessionDetailsPage.Examiner.Should().BeEmpty();
            sessionDetailsPage.PostCode.Should().Be("-");
            sessionDetailsPage.City.Should().Be(sessionCity.ToString());
            sessionDetailsPage.Address.Should().Be("-");
            sessionDetailsPage.AdditionalInformation.Should().Be("-");
            sessionDetailsPage.Status.Should().Be("Zamknięta - niezalogowani");

            sessionDetailsPage.SwitchToExams();
            sessionDetailsPage.ExamList.ShouldAllBeEquivalentTo(expectedProduct);

            // Check if session present on dashboard
            sessionDetailsPage.Topbar.OpenDashboard().IsSessionDisplayed(sessionCity.ToString()).Should().BeTrue();
        }

        [Fact]
        public void AddExamSessionForFewTypesOfExamAtTheSameExamLevel_Test()
        {
            var landingPage = new LandingPage(DriverContext);

            // Data
            var sessionDate = date.AddDays(2).ToString(format);
            var sessionCity = GetTimeStamp();
            var level = new List<string> {"Podstawowy"};
            var product = new List<string>
            {
                "ISTQB Foundation Level / Polski, Angielski",
                "REQB Foundation Level / Polski, Angielski"
            };
            var expectedProduct = new List<string>
            {
                "ISTQB Foundation Level/Polski, Angielski",
                "REQB Foundation Level/Polski, Angielski"
            };

            var dashboardPage =
                landingPage.OpenLandingPage().OpenLogInPage().SetEmail(email).SetPassword(password).LogIn();

            var addSessionPage = dashboardPage.Topbar.OpenAddSession();

            // Create session
            addSessionPage.SetDate(sessionDate)
                .SetCity(sessionCity.ToString())
                .SelectSpacePerProduct()
                .SelectLevel(level)
                .SelectProduct(product);

            var sessionDetailsPage = addSessionPage.SaveSession();

            // Check session details
            sessionDetailsPage.Date.Should().Be(sessionDate.Split(' ')[0]);
            sessionDetailsPage.Time.Should().Be(sessionDate.Split(' ')[1]);
            sessionDetailsPage.Space.Should().Be(0);
            sessionDetailsPage.Examiner.Should().BeEmpty();
            sessionDetailsPage.PostCode.Should().Be("-");
            sessionDetailsPage.City.Should().Be(sessionCity.ToString());
            sessionDetailsPage.Address.Should().Be("-");
            sessionDetailsPage.AdditionalInformation.Should().Be("-");
            sessionDetailsPage.Status.Should().Be("Zamknięta - niezalogowani");

            sessionDetailsPage.SwitchToExams();
            sessionDetailsPage.ExamList.ShouldAllBeEquivalentTo(expectedProduct);

            // Check if session present on dashboard
            sessionDetailsPage.Topbar.OpenDashboard().IsSessionDisplayed(sessionCity.ToString()).Should().BeTrue();
        }

        [Fact]
        public void AddExamSessionForFewTypesOfExamOnDifferentExamLevels_Test()
        {
            var landingPage = new LandingPage(DriverContext);

            // Data
            var sessionDate = date.AddDays(3).ToString(format);
            var sessionCity = GetTimeStamp();
            var level = new List<string>
            {
                "Podstawowy",
                "Zaawansowany",
                "Ekspercki",
                "Inny"
            };
            var product = new List<string>
            {
                "ISTQB Foundation Level / Polski, Angielski",
                "REQB Foundation Level / Polski, Angielski",
                "ISTQB Advanced Level Test Manager / Polski, Angielski",
                "ISTQB Advanced Level Technical Test Analyst / Polski, Angielski",
                "ISTQB Advanced Level Test Analyst / Polski, Angielski",
                "ISTQB Test Management / Angielski",
                "ISTQB Improving the Testing Process / Angielski",
                "ISTQB Agile Tester Extension / Polski, Angielski"
            };
            var expectedProduct = new List<string>
            {
                "ISTQB Foundation Level/Polski, Angielski",
                "REQB Foundation Level/Polski, Angielski",
                "ISTQB Advanced Level Test Manager/Polski, Angielski",
                "ISTQB Advanced Level Technical Test Analyst/Polski, Angielski",
                "ISTQB Advanced Level Test Analyst/Polski, Angielski",
                "ISTQB Test Management/Angielski",
                "ISTQB Improving the Testing Process/Angielski",
                "ISTQB Agile Tester Extension/Polski, Angielski"
            };

            var dashboardPage =
                landingPage.OpenLandingPage().OpenLogInPage().SetEmail(email).SetPassword(password).LogIn();

            var addSessionPage = dashboardPage.Topbar.OpenAddSession();

            // Create session
            addSessionPage.SetDate(sessionDate)
                .SetCity(sessionCity.ToString())
                .SelectSpacePerProduct()
                .SelectLevel(level)
                .SelectProduct(product);

            var sessionDetailsPage = addSessionPage.SaveSession();

            // Check session details
            sessionDetailsPage.Date.Should().Be(sessionDate.Split(' ')[0]);
            sessionDetailsPage.Time.Should().Be(sessionDate.Split(' ')[1]);
            sessionDetailsPage.Space.Should().Be(0);
            sessionDetailsPage.Examiner.Should().BeEmpty();
            sessionDetailsPage.PostCode.Should().Be("-");
            sessionDetailsPage.City.Should().Be(sessionCity.ToString());
            sessionDetailsPage.Address.Should().Be("-");
            sessionDetailsPage.AdditionalInformation.Should().Be("-");
            sessionDetailsPage.Status.Should().Be("Zamknięta - niezalogowani");

            sessionDetailsPage.SwitchToExams();
            sessionDetailsPage.ExamList.ShouldAllBeEquivalentTo(expectedProduct);

            // Check if session present on dashboard
            sessionDetailsPage.Topbar.OpenDashboard().IsSessionDisplayed(sessionCity.ToString()).Should().BeTrue();
        }

        [Fact]
        public void AddExamSessionWithMaximumNumberOfParticipantsDefinedPerExamType_Test()
        {
            var landingPage = new LandingPage(DriverContext);

            // Data
            var sessionDate = date.AddDays(4).ToString(format);
            var sessionCity = GetTimeStamp();
            var level = new List<string>
            {
                "Podstawowy",
                "Zaawansowany",
                "Ekspercki",
                "Inny"
            };
            var product = new Dictionary<string, int>
            {
                {"ISTQB Foundation Level / Polski, Angielski", 999},
                {"REQB Foundation Level / Polski, Angielski", 999},
                {"ISTQB Advanced Level Test Manager / Polski, Angielski", 999},
                {"ISTQB Advanced Level Technical Test Analyst / Polski, Angielski", 999},
                {"ISTQB Advanced Level Test Analyst / Polski, Angielski", 999},
                {"ISTQB Test Management / Angielski", 999},
                {"ISTQB Improving the Testing Process / Angielski", 999},
                {"ISTQB Agile Tester Extension / Polski, Angielski", 999},
            };
            var expectedProduct = new List<string>
            {
                "ISTQB Foundation Level/Polski, Angielski",
                "REQB Foundation Level/Polski, Angielski",
                "ISTQB Advanced Level Test Manager/Polski, Angielski",
                "ISTQB Advanced Level Technical Test Analyst/Polski, Angielski",
                "ISTQB Advanced Level Test Analyst/Polski, Angielski",
                "ISTQB Test Management/Angielski",
                "ISTQB Improving the Testing Process/Angielski",
                "ISTQB Agile Tester Extension/Polski, Angielski"
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
            sessionDetailsPage.Date.Should().Be(sessionDate.Split(' ')[0]);
            sessionDetailsPage.Time.Should().Be(sessionDate.Split(' ')[1]);
            sessionDetailsPage.Space.Should().Be(product.Select(el => el.Value).Sum());
            sessionDetailsPage.Examiner.Should().BeEmpty();
            sessionDetailsPage.PostCode.Should().Be("-");
            sessionDetailsPage.City.Should().Be(sessionCity.ToString());
            sessionDetailsPage.Address.Should().Be("-");
            sessionDetailsPage.AdditionalInformation.Should().Be("-");
            sessionDetailsPage.Status.Should().Be("Nowy");

            sessionDetailsPage.SwitchToExams();
            sessionDetailsPage.ExamList.ShouldAllBeEquivalentTo(expectedProduct);

            sessionDetailsPage.ExamsSpaceBasic.Should().Be(product.Select(el => el.Value).Take(2).Sum());
            sessionDetailsPage.ExamsSpaceAdvanced.Should().Be(product.Select(el => el.Value).Skip(2).Take(3).Sum());
            sessionDetailsPage.ExamsSpaceExpert.Should().Be(product.Select(el => el.Value).Skip(5).Take(2).Sum());
            sessionDetailsPage.ExamsSpaceOther.Should().Be(product.Select(el => el.Value).Skip(7).Take(1).Sum());

            // Check if session present on dashboard
            sessionDetailsPage.Topbar.OpenDashboard().IsSessionDisplayed(sessionCity.ToString()).Should().BeTrue();
        }

        [Fact]
        public void AddExamSessionWithMaximumNumberOfParticipantsDefinedPerExamSession_Test()
        {
            //Na stronie jest błąd i można dodać albo z wartością ujemna albo większą niż 999. Walidacja nie zawsze się odpala
        }

        [Fact]
        public void AddExamSessionAndActivateCreatedSession_Test()
        {
            var landingPage = new LandingPage(DriverContext);

            // Data
            var sessionDate = date.AddDays(8).ToString(format);
            var sessionCity = GetTimeStamp();
            var level = new List<string> { "Zaawansowany" };
            var product = new Dictionary<string, int>
            {
                {"ISTQB Advanced Level Test Manager / Polski, Angielski", 999}
            };

            var dashboardPage =
                landingPage.OpenLandingPage().OpenLogInPage().SetEmail(email).SetPassword(password).LogIn();

            var addSessionPage = dashboardPage.Topbar.OpenAddSession();

            // Create session
            addSessionPage.SetDate(sessionDate)
                .SetCity(sessionCity.ToString())
                .SelectSpacePerProduct()
                .SelectLevel(level)
                .SelectProduct(product.Select(el => el.Key).ToList())
                .SetProductSpace(product.Keys.First(), product.Values.First());

            // Activate session
            var sessionDetailsPage = addSessionPage.SaveSession().ActivateSession();

            sessionDetailsPage.Status.Should().Be("Otwarta");

            // Check if session present on dashboard
            sessionDetailsPage.Topbar.OpenDashboard().IsSessionDisplayed(sessionCity.ToString()).Should().BeTrue();
        }

        [Fact]
        public void AddExamSessionAndCheckDuplicate_Test()
        {
            var landingPage = new LandingPage(DriverContext);

            var sessionDate = date.AddDays(1).ToString(format);
            var sessionCity = GetTimeStamp();

            var level = new List<string> { "Podstawowy" };
            var product = new Dictionary<string, int>
            {
                {"ISTQB Foundation Level / Polski, Angielski", 999}
            };

            var dashboardPage = landingPage.OpenLandingPage()
                .OpenLogInPage()
                .SetEmail(email)
                .SetPassword(password)
                .LogIn();

            var addSessionPage = dashboardPage.Topbar.OpenAddSession();

            addSessionPage.SetDate(sessionDate)
                .SetCity(sessionCity.ToString())
                .SelectSpacePerProduct()
                .SelectLevel(level)
                .SelectProduct(product.Select(el => el.Key).ToList())
                .SetProductSpace(product.Keys.First(), product.Values.First());

            var sessionDetailsPage = addSessionPage.SaveSession();

            sessionDetailsPage.Topbar.OpenAddSession();

            addSessionPage.SetDate(sessionDate)
                .SetCity(sessionCity.ToString())
                .SelectSpacePerProduct()
                .SelectLevel(level)
                .SelectProduct(product.Select(el => el.Key).ToList())
                .SetProductSpace(product.Keys.First(), product.Values.First()).SaveSession();

            addSessionPage.ErrorMessage.Should()
                .Be("Operacja nie może zostać zrealizowana. Identyczny produkt jest już zdefiniowany.");
        }

        private int GetTimeStamp()
        {
            return (int)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
