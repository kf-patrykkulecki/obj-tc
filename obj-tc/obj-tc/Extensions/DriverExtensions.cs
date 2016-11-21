using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Extensions;
using Objectivity.Test.Automation.Common.Types;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace obj_tc.Extensions
{
    public static class DriverExtensions
    {
        public static void WaitForElementToBeDisplayed(this IWebDriver driver, ElementLocator locator, double timeoutInSeconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds)) { Message = "Element not displayed" };
            wait.Until(ExpectedConditions.ElementToBeClickable(locator.ToBy()));
        }

        public static void WaitForElementToBeDisplayed(this IWebDriver driver, ElementLocator locator)
        {
            WaitForElementToBeDisplayed(driver, locator, BaseConfiguration.LongTimeout);
        }

        public static void SendKeys(this IWebDriver driver, ElementLocator locator, object text)
        {
            if (text != null && text.ToString().Equals(string.Empty))
            {
                driver.WaitForElementToBeDisplayed(locator, BaseConfiguration.LongTimeout);
                driver.GetElement(locator).Clear();
            }
            else if (text != null)
            {
                driver.WaitForElementToBeDisplayed(locator, BaseConfiguration.LongTimeout);
                driver.GetElement(locator).Clear();
                driver.GetElement(locator).SendKeys(text.ToString());
            }
        }

        public static void Click(this IWebDriver driver, ElementLocator locator)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(BaseConfiguration.LongTimeout))
            {
                Message = "Element not clickable"
            };
            wait.Until(ExpectedConditions.ElementToBeClickable(locator.ToBy()));
               //driver.ScrollIntoMiddle(locator);
            driver.WaitForElementToBeDisplayed(locator, BaseConfiguration.LongTimeout);
            driver.GetElement(locator).Click();
        }

        public static bool IsElementPresentInDom(this IWebDriver driver, ElementLocator locator)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(BaseConfiguration.ShortTimeout))
            {
                Message = "Element not present in DOM"
            };
            wait.Until(ExpectedConditions.ElementExists(locator.ToBy()));

            return driver.FindElements(locator.ToBy()).Count > 0;
        }
    }
}
