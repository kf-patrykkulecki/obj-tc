using Objectivity.Test.Automation.Tests.PageObjects;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Types;
using Objectivity.Test.Automation.Common.Extensions;
using obj_tc.Extensions;

namespace obj_tc.Page
{
    public class DashboardPage : ProjectPageBase
    {
        private readonly ElementLocator userName = new ElementLocator(Locator.CssSelector, ".userName");
        private readonly ElementLocator addSessionLink = new ElementLocator(Locator.Id, "navItem-Session");

        public DashboardPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public string UserName
        {
            get
            {
                this.Driver.WaitForElementToBeDisplayed(userName, BaseConfiguration.MediumTimeout);
                return this.Driver.GetElement(userName).Text;
            }
        }

        public AddSessionPage AddSession()
        {
            this.Driver.Click(addSessionLink);
            return new AddSessionPage(DriverContext);
        }
    }
}
