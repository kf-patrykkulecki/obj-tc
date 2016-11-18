using System;
using Objectivity.Test.Automation.Tests.PageObjects;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Types;
using Objectivity.Test.Automation.Common.Extensions;
using obj_tc.Extensions;
using OpenQA.Selenium.Support.UI;

namespace obj_tc.Page
{
    public class DashboardPage : ProjectPageBase
    {
        private readonly ElementLocator session = new ElementLocator(Locator.XPath, "//span[text() = '{0}']");
        private readonly ElementLocator registerButton = new ElementLocator(Locator.CssSelector, "a.btn[href= '/obj']");

        public DashboardPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public TopbarPage Topbar => new TopbarPage(this.DriverContext);

        public bool IsSessionDisplayed(string text)
        {
            return this.Driver.IsElementPresentInDom(this.session.Format(text));
        }

        public LandingPage Register()
        {
            this.Driver.Click(registerButton);
            return new LandingPage(this.DriverContext);
        }
    }
}
