using Objectivity.Test.Automation.Tests.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Types;
using Objectivity.Test.Automation.Common.Extensions;
using obj_tc.Extensions;

namespace obj_tc.Page
{
    public class LogInPage : ProjectPageBase
    {
        private readonly ElementLocator emailInput = new ElementLocator(Locator.Id, "Email");
        private readonly ElementLocator passwordInput = new ElementLocator(Locator.Id, "PasswordPass");
        private readonly ElementLocator loginButton = new ElementLocator(Locator.CssSelector, ".Login-buttonsContainer [type='Submit']");

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
    }
}
