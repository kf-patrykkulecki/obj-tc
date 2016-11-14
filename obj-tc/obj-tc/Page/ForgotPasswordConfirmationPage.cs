using obj_tc.Extensions;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Extensions;
using Objectivity.Test.Automation.Common.Types;
using Objectivity.Test.Automation.Tests.PageObjects;

namespace obj_tc.Page
{
    public class ForgotPasswordConfirmationPage : ProjectPageBase
    {
        private readonly ElementLocator confiramtionMessage = new ElementLocator(Locator.CssSelector, ".RegistrationProblem-emailToSend");

        public ForgotPasswordConfirmationPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public string ConfirmationMessage
        {
            get
            {
                this.Driver.WaitForElementToBeDisplayed(confiramtionMessage);
                return this.Driver.GetElement(confiramtionMessage).Text;
            }
        }
    }
}
