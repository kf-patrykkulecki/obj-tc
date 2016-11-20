using obj_tc.Extensions;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Types;
using Objectivity.Test.Automation.Tests.PageObjects;

namespace obj_tc.Page
{
    public class ProductListPage : ProjectPageBase
    {
        private readonly ElementLocator productDetailsLink = new ElementLocator(Locator.XPath, "//td[text() = '{0}']/ancestor::tr//a");

        public ProductListPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public ProductDetailsPage OpenProductDetails(string text)
        {
            this.Driver.Click(productDetailsLink.Format(text));
            return new ProductDetailsPage(DriverContext);
        }
    }
}
