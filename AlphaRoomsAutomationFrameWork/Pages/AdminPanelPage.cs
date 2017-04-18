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
    public class AdminPanelPage
    {
        private static AdminPanelPageData data = new AdminPanelPageData();
        public static AdminPanelPageData Data { get { return data; } }


        public static bool IsDisplayed()
        {
            return Driver.Instance.IsElementDisplayedById("main");
        }

        internal static void WaitForLoad()
        {
            AdminPanelPage.ResetData();
            NonFunctionalReq.CaptureTime(() => Driver.Instance.FindElementWithTimeout(By.Id("gview_itineraryGrid"), 60, "Conformation Page  not loaded within 60 sec"), "Conformation page load time is");
        }

        internal static void ResetData()
        {
            AdminPanelPage.data = new AdminPanelPageData();
        }

        //Select enviorment to run the automation tests
        public static void Navigate(AdminPanelEnviorment Env)
        {
            switch (Env)
            {
                case AdminPanelEnviorment.Live:
                    //Capture Page Load Times
                    NonFunctionalReq.CaptureTime(() => Driver.Instance.Navigate().GoToUrl("http://adminpanel/"), "AdminPanel Page load time is");
                    break;

                case AdminPanelEnviorment.Test:
                    //Capture Page Load Times
                    NonFunctionalReq.CaptureTime(() => Driver.Instance.Navigate().GoToUrl("http://adminpanel.alpha2.com/"), "AdminPanel Page load time is");
                    break;
            }
        }

        public static void SearchBookingByRefrence(string refr)
        {
            TypeRefrenceNumber(refr);   
        }

        private static void TypeRefrenceNumber(string refr)
        {
            Logger.AddTypeAction("Booking Reference", refr);
            IWebElement referenceTextField = Driver.Instance.FindElement(By.Id("gs_Reference"));
            referenceTextField.Click();
            referenceTextField.SendKeys(refr);
            referenceTextField.SendKeys(Keys.Enter);
        }

        public static bool IsBookingFound()
        {
            IWebElement itinerayGrid = getItinerayGrid();
            ReadOnlyCollection<IWebElement> gridRows = itinerayGrid.FindElements(By.CssSelector("tbody tr"));

            if (gridRows.Count > 2)
            {
                throw new Exception(string.Format("More than one result found for {0} refrence number", ConfirmationPage.Data.ItineraryNo));
            }
            else if (gridRows.Count < 2)
            {
                throw new Exception(string.Format("No search result found for {0} refrence number", ConfirmationPage.Data.ItineraryNo));
            }

            return true;
        }

        public static bool IsBookingValid(string itineraryNo)
        {
            IWebElement itinerayGrid = getItinerayGrid();
            IWebElement foundBooking = Driver.Instance.FindElement(By.CssSelector("tbody tr[class='ui-widget-content jqgrow ui-row-ltr ui-state-highlight']"));
            AdminPanelPage.Data.ItineraryNo = foundBooking.FindElement(By.CssSelector("td[aria-describedby='itineraryGrid_Reference']")).Text;

            if (AdminPanelPage.Data.ItineraryNo == itineraryNo)
                return true;
            return false;
        }


        public static void SaveBookingDetails()
        {
            IWebElement itinerayGrid = getItinerayGrid();
            IWebElement foundBooking = Driver.Instance.FindElement(By.CssSelector("tbody tr[class='ui-widget-content jqgrow ui-row-ltr ui-state-highlight']"));
            AdminPanelPage.Data.PNRNo = foundBooking.FindElement(By.CssSelector("td[aria-describedby='itineraryGrid_ID']")).Text;
            AdminPanelPage.Data.ItineraryNo = foundBooking.FindElement(By.CssSelector("td[aria-describedby='itineraryGrid_Reference']")).Text;
            AdminPanelPage.Data.BookedByName = foundBooking.FindElement(By.CssSelector("td[aria-describedby='itineraryGrid_CustomerName']")).Text;
            string totalPrice = foundBooking.FindElement(By.CssSelector("td[aria-describedby='itineraryGrid_TotalSalePrice']")).Text;
            AdminPanelPage.Data.TotalPrice = decimal.Parse(totalPrice.Split(' ').First().Trim());
        }

        private static IWebElement getItinerayGrid()
        {
            return Driver.Instance.FindElement(By.Id("itineraryGrid"));
        }
    }
}
