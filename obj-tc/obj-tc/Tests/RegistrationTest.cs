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
    public class RegistrationTest : ProjectTestBase
    {
        private readonly string email = BaseConfiguration.Username;
        private readonly string password = BaseConfiguration.Password;
        private DateTime date = DateTime.Now;
        private const string format = "dd.MM.yyyy HH:mm";

        [Fact]
        public void RegisterToExamInOrderToParticipateInIt_Test()
        {
            var landingPage = new LandingPage(DriverContext);

            // Prepare exam session 
            var examiner = "Objectivity 1 Test";
            var sessionDate = date.AddDays(1).ToString(format);
            var sessionCity = GetTimeStamp();
            var sessionPostCode = "54-429";
            var sessionAddress = StringExtensions.GenerateMaxAlphanumericString(50);
            var sessionAdditionalInformation = StringExtensions.GenerateMaxAlphanumericString(500);
            var level = new List<string> { "Ekspercki" };
            var product = new Dictionary<string, int>
            {
                {"ISTQB Improving the Testing Process / Angielski", 1}
            };
            var certificateNumber = StringExtensions.GenerateMaxAlphanumericString(20);
            var certificateDate = this.date.ToString(format).Split(' ')[0];
            var certificateProvider = StringExtensions.GenerateMaxAlphanumericString(20);
            var contactUserName = StringExtensions.GenerateMaxAlphanumericString(50);
            var contactUserSurname = StringExtensions.GenerateMaxAlphanumericString(50);
            var contactUserEmail = "test@test.pl";
            var contactUserPostCode = "54-420";
            var contactUserCity = StringExtensions.GenerateMaxAlphanumericString(50);
            var contactUserAddress = StringExtensions.GenerateMaxAlphanumericString(50);

            var dashboardPage =
                landingPage.OpenLandingPage().OpenLogInPage().SetEmail(email).SetPassword(password).LogIn();

            var addSessionPage = dashboardPage.Topbar.OpenAddSession();

            // Create session
            addSessionPage.SetDate(sessionDate)
                .SetPostCode(sessionPostCode)
                .SetCity(sessionCity.ToString())
                .SetAddress(sessionAddress)
                .SetAdditionalInformation(sessionAdditionalInformation)
                .SetSpacePerSession(product.Select(el => el.Value).First().ToString())
                .SelectLevel(level)
                .SelectProduct(product.Select(el => el.Key).ToList())
                .SelectExaminer(examiner);

            // Activate session
            var sessionDetailsPage = addSessionPage.SaveSession().ActivateSession();

            sessionDetailsPage.Status.Should().Be("Otwarta - potwierdzony");

            // Check if session present on dashboard
            sessionDetailsPage.Topbar.OpenDashboard().IsSessionDisplayed(sessionCity.ToString()).Should().BeTrue();

            // Register to session
            var registerPage = dashboardPage.Register().RegisterToSession(sessionCity.ToString());

            registerPage.SelectLanguage("Angielski")
                .SelectForm("papierowa")
                .SetCertificateNumber(certificateNumber)
                .SetCertificateDate(certificateDate)
                .SetCertificateProvider(certificateProvider)
                .GoForward()
                .SetName(contactUserName)
                .SetSurname(contactUserSurname)
                .SetEmail(contactUserEmail)
                .GoForward()
                .SetCertificateName(contactUserName)
                .SetCertificateSurname(contactUserSurname)
                .SetCertificatePostCode(contactUserPostCode)
                .SetCertificateCity(contactUserCity)
                .SetCertificateAddres(contactUserAddress)
                .SelectNoInvoice()
                .SelectAcceptPrivacyPolicy()
                .GoForward();

            registerPage.SuccessExamName.Should().Be(product.Select(el => el.Key).First().Split('/')[0].Trim());
            registerPage.SuccessCntactEmail.Should().Be(contactUserEmail);
            registerPage.SuccessThankyouMessage.Should().Be("Dziękujemy za zapisanie się na egzamin");

            //TODO improve assertions
        }

        [Fact]
        public void RegisterToExamWhichIsFull_Test()
        {
            var landingPage = new LandingPage(DriverContext);

            // Prepare exam session 
            var examiner = "Objectivity 1 Test";
            var sessionDate = date.AddDays(1).ToString(format);
            var sessionCity = GetTimeStamp();
            var sessionPostCode = "54-429";
            var sessionAddress = StringExtensions.GenerateMaxAlphanumericString(50);
            var sessionAdditionalInformation = StringExtensions.GenerateMaxAlphanumericString(500);
            var level = new List<string> { "Ekspercki" };
            var product = new Dictionary<string, int>
            {
                {"ISTQB Improving the Testing Process / Angielski", 1}
            };
            var certificateNumber = StringExtensions.GenerateMaxAlphanumericString(20);
            var certificateDate = this.date.ToString(format).Split(' ')[0];
            var certificateProvider = StringExtensions.GenerateMaxAlphanumericString(20);
            var contactUserName = StringExtensions.GenerateMaxAlphanumericString(50);
            var contactUserSurname = StringExtensions.GenerateMaxAlphanumericString(50);
            var contactUserEmail = "test@test.pl";
            var contactUserPostCode = "54-420";
            var contactUserCity = StringExtensions.GenerateMaxAlphanumericString(50);
            var contactUserAddress = StringExtensions.GenerateMaxAlphanumericString(50);

            var dashboardPage =
                landingPage.OpenLandingPage().OpenLogInPage().SetEmail(email).SetPassword(password).LogIn();

            var addSessionPage = dashboardPage.Topbar.OpenAddSession();

            // Create session
            addSessionPage.SetDate(sessionDate)
                .SetPostCode(sessionPostCode)
                .SetCity(sessionCity.ToString())
                .SetAddress(sessionAddress)
                .SetAdditionalInformation(sessionAdditionalInformation)
                .SetSpacePerSession(product.Select(el => el.Value).First().ToString())
                .SelectLevel(level)
                .SelectProduct(product.Select(el => el.Key).ToList())
                .SelectExaminer(examiner);

            // Activate session
            var sessionDetailsPage = addSessionPage.SaveSession().ActivateSession();

            sessionDetailsPage.Status.Should().Be("Otwarta - potwierdzony");

            // Check if session present on dashboard
            sessionDetailsPage.Topbar.OpenDashboard().IsSessionDisplayed(sessionCity.ToString()).Should().BeTrue();

            // Register to session
            var registerPage = dashboardPage.Register().RegisterToSession(sessionCity.ToString());

            registerPage.SelectLanguage("Angielski")
                .SelectForm("papierowa")
                .SetCertificateNumber(certificateNumber)
                .SetCertificateDate(certificateDate)
                .SetCertificateProvider(certificateProvider)
                .GoForward()
                .SetName(contactUserName)
                .SetSurname(contactUserSurname)
                .SetEmail(contactUserEmail)
                .GoForward()
                .SetCertificateName(contactUserName)
                .SetCertificateSurname(contactUserSurname)
                .SetCertificatePostCode(contactUserPostCode)
                .SetCertificateCity(contactUserCity)
                .SetCertificateAddres(contactUserAddress)
                .SelectNoInvoice()
                .SelectAcceptPrivacyPolicy()
                .GoForward();

            registerPage.SuccessExamName.Should().Be(product.Select(el => el.Key).First().Split('/')[0].Trim());
            registerPage.SuccessCntactEmail.Should().Be(contactUserEmail);
            registerPage.SuccessThankyouMessage.Should().Be("Dziękujemy za zapisanie się na egzamin");

            Assert.Equal("Rejestracja nieaktywna",dashboardPage.Register().GetRegisterButtonText(sessionCity.ToString()));
           
        }

        [Fact]
        public void GetHoursTest() {
            var landingPage = new LandingPage(DriverContext);

            // Prepare exam session 
            var examiner = "Objectivity 1 Test";
            var sessionDate = "22.11.2016 12:00";
            var sessionCity = GetTimeStamp();
            var sessionPostCode = "54-429";
            var sessionAddress = StringExtensions.GenerateMaxAlphanumericString(50);
            var sessionAdditionalInformation = StringExtensions.GenerateMaxAlphanumericString(500);
            var level = new List<string> { "Ekspercki" };
            var product = new Dictionary<string, int>
            {
                {"ISTQB Improving the Testing Process / Angielski", 1}
            };
            var certificateNumber = StringExtensions.GenerateMaxAlphanumericString(20);
            var certificateDate = this.date.ToString(format).Split(' ')[0];
            var certificateProvider = StringExtensions.GenerateMaxAlphanumericString(20);
            var contactUserName = StringExtensions.GenerateMaxAlphanumericString(50);
            var contactUserSurname = StringExtensions.GenerateMaxAlphanumericString(50);
            var contactUserEmail = "test@test.pl";
            var contactUserPostCode = "54-420";
            var contactUserCity = StringExtensions.GenerateMaxAlphanumericString(50);
            var contactUserAddress = StringExtensions.GenerateMaxAlphanumericString(50);

            var dashboardPage =
                landingPage.OpenLandingPage().OpenLogInPage().SetEmail(email).SetPassword(password).LogIn();

            var addSessionPage = dashboardPage.Topbar.OpenAddSession();

            // Create session
            addSessionPage.SetDate(sessionDate)
                .SetPostCode(sessionPostCode)
                .SetCity(sessionCity.ToString())
                .SetAddress(sessionAddress)
                .SetAdditionalInformation(sessionAdditionalInformation)
                .SetSpacePerSession(product.Select(el => el.Value).First().ToString())
                .SelectLevel(level)
                .SelectProduct(product.Select(el => el.Key).ToList())
                .SelectExaminer(examiner);

            // Activate session
            var sessionDetailsPage = addSessionPage.SaveSession().ActivateSession();

            sessionDetailsPage.Status.Should().Be("Otwarta - potwierdzony");

            var examiner1 = "Objectivity 1 Test";
            var sessionDate1 = "23.11.2016 12:01";
            var sessionPostCode1 = "54-429";
            var sessionAddress1 = StringExtensions.GenerateMaxAlphanumericString(50);
            var sessionAdditionalInformation1 = StringExtensions.GenerateMaxAlphanumericString(500);
            var level1 = new List<string> { "Ekspercki" };
            var product1 = new Dictionary<string, int>
            {
                {"ISTQB Improving the Testing Process / Angielski", 1}
            };
            var certificateNumber1 = StringExtensions.GenerateMaxAlphanumericString(20);
            var certificateDate1 = this.date.ToString(format).Split(' ')[0];
            var certificateProvider1 = StringExtensions.GenerateMaxAlphanumericString(20);
            var contactUserName1 = StringExtensions.GenerateMaxAlphanumericString(50);
            var contactUserSurname1 = StringExtensions.GenerateMaxAlphanumericString(50);
            var contactUserEmail1 = "test@test.pl";
            var contactUserPostCode1 = "54-420";
            var contactUserCity1 = StringExtensions.GenerateMaxAlphanumericString(50);
            var contactUserAddress1 = StringExtensions.GenerateMaxAlphanumericString(50);

            addSessionPage = dashboardPage.Topbar.OpenAddSession();

            // Create session
            addSessionPage.SetDate(sessionDate1)
                .SetPostCode(sessionPostCode1)
                .SetCity(sessionCity.ToString())
                .SetAddress(sessionAddress1)
                .SetAdditionalInformation(sessionAdditionalInformation1)
                .SetSpacePerSession(product1.Select(el => el.Value).First().ToString())
                .SelectLevel(level1)
                .SelectProduct(product1.Select(el => el.Key).ToList())
                .SelectExaminer(examiner1);

            // Activate session
            sessionDetailsPage = addSessionPage.SaveSession().ActivateSession();

            sessionDetailsPage.Status.Should().Be("Otwarta - potwierdzony");

            // Check if session present on dashboard
            sessionDetailsPage.Topbar.ClickLogo();

            var test = dashboardPage.GetExamList("23 listopada 2016", sessionCity.ToString(),"12:01");

            Assert.Equal("ISTQB Improving the Testing Process", test[0]);
        }

        private int GetTimeStamp()
        {
            return (int)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}