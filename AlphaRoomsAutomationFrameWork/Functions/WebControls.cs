using AlphaRooms.AutomationFramework.Selenium;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Functions
{
    public class WebControls
    {
        public static void TypeAndSelectDropDown(string elementId, string panelId, string value)
        {
            IWebElement input = Driver.Instance.FindElement(By.Id(elementId));
            if (!WebControls.TypeAndSelectValue(input, panelId, value, false))
            {
                int count = 0;
                while (!WebControls.TypeAndSelectValue(input, panelId, value, true))
                {
                    if (count == 3) throw new Exception(string.Format("\"{0}\" cannot be found.", value));
                    count++;
                }
            }
        }

        private static bool TypeAndSelectValue(IWebElement input, string panelId, string value, bool useBackspace)
        {
            input.Clear();
            input.SendKeys(value);
            if (useBackspace) input.SendKeys(Keys.Backspace);
            int count = 0;
            do
            {
                Thread.Sleep(1000);
                Driver.Instance.WaitForAjax();
                if (count == 5) return false;
                count++;
            } while (!Driver.Instance.FindElement(By.Id(panelId)).Displayed);
            //
            input.SendKeys(Keys.Enter);
            return true;
        }

        public static void TypeAndSelectDestinationDropDown(string elementId, string panelId, string value, bool isHotel)
        {
            IWebElement input = Driver.Instance.FindElement(By.Id(elementId));
            if (!WebControls.TypeAndSelectDestinationValue(input, panelId, value, false, isHotel))
            {
                int count = 0;
                while (!WebControls.TypeAndSelectDestinationValue(input, panelId, value, true, isHotel))
                {
                    if (count == 3) throw new Exception(string.Format("\"{0}\" cannot be found.", value));
                    count++;
                }
            }
        }

        private static bool TypeAndSelectDestinationValue(IWebElement input, string panelId, string value, bool useBackspace, bool isHotel)
        {
            input.Clear();
            input.SendKeys(value);
            if (useBackspace) input.SendKeys(Keys.Backspace);
            int count = 0;
            do
            {
                Thread.Sleep(1000);
                Driver.Instance.WaitForAjax();
                if (count == 5) return false;
                count++;
            } while (!Driver.Instance.FindElement(By.Id(panelId)).Displayed);
            //look for the Hotels tag
            string dest = (isHotel ? "Hotels" : "Destinations");
            IWebElement dropDown = Driver.Instance.FindElement(By.Id(panelId));
            IWebElement dropDownContainer = dropDown.FindElement(By.XPath("../ul"));
            ReadOnlyCollection<IWebElement> dropDownList = dropDownContainer.FindElements(By.XPath("li"));
            int i = 0;
            foreach (IWebElement list in dropDownList)
            {
                try
                {
                    if (list.Text == dest)
                    {
                        dropDownList[i + 1].Click();
                        break;
                    }
                }
                catch
                {
                }
                i++;
            }
            return true;
        }
        
        public static void SelectDateBox(string elementId, string dropdown, string date)
        {
            SelectDateBoxYearMonth(elementId, dropdown, date);
            SelectDateBoxDay(elementId, dropdown, date);
        }

        private static void SelectDateBoxYearMonth(string elementId, string dropdown, string dt)
        {
            DateTime selectDate = DateTime.Parse("01 " + Calendar.FormatMonthYear(dt));

            //click on checkin input box
            var checkinDatePicker = Driver.Instance.FindElement(By.Id(elementId));
            checkinDatePicker.Click();

            var monthYearCal = Driver.Instance.FindElement(By.XPath(String.Format("{0}/div[1]/table/thead/tr[1]", dropdown)));
            
            DateTime actualDate = DateTime.Parse("01 " + monthYearCal.Text);

            //next arrow button
            var gotoPrevMonth = Driver.Instance.FindElement(By.XPath(String.Format("{0}/div[1]/table/thead/tr[1]/th[1]", dropdown)));
            var gotoNextMonth = Driver.Instance.FindElement(By.XPath(String.Format("{0}/div[1]/table/thead/tr[1]/th[3]", dropdown)));

            if (elementId != "toDate")
            {
                do
                {
                    if (actualDate < selectDate)
                    {
                        gotoNextMonth.Click();
                    }
                    else if (actualDate > selectDate)
                    {
                        gotoPrevMonth.Click();
                    }
                    actualDate = DateTime.Parse("01 " + monthYearCal.Text);
                } while (actualDate != selectDate);
            }
        }

        private static void SelectDateBoxDay(string elementName, string dropdown, string date)
        {
            //select the date
            ReadOnlyCollection<IWebElement> calDatesTr = Driver.Instance.FindElements(By.XPath(string.Format("{0}/div/table/tbody/tr", dropdown)));
            string[] splitDate = date.Split('/');
            splitDate[0] = splitDate[0].TrimStart('0');
            foreach (IWebElement dateElement in calDatesTr)
            {
                Driver.Instance.TurnOffWait();
                ReadOnlyCollection<IWebElement> calDatesTd1 = dateElement.FindElements(By.CssSelector("td[class='day'],td[class='day active']"));
                IWebElement td = calDatesTd1.FirstOrDefault(i => i.Text == splitDate[0]);
                if (td != null) 
                {
                    td.Click();
                    Driver.Instance.TurnOnWait();
                    return;
                }
            }
        }

        public static void SelectDropDown(IWebElement dropDown, string value)
        {
            IWebElement dropDownContainer = dropDown.FindElement(By.XPath("../ul"));
            ReadOnlyCollection<IWebElement> dropDownList = dropDownContainer.FindElements(By.XPath("li"));
            IWebElement itemElement = dropDownList.FirstOrDefault(i => string.Equals(i.FindElement(By.CssSelector("a span.text")).GetAttribute("innerHTML"), value, StringComparison.CurrentCultureIgnoreCase));
            if (itemElement != null)
            {
                dropDown.Click();
                itemElement.FindElement(By.TagName("a")).Click();
                return;
            }
            throw new Exception(string.Format("\"{0}\" cannot be found.", value));
        }
    }
}
