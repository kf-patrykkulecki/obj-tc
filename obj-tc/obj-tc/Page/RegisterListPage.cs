using obj_tc.Extensions;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Types;
using Objectivity.Test.Automation.Tests.PageObjects;

namespace obj_tc.Page
{
    public class RegisterListPage : ProjectPageBase
    {
        private readonly ElementLocator detailsLink = new ElementLocator(Locator.XPath, "//td[text() = '{0}']/ancestor::tr//a[contains(@href, 'IndividualDetails')]");
        public RegisterListPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public RegisterDetailsPage OpenRegistrationDetails(string text)
        {
            this.Driver.Click(detailsLink.Format(text));
            return new RegisterDetailsPage(DriverContext);
        }
    }
}
