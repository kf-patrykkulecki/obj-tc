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

        public string[] GetExamHours(string date, string city)
        {
            var dashboardHours = new ElementLocator(Locator.XPath,string.Format( "//h5[contains(.,'{0}, {1}')]//..//..//span",date,city));
            var hours = this.Driver.GetElements(dashboardHours);
            string[] hoursList = new string[hours.Count];
            int i = 0;
            foreach (var a in hours) {
             
                hoursList[i] = a.Text;
                i++;
            }
            return hoursList;
            }

        public string[] GetExamList(string date, string city, string hour) {
            //h5[contains(.,'22 listopada 2016, 1479717515')]/../../div[contains(.,'12:00')]//td
            var dashboardHours = new ElementLocator(Locator.XPath, string.Format("//h5[contains(.,'22 listopada 2016, {0}')]/../../div[contains(.,'{1}')]//td",city,hour));
            var hours = this.Driver.GetElements(dashboardHours);
            string[] levelsList = new string[hours.Count];
            int i = 0;
            foreach (var a in hours)
            {

                levelsList[i] = a.Text;
                i++;
            }
            return levelsList;
        
    }
    }
}
