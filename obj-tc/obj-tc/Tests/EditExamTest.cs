using FluentAssertions;
using obj_tc.Extensions;
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

            sessionDetailsPage.EditSession();

            addSessionPage.SetSpacePerSession(200.ToString()).SaveSession();

            // Check session details
            sessionDetailsPage.Date.Should().Be(sessionDate.Split(' ')[0]);
            sessionDetailsPage.Time.Should().Be(sessionDate.Split(' ')[1]);
            sessionDetailsPage.Space.Should().Be(200);
            sessionDetailsPage.Examiner.Should().BeEmpty();
            sessionDetailsPage.PostCode.Should().Be("-");
            sessionDetailsPage.City.Should().Be(sessionCity.ToString());
            sessionDetailsPage.Address.Should().Be("-");
            sessionDetailsPage.AdditionalInformation.Should().Be("-");
            sessionDetailsPage.Status.Should().Be("Nowy");

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

            sessionDetailsPage.EditSession();

            addSessionPage.SelectSpacePerSession().SetSpacePerSession(100.ToString()).SaveSession();

            // Check session details
            sessionDetailsPage.Date.Should().Be(sessionDate.Split(' ')[0]);
            sessionDetailsPage.Time.Should().Be(sessionDate.Split(' ')[1]);
            sessionDetailsPage.Space.Should().Be(100);
            sessionDetailsPage.Examiner.Should().BeEmpty();
            sessionDetailsPage.PostCode.Should().Be("-");
            sessionDetailsPage.City.Should().Be(sessionCity.ToString());
            sessionDetailsPage.Address.Should().Be("-");
            sessionDetailsPage.AdditionalInformation.Should().Be("-");
            sessionDetailsPage.Status.Should().Be("Nowy");

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

        [Fact]
        public void EditExamSessionInOrderAndChangeLocationOfTheExam_Test()
        {
            var landingPage = new LandingPage(DriverContext);

            // Data
            var sessionDate = date.AddDays(2).ToString(format);
            var sessionCity = StringExtensions.GenerateMaxAlphanumericString(50);
            var sessionPostCode = "54-429";
            var sessionAddress = StringExtensions.GenerateMaxAlphanumericString(50);
            var sessionAdditionalInformation = StringExtensions.GenerateMaxAlphanumericString(500);
            var examiner = "Objectivity 1 Test";
            var level = new List<string> { "Ekspercki" };
            var product = new Dictionary<string, int>
            {
                {"ISTQB Improving the Testing Process / Angielski", 999}
            };
            var expectedProduct = new List<string>
            {
                "ISTQB Improving the Testing Process/Angielski"
            };

            var dashboardPage = landingPage.OpenLandingPage()
                .OpenLogInPage()
                .SetEmail(email)
                .SetPassword(password)
                .LogIn();

            var addSessionPage = dashboardPage.Topbar.OpenAddSession();

            // Create session
            addSessionPage.SetDate(sessionDate)
                .SetPostCode(sessionPostCode)
                .SetCity(sessionCity)
                .SetAddress(sessionAddress)
                .SetAdditionalInformation(sessionAdditionalInformation)
                .SelectSpacePerProduct()
                .SelectLevel(level)
                .SelectProduct(product.Select(el => el.Key).ToList())
                .SetProductSpace(product.Keys.First(), product.Values.First())
                .SelectExaminer(examiner);

            var sessionDetailsPage = addSessionPage.SaveSession();

            // Check session details
            sessionDetailsPage.Date.Should().Be(sessionDate.Split(' ')[0]);
            sessionDetailsPage.Time.Should().Be(sessionDate.Split(' ')[1]);
            sessionDetailsPage.Space.Should().Be(product.Select(el => el.Value).Sum());
            sessionDetailsPage.Examiner.Should().Be(examiner);
            sessionDetailsPage.PostCode.Should().Be(sessionPostCode);
            sessionDetailsPage.City.Should().Be(sessionCity);
            sessionDetailsPage.Address.Should().Be(sessionAddress);
            sessionDetailsPage.AdditionalInformation.Should().Be(sessionAdditionalInformation);
            sessionDetailsPage.Status.Should().Be("Nowy");

            sessionDetailsPage.SwitchToExams();
            sessionDetailsPage.ExamList.ShouldAllBeEquivalentTo(expectedProduct);
            sessionDetailsPage.ExamsSpaceExpert.Should().Be(product.Select(el => el.Value).Take(1).Sum());

            sessionDetailsPage.EditSession();

            var updatedCity = StringExtensions.GenerateMaxAlphanumericString(50);
            var updatedAddress = StringExtensions.GenerateMaxAlphanumericString(50);
            var updatedPostCode = "00-010";

            addSessionPage.SetCity(updatedCity)
                .SetAddress(updatedAddress)
                .SetPostCode(updatedPostCode)
                .SaveSession();

            // Check session details
            sessionDetailsPage.Date.Should().Be(sessionDate.Split(' ')[0]);
            sessionDetailsPage.Time.Should().Be(sessionDate.Split(' ')[1]);
            sessionDetailsPage.Space.Should().Be(product.Select(el => el.Value).Sum());
            sessionDetailsPage.Examiner.Should().Be(examiner);
            sessionDetailsPage.PostCode.Should().Be(updatedPostCode);
            sessionDetailsPage.City.Should().Be(updatedCity);
            sessionDetailsPage.Address.Should().Be(updatedAddress);
            sessionDetailsPage.AdditionalInformation.Should().Be(sessionAdditionalInformation);
            sessionDetailsPage.Status.Should().Be("Nowy");

            // Check if session present on dashboard
            sessionDetailsPage.Topbar.OpenDashboard().IsSessionDisplayed(updatedCity.ToString()).Should().BeTrue();
        }

        [Fact]
        public void EditExamSessionInOrderAndChangeExaminer_Test()
        {
            var landingPage = new LandingPage(DriverContext);

            // Data
            var sessionDate = date.AddDays(2).ToString(format);
            var sessionCity = StringExtensions.GenerateMaxAlphanumericString(50);
            var sessionPostCode = "54-429";
            var sessionAddress = StringExtensions.GenerateMaxAlphanumericString(50);
            var sessionAdditionalInformation = StringExtensions.GenerateMaxAlphanumericString(500);
            var examiner = "Objectivity 1 Test";
            var level = new List<string> { "Ekspercki" };
            var product = new Dictionary<string, int>
            {
                {"ISTQB Improving the Testing Process / Angielski", 999}
            };
            var expectedProduct = new List<string>
            {
                "ISTQB Improving the Testing Process/Angielski"
            };

            var dashboardPage = landingPage.OpenLandingPage()
                .OpenLogInPage()
                .SetEmail(email)
                .SetPassword(password)
                .LogIn();

            var addSessionPage = dashboardPage.Topbar.OpenAddSession();

            // Create session
            addSessionPage.SetDate(sessionDate)
                .SetPostCode(sessionPostCode)
                .SetCity(sessionCity)
                .SetAddress(sessionAddress)
                .SetAdditionalInformation(sessionAdditionalInformation)
                .SelectSpacePerProduct()
                .SelectLevel(level)
                .SelectProduct(product.Select(el => el.Key).ToList())
                .SetProductSpace(product.Keys.First(), product.Values.First())
                .SelectExaminer(examiner);

            var sessionDetailsPage = addSessionPage.SaveSession();

            // Check session details
            sessionDetailsPage.Date.Should().Be(sessionDate.Split(' ')[0]);
            sessionDetailsPage.Time.Should().Be(sessionDate.Split(' ')[1]);
            sessionDetailsPage.Space.Should().Be(product.Select(el => el.Value).Sum());
            sessionDetailsPage.Examiner.Should().Be(examiner);
            sessionDetailsPage.PostCode.Should().Be(sessionPostCode);
            sessionDetailsPage.City.Should().Be(sessionCity);
            sessionDetailsPage.Address.Should().Be(sessionAddress);
            sessionDetailsPage.AdditionalInformation.Should().Be(sessionAdditionalInformation);
            sessionDetailsPage.Status.Should().Be("Nowy");

            sessionDetailsPage.SwitchToExams();
            sessionDetailsPage.ExamList.ShouldAllBeEquivalentTo(expectedProduct);
            sessionDetailsPage.ExamsSpaceExpert.Should().Be(product.Select(el => el.Value).Take(1).Sum());

            sessionDetailsPage.EditSession();

            var updatedExaminer = "Objectivity 2 Test";

            addSessionPage.SelectExaminer(updatedExaminer).SaveSession();

            // Check session details
            sessionDetailsPage.Date.Should().Be(sessionDate.Split(' ')[0]);
            sessionDetailsPage.Time.Should().Be(sessionDate.Split(' ')[1]);
            sessionDetailsPage.Space.Should().Be(product.Select(el => el.Value).Sum());
            sessionDetailsPage.Examiner.Should().Be(updatedExaminer);
            sessionDetailsPage.PostCode.Should().Be(sessionPostCode);
            sessionDetailsPage.City.Should().Be(sessionCity);
            sessionDetailsPage.Address.Should().Be(sessionAddress);
            sessionDetailsPage.AdditionalInformation.Should().Be(sessionAdditionalInformation);
            sessionDetailsPage.Status.Should().Be("Nowy");
        }

        [Fact]
        public void EditExamSessionInOrderToAddProduct_Test()
        {
            var landingPage = new LandingPage(DriverContext);

            // Data
            var sessionDate = date.AddDays(2).ToString(format);
            var sessionCity = StringExtensions.GenerateMaxAlphanumericString(50);
            var sessionPostCode = "54-429";
            var sessionAddress = StringExtensions.GenerateMaxAlphanumericString(50);
            var sessionAdditionalInformation = StringExtensions.GenerateMaxAlphanumericString(500);
            var examiner = "Objectivity 1 Test";
            var level = new List<string> { "Ekspercki" };
            var product = new Dictionary<string, int>
            {
                {"ISTQB Improving the Testing Process / Angielski", 999}
            };
            var expectedProduct = new List<string>
            {
                "ISTQB Improving the Testing Process/Angielski"
            };

            var dashboardPage = landingPage.OpenLandingPage()
                .OpenLogInPage()
                .SetEmail(email)
                .SetPassword(password)
                .LogIn();

            var addSessionPage = dashboardPage.Topbar.OpenAddSession();

            // Create session
            addSessionPage.SetDate(sessionDate)
                .SetPostCode(sessionPostCode)
                .SetCity(sessionCity)
                .SetAddress(sessionAddress)
                .SetAdditionalInformation(sessionAdditionalInformation)
                .SelectSpacePerProduct()
                .SelectLevel(level)
                .SelectProduct(product.Select(el => el.Key).ToList())
                .SetProductSpace(product.Keys.First(), product.Values.First())
                .SelectExaminer(examiner);

            var sessionDetailsPage = addSessionPage.SaveSession();

            sessionDetailsPage.EditSession();

            var updateLevel = new List<string> { "Podstawowy" };
            var updateProduct = new Dictionary<string, int>
            {
                {"ISTQB Foundation Level / Polski, Angielski", 999}
            };
            var updateExpectedProduct = new List<string>
            {
                "ISTQB Foundation Level/Polski, Angielski"
            };

            addSessionPage.SelectLevel(updateLevel)
                .SelectProduct(updateProduct.Select(el => el.Key).ToList())
                .SetProductSpace(updateProduct.Keys.First(), updateProduct.Values.First())
                .SaveSession();

            // Check session details
            sessionDetailsPage.Date.Should().Be(sessionDate.Split(' ')[0]);
            sessionDetailsPage.Time.Should().Be(sessionDate.Split(' ')[1]);
            sessionDetailsPage.Space.Should().Be(product.Select(el => el.Value).Sum() + updateProduct.Select(el => el.Value).Sum());
            sessionDetailsPage.Examiner.Should().Be(examiner);
            sessionDetailsPage.PostCode.Should().Be(sessionPostCode);
            sessionDetailsPage.City.Should().Be(sessionCity);
            sessionDetailsPage.Address.Should().Be(sessionAddress);
            sessionDetailsPage.AdditionalInformation.Should().Be(sessionAdditionalInformation);
            sessionDetailsPage.Status.Should().Be("Nowy");

            sessionDetailsPage.SwitchToExams();
            sessionDetailsPage.ExamList.ShouldAllBeEquivalentTo(updateExpectedProduct.Concat(expectedProduct));
            sessionDetailsPage.ExamsSpaceBasic.Should().Be(updateProduct.Select(el => el.Value).Sum());
            sessionDetailsPage.ExamsSpaceExpert.Should().Be(product.Select(el => el.Value).Sum());
        }

        [Fact]
        public void EditExamSessionInOrderToChangeProduct_Test()
        {
            var landingPage = new LandingPage(DriverContext);

            // Data
            var sessionDate = date.AddDays(2).ToString(format);
            var sessionCity = StringExtensions.GenerateMaxAlphanumericString(50);
            var sessionPostCode = "54-429";
            var sessionAddress = StringExtensions.GenerateMaxAlphanumericString(50);
            var sessionAdditionalInformation = StringExtensions.GenerateMaxAlphanumericString(500);
            var examiner = "Objectivity 1 Test";
            var level = new List<string> { "Ekspercki" };
            var product = new Dictionary<string, int>
            {
                {"ISTQB Improving the Testing Process / Angielski", 999}
            };
            var expectedProduct = new List<string>
            {
                "ISTQB Improving the Testing Process/Angielski"
            };

            var dashboardPage = landingPage.OpenLandingPage()
                .OpenLogInPage()
                .SetEmail(email)
                .SetPassword(password)
                .LogIn();

            var addSessionPage = dashboardPage.Topbar.OpenAddSession();

            // Create session
            addSessionPage.SetDate(sessionDate)
                .SetPostCode(sessionPostCode)
                .SetCity(sessionCity)
                .SetAddress(sessionAddress)
                .SetAdditionalInformation(sessionAdditionalInformation)
                .SelectSpacePerProduct()
                .SelectLevel(level)
                .SelectProduct(product.Select(el => el.Key).ToList())
                .SetProductSpace(product.Keys.First(), product.Values.First())
                .SelectExaminer(examiner);

            var sessionDetailsPage = addSessionPage.SaveSession();

            sessionDetailsPage.EditSession();

            var updateLevel = new List<string> { "Podstawowy" };
            var updateProduct = new Dictionary<string, int>
            {
                {"ISTQB Foundation Level / Polski, Angielski", 999}
            };
            var updateExpectedProduct = new List<string>
            {
                "ISTQB Foundation Level/Polski, Angielski"
            };

            addSessionPage.UnSelectProduct(product.Select(el => el.Key).ToList())
                .UnSelectLevel(level)
                .SelectLevel(updateLevel)
                .SelectProduct(updateProduct.Select(el => el.Key).ToList())
                .SetProductSpace(updateProduct.Keys.First(), updateProduct.Values.First())
                .SaveSession();

            // Check session details
            sessionDetailsPage.Date.Should().Be(sessionDate.Split(' ')[0]);
            sessionDetailsPage.Time.Should().Be(sessionDate.Split(' ')[1]);
            sessionDetailsPage.Space.Should().Be(updateProduct.Select(el => el.Value).Sum());
            sessionDetailsPage.Examiner.Should().Be(examiner);
            sessionDetailsPage.PostCode.Should().Be(sessionPostCode);
            sessionDetailsPage.City.Should().Be(sessionCity);
            sessionDetailsPage.Address.Should().Be(sessionAddress);
            sessionDetailsPage.AdditionalInformation.Should().Be(sessionAdditionalInformation);
            sessionDetailsPage.Status.Should().Be("Nowy");

            sessionDetailsPage.SwitchToExams();
            sessionDetailsPage.ExamList.ShouldAllBeEquivalentTo(updateExpectedProduct);
            sessionDetailsPage.ExamsSpaceBasic.Should().Be(updateProduct.Select(el => el.Value).Sum());
        }

        [Fact]
        public void EditExamSessionInOrderToDeleteProduct_Test()
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
            // Check if session present on dashboard
            sessionDetailsPage.EditSession();

            addSessionPage.RemoveProduct(product.First().Key).SaveSession();

            // Check session details
            sessionDetailsPage.Date.Should().Be(sessionDate.Split(' ')[0]);
            sessionDetailsPage.Time.Should().Be(sessionDate.Split(' ')[1]);
            sessionDetailsPage.Space.Should().Be(product.Last().Value);
            sessionDetailsPage.Examiner.Should().BeEmpty();
            sessionDetailsPage.PostCode.Should().Be("-");
            sessionDetailsPage.City.Should().Be(sessionCity.ToString());
            sessionDetailsPage.Address.Should().Be("-");
            sessionDetailsPage.AdditionalInformation.Should().Be("-");
            sessionDetailsPage.Status.Should().Be("Nowy");

            sessionDetailsPage.SwitchToExams();
            sessionDetailsPage.ExamList.ShouldAllBeEquivalentTo(expectedProduct);
            sessionDetailsPage.ExamsSpaceBasic.Should().Be(999);
            sessionDetailsPage.ExamsSpaceExpert.Should().Be(999);
        }

        private int GetTimeStamp()
        {
            return (int)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
