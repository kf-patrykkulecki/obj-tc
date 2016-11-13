using Objectivity.Test.Automation.Tests.PageObjects;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Types;
using obj_tc.Extensions;
using Objectivity.Test.Automation.Common.Extensions;

namespace obj_tc.Page
{
    public class AddSessionPage : ProjectPageBase
    {
        private readonly ElementLocator dateInput = new ElementLocator(Locator.Id, "SessionDto_Date");
        private readonly ElementLocator postCodeInput = new ElementLocator(Locator.Id, "SessionDto_Date");
        private readonly ElementLocator cityInput = new ElementLocator(Locator.Id, "SessionDto_Location_City");
        private readonly ElementLocator addressInput = new ElementLocator(Locator.Id, "SessionDto_Location_Address");
        private readonly ElementLocator additionalInofrmationInput = new ElementLocator(Locator.Id, "SessionDto_AdditionalInformation");

        private readonly ElementLocator spacePerProductRadio = new ElementLocator(Locator.Id, "spacePerProduct");
        private readonly ElementLocator spacePerSessionRadio = new ElementLocator(Locator.Id, "spacePerSession");
        private readonly ElementLocator spacePerSessionInput = new ElementLocator(Locator.Name, "SessionDto.SpaceForSession");

        private readonly ElementLocator levelSelect = new ElementLocator(Locator.CssSelector, "div.level");
        private readonly ElementLocator levelSelectValue = new ElementLocator(Locator.XPath, "//div[contains(@class, 'level')]//span[text() = '{0}']");
        private readonly ElementLocator levelSelectValueSelected = new ElementLocator(Locator.XPath, "//div[contains(@class, 'level')]//span[text() = '{0}']/ancestor::li[@class='selected']");

        private readonly ElementLocator productSelect = new ElementLocator(Locator.CssSelector, "div.product");
        private readonly ElementLocator productSelectValue = new ElementLocator(Locator.XPath, "//div[contains(@class, 'product')]//span[text() = '{0}']");
        private readonly ElementLocator productSelectValueSelected = new ElementLocator(Locator.XPath, "//div[contains(@class, 'product')]//span[text() = '{0}']/ancestor::li[@class='selected']");

        private readonly ElementLocator examinerSelect = new ElementLocator(Locator.CssSelector, "[data-id*='Examiner']");
        private readonly ElementLocator examinerSelectValue = new ElementLocator(Locator.XPath, "//span[text() = '{0}']");
        private readonly ElementLocator examinerSelectValueSelected = new ElementLocator(Locator.XPath, "//span[contains(@class, 'filter-option') and text() = '{0}']");

        private readonly ElementLocator saveSessionButton = new ElementLocator(Locator.CssSelector, ".Backoffice-buttonsContainerBottom [type='submit']");

        public AddSessionPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public AddSessionPage SetDate(string text)
        {
            this.Driver.SendKeys(dateInput, text);
            return this;
        }

        public AddSessionPage SetPostCode(string text)
        {
            this.Driver.SendKeys(postCodeInput, text);
            return this;
        }

        public AddSessionPage SetCity(string text)
        {
            this.Driver.SendKeys(cityInput, text);
            return this;
        }

        public AddSessionPage SetAddress(string text)
        {
            this.Driver.SendKeys(addressInput, text);
            return this;
        }

        public AddSessionPage SetAdditionalInformation(string text)
        {
            this.Driver.SendKeys(additionalInofrmationInput, text);
            return this;
        }

        public AddSessionPage SelectSpacePerProduct()
        {
            this.Driver.Click(spacePerProductRadio);
            this.Driver.WaitForAjax();
            return this;
        }

        public AddSessionPage SelectSpacePerSession()
        {
            this.Driver.Click(spacePerSessionRadio);
            this.Driver.WaitForAjax();
            return this;
        }

        public AddSessionPage SetSpacePerSession(string text)
        {
            this.Driver.SendKeys(spacePerSessionInput, text);
            return this;
        }

        public AddSessionPage SelectLevel(string[] text)
        {
            this.Driver.Click(levelSelect);
            foreach(string el in text)
            {
                this.Driver.Click(levelSelectValue.Format(el));
                this.Driver.WaitForElementToBeDisplayed(levelSelectValueSelected.Format(el));
            }
            this.Driver.Click(levelSelect);
            return this;
        }

        public AddSessionPage SelectProduct(string[] text)
        {
            this.Driver.Click(productSelect);
            foreach(string el in text)
            {
                this.Driver.Click(productSelectValue.Format(el));
                this.Driver.WaitForElementToBeDisplayed(productSelectValueSelected.Format(el));
            }
            this.Driver.Click(productSelect);
            return this;
        }

        public AddSessionPage SelectExaminer(string text)
        {
            this.Driver.Click(examinerSelect);
            this.Driver.Click(examinerSelectValue.Format(text));
            this.Driver.WaitForElementToBeDisplayed(examinerSelectValueSelected.Format(text));
            return this;
        }

        public void SaveSession()
        {
            this.Driver.Click(saveSessionButton);
        }
    }
}
