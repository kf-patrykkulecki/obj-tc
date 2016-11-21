using System.Collections.Generic;
using Objectivity.Test.Automation.Tests.PageObjects;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Types;
using obj_tc.Extensions;
using Objectivity.Test.Automation.Common.Extensions;
using OpenQA.Selenium;

namespace obj_tc.Page
{
    public class ClosedSessionPage : ProjectPageBase
    {
        private readonly ElementLocator dateInput = new ElementLocator(Locator.Id, "ClosedRegistrationDateAndPlaceDto_ProposedDateTime");
        private readonly ElementLocator postCodeInput = new ElementLocator(Locator.Id, "ClosedRegistrationDateAndPlaceDto_PostalCode");
        private readonly ElementLocator cityInput = new ElementLocator(Locator.Id, "ClosedRegistrationDateAndPlaceDto_City");
        private readonly ElementLocator addressInput = new ElementLocator(Locator.Id, "ClosedRegistrationDateAndPlaceDto_Address");
        private readonly ElementLocator additionalInofrmationInput = new ElementLocator(Locator.Id, "ClosedRegistrationDateAndPlaceDto_AdditionalInformation");

        private readonly ElementLocator spacePerProductLabel = new ElementLocator(Locator.CssSelector, "#spacePerProduct ~ label");
        private readonly ElementLocator spacePerSessionLabel = new ElementLocator(Locator.CssSelector, "#spacePerSession ~ label");
        private readonly ElementLocator spacePerSessionInput = new ElementLocator(Locator.Name, "SessionDto.SpaceForSession");

        private readonly ElementLocator levelSelect = new ElementLocator(Locator.CssSelector, "div.level");
        private readonly ElementLocator levelSelectValue = new ElementLocator(Locator.XPath, "//div[contains(@class, 'level')]//span[text() = '{0}']");
        private readonly ElementLocator levelSelectValueSelected = new ElementLocator(Locator.XPath, "//div[contains(@class, 'level')]//span[text() = '{0}']/ancestor::li[@class='selected']");

        private readonly ElementLocator productSelect = new ElementLocator(Locator.CssSelector, "div.product");
        private readonly ElementLocator productSelectValue = new ElementLocator(Locator.XPath, "//div[contains(@class, 'product')]//span[text() = '{0}']");
        private readonly ElementLocator productSelectValueSelected = new ElementLocator(Locator.XPath, "//div[contains(@class, 'product')]//span[text() = '{0}']/ancestor::li[@class='selected']");
        private readonly ElementLocator productSpace = new ElementLocator(Locator.XPath, "//input[@value = '{0}']/following-sibling::input[contains(@name, 'CapacityForProductSession')]");

        private readonly ElementLocator examinerSelect = new ElementLocator(Locator.CssSelector, "[data-id*='Examiner']");
        private readonly ElementLocator examinerSelectValue = new ElementLocator(Locator.XPath, "//span[text() = '{0}']");
        private readonly ElementLocator examinerSelectValueSelected = new ElementLocator(Locator.XPath, "//span[contains(@class, 'filter-option') and text() = '{0}']");

        private readonly ElementLocator saveSessionButton = new ElementLocator(Locator.CssSelector, "input[type='submit']");

        private readonly ElementLocator errorMessage = new ElementLocator(Locator.CssSelector, ".field-validation-error");

        private readonly ElementLocator cityValidationMessage = new ElementLocator(Locator.CssSelector, "span[data-valmsg-for='SessionDto.Location.City']");
        private readonly ElementLocator dateValidationMessage = new ElementLocator(Locator.CssSelector, "span[data-valmsg-for='SessionDto.Date']");
        private readonly ElementLocator productValidationMessage = new ElementLocator(Locator.CssSelector, "span[data-valmsg-for='SessionDto.Products']");
        private readonly ElementLocator spaceValidationMessage = new ElementLocator(Locator.CssSelector, "span[data-valmsg-for='SessionDto.Space']");

        private readonly ElementLocator name = new ElementLocator(Locator.CssSelector, "input.form-control.registration__input--name");
        private readonly ElementLocator surname = new ElementLocator(Locator.CssSelector, "input.form-control.registration__input--surname");
        private readonly ElementLocator email = new ElementLocator(Locator.CssSelector, "input.form-control.registration__input--email");
        private readonly ElementLocator phone = new ElementLocator(Locator.CssSelector, "input.form-control.registration__input--phone");

        private readonly ElementLocator persname = new ElementLocator(Locator.Id, "PersonDataDto_Name");
        private readonly ElementLocator perssurname = new ElementLocator(Locator.Id, "PersonDataDto_Surname");
        private readonly ElementLocator persemail = new ElementLocator(Locator.Id, "PersonDataDto_Email");
        private readonly ElementLocator persphone = new ElementLocator(Locator.Id, "PersonDataDto_Phone");

