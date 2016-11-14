using Objectivity.Test.Automation.Tests.PageObjects;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Types;
using obj_tc.Extensions;

namespace obj_tc.Page
{
    public class LogInPage : ProjectPageBase
    {
        private readonly ElementLocator emailInput = new ElementLocator(Locator.Id, "Email");
        private readonly ElementLocator passwordInput = new ElementLocator(Locator.Id, "PasswordPass");
        private readonly ElementLocator loginButton = new ElementLocator(Locator.CssSelector, ".Login-buttonsContainer [type='Submit']");
        private readonly ElementLocator forgotPasswordButton = new ElementLocator(Locator.CssSelector, ".Login-buttonsContainer div.btn a");

        public LogInPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public LogInPage SetEmail(string text)
        {
            this.Driver.SendKeys(emailInput, text);
            return this;
        }

        public LogInPage SetPassword(string text)
        {
            this.Driver.SendKeys(passwordInput, text);
            return this;
        }

        public DashboardPage LogIn()
        {
            this.Driver.Click(loginButton);
            return new DashboardPage(DriverContext);
        }

        public ForgotPasswordPage ForgotPassword()
        {
            this.Driver.Click(forgotPasswordButton);
            return new ForgotPasswordPage(DriverContext);
        }
    }
}
