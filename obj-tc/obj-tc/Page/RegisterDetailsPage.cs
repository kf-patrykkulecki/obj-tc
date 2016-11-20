using Objectivity.Test.Automation.Tests.PageObjects;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Types;
using System.Collections.Generic;
using Objectivity.Test.Automation.Common.Extensions;
using System.Linq;
using obj_tc.Extensions;

namespace obj_tc.Page
{
    public class RegisterDetailsPage : ProjectPageBase
    {
        private readonly ElementLocator details = new ElementLocator(Locator.CssSelector, ".BackofficeDetails-item div:nth-of-type(2)");
        private readonly ElementLocator personDetailsLink = new ElementLocator(Locator.Id, "sidebarItem-RegisteringPerson");
        private readonly ElementLocator personDetails = new ElementLocator(Locator.CssSelector, ".BackofficeDetails-content");
        private readonly ElementLocator certDetailsLink = new ElementLocator(Locator.Id, "sidebarItem-DataForCertification");
        private readonly ElementLocator certDetails = new ElementLocator(Locator.CssSelector, ".BackofficeDetails-content");

        public RegisterDetailsPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public List<string> Details => this.Driver.GetElements(details).Select(el => el.Text).ToList();

        public List<string> PersonDetails => this.Driver.GetElements(personDetails).Select(el => el.Text).ToList();

        public List<string> CertDetails => this.Driver.GetElements(certDetails).Select(el => el.Text).ToList();

        public RegisterDetailsPage OpenPersonDetails()
        {
            this.Driver.Click(personDetailsLink);
            return this;
        }

        public RegisterDetailsPage OpenCertDetails()
        {
            this.Driver.Click(certDetailsLink);
            return this;
        }
    }
}
