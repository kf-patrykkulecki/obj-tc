using System.Configuration;
using obj_tc.Extensions;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Extensions;
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
        private readonly ElementLocator certNameInput = new ElementLocator(Locator.Id, "AddressDto_Name");
        private readonly ElementLocator certSurnameInput = new ElementLocator(Locator.Id, "AddressDto_Surname");
        private readonly ElementLocator certPostCodeInput = new ElementLocator(Locator.Id, "AddressDto_PostalCode");
        private readonly ElementLocator certCityInput = new ElementLocator(Locator.Id, "AddressDto_City");
        private readonly ElementLocator certAddressInput = new ElementLocator(Locator.Id, "AddressDto_Address");
        private readonly ElementLocator certCommentInput = new ElementLocator(Locator.Id, "AddressDto_Comment");
        private readonly ElementLocator noInvoiceRadio = new ElementLocator(Locator.CssSelector, "label[for='AddressDto_InvoiceTypesNone']");
        private readonly ElementLocator electronicInvoiceRadio = new ElementLocator(Locator.CssSelector, "label[for='AddressDto_InvoiceTypesElectronic']");
        private readonly ElementLocator paperInvoiceRadio = new ElementLocator(Locator.CssSelector, "label[for='AddressDto_InvoiceTypesPaper']");
        private readonly ElementLocator acceptPrivacyPolicyRadio = new ElementLocator(Locator.CssSelector, "label[for='AddressDto_AcceptedPrivacyPolicy']");
        private readonly ElementLocator acceptMarketingPolicyRadio = new ElementLocator(Locator.CssSelector, "label[for='AddressDto_AcceptedMarketingPolicy']");
        // Complete
        private readonly ElementLocator examName = new ElementLocator(Locator.CssSelector, ".RegisterComplete-products .u-isRegular");
        private readonly ElementLocator contactEmail = new ElementLocator(Locator.Id, "contact-email");
        private readonly ElementLocator thankyouMessage = new ElementLocator(Locator.CssSelector, ".space3");

        public RegisterPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public string SuccessExamName => this.Driver.GetElement(examName).Text;

        public string SuccessCntactEmail => this.Driver.GetElement(contactEmail).Text;

        public string SuccessThankyouMessage => this.Driver.GetElement(thankyouMessage).Text;


        public RegisterPage GoForward()
        {
            this.Driver.Click(goForwardButton);
            this.Driver.WaitForAjax();
            return this;
        }

        public RegisterPage SelectLanguage(string text)
        {
            this.Driver.Click(langaugeRadio.Format(text));
            return this;
        }

        public RegisterPage SelectForm(string text)
        {
            this.Driver.Click(productFormRadio.Format(text));
            return this;
        }

        public RegisterPage SetCertificateNumber(string text)
        {
            this.Driver.SendKeys(certificateNumberInput, text);
            return this;
        }

        public RegisterPage SetCertificateDate(string text)
        {
            this.Driver.SendKeys(certificateDateInput, text);
            return this;
        }
        public RegisterPage SetCertificateProvider(string text)
        {
            this.Driver.SendKeys(certificateProviderInput, text);
            return this;
        }

        public RegisterPage SetName(string text)
        {
            this.Driver.SendKeys(nameInput, text);
            return this;
        }
        public RegisterPage SetSurname(string text)
        {
            this.Driver.SendKeys(surnameInput, text);
            return this;
        }
        public RegisterPage SetEmail(string text)
        {
            this.Driver.SendKeys(emailInput, text);
            return this;
        }
        public RegisterPage SetPhone(string text)
        {
            this.Driver.SendKeys(phoneInput, text);
            return this;
        }

        public RegisterPage SetCertificateName(string text)
        {
            this.Driver.SendKeys(certNameInput, text);
            return this;
        }

        public RegisterPage SetCertificateSurname(string text)
        {
            this.Driver.SendKeys(certSurnameInput, text);
            return this;
        }

        public RegisterPage SetCertificatePostCode(string text)
        {
            this.Driver.SendKeys(certPostCodeInput, text);
            return this;
        }

        public RegisterPage SetCertificateCity(string text)
        {
            this.Driver.SendKeys(certCityInput, text);
            return this;
        }

        public RegisterPage SetCertificateAddres(string text)
        {
            this.Driver.SendKeys(certAddressInput, text);
            return this;
        }

        public RegisterPage SetCertificateComment(string text)
        {
            this.Driver.SendKeys(certCommentInput, text);
            return this;
        }

        public RegisterPage SelectNoInvoice()
        {
            this.Driver.Click(noInvoiceRadio);
            return this;
        }

        public RegisterPage SelectElectronicInvoice()
        {
            this.Driver.Click(electronicInvoiceRadio);
            return this;
        }

        public RegisterPage SelectPaperInvoice()
        {
            this.Driver.Click(paperInvoiceRadio);
            return this;
        }

        public RegisterPage SelectAcceptPrivacyPolicy()
        {
            this.Driver.Click(acceptPrivacyPolicyRadio);
            return this;
        }

        public RegisterPage SelectAcceptMarketingPolicy()
        {
            this.Driver.Click(acceptMarketingPolicyRadio);
            return this;
        }
    }
}
