using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using obj_tc.Extensions;
using obj_tc.Page;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Tests.Xunit;
using Xunit;

namespace obj_tc.Tests
{
    public class ClosedSessionTest : ProjectTestBase
    {
        private readonly string email = BaseConfiguration.Username;
        private readonly string password = BaseConfiguration.Password;
        private DateTime date = DateTime.Now;
        private const string format = "dd.MM.yyyy HH:mm";

        [Fact]
        public void CheckFormValidation_Test()
        {
            var landingPage = new LandingPage(DriverContext);
            var closedSessionPage =
                landingPage.OpenLandingPage().ProposeExamPage();

            
            var city = "Konin";
            var examDate = date.AddDays(2).ToString(format);
            var postCode = "55-330";
            var address = "cwiartki 3/4";
            var additionalInformation = "abc";
            var name = "alf";
            var surname = "alfowski";
            var phone = "700777888";
            var email = "alf@alf.com";
            var level = new List<string> { "Ekspercki" };
            var product = new Dictionary<string, int>
            {
                {"ISTQB Improving the Testing Process / Angielski", 999}
            };
            var expectedProduct = new List<string>
            {
                "ISTQB Improving the Testing Process/Angielski"
            };

            closedSessionPage.SetDate(examDate);
            closedSessionPage.SetCity(city);
            closedSessionPage.SetPostCode(postCode);
            closedSessionPage.SetAddress(address);
            closedSessionPage.SetAdditionalInformation(additionalInformation);


            var result = closedSessionPage.SaveSessionReturnAddSession();

            closedSessionPage.SetName(name);
            closedSessionPage.SetSurname(surname);
            closedSessionPage.SetEmail(email);
            closedSessionPage.SetPhone(phone);
            closedSessionPage.ClickRadioButton("Podstawowy");
            closedSessionPage.ClickRadioButton("Polski");
            closedSessionPage.ClickRadioButton("papierowa");
            closedSessionPage.ClickRadioButton("ISTQB Foundation Level");
            closedSessionPage.CliskAddUser();
            closedSessionPage.ClickContactDetails();

            closedSessionPage.SetPersName(name);
            closedSessionPage.SetPersSurname(surname);
            closedSessionPage.SetPersEmail(email);
            //closedSessionPage.SetPersPhone(phone);
            closedSessionPage.ClickSendData();

            closedSessionPage.ClickAgree1();
            closedSessionPage.SetAddrName(name);
            closedSessionPage.SetAddrSurname(surname);
            closedSessionPage.SetAddrCity(city);
            closedSessionPage.SetAddrPostal(postCode);
            closedSessionPage.SetAddrAddr(address);
            closedSessionPage.ClickRadioButton("Nie chcę faktury VAT");
            //closedSessionPage.ClickAgree1();
            closedSessionPage.ClickAgree2();
            closedSessionPage.ClickSendData();

            Assert.Equal("ISTQB Foundation Level", closedSessionPage.SucessText());
            //result = closedSessionPage.SaveSessionReturnAddSession();
        }

        private int GetTimeStamp()
        {
            return (int)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}