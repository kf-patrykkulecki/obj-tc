using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Extensions;
using Objectivity.Test.Automation.Common.Types;
using Objectivity.Test.Automation.Tests.PageObjects;
using System.Collections.Generic;
using System.Linq;

namespace obj_tc.Page
{
    public class ProductDetailsPage : ProjectPageBase
    {
        private ElementLocator title = new ElementLocator(Locator.CssSelector, ".Registration-title h3");
        private ElementLocator details = new ElementLocator(Locator.CssSelector, ".BackofficeDetails-content");
        private ElementLocator certificateProvider = new ElementLocator(Locator.CssSelector, ".BackofficeDetails-providerItem");

        public ProductDetailsPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public string Title => this.Driver.GetElement(title).Text;

        public List<string> ProductDetails => this.Driver.GetElements(details).Select(el => el.Text).ToList();

        public string CertificateProvider => this.Driver.GetElement(certificateProvider).Text;
    }
}
