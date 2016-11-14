using obj_tc.Extensions;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Types;
using Objectivity.Test.Automation.Tests.PageObjects;

namespace obj_tc.Page
{
    public class ForgotPasswordPage : ProjectPageBase
    {
        private readonly ElementLocator emailInput = new ElementLocator(Locator.Id, "Email");
        private readonly ElementLocator resetPasswordButton = new ElementLocator(Locator.CssSelector, ".Login-buttonsContainer [type='submit']");

        public ForgotPasswordPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public ForgotPasswordPage SetEmail(string text)
        {
            this.Driver.SendKeys(emailInput, text);
            return this;
        }

        public ForgotPasswordConfirmationPage ResetPassword()
        {
            this.Driver.Click(resetPasswordButton);
            return new ForgotPasswordConfirmationPage(DriverContext);
        }
    }
}
