using obj_tc.Extensions;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Extensions;
using Objectivity.Test.Automation.Common.Types;
using Objectivity.Test.Automation.Tests.PageObjects;

namespace obj_tc.Page
{
    public class TopbarPage : ProjectPageBase
    {
        private readonly ElementLocator userName = new ElementLocator(Locator.CssSelector, ".userName");
        private readonly ElementLocator addSessionLink = new ElementLocator(Locator.Id, "navItem-Session");
        private readonly ElementLocator dashboardLink = new ElementLocator(Locator.Id, "navItem-Dashboard");
        private readonly ElementLocator registrationLink = new ElementLocator(Locator.Id, "navItem-Registration");
        private readonly ElementLocator productsLink = new ElementLocator(Locator.Id, "navItem-Products");

        public TopbarPage(DriverContext driverContext) : base(driverContext)
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

        public AddSessionPage OpenAddSession()
        {
            this.Driver.Click(addSessionLink);
            return new AddSessionPage(DriverContext);
        }

        public DashboardPage OpenDashboard()
        {
            this.Driver.Click(dashboardLink);
            return new DashboardPage(this.DriverContext);
        }

        public RegisterPage OpenRegistration()
        {
            this.Driver.Click(registrationLink);
            return new RegisterPage(this.DriverContext);
        }

        public ProductListPage OpenProducts()
        {
            this.Driver.Click(productsLink);
            return new ProductListPage(DriverContext);
        }
    }
}
