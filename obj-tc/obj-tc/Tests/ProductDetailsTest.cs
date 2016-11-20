using FluentAssertions;
using obj_tc.Page;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Tests.Xunit;
using System;
using System.Collections.Generic;
using Xunit;

namespace obj_tc.Tests
{
    public class ProductDetailsTest : ProjectTestBase
    {
        private readonly string email = BaseConfiguration.Username;
        private readonly string password = BaseConfiguration.Password;
        private DateTime date = DateTime.Now;
        private const string format = "dd.MM.yyyy HH:mm";

        [Fact]
        public void CheckProductDetails()
        {
            var landingPage = new LandingPage(DriverContext);

            var product = "ISTQB Advanced Level Technical Test Analyst";
            var certProvider = "SJSI";
            var expectedProductDetails = new List<string>
            {
                "08.11.2016",
                "09.11.2016 -",
                "240 min",
                "Tak",
                "Zaawansowany",
                "Polski, Angielski",
                "papierowa, elektroniczna",
                "Otwarta, Grupowa"
            };

            var productListPage =
                landingPage.OpenLandingPage().OpenLogInPage().SetEmail(email).SetPassword(password)
                .LogIn().Topbar.OpenProducts();

            var productDetailsPage = productListPage.OpenProductDetails(product);

            // Check details
            product.Should().Be(productDetailsPage.Title);
            expectedProductDetails.ShouldAllBeEquivalentTo(productDetailsPage.ProductDetails);
            certProvider.Should().Be(productDetailsPage.CertificateProvider);
        }
    }
}
