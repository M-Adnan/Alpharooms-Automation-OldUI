using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using AlphaRooms.AutomationFramework.Selenium;
using System.Collections.ObjectModel;
using AlphaRooms.AutomationFramework.Processes.ChangeSearch;
using AlphaRooms.AutomationFramework.PageData;
using AlphaRooms.AutomationFramework.Functions;

namespace AlphaRooms.AutomationFramework.Panels
{
    public class ChangeSearchPanel
    {
        private static ChangeSearchPanelData data = new ChangeSearchPanelData();
        public static ChangeSearchPanelData Data { get { return data; } }

        //private static TopPanel topPanel = new TopPanel();
        //public static TopPanel TopPanel { get { return topPanel; } }

        internal static void ResetData()
        {
            Logger.Clear();
            data = new ChangeSearchPanelData();
            //topPanel = new TopPanel();
        }

        public ChangeSearchStarter ChangeSearch()
        {
            return new ChangeSearchStarter(this);
        }

        public void ClickChangeSearch()
        {
            Logger.AddClickAction("Change Search");
            IWebElement changeSearchBtn = Driver.Instance.FindElement(By.CssSelector("div.summary-detail button[class='btn btn-orange']"));
            changeSearchBtn.Click();
        }

        public void ClickAddAnotherRoom()
        {
            throw new NotImplementedException();
        }

        public void ClickShowPrices()
        {
            Logger.AddClickAction("Show Prices");
            IWebElement showPrices = Driver.Instance.FindElement(By.CssSelector("div.span12 button[class='btn btn-orange pull-right search-btn-wide']"));
            showPrices.Click();
            data.SearchTime = NonFunctionalReq.CaptureTime(() => Driver.Instance.FindElementWithTimeout(By.CssSelector("div.row-fluid a.pull-left div.logo"), 60, "Result page is not displayed within 60 Sec"), "Search Page load time is");
            
        }

        public void ClickShowPricesAndCapture()
        {
            Logger.AddClickAction("Show Prices");
            IWebElement showPrices = Driver.Instance.FindElement(By.CssSelector("div.span12 button[class='btn btn-orange pull-right search-btn-wide']"));
            showPrices.Click();
            data.SearchTime = NonFunctionalReq.CaptureTime(() => Driver.Instance.FindElementWithTimeout(By.CssSelector("div.row-fluid a.pull-left div.logo"), 60, "Result page is not displayed within 60 Sec"), "Search Page load time is");
        }

        public void ClickFlightOnly()
        {
            Logger.AddClickAction("FlightOnly");
            var searchOptions = GetSearchOptionsButtons();
            searchOptions[1].Click();
            data.SearchOption = SearchOption.FlightOnly;
        }

        private static ReadOnlyCollection<IWebElement> GetSearchOptionsButtons()
        {
            return Driver.Instance.FindElementsWithTimeout(By.CssSelector("div#searchform.search-box form.slim-form div.search-type div.btn-small"), 10);
        }

        public void ClickHotelOnly()
        {
            Logger.AddClickAction("HotelOnly");
            var searchOptions = GetSearchOptionsButtons();
            searchOptions[0].Click();
            data.SearchOption = SearchOption.HotelOnly;
        }

        public void ClickFlightAndHotel()
        {
            Logger.AddClickAction("FlightAndHotel");
            var searchOptions = GetSearchOptionsButtons();
            searchOptions[0].Click();
            data.SearchOption = SearchOption.FlightAndHotel;
        }

        public void TypeFlightDestination(string destination)
        {
            Logger.AddTypeAction("FlightDestination", destination);
            //Enter destination in destination text field
            WebControls.TypeAndSelectDropDown("DestinationAirportName", "ui-id-2", destination);
            data.Destination = destination;
        }

        public void TypeHotelDestination(string destination)
        {
            Logger.AddTypeAction("HotelDestination", destination);
            //Enter destination in destination text field
            WebControls.TypeAndSelectDropDown("Destination", "ui-id-1", destination);
            data.Destination = destination;
        }

        public void SelectCheckIn(string checkIn)
        {
            if (string.IsNullOrEmpty(checkIn)) throw new ArgumentNullException("checkIn");
            if (DateTime.Parse(checkIn) < DateTime.Today) throw new ArgumentOutOfRangeException("checkIn", checkIn, "Checkin date cannot be in the past.");
            Logger.AddSelectAction("CheckIn", checkIn);
            WebControls.SelectDateBox("fromDate", "/html/body/div[3]", checkIn);
            data.CheckInDate = Calendar.FormatDate(checkIn);
        }

