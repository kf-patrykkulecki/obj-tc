using obj_tc.Extensions;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Extensions;
using Objectivity.Test.Automation.Common.Types;
using Objectivity.Test.Automation.Tests.PageObjects;

namespace obj_tc.Page
{
    public class TopbarPage : ProjectPageBase
    {
        private readonly ElementLocator userName = new ElementLocator(Locator.CssSelector, "#dropdownMenu-user");
        private readonly ElementLocator addSessionLink = new ElementLocator(Locator.Id, "navItem-Session");
        private readonly ElementLocator dashboardLink = new ElementLocator(Locator.Id, "navItem-Dashboard");
        private readonly ElementLocator registrationLink = new ElementLocator(Locator.Id, "navItem-Registration");
        private readonly ElementLocator productsLink = new ElementLocator(Locator.Id, "navItem-Products");
        private readonly ElementLocator pgsLogo = new ElementLocator(Locator.CssSelector, "img.Header-logo.u-verticalMiddleAligned.u-hideOnMobile");
        private readonly ElementLocator logOut = new ElementLocator(Locator.CssSelector, ".Navbar-userMenu [href *= 'LogOff']");

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

        public RegisterListPage OpenRegistration()
        {
            this.Driver.Click(registrationLink);
            return new RegisterListPage(this.DriverContext);
        }

        public ProductListPage OpenProducts()
        {
            this.Driver.Click(productsLink);
            return new ProductListPage(DriverContext);
        }

        public ProductListPage ClickLogo()
        {
            this.Driver.Click(pgsLogo);
            return new ProductListPage(DriverContext);
        }

        public LandingPage LogOut()
        {
            this.Driver.Click(userName);
            this.Driver.WaitForElementToBeDisplayed(logOut);
            this.Driver.Click(logOut);
            return new LandingPage(DriverContext);
        }
    }
}
