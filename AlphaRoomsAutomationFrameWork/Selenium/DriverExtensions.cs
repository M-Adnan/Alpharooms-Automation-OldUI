using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Selenium
{
    public static class DriverExtensions
    {
        public static void TurnOnWait(this IWebDriver value)
        {
            if (Driver.WaitOff)
            {
                value.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
                Driver.WaitOff = false;
            }
        }

        public static void TurnOffWait(this IWebDriver value)
        {
            if (Driver.WaitOff) return;
            value.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            Driver.WaitOff = true;
        }

        public static IWebElement FindElementWithTimeout(this IWebDriver value, By by, int timeoutInSeconds, string errMsg)
        {
            if (timeoutInSeconds > 0)
            {
                value.TurnOffWait();
                var wait = new WebDriverWait(value, TimeSpan.FromSeconds(timeoutInSeconds));
                try
                {
                    var webelement = wait.Until(drv => drv.FindElement(by));
                    value.TurnOnWait();
                    return webelement;
                }
                catch
                {
                    throw new Exception(errMsg);
                }
            }
            else if (timeoutInSeconds == 0)
            {
                value.TurnOffWait();
                var wait = new WebDriverWait(value, TimeSpan.FromSeconds(timeoutInSeconds));
                var webelement = wait.Until(drv => drv.FindElement(by));
                value.TurnOnWait();
                return webelement;
            }
            return value.FindElement(by);
        }

        public static ReadOnlyCollection<IWebElement> FindElementsWithTimeout(this IWebDriver value, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                value.TurnOffWait();
                var wait = new WebDriverWait(value, TimeSpan.FromSeconds(timeoutInSeconds));
                ReadOnlyCollection<IWebElement> webelements = wait.Until(drv => drv.FindElements(by));
                value.TurnOnWait();
                return webelements;
            }
            else if (timeoutInSeconds == 0)
            {
                value.TurnOffWait();
                var wait = new WebDriverWait(value, TimeSpan.FromSeconds(timeoutInSeconds));
                ReadOnlyCollection<IWebElement> webelements = wait.Until(drv => drv.FindElements(by));
                value.TurnOnWait();
                return webelements;
            }
            return value.FindElements(by);
        }
        
        public static void WaitForAjax(this IWebDriver value)
        {
            while (!(bool)(value as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0")) // Handle timeout somewhere
            {
                Thread.Sleep(100);
            }
        }

        public static bool IsElementDisplayedBy(this IWebDriver value, By by)
        {
            value.WaitForAjax();
            try
            {
                return value.FindElement(by).Displayed;
            }
            catch
            {
            }
            return false;
        }

        public static bool IsElementDisplayedById(this IWebDriver value, string id)
        {
            return value.IsElementDisplayedBy(By.Id(id));
        }
    }
}
