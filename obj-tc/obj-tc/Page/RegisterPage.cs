using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Types;
using Objectivity.Test.Automation.Tests.PageObjects;

namespace obj_tc.Page
{
    public class RegisterPage : ProjectPageBase
    {
        private readonly ElementLocator goForwardButton = new ElementLocator(Locator.CssSelector, ".Register-forwardBtn");
        // Basic data
        private readonly ElementLocator langaugeRadio = new ElementLocator(Locator.XPath, "//label[text() = '{0}']");
        private readonly ElementLocator productFormRadio = new ElementLocator(Locator.XPath, "//label[text() = '{0}']");
        private readonly ElementLocator certificateNumberInput = new ElementLocator(Locator.Id, "ProductSelectionDto_CertificateNumber");
        private readonly ElementLocator certificateDateInput = new ElementLocator(Locator.Id, "ProductSelectionDto_CertificateDateOfIssue");
        private readonly ElementLocator certificateProviderInput = new ElementLocator(Locator.Id, "ProductSelectionDto_CertificateProvider");
        // Contact data
        private readonly ElementLocator nameInput = new ElementLocator(Locator.Id, "PersonDataDto_Name");
        private readonly ElementLocator surnameInput = new ElementLocator(Locator.Id, "PersonDataDto_Surname");
        private readonly ElementLocator emailInput = new ElementLocator(Locator.Id, "PersonDataDto_Email");
        private readonly ElementLocator phoneInput = new ElementLocator(Locator.Id, "PersonDataDto_Phone");
        // Address data
        private readonly ElementLocator addressNameInput = new ElementLocator(Locator.Id, "AddressDto_Name");
        private readonly ElementLocator addressSurnameInput = new ElementLocator(Locator.Id, "AddressDto_Surname");
        private readonly ElementLocator addressPostCodeInput = new ElementLocator(Locator.Id, "AddressDto_PostalCode");
        private readonly ElementLocator addressCityInput = new ElementLocator(Locator.Id, "AddressDto_City");
        private readonly ElementLocator addressAddressInput = new ElementLocator(Locator.Id, "AddressDto_Address");
        private readonly ElementLocator addressCommentInput = new ElementLocator(Locator.Id, "AddressDto_Comment");
        private readonly ElementLocator noInvoiceRadio = new ElementLocator(Locator.Id, "label[for='AddressDto_InvoiceTypesNone']");
        private readonly ElementLocator electronicInvoiceRadio = new ElementLocator(Locator.Id, "label[for='AddressDto_InvoiceTypesElectronic']");
        private readonly ElementLocator paperInvoiceRadio = new ElementLocator(Locator.Id, "label[for='AddressDto_InvoiceTypesPaper']");
        private readonly ElementLocator acceptPrivacyPolicyRadio = new ElementLocator(Locator.Id, "label[for='AddressDto_AcceptedPrivacyPolicy']");
        private readonly ElementLocator acceptMarketingPolicyRadio = new ElementLocator(Locator.Id, "label[for='AddressDto_AcceptedMarketingPolicy']");

        public RegisterPage(DriverContext driverContext) : base(driverContext)
        {
        }
    }
}
