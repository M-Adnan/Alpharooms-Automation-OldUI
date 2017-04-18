using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support;
using AlphaRooms.AutomationFramework;
using System.Threading;
using AlphaRooms.AutomationFramework.PageData;
using AlphaRooms.AutomationFramework.Functions;
using AlphaRooms.AutomationFramework.Panels;
using AlphaRooms.AutomationFramework.Processes.Home;
using AlphaRooms.AutomationFramework.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AlphaRooms.AutomationFramework
{
    public class HomePage
    {
        private static HomePageData data = new HomePageData();
        public static HomePageData Data { get { return data; } }

        private static TopPanel topPanel = new TopPanel();
        public static TopPanel TopPanel { get { return topPanel; } }

        internal static void ResetData()
        {
            Logger.Clear();
            HomePage.data = new HomePageData();
            HomePage.topPanel = new TopPanel();
        }

        //Select enviorment to run the automation tests
        public static void Navigate(TestEnvironment Env)
        {

            switch (Env)
            {
                case TestEnvironment.Live:
                    //Capture Page Load Times
                    NonFunctionalReq.CaptureTime(() => Driver.Instance.Navigate().GoToUrl("https://www.alpharooms.com"), "Landing Page load time is");
                    break;

                case TestEnvironment.Staging:
                    //Capture Page Load Times
                    NonFunctionalReq.CaptureTime(() => Driver.Instance.Navigate().GoToUrl("https://stagingaccomsoaweb.alpharooms.com/"), "Landing Page load time is");
                    break;

                case TestEnvironment.QA:
                    //Capture Page Load Times
                    NonFunctionalReq.CaptureTime(() => Driver.Instance.Navigate().GoToUrl("http://flightshotfix.alpha2.com/"), "Landing Page load time is");
                    break;


                case TestEnvironment.SOAFlights:
                    //Capture Page Load Times
                    NonFunctionalReq.CaptureTime(() => Driver.Instance.Navigate().GoToUrl("http://flights3.alpha2.com/"), "Landing Page load time is");
                    break;
            }
            Logger.Clear();
            HomePage.ResetData();
            FlightResultsPage.ResetData();
            HotelResultsPage.ResetData();
            HotelDetailPage.ResetData();
            ExtrasPage.ResetData();
            InsurancePage.ResetData();
            PaymentPage.ResetData();
        }

        //Home
        public static bool IsDisplayed()
        {
            return Driver.Instance.IsElementDisplayedById("homepage");
        }

        private static IWebElement GetSearchButton()
        {
            return Driver.Instance.FindElement(By.Id("searchFormSubmit"));
        }

        private static ReadOnlyCollection<IWebElement> GetSearchOptionsButtons()
        {
            return Driver.Instance.FindElementsWithTimeout(By.CssSelector("div#searchform div.search-box-tabs span.tab"), 0);
        }

        public static void ClickAddAnotherRoom()
        {
            Logger.AddClickAction("AddAnotherRoom");
            Driver.Instance.FindElement(By.CssSelector("i.icon-plus")).Click();
            HomePage.Data.AddRoom();
        }

        public static void ClickSearch()
        {
            Logger.AddClickAction("Search");

            //click search button
            GetSearchButton().Click();

            //capture the page load time
            //HomePage.Data.SearchTime = NonFunctionalReq.CaptureTime(() => Driver.Instance.FindElementWithTimeout(By.CssSelector("div.row-fluid a.pull-left div.logo"), 65, "Result page is not displayed within 60 Sec"), "Search Page load time is");

            switch (HomePage.Data.SearchOption)
            {
                case SearchOption.FlightOnly:
                    //Wait for java script to load
                    FlightResultsPage.WaitToLoad();
                    break;

                case SearchOption.HotelOnly:
                    //Wait for java script to load
                    HotelResultsPage.WaitToLoad();
                    break;

                case SearchOption.FlightAndHotel:
                    //Wait for java script to load
                    FlightResultsPage.WaitToLoad();
                    break;
            }
        }

        public static void ClickSearchAndCapture()
        {
            Logger.AddClickAction("SearchAndCapture");

            //click search button
            GetSearchButton().Click();

            //capture the page load time
            HomePage.Data.SearchTime = NonFunctionalReq.CaptureTime(() => Driver.Instance.FindElementWithTimeout(By.CssSelector("div.row-fluid div.pull-left.logo-menu-container"), 60, "Result page is not displayed within 60 Sec"), "Search Page load time is");

            switch (HomePage.Data.SearchOption)
            {
                case SearchOption.FlightOnly:
                    //Wait for java script to load
                    FlightResultsPage.WaitToLoad();

                    //capture screenshot
                    //NonFunctionalReq.GetScreenShot("Hotel Booking Summary Page");
                    break;

                case SearchOption.HotelOnly:
                    //Wait for java script to load
                    HotelResultsPage.WaitToLoad();

                    //capture screenshot
                    //NonFunctionalReq.GetScreenShot("Hotel Booking Summary Page");
                    break;

                case SearchOption.FlightAndHotel:
                    //Wait for java script to load
                    FlightResultsPage.WaitToLoad();

                    //capture screenshot
                    //NonFunctionalReq.GetScreenShot("Hotel Booking Summary Page");
                    break;
            }
        }

        public static void ClickFlightOnly()
        {
            Logger.AddClickAction("FlightOnly");
            var searchOptions = GetSearchOptionsButtons();
            searchOptions[1].FindElement(By.CssSelector("span.tab label span")).Click();
            HomePage.Data.SearchOption = SearchOption.FlightOnly;
        }

        public static void ClickHotelOnly()
        {
            Logger.AddClickAction("HotelOnly");
            var searchOptions = GetSearchOptionsButtons();
            searchOptions[0].FindElement(By.CssSelector("span.tab label span")).Click();
            HomePage.Data.SearchOption = SearchOption.HotelOnly;
        }

        public static void ClickFlightAndHotel()
        {
            Logger.AddClickAction("FlightAndHotel");
            var searchOptions = GetSearchOptionsButtons();
            searchOptions[2].FindElement(By.CssSelector("span.tab label span")).Click();
            HomePage.Data.SearchOption = SearchOption.FlightAndHotel;
        }

        //fluid coding style enters all required information for SearchFlight only
        public static SearchStarter SearchFor()
        {
            return new SearchStarter();
        }

        public static void TypeFlightDestination(string destination)
        {
            Logger.AddTypeAction("FlightDestination", destination);
            //Enter destination in destination text field
            WebControls.TypeAndSelectDropDown("DestinationAirportName", "ui-id-2", destination);
            HomePage.Data.Destination = destination;
        }

        //public static void TypeHotelDestination(string destination)
        //{
        //    Logger.AddTypeAction("HotelDestination", destination);
        //    //Enter destination in destination text field
        //    WebControls.TypeAndSelectDropDown("Destination", "ui-id-1", destination);
        //    HomePage.Data.Destination = destination;
        //}

        public static void TypeHotelDestination(string destination, bool isHotel)
        {
            Logger.AddTypeAction("HotelDestination", destination);
            //Enter destination in destination text field
            WebControls.TypeAndSelectDestinationDropDown("Destination", "ui-id-1", destination, isHotel);
            HomePage.Data.Destination = destination;
        }

        public static void SelectCheckIn(string checkIn)
        {
            Logger.AddSelectAction("CheckIn", checkIn);
            if (string.IsNullOrEmpty(checkIn)) throw new ArgumentNullException("checkIn");
            //if (DateTime.Parse(checkIn) < DateTime.Today) throw new ArgumentOutOfRangeException("checkIn", checkIn, "Checkin date cannot be in the past.");
            //Logger.AddSelectAction("CheckIn", checkIn);
            WebControls.SelectDateBox("fromDate", "/html/body/div[4]", checkIn);
            HomePage.Data.CheckInDate = Calendar.FormatDate(checkIn);
        }

        public static void SelectCheckOut(string checkOut)
        {
            if (string.IsNullOrEmpty(checkOut)) throw new ArgumentNullException("checkOut");
            if (DateTime.Parse(checkOut) < DateTime.Today) throw new ArgumentOutOfRangeException("checkOut", checkOut, "Checkout date cannot be in the past.");
            Logger.AddSelectAction("CheckOut", checkOut);
            WebControls.SelectDateBox("toDate", "/html/body/div[5]", checkOut);
            HomePage.Data.CheckOutDate = Calendar.FormatDate(checkOut);
        }

        public static void SelectAirport(string deptAirport)
        {
            Logger.AddSelectAction("Airport", deptAirport);
            var dropdownDepartureAirport = Driver.Instance.FindElement(By.XPath("//*[@data-id='departureAirportName']"));
            WebControls.SelectDropDown(dropdownDepartureAirport, deptAirport);
            HomePage.Data.DepartureAirport = deptAirport;
        }

        public static void SelectAdults(int adults)
        {
            if (adults < 1 || adults > 6) throw new ArgumentOutOfRangeException("adults", adults, "The number of adults must be between 1 and 6.");
            Logger.AddSelectAction("Adults", adults);
            string adultsl = "0 adults";
            if (adults == 1)
                adultsl = string.Format("{0} adult", adults);
            else if (adults > 1)
                adultsl = string.Format("{0} adults", adults);
            ReadOnlyCollection<IWebElement> dropdownAdults = Driver.Instance.FindElements(By.CssSelector("[class*='selectpicker person-select']"));
            IWebElement dropdownAdult = dropdownAdults.First();
            var selectAdultElement = new SelectElement(dropdownAdult);
            selectAdultElement.SelectByText(adultsl);
            HomePage.Data.Rooms.Last().AdultsLabel = adultsl;
            HomePage.Data.Rooms.Last().Adults = adults;
        }

        public static void SelectChildren(int children, int[] childrenAges)
        {
            if (children < 0 || children > 4) throw new ArgumentOutOfRangeException("children", children, "The number of children must be between 0 and 4.");
            if (childrenAges == null) throw new ArgumentNullException("childrenAges");
            if (children != childrenAges.Length) throw new ArgumentException("children ages must have the same number of elements as number of children.");
            if (childrenAges.Any(i => i < 0 || i > 12)) throw new ArgumentOutOfRangeException("Children age must be between 0 and 12.");
            Logger.AddSelectAction("Children", children);
            Logger.AddSelectAction("ChildrenAge", childrenAges);
            string Childrenl = "0 children";
            if (children == 1)
                Childrenl = string.Format("{0} child", children);
            else if (children > 1)
                Childrenl = string.Format("{0} children", children);
            //ReadOnlyCollection<IWebElement> dropdownChildrens = Driver.Instance.FindElements(By.XPath("//button[contains(@data-id, 'childrenList') or contains(@data-id, 'childList')]"));
            ReadOnlyCollection<IWebElement> dropdownChildrens = Driver.Instance.FindElements(By.CssSelector("[class*='selectpicker person-select']"));
            IWebElement dropdownChildren = dropdownChildrens.Last();
            var selectChildElement = new SelectElement(dropdownChildren);
            selectChildElement.SelectByText(Childrenl);
            if (Childrenl != "0 Children")
            {
                List<int> childrenAge = new List<int>();
                ReadOnlyCollection<IWebElement> childageDropDownsDivs = Driver.Instance.FindElements(By.XPath("//*[@class='child-age']"));
                //IWebElement childageDropDownsDiv = childageDropDownsDivs[childageDropDownsDivs.Count - 1];
                //ReadOnlyCollection<IWebElement> childAgeBtnGroup = childageDropDownsDiv.FindElements(By.XPath(".//*[@class='spanfix span3 childAge' or @class='span3 childAge']"));
                if (childageDropDownsDivs.Count == children)
                {
                    int counter = 0;
                    foreach (IWebElement btngroup in childageDropDownsDivs)
                    {
                        try
                        {
                            var selectAgeElement = new SelectElement(btngroup);
                            selectAgeElement.SelectByText(childrenAges[counter].ToString());


                            /*btngroup.FindElement(By.CssSelector("button.btn")).Click();

                            ReadOnlyCollection<IWebElement> liAgeDropDown = btngroup.FindElements(By.TagName("li"));
                            IWebElement age = liAgeDropDown[childrenAges[counter] + 1];
                            age.FindElement(By.TagName("a")).Click();*/
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
            HomePage.Data.Rooms.Last().ChildrenLabel = Childrenl;
            HomePage.Data.Rooms.Last().Children = children;
            HomePage.Data.Rooms.Last().ChildrenAges = childrenAges;
        }
    }
}
