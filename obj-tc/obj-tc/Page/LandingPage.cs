using Objectivity.Test.Automation.Tests.PageObjects;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Types;
using Objectivity.Test.Automation.Common.Extensions;
using obj_tc.Extensions;

namespace obj_tc.Page
{
    public class LandingPage : ProjectPageBase
    {
        private readonly ElementLocator logInLink = new ElementLocator(Locator.Id, "loginLink");
        private readonly ElementLocator registerIndividual = new ElementLocator(Locator.XPath, "//h5[contains(., '{0}')]/ancestor::div[contains(@class, 'Agenda-dateRow')]/following-sibling::div[contains(@class, 'Agenda-dateContentContainer')]//td[contains(@class, 'btn')]");

        public LandingPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public LandingPage OpenLandingPage()
        {
            this.Driver.NavigateTo(
                new System.Uri(BaseConfiguration.Protocol + BaseConfiguration.Host + BaseConfiguration.Url));
            return this;
        }

        public LogInPage OpenLogInPage()
        {
            this.Driver.Click(logInLink);
            return new LogInPage(DriverContext);
        }

        public RegisterPage RegisterToSession(string text)
        {
            this.Driver.Click(registerIndividual.Format(text));
            return new RegisterPage(this.DriverContext);
        }

        public string GetRegisterButtonText(string text)
        {
            return this.Driver.GetElement(registerIndividual.Format(text)).Text; 
        }
    }
}
