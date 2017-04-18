using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using AlphaRooms.AutomationFramework.Processes.Extras;
using AlphaRooms.AutomationFramework.PageData;
using AlphaRooms.AutomationFramework.Functions;
using AlphaRooms.AutomationFramework.Panels;
using AlphaRooms.AutomationFramework.Selenium;

namespace AlphaRooms.AutomationFramework
{
    public class ConfirmationPage
    {
        private static ConfirmationPageData data = new ConfirmationPageData();
        public static ConfirmationPageData Data { get { return data; } }

        public static bool IsDisplayed()
        {
            return Driver.Instance.IsElementDisplayedById("confirmation");
        }

        internal static void WaitForLoad()
        {
            ConfirmationPage.ResetData();
            NonFunctionalReq.CaptureTime(() => Driver.Instance.FindElementWithTimeout(By.Id("accordion-Flight"), 60, "Conformation Page  not loaded within 60 sec"), "Conformation page load time is");
        }

        internal static void ResetData()
        {
            ConfirmationPage.data = new ConfirmationPageData();
        }

        public static bool PNRNoIsDisplayed()
        {
            try
            {
                //capture the page load time
                //IWebElement pnrNo = Driver.Instance.FindElement(By.XPath("div[2]/div[5]/h2/span"));
                ReadOnlyCollection<IWebElement> infoElements = GetInformationElements();
                if (infoElements[1].Text.Length != 0)
                return true;
                return false;
            }
            catch { return false; }
        }

        private static ReadOnlyCollection<IWebElement> GetInformationElements()
        {
            return Driver.Instance.FindElements(By.CssSelector("div#confirmation.container div.box h2 span.orange-text-color"));
        }
        public static void SaveItineraryNo()
        {
            ReadOnlyCollection<IWebElement> infoElements = GetInformationElements();
            ConfirmationPage.Data.ItineraryNo = infoElements[0].Text;
        }

        public static void SaveBookedByDetails()
        {
            IWebElement bookedByDiv = Driver.Instance.FindElement(By.CssSelector("#confirmation > div:nth-child(6) > div"));
            ReadOnlyCollection<IWebElement> bookedByInfo = bookedByDiv.FindElements(By.TagName("span"));
            ConfirmationPage.Data.BookedByName = bookedByInfo[1].Text;
            ConfirmationPage.Data.BookedByPhone = bookedByInfo[3].Text;
            ConfirmationPage.Data.BookedByEmail = bookedByInfo[5].Text;

        }

        public static void SavePNRNo()
        {
            ReadOnlyCollection<IWebElement> infoElements = GetInformationElements();
            ConfirmationPage.Data.PNRNo = infoElements[1].Text;
        }

        public static void SaveFlightDetails()
        {
            IWebElement flightInfoDiv = Driver.Instance.FindElement(By.CssSelector("#confirmation > div:nth-child(10)"));
            IWebElement outboundFlightInfo = flightInfoDiv.FindElement(By.CssSelector("div:nth-child(1) > div > div"));
            IWebElement inboundFlightInfo = flightInfoDiv.FindElement(By.CssSelector("div.row-fluid.border-top > div > div"));

            ReadOnlyCollection<IWebElement> outboundFlightSpans = outboundFlightInfo.FindElements(By.TagName("span"));
            ConfirmationPage.Data.OutboundFlightNo = outboundFlightSpans[1].Text;
            ConfirmationPage.Data.OutboundDepartureAirport = outboundFlightSpans[2].Text.Split('\n').First().Trim();
            ConfirmationPage.Data.OutboundDepartureTime = outboundFlightSpans[2].Text.Split('\n').Last().Trim();
            ConfirmationPage.Data.OutboundArrivalAirport = outboundFlightSpans[4].Text.Split('\n').First().Trim(); ;
            ConfirmationPage.Data.OutboundArrivalTime = outboundFlightSpans[4].Text.Split('\n').Last().Trim(); ;

            ReadOnlyCollection<IWebElement> inboundFlightSpans = inboundFlightInfo.FindElements(By.TagName("span"));
            ConfirmationPage.Data.InboundFlightNo = inboundFlightSpans[1].Text;
            ConfirmationPage.Data.InboundDepartureAirport = inboundFlightSpans[2].Text.Split('\n').First().Trim();
            ConfirmationPage.Data.InboundDepartureTime = inboundFlightSpans[2].Text.Split('\n').Last().Trim();
            ConfirmationPage.Data.InboundArrivalAirport = inboundFlightSpans[4].Text.Split('\n').First().Trim(); ;
            ConfirmationPage.Data.InboundArrivalTime = inboundFlightSpans[4].Text.Split('\n').Last().Trim(); ;
       
        }

        public static void SaveTotalPrice()
        {
            IWebElement totalPrice = Driver.Instance.FindElement(By.CssSelector("div.pull-right.bold.orange-text-color.verylarge-text"));
            ConfirmationPage.Data.TotalPrice = Decimal.Parse(totalPrice.Text.Remove(0, 1));
        }

        public static void SavePriceSummary()
        { 
        }
    }
}
