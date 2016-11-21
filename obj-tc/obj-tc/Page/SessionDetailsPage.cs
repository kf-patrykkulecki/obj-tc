using System.Collections.Generic;
using System.Linq;
using obj_tc.Extensions;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Extensions;
using Objectivity.Test.Automation.Common.Types;
using Objectivity.Test.Automation.Tests.PageObjects;

namespace obj_tc.Page
{
    public class SessionDetailsPage : ProjectPageBase
    {
        private readonly ElementLocator sessionStatus = new ElementLocator(Locator.CssSelector, ".js-session-status");
        //private readonly ElementLocator sessionActivateButton = new ElementLocator(Locator.CssSelector, ".Sidebar-itemValue input:nth-of-type(1)");
        private readonly ElementLocator sessionActivateButton = new ElementLocator(Locator.XPath, "//div[label[@data-content='Aktywowanie sesji']]");
        // Links
        private readonly ElementLocator examsLink = new ElementLocator(Locator.Id, "sidebarItem-SessionExams");
        // Details
        private readonly ElementLocator sessionDate = new ElementLocator(Locator.CssSelector, ".BackofficeDetails-content .BackofficeDetails-half:nth-of-type(1)");
        private readonly ElementLocator sessionTime = new ElementLocator(Locator.CssSelector, ".BackofficeDetails-content .BackofficeDetails-half:nth-of-type(2)");
        private readonly ElementLocator sessionSpace = new ElementLocator(Locator.CssSelector, ".Backoffice-contentContainer > div > div:nth-of-type(1) .BackofficeDetails-item:nth-of-type(2) .BackofficeDetails-content");
        private readonly ElementLocator sessionExaminer = new ElementLocator(Locator.CssSelector, ".Backoffice-contentContainer > div > div:nth-of-type(1) .BackofficeDetails-item:nth-of-type(3) .BackofficeDetails-content");
        private readonly ElementLocator sessionPostCode = new ElementLocator(Locator.CssSelector, ".Backoffice-contentContainer > div > div:nth-of-type(2) .row .BackofficeDetails-item:nth-of-type(1) .BackofficeDetails-content");
        private readonly ElementLocator sessionCity = new ElementLocator(Locator.CssSelector, ".Backoffice-contentContainer > div > div:nth-of-type(2) .row .BackofficeDetails-item:nth-of-type(2) .BackofficeDetails-content");
        private readonly ElementLocator sessionAddress = new ElementLocator(Locator.CssSelector, ".Backoffice-contentContainer > div > div:nth-of-type(2) > .BackofficeDetails-item:nth-of-type(2) .BackofficeDetails-content");
        private readonly ElementLocator sessionAdditionalInformation = new ElementLocator(Locator.CssSelector, ".Backoffice-contentContainer > div > div:nth-of-type(2) > .BackofficeDetails-item:nth-of-type(3) .BackofficeDetails-content");
        // Exams
        private readonly ElementLocator examsList = new ElementLocator(Locator.CssSelector, ".Exam-examList .Exam-examItem > div");
        private readonly ElementLocator examsSpace = new ElementLocator(Locator.XPath, "//div[text() = '{0}']/following-sibling::div/span");
        // Edit
        private readonly ElementLocator editSessionButton = new ElementLocator(Locator.CssSelector, ".Backoffice-actionButtons a");
        
        public SessionDetailsPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public TopbarPage Topbar => new TopbarPage(this.DriverContext);

        public string Status
        {
            get
            {
                this.Driver.WaitForElementToBeDisplayed(sessionStatus);
                return this.Driver.GetElement(sessionStatus).Text;
            }
        }

        public string Date
        {
            get
            {
                this.Driver.WaitForElementToBeDisplayed(sessionDate);
                return this.Driver.GetElement(sessionDate).Text;
            }
        }

        public string Time
        {
            get
            {
                this.Driver.WaitForElementToBeDisplayed(sessionTime);
                return this.Driver.GetElement(sessionTime).Text;
            }
        }

        public int Space
        {
            get
            {
                this.Driver.WaitForElementToBeDisplayed(sessionSpace);
                return int.Parse(this.Driver.GetElement(sessionSpace).Text);
            }
        }

        public string Examiner
        {
            get
            {
                this.Driver.WaitForElementToBeDisplayed(sessionExaminer);
                return this.Driver.GetElement(sessionExaminer).Text;
            }
        }

        public string PostCode
        {
            get
            {
                this.Driver.WaitForElementToBeDisplayed(sessionPostCode);
                return this.Driver.GetElement(sessionPostCode).Text;
            }
        }

        public string City
        {
            get
            {
                this.Driver.WaitForElementToBeDisplayed(sessionCity);
                return this.Driver.GetElement(sessionCity).Text;
            }
        }

        public string Address
        {
            get
            {
                this.Driver.WaitForElementToBeDisplayed(sessionAddress);
                return this.Driver.GetElement(sessionAddress).Text;
            }
        }

        public string AdditionalInformation
        {
            get
            {
                this.Driver.WaitForElementToBeDisplayed(sessionAdditionalInformation);
                return this.Driver.GetElement(sessionAdditionalInformation).Text;
            }
        }

        public List<string> ExamList
        {
            get
            {
                this.Driver.WaitForElementToBeDisplayed(examsList);
                return this.Driver.GetElements(examsList).Select(el => el.Text).ToList();
            }
        }

        public int ExamsSpaceBasic => int.Parse(this.Driver.GetElement(examsSpace.Format("Podstawowy")).Text.Split(' ')[2]);

        public int ExamsSpaceAdvanced => int.Parse(this.Driver.GetElement(examsSpace.Format("Zaawansowany")).Text.Split(' ')[2]);

        public int ExamsSpaceExpert => int.Parse(this.Driver.GetElement(examsSpace.Format("Ekspercki")).Text.Split(' ')[2]);

        public int ExamsSpaceOther => int.Parse(this.Driver.GetElement(examsSpace.Format("Inny")).Text.Split(' ')[2]);

        public SessionDetailsPage SwitchToExams()
        {
            this.Driver.Click(examsLink);
            return this;
        }

        public SessionDetailsPage ActivateSession()
        {
            this.Driver.Click(sessionActivateButton);
            this.Driver.JavaScriptAlert().ConfirmJavaScriptAlert();
            this.Driver.WaitForAjax();
            return this;
        }

        public void EditSession()
        {
            //TODO if implemented
            this.Driver.Click(editSessionButton);
        }
    }
}