        public void SelectCheckOut(string checkOut)
        {
            if (string.IsNullOrEmpty(checkOut)) throw new ArgumentNullException("checkOut");
            if (DateTime.Parse(checkOut) < DateTime.Today) throw new ArgumentOutOfRangeException("checkOut", checkOut, "Checkout date cannot be in the past.");
            Logger.AddSelectAction("CheckOut", checkOut);
            WebControls.SelectDateBox("toDate", "/html/body/div[4]", checkOut);
            data.CheckOutDate = Calendar.FormatDate(checkOut);
        }

        public void SelectAirport(string deptAirport)
        {
            Logger.AddSelectAction("Airport", deptAirport);
            var dropdownDepartureAirport = Driver.Instance.FindElement(By.XPath("//*[@data-id='departureAirportName']"));
            WebControls.SelectDropDown(dropdownDepartureAirport, deptAirport);
            data.DepartureAirport = deptAirport;
        }

        public void SelectAdults(int adults)
        {
            if (adults < 1 || adults > 6) throw new ArgumentOutOfRangeException("adults", adults, "The number of adults must be between 1 and 6.");
            Logger.AddSelectAction("Adults", adults);
            string adultsl = "0 Adults";
            if (adults == 1)
                adultsl = string.Format("{0} Adult", adults);
            else if (adults > 1)
                adultsl = string.Format("{0} Adults", adults);
            ReadOnlyCollection<IWebElement> dropdownAdults = Driver.Instance.FindElements(By.XPath("//button[contains(@data-id, 'adultList')]"));
            IWebElement dropdownAdult = dropdownAdults.Last();
            WebControls.SelectDropDown(dropdownAdult, adultsl);
            data.Rooms.Last().AdultsLabel = adultsl;
            data.Rooms.Last().Adults = adults;
        }

        public void SelectChildren(int children, int[] childrenAges)
        {
            if (children < 0 || children > 4) throw new ArgumentOutOfRangeException("children", children, "The number of children must be between 0 and 4.");
            if (childrenAges == null) throw new ArgumentNullException("childrenAges");
            if (children != childrenAges.Length) throw new ArgumentException("children ages must have the same number of elements as number of children.");
            if (childrenAges.Any(i => i < 0 || i > 12)) throw new ArgumentOutOfRangeException("Children age must be between 0 and 12.");
            Logger.AddSelectAction("Children", children);
            Logger.AddSelectAction("ChildrenAge", childrenAges);
            string Childrenl = "0 Children";
            if (children == 1)
                Childrenl = string.Format("{0} Child", children);
            else if (children > 1)
                Childrenl = string.Format("{0} Children", children);
            ReadOnlyCollection<IWebElement> dropdownChildrens = Driver.Instance.FindElements(By.XPath("//button[contains(@data-id, 'childrenList') or contains(@data-id, 'childList')]"));
            IWebElement dropdownChildren = dropdownChildrens.Last();
            WebControls.SelectDropDown(dropdownChildren, Childrenl);
            if (Childrenl != "0 Children")
            {
                List<int> childrenAge = new List<int>();
                ReadOnlyCollection<IWebElement> childageDropDownsDivs = Driver.Instance.FindElements(By.XPath("//*[@class='roomform']"));
                IWebElement childageDropDownsDiv = childageDropDownsDivs[childageDropDownsDivs.Count - 1];
                ReadOnlyCollection<IWebElement> childAgeBtnGroup = childageDropDownsDiv.FindElements(By.XPath(".//*[@class='spanfix span3 childAge' or @class='span3 childAge']"));
                if (childAgeBtnGroup.Count == children)
                {
                    int counter = 0;
                    foreach (IWebElement btngroup in childAgeBtnGroup)
                    {
                        try
                        {
                            btngroup.FindElement(By.CssSelector("button.btn")).Click();

                            ReadOnlyCollection<IWebElement> liAgeDropDown = btngroup.FindElements(By.TagName("li"));
                            IWebElement age = liAgeDropDown[childrenAges[counter] + 1];
                            age.FindElement(By.TagName("a")).Click();
                            counter++;
                        }
                        catch (NoSuchElementException)
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    throw new Exception("The number of children selected doesn't match with the number of 'age' boxes appeared on the page");
                }
            }
            data.Rooms.Last().ChildrenLabel = Childrenl;
            data.Rooms.Last().Children = children;
            data.Rooms.Last().ChildrenAges = childrenAges;
        }
    }
}