        private readonly ElementLocator addrsname = new ElementLocator(Locator.Id, "AddressDto_Name");
        private readonly ElementLocator addrsurname = new ElementLocator(Locator.Id, "AddressDto_Surname");
        private readonly ElementLocator addrcity = new ElementLocator(Locator.Id, "AddressDto_City");
        private readonly ElementLocator addrpostal = new ElementLocator(Locator.Id, "AddressDto_PostalCode");
        private readonly ElementLocator addraddr = new ElementLocator(Locator.Id, "AddressDto_Address");
        private readonly ElementLocator addraddinfo = new ElementLocator(Locator.Id, "AddressDto_Comment");
        private readonly ElementLocator addrAgree1 = new ElementLocator(Locator.CssSelector, "label[for='AddressDto_AcceptedPrivacyPolicy']");
        private readonly ElementLocator addrAgree2 = new ElementLocator(Locator.CssSelector, "label[for='AddressDto_AcceptedMarketingPolicy']");

        private readonly ElementLocator radioButton = new ElementLocator(Locator.XPath, "//label[contains(.,'{0}')]");
        private readonly ElementLocator addUser = new ElementLocator(Locator.CssSelector, "button.btn.btn-light.btn-registrationAddAttendee");
        private readonly ElementLocator contactDetails = new ElementLocator(Locator.CssSelector, "button.btn.btn-dark.button--accept.btn-form");
        private readonly ElementLocator sendData = new ElementLocator(Locator.CssSelector, "button.btn.btn-dark.btn-form");

        private readonly ElementLocator success = new ElementLocator(Locator.CssSelector, "span.u-isRegular");


        public ClosedSessionPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public string ErrorMessage => this.Driver.GetElement(errorMessage).Text;

        public string cityValidationText => this.Driver.GetElement(cityValidationMessage).Text;
        public string dateValidationText => this.Driver.GetElement(dateValidationMessage).Text;
        public string productValidationText => this.Driver.GetElement(productValidationMessage).Text;

        public bool cityValidationPresent => this.Driver.IsElementPresent(cityValidationMessage, 3);
        public bool dateValidationPresent => this.Driver.IsElementPresent(dateValidationMessage, 3);
        public bool productValidationPresent => this.Driver.IsElementPresent(productValidationMessage, 3);
        public bool spaceValidationPresent => this.Driver.IsElementPresent(spaceValidationMessage, 3);

        public bool spacePerSessionInputPresent => this.Driver.IsElementPresent(spacePerSessionInput, 3);

        public ClosedSessionPage SetDate(string text)
        {
            this.Driver.Click(dateInput);
            this.Driver.SendKeys(dateInput, text + Keys.Enter);
            return this;
        }

        public ClosedSessionPage SetPostCode(string text)
        {
            this.Driver.SendKeys(postCodeInput, text);
            return this;
        }

        public ClosedSessionPage SetCity(string text)
        {
            this.Driver.SendKeys(cityInput, text);
            return this;
        }

        public ClosedSessionPage SetAddress(string text)
        {
            this.Driver.SendKeys(addressInput, text);
            return this;
        }

        public ClosedSessionPage SetAdditionalInformation(string text)
        {
            this.Driver.SendKeys(additionalInofrmationInput, text);
            return this;
        }

        public ClosedSessionPage SelectSpacePerProduct()
        {
            this.Driver.Click(spacePerProductLabel);
            this.Driver.WaitForAjax();
            return this;
        }

        public ClosedSessionPage SelectSpacePerSession()
        {
            this.Driver.Click(spacePerSessionLabel);
            this.Driver.WaitForAjax();
            return this;
        }

        public ClosedSessionPage SetSpacePerSession(string text)
        {
            this.Driver.Click(spacePerSessionLabel);
            this.Driver.WaitForAjax();
            this.Driver.SendKeys(spacePerSessionInput, text);
            return this;
        }

        public ClosedSessionPage SelectLevel(List<string> text)
        {
            this.Driver.Click(levelSelect);
            foreach (var el in text)
            {
                this.Driver.Click(levelSelectValue.Format(el));
                this.Driver.WaitForElementToBeDisplayed(levelSelectValueSelected.Format(el));
            }
            this.Driver.Click(levelSelect);
            return this;
        }

        public ClosedSessionPage SelectProduct(List<string> text)
        {
            this.Driver.Click(productSelect);
            foreach (var el in text)
            {
                this.Driver.Click(productSelectValue.Format(el));
                this.Driver.WaitForElementToBeDisplayed(productSelectValueSelected.Format(el));
            }
            this.Driver.Click(productSelect);
            return this;
        }

        public ClosedSessionPage SetProductSpace(string text, int space)
        {
            this.Driver.SendKeys(productSpace.Format(text), space);
            return this;
        }

        public ClosedSessionPage SelectExaminer(string text)
        {
            this.Driver.Click(examinerSelect);
            this.Driver.Click(examinerSelectValue.Format(text));
            this.Driver.WaitForElementToBeDisplayed(examinerSelectValueSelected.Format(text));
            return this;
        }

        public ClosedSessionPage SetName(string text)
        {
            this.Driver.Click(name);
            this.Driver.SendKeys(name, text + Keys.Enter);
            return this;
        }

        public ClosedSessionPage SetSurname(string text)
        {
            this.Driver.Click(surname);
            this.Driver.SendKeys(surname, text + Keys.Enter);
            return this;
        }

        public ClosedSessionPage SetEmail(string text)
        {
            this.Driver.Click(email);
            this.Driver.SendKeys(email, text + Keys.Enter);
            return this;
        }
        public ClosedSessionPage SetPhone(string text)
        {
            this.Driver.Click(phone);
            this.Driver.SendKeys(phone, text + Keys.Enter);
            return this;
        }

        public ClosedSessionPage SetPersName(string text)
        {
            this.Driver.WaitForElementToBeDisplayed(persname);
            this.Driver.WaitForAjax();
            this.Driver.SendKeys(persname, text + Keys.Enter);
            return this;
        }

        public ClosedSessionPage SetPersSurname(string text)
        {
            this.Driver.Click(perssurname);
            this.Driver.SendKeys(perssurname, text + Keys.Enter);
            return this;
        }

        public ClosedSessionPage SetPersEmail(string text)
        {
            this.Driver.Click(persemail);
            this.Driver.SendKeys(persemail, text + Keys.Enter);
            return this;
        }
        public ClosedSessionPage SetPersPhone(string text)
        {
            this.Driver.Click(persphone);
            this.Driver.SendKeys(persphone, text + Keys.Enter);
            return this;
        }

        public ClosedSessionPage SetAddrName(string text)
        {
            this.Driver.WaitForElementToBeDisplayed(addrsname);
            this.Driver.WaitForAjax();
            //this.Driver.Click(addrsname);
            this.Driver.SendKeys(addrsname, text + Keys.Enter);
            return this;
        }

        public ClosedSessionPage SetAddrSurname(string text)
        {
            this.Driver.Click(addrsurname);
            this.Driver.SendKeys(addrsurname, text + Keys.Enter);
            return this;
        }

        public ClosedSessionPage SetAddrCity(string text)
        {
            this.Driver.Click(addrcity);
            this.Driver.SendKeys(addrcity, text + Keys.Enter);
            return this;
        }

        public ClosedSessionPage SetAddrPostal(string text)
        {
            this.Driver.Click(addrpostal);
            this.Driver.SendKeys(addrpostal, text + Keys.Enter);
            return this;
        }

        public ClosedSessionPage SetAddrAddr(string text)
        {
            this.Driver.Click(addraddr);
            this.Driver.SendKeys(addraddr, text + Keys.Enter);
            return this;
        }

        public ClosedSessionPage SetAddrAddInfo(string text)
        {
            this.Driver.Click(addraddinfo);
            this.Driver.SendKeys(addraddinfo, text + Keys.Enter);
            return this;
        }

        public ClosedSessionPage CliskAddUser()
        {
            this.Driver.Click(addUser);
            return this;
        }

        public ClosedSessionPage ClickContactDetails()
        {
            this.Driver.Click(contactDetails);
            return this;
        }

        public ClosedSessionPage ClickSendData()
        {
            this.Driver.Click(sendData);
            this.Driver.WaitForAjax();
            return this;
        }

        public ClosedSessionPage ClickRadioButton(string text) {
            this.Driver.Click(radioButton.Format(text));
            return this;
        }

        public ClosedSessionPage ClickAgree1()
        {
            this.Driver.WaitForAjax();
            this.Driver.WaitForElementToBeDisplayed(addrAgree1);
            this.Driver.Click(addrAgree1);
            return this;
        }
        public ClosedSessionPage ClickAgree2()
        {
            this.Driver.Click(addrAgree2);
            return this;
        }

        public string SucessText() {
            this.Driver.WaitForAjax();
            return this.Driver.GetElement(success).Text;

        }


        public ClosedSessionPage SaveSessionReturnAddSession()
        {
            this.Driver.Click(saveSessionButton);
            return this;
        }

    }
}
