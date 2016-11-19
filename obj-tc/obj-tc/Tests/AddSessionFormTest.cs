﻿using System;
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

            addSessionPage.SetDate(examDate);

            result = addSessionPage.SaveSessionReturnAddSession();

            result.productValidationText.Should().Be("Musisz wybrać co najmniej jeden produkt");

            addSessionPage.SelectLevel(level)
                .SelectProduct(product.Select(el => el.Key).ToList());

            var sessionDetailsPage = addSessionPage.SaveSession();

            sessionDetailsPage.Date.Should().Be(examDate.Split(' ')[0]);
            sessionDetailsPage.Time.Should().Be(examDate.Split(' ')[1]);
            sessionDetailsPage.City.Should().Be(city);

            sessionDetailsPage.SwitchToExams();
            sessionDetailsPage.ExamList.ShouldAllBeEquivalentTo(expectedProduct);     

        }

        private int GetTimeStamp()
        {
            return (int)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
