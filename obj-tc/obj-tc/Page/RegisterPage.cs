using System.Configuration;
using obj_tc.Extensions;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Extensions;
using Objectivity.Test.Automation.Common.Types;
using Objectivity.Test.Automation.Tests.PageObjects;
using System.Linq;
using System.Collections.Generic;

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
        // Invoice
        private readonly ElementLocator invoiceCompany = new ElementLocator(Locator.Id, "AddressDto_InvoiceCompanyName");
        private readonly ElementLocator invoicePostCode = new ElementLocator(Locator.Id, "AddressDto_InvoicePostalCode");
        private readonly ElementLocator invoiceCity = new ElementLocator(Locator.Id, "AddressDto_InvoiceCity");
        private readonly ElementLocator invoiceAddress = new ElementLocator(Locator.Id, "AddressDto_InvoiceAddress");
        private readonly ElementLocator invoiceNip = new ElementLocator(Locator.Id, "AddressDto_InvoiceNIP");
        private readonly ElementLocator invoiceEmail = new ElementLocator(Locator.Id, "AddressDto_InvoiceEmail");
        // Address 
        private ElementLocator letterData = new ElementLocator(Locator.CssSelector, "[name = 'AddressDto.InvoiceAddressIsTheSame'] + label");
        private ElementLocator letterCompany = new ElementLocator(Locator.Id, "AddressDto_LetterCompanyName");
        private ElementLocator letterPostCode = new ElementLocator(Locator.Id, "AddressDto_LetterPostalCode");
        private ElementLocator letterCity = new ElementLocator(Locator.Id, "AddressDto_LetterCity");
        private ElementLocator letterAddress = new ElementLocator(Locator.Id, "AddressDto_LetterAddress");
        // Complete
        private readonly ElementLocator examName = new ElementLocator(Locator.CssSelector, ".RegisterComplete-products .u-isRegular");
        private readonly ElementLocator examNameFull = new ElementLocator(Locator.CssSelector, ".u-isLight");
        private readonly ElementLocator contactEmail = new ElementLocator(Locator.Id, "contact-email");
        private readonly ElementLocator thankyouMessage = new ElementLocator(Locator.CssSelector, ".space3");
        // Group
        private ElementLocator groupUserName = new ElementLocator(Locator.CssSelector, "[name = 'name']");
        private ElementLocator groupUserSurname = new ElementLocator(Locator.CssSelector, "[name = 'surname']");
        private ElementLocator groupUserEmail = new ElementLocator(Locator.CssSelector, "[name = 'email']");
        private ElementLocator groupUserPhone = new ElementLocator(Locator.CssSelector, "[name = 'phone']");
        private ElementLocator groupUserExam = new ElementLocator(Locator.XPath, "//span[contains(text(), '{0}')]");
        private ElementLocator groupUserExamLanguage = new ElementLocator(Locator.XPath, "//label[contains(text(), '{0}')]");
        private ElementLocator groupUserExamForm = new ElementLocator(Locator.XPath, "//label[contains(text(), '{0}')]");
        private ElementLocator addGroupUserToList = new ElementLocator(Locator.CssSelector, ".btn-registrationAddAttendee");

        public RegisterPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public TopbarPage Topbar => new TopbarPage(DriverContext);

        public string SuccessExamName => this.Driver.GetElement(examName).Text;

        public List<string> SuccessExamNameList => this.Driver.GetElements(examName).Select(el => el.Text).ToList();

        public List<string> SuccessExamNameFull => this.Driver.GetElements(examNameFull).Select(el => el.Text).ToList();

        public string SuccessCntactEmail => this.Driver.GetElement(contactEmail).Text;

        public string SuccessThankyouMessage => this.Driver.GetElement(thankyouMessage).Text;

        public string FreePlaceForGroupExam(string text)
        {
            this.Driver.WaitForAjax();
            return this.Driver.GetElement(groupUserExam.Format(text)).Text.Split('-')[1].Trim().Split(' ')[0];
        }


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
        
        public RegisterPage SetInvoiceCompany(string text)
        {
            this.Driver.SendKeys(invoiceCompany, text);
            return this;
        }

        public RegisterPage SetInvoicePostCode(string text)
        {
            this.Driver.SendKeys(invoicePostCode, text);
            return this;
        }

        public RegisterPage SetInvoiceCity(string text)
        {
            this.Driver.SendKeys(invoiceCity, text);
            return this;
        }

        public RegisterPage SetInvoiceAddress(string text)
        {
            this.Driver.SendKeys(invoiceAddress, text);
            return this;
        }

        public RegisterPage SetInvoiceNip(string text)
        {
            this.Driver.SendKeys(invoiceNip, text);
            return this;
        }

        public RegisterPage SetInvoiceEmail(string text)
        {
            this.Driver.SendKeys(invoiceEmail, text);
            return this;
        }

        public RegisterPage SetDifferentAddress()
        {
            this.Driver.Click(letterData);
            return this;
        }

        public RegisterPage SetLetterComapny(string text)
        {
            this.Driver.SendKeys(letterCompany, text);
            return this;
        }

        public RegisterPage SetLetterPostCode(string text)
        {
            this.Driver.SendKeys(letterPostCode, text);
            return this;
        }

        public RegisterPage SetLetterCity(string text)
        {
            this.Driver.SendKeys(letterCity, text);
            return this;
        }

        public RegisterPage SetLetterAddress(string text)
        {
            this.Driver.SendKeys(letterAddress, text);
            return this;
        }

        public RegisterPage SetGroupUserName(string text)
        {
            this.Driver.SendKeys(groupUserName, text);
            return this;
        }

        public RegisterPage SetgroupUserSurname(string text)
        {
            this.Driver.SendKeys(groupUserSurname, text);
            return this;
        }

        public RegisterPage SetgroupUserEmail(string text)
        {
            this.Driver.SendKeys(groupUserEmail, text);
            return this;
        }

        public RegisterPage SetGroupUserPhone(string text)
        {
            this.Driver.SendKeys(groupUserPhone, text);
            return this;
        }

        public RegisterPage SelectExamForGroupUser(string text)
        {
            this.Driver.Click(groupUserExam.Format(text));
            return this;
        }

        public RegisterPage SelectGroupUserExamLanguage(string text)
        {
            this.Driver.Click(groupUserExamLanguage.Format(text));
            return this;
        }

        public RegisterPage SelectGroupUserExamForm(string text)
        {
            this.Driver.Click(groupUserExamForm.Format(text));
            return this;
        }

        public RegisterPage AddGroupUserToList()
        {
            this.Driver.Click(addGroupUserToList);
            return this;
        }
    }
}
