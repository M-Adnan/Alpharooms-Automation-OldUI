using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Threading;
using AlphaRooms.AutomationFramework;
using AlphaRooms.AutomationFramework.PageData;
using AlphaRooms.AutomationFramework.Functions;
using AlphaRooms.AutomationFramework.Panels;
using AlphaRooms.AutomationFramework.Processes.FlightResults;
using AlphaRooms.AutomationFramework.Selenium;

namespace AlphaRooms.AutomationFramework
{
    public static class FlightResultsPage
    {
        private static FlightResultsPageData data = new FlightResultsPageData();
        public static FlightResultsPageData Data { get { return data; } }

        private static TopPanel topPanel = new TopPanel();
        public static TopPanel TopPanel { get { return topPanel; } }

        private static ChangeSearchPanel changeSearchPanel = new ChangeSearchPanel();
        public static ChangeSearchPanel ChangeSearchPanel { get { return changeSearchPanel; } }

        internal static void WaitToLoad()
        {
            FlightResultsPage.ResetData();
            //Wait for java script to load
            FlightResultsPage.Data.LoadingTime = NonFunctionalReq.CaptureTime(() => Driver.Instance.WaitForAjax(), "Flight Result Page Rendering Time");
        }

        internal static void ResetData()
        {
            FlightResultsPage.data = new FlightResultsPageData();
            FlightResultsPage.topPanel = new TopPanel();
            FlightResultsPage.changeSearchPanel = new ChangeSearchPanel();
        }

        public static bool IsDisplayed()
        {
            return Driver.Instance.IsElementDisplayedById("flightresults");
        }

        private static ReadOnlyCollection<IWebElement> GetFlightResultPanels()
        {
            IWebElement searchResults = Driver.Instance.FindElement(By.CssSelector("div.span9 div.results-wrap div"));
            return searchResults.FindElements(By.CssSelector("div.flights-result-box"));
        }

        private static IWebElement GetFlightPanel(int flightNumber)
        {
            return GetFlightResultPanels()[flightNumber - 1].FindElement(By.XPath("div[2]"));
        }

        private static IWebElement GetAvailableFlightButton(int flightNumber)
        {
            IWebElement fullpriceinformation = GetFlightPanel(flightNumber).FindElement(By.CssSelector("div.select-btn-container"));
            return fullpriceinformation.FindElement(By.CssSelector("div.row-fluid form div.select-flight-btn"));
        }

        public static void ClickResultPage(int pageNo)
        {
            if (pageNo < 1) throw new ArgumentOutOfRangeException("pageNo", pageNo, "The page number must be 1 or higher.");
            Logger.AddClickAction("FlightPage", "PageNumber", pageNo);
            TopResultPagination.SelectPage(pageNo);
            FlightResultsPage.Data.ResultPageNo = pageNo;
            Driver.Instance.WaitForAjax();
            Thread.Sleep(2000);
        }

        public static void ClickFlight(int flightNumber)
        {
            if (flightNumber < 1) throw new ArgumentOutOfRangeException("flightNumber", flightNumber, "The flight number must be 1 or higher.");
            Logger.AddClickAction("Flight", "FlightNumber", flightNumber);
            SaveSearchGUID();
            SaveFlightData(flightNumber);
            IWebElement flightBookNowBtn = GetAvailableFlightButton(flightNumber);
            NonFunctionalReq.ExecuteAndRetry(() => flightBookNowBtn.Click(), "Selected flight is Ryanair.");

            //capture screenshot
            try
            {
                if (HomePage.Data.SearchOption == SearchOption.FlightOnly)
                {
                    ExtrasPage.WaitForLoad();
                }
                else if (HomePage.Data.SearchOption == SearchOption.FlightAndHotel)
                {
                    HotelResultsPage.WaitToLoad();
                }
            }
            catch (Exception ex)
            {
                if (Driver.Instance.IsElementDisplayedBy(By.CssSelector("div.alert h4")))
                    throw new Exception(string.Format("Flight Number {0} is no longer available.", flightNumber));
                if (Driver.Instance.IsElementDisplayedBy(By.Id("search-timeout")))
                    throw new Exception(string.Format("Sun Error Page is displayed for Flight Number {0}", flightNumber));
                throw ex;
            }
        }

        public static void ClickFlightAndCapture(int flightNumber)
        {
            if (flightNumber < 1) throw new ArgumentOutOfRangeException("flightNumber", flightNumber, "The flight number must be 1 or higher.");
            Logger.AddClickAction("FlightAndCapture", "FlightNumber", flightNumber);
            SaveSearchGUID();
            SaveFlightData(flightNumber);
            IWebElement flightBookNowBtn = GetAvailableFlightButton(flightNumber);
            NonFunctionalReq.ExecuteAndRetry(() => flightBookNowBtn.Click(), "Selected flight is Ryanair.");

            //capture screenshot
            try
            {
                if (HomePage.Data.SearchOption == SearchOption.FlightOnly)
                {
                    ExtrasPage.WaitForLoad();

                    //capture screenshot
                    //NonFunctionalReq.GetScreenShot("Hotel Booking Summary Page");
                }
                else if (HomePage.Data.SearchOption == SearchOption.FlightAndHotel)
                {
                    HotelResultsPage.WaitToLoad();

                    //capture screenshot
                    //NonFunctionalReq.GetScreenShot("Hotel Booking Summary Page");
                }
            }
            catch (Exception ex)
            {
                if (Driver.Instance.IsElementDisplayedBy(By.CssSelector("div.alert h4")))
                    throw new Exception(string.Format("Flight Number {0} is no longer available.", flightNumber));
                if (Driver.Instance.IsElementDisplayedBy(By.Id("search-timeout")))
                    throw new Exception(string.Format("Sun Error Page is displayed for Flight Number {0}", flightNumber));
                throw ex;
            }
        }

        public static bool IsFlexibleExpanded
        {
            get { return  Driver.Instance.IsElementDisplayedBy(By.ClassName("alt-days-navbar")); }
        }

        public static void ExpandFlexible()
        {
            if (!IsFlexibleExpanded)Driver.Instance.FindElement(By.CssSelector("a.flexible-text-orientation i.icon-chevron-down")).Click();
        }

        public static void ClickSearchDateRange(SearchDateRanges range)
        {
            Logger.AddClickAction("SearchDateRange", "DateRange", range);
            IWebElement dateRanges = Driver.Instance.FindElement(By.ClassName("alt-days-navbar"));
            ReadOnlyCollection<IWebElement> rows = dateRanges.FindElements(By.TagName("tr"));
            IWebElement rangeRow = rows[1 + (int)range];
            IWebElement selectButton = rangeRow.FindElement(By.CssSelector("td div.pull-right"));
            selectButton.Click();
        }

        public static bool ContainsFlightFromSupplier(string supplier)
        {
            return FindFlightForFirstSupplierorProvider(supplier,2) > -1;
        }

        public static bool ContainsFlightFromProvider(string supplier)
        {
            return false;//FindFlightForFirstSupplierprovider(supplier, 1) > -1;
        }


        

        public static int FindFlightForFirstSupplierorProvider(string supplier, int arrayindex)
        {
            ReadOnlyCollection<IWebElement> divallSearchResults;
            divallSearchResults = GetFlightResultPanels();
            //IWebElement searchResult;
            //int i = 0;
            for (int i = 0; i <= divallSearchResults.Count-1; i++)
            {
                //IWebElement searchResult=divallSearchResults[i-1];
                string[] staffInfoArray = GetStaffInformation(divallSearchResults[i]);
                string csupplier = staffInfoArray[arrayindex].Substring(staffInfoArray[arrayindex].IndexOf(':')+2);

                if (csupplier == supplier)
                {
                    FlightResultsPage.Data.SupplierFlightNo = i + 1;
                    return i+1;
                }
                else if (i == divallSearchResults.Count-1)
                {
                    IWebElement ulPagination = Driver.Instance.FindElement(By.CssSelector("div.results-wrap div div.pagination ul"));
                    ReadOnlyCollection<IWebElement> liPagination = ulPagination.FindElements(By.TagName("li"));
                    try
                    {
                        Driver.Instance.TurnOffWait();
                        IWebElement nextPaginationLink = liPagination[liPagination.Count-1].FindElement(By.CssSelector("a[class='disabled']"));
                        Driver.Instance.TurnOnWait();
                        return -1;
                    }
                    catch
                    {
                        Driver.Instance.TurnOnWait();
                        liPagination[liPagination.Count-1].FindElement(By.CssSelector("a")).Click();
                        Driver.Instance.WaitForAjax();
                        divallSearchResults = GetFlightResultPanels();
                        i = -1;
                    }

                    /*int k =0;
                    foreach (IWebElement searchResult1 in divallSearchResults)
                    {
                        IWebElement ulPagination = Driver.Instance.FindElement(By.CssSelector("div.results-wrap div div.pagination ul"));
                        ReadOnlyCollection<IWebElement> liPagination = ulPagination.FindElements(By.TagName("li"));
                        try
                        {
                            IWebElement nextPaginationLink = liPagination[liPagination.Count].FindElement(By.CssSelector("a[class='disabled']"));
                            return -1;
                        }
                        catch
                        {
                            liPagination[liPagination.Count].FindElement(By.CssSelector("a")).Click();
                            Driver.Instance.WaitForAjax();
                            i = -1;
                        }

                        //string[] staffInfoArray1 = GetStaffInformation(divallSearchResults[k]);
                        //string csupplier1 = staffInfoArray1[4].Substring(staffInfoArray[4].IndexOf(':') + 2);

                        if (csupplier1 == supplier)
                        {
                            FlightResultsPage.Data.SupplierFlightNo = k + 1;
                            return i + 1;
                        }
                        k++;
                    }*/

                }
                //i++;
            }
            return -1;
        }

        private static String[] GetStaffInformation(IWebElement divallSearchResults)
        {
            var fullresult = divallSearchResults.FindElement(By.XPath("div[2]"));
            var fullflightinformation = fullresult.FindElement(By.CssSelector("div.span10"));
            var staffinformation = fullresult.FindElement(By.CssSelector("div.span11"));
            string fullstaffInfo = staffinformation.Text;
            if (string.IsNullOrWhiteSpace(fullstaffInfo)) throw new Exception("Staff info is not available.");
            string[] staffInfoArray = fullstaffInfo.Split(';');
            return staffInfoArray;
        }

        public static bool ValidateResultDates()
        {
            ReadOnlyCollection<IWebElement> divallSearchResults = GetFlightResultPanels();
            int i = 0;
            foreach (IWebElement searchResult in divallSearchResults)
            {
                var fullresult = searchResult.FindElement(By.XPath("div[2]"));
                var fullflightinformation = fullresult.FindElement(By.CssSelector("div.span10"));

                IWebElement outBoundFlightInfo = fullflightinformation.FindElement(By.XPath("div/div[1]"));
                IWebElement fullOutBoundDepartureFlightInfo = outBoundFlightInfo.FindElement(By.XPath("div[2]"));

                IWebElement inBoundFlightInfo = fullflightinformation.FindElement(By.XPath("div/div[2]"));
                IWebElement fullInBoundDepartureFlightInfo = inBoundFlightInfo.FindElement(By.XPath("div[2]"));

                IWebElement outBoundDepartureFlightTime = fullOutBoundDepartureFlightInfo.FindElement(By.XPath("div[2]"));
                IWebElement outBoundDepartureFlightDate = outBoundDepartureFlightTime.FindElement(By.CssSelector("span.hidden-phone"));

                IWebElement inBoundDepartureFlightTime = fullInBoundDepartureFlightInfo.FindElement(By.XPath("div[2]"));
                IWebElement inBoundDepartureFlightDate = inBoundDepartureFlightTime.FindElement(By.CssSelector("span.hidden-phone"));

                if (outBoundDepartureFlightDate.Text == HomePage.Data.CheckInDate && inBoundDepartureFlightDate.Text == HomePage.Data.CheckOutDate)
                {
                    if (i == divallSearchResults.Count-1)
                        return true;
                }
                else
                    return false;
                i++;
            }
            return false;
        }

        public static bool ValidateResultDestinations()
        {
            ReadOnlyCollection<IWebElement> divallSearchResults = GetFlightResultPanels();
            int i = 0;
            foreach (IWebElement searchResult in divallSearchResults)
            {
                var fullresult = searchResult.FindElement(By.XPath("div[2]"));
                var fullflightinformation = fullresult.FindElement(By.CssSelector("div.span10"));

                IWebElement outBoundFlightInfo = fullflightinformation.FindElement(By.XPath("div/div[1]"));
                IWebElement fullOutBoundDepartureFlightInfo = outBoundFlightInfo.FindElement(By.XPath("div[2]"));
                IWebElement fullOutBoundArrivalFlightInfo = outBoundFlightInfo.FindElement(By.XPath("div[3]"));
                IWebElement outBoundDepartureAiport = fullOutBoundDepartureFlightInfo.FindElement(By.XPath("div[1]"));
                IWebElement outBoundArrivalAiport = fullOutBoundArrivalFlightInfo.FindElement(By.XPath("div[1]"));

                IWebElement inBoundFlightInfo = fullflightinformation.FindElement(By.XPath("div/div[2]"));
                IWebElement fullInBoundDepartureFlightInfo = inBoundFlightInfo.FindElement(By.XPath("div[2]"));
                IWebElement fullInBoundArrivalFlightInfo = inBoundFlightInfo.FindElement(By.XPath("div[3]"));
                IWebElement inBoundDepartureAiport = fullInBoundDepartureFlightInfo.FindElement(By.XPath("div[1]"));
                IWebElement inBoundArrivalAiport = fullInBoundArrivalFlightInfo.FindElement(By.XPath("div[1]"));

                if (outBoundDepartureAiport.Text == HomePage.Data.DepartureAirport && outBoundArrivalAiport.Text == HomePage.Data.Destination
                    && inBoundDepartureAiport.Text == HomePage.Data.Destination && inBoundArrivalAiport.Text == HomePage.Data.DepartureAirport)
                {
                    if (i == divallSearchResults.Count - 1)
                        return true;
                }
                else
                    return false;
                i++;
            }
            return false;
        }

        public static bool ContainsFlightFromAirline(string airline)
        {
            return FindFlightForFirstAirline(airline) > -1;
        }

        public static int FindFlightForFirstAirline(string airline)
        {
            ReadOnlyCollection<IWebElement> divallSearchResults = GetFlightResultPanels();
            int i = 0;
            foreach (IWebElement searchResult in divallSearchResults)
            {
                var fullresult = searchResult.FindElement(By.XPath("div[2]"));
                var fullflightinformation = fullresult.FindElement(By.CssSelector("div.span10"));
                IWebElement outBoundFlightInfo = fullflightinformation.FindElement(By.XPath("div/div[1]"));
                string airlineName = outBoundFlightInfo.FindElement(By.TagName("img")).GetAttribute("alt");

                if (airlineName == airline)
                {
                    return i + 1;
                }
                i++;
            }
            return -1;
        }

        public static SelectFlightStarter SelectFlight()
        {
            return new SelectFlightStarter();
        }

        private static void SaveFlightData(int flightNumber)
        {
            var fullresult = GetFlightPanel(flightNumber);
            var fullflightinformation = fullresult.FindElement(By.CssSelector("div.span10"));

            IWebElement outBoundFlightInfo = fullflightinformation.FindElement(By.XPath("div/div[1]"));
            IWebElement inBoundFlightInfo = fullflightinformation.FindElement(By.XPath("div/div[2]"));

            FlightResultsPage.Data.OutboundAirline = outBoundFlightInfo.FindElement(By.TagName("img")).GetAttribute("alt");

            IWebElement fullOutBoundDepartureFlightInfo = outBoundFlightInfo.FindElement(By.XPath("div[2]"));

            IWebElement outBoundDepartureAiport = fullOutBoundDepartureFlightInfo.FindElement(By.XPath("div[1]"));
            FlightResultsPage.Data.OutboundDepartureAirport = outBoundDepartureAiport.Text;

            IWebElement outBoundDepartureFlightTime = fullOutBoundDepartureFlightInfo.FindElement(By.XPath("div[2]"));
            FlightResultsPage.Data.OutboundDepartureTime = string.Concat(outBoundDepartureFlightTime.Text.Remove(0, 6), " ", outBoundDepartureFlightTime.Text.Remove(5));
            IWebElement outBoundFlightNumber = fullOutBoundDepartureFlightInfo.FindElement(By.XPath("div[3]"));
            FlightResultsPage.Data.OutboundFlightNo = outBoundFlightNumber.Text;

            IWebElement fullOutBoundArrivalFlightInfo = outBoundFlightInfo.FindElement(By.XPath("div[3]"));

            IWebElement outBoundArrivalAiport = fullOutBoundArrivalFlightInfo.FindElement(By.XPath("div[1]"));
            FlightResultsPage.Data.OutboundArrivalAirport = outBoundArrivalAiport.Text;
            IWebElement outBoundArrivalFlightTime = fullOutBoundArrivalFlightInfo.FindElement(By.XPath("div[2]"));
            FlightResultsPage.Data.OutboundArrivalTime = string.Concat(outBoundArrivalFlightTime.Text.Remove(0, 6), " ", outBoundArrivalFlightTime.Text.Remove(5));
            IWebElement outBoundFlightType = fullOutBoundArrivalFlightInfo.FindElement(By.XPath("div[3]"));
            FlightResultsPage.Data.OutboundFlightType = outBoundFlightType.Text;

            IWebElement outBoundFlightJourneyDetails = outBoundFlightInfo.FindElement(By.XPath("div[4]"));
            IWebElement outBoundFlightStops = outBoundFlightJourneyDetails.FindElement(By.CssSelector("span"));
            FlightResultsPage.Data.OutboundJourney = outBoundFlightStops.Text;
            IWebElement outBoundFlightTime = outBoundFlightJourneyDetails.FindElement(By.XPath("div/span"));
            FlightResultsPage.Data.OutboundJourneyTime = outBoundFlightTime.Text;


            FlightResultsPage.Data.InboundAirline = inBoundFlightInfo.FindElement(By.TagName("img")).GetAttribute("alt");

            IWebElement fullInBoundDepartureFlightInfo = inBoundFlightInfo.FindElement(By.XPath("div[2]"));

            IWebElement inBoundDepartureAiport = fullInBoundDepartureFlightInfo.FindElement(By.XPath("div[1]"));
            FlightResultsPage.Data.InboundDepartureAirport = inBoundDepartureAiport.Text;
            IWebElement inBoundDepartureFlightTime = fullInBoundDepartureFlightInfo.FindElement(By.XPath("div[2]"));
            FlightResultsPage.Data.InboundDepartureTime = string.Concat(inBoundDepartureFlightTime.Text.Remove(0, 6), " ", inBoundDepartureFlightTime.Text.Remove(5));
            IWebElement inBoundFlightNumber = fullInBoundDepartureFlightInfo.FindElement(By.XPath("div[3]"));
            FlightResultsPage.Data.InboundFlightNo = inBoundFlightNumber.Text;

            IWebElement fullInBoundArrivalFlightInfo = inBoundFlightInfo.FindElement(By.XPath("div[3]"));

            IWebElement inBoundArrivalAiport = fullInBoundArrivalFlightInfo.FindElement(By.XPath("div[1]"));
            FlightResultsPage.Data.InboundArrivalAirport = inBoundArrivalAiport.Text;
            IWebElement inBoundArrivalFlightTime = fullInBoundArrivalFlightInfo.FindElement(By.XPath("div[2]"));
            FlightResultsPage.Data.InboundArrivalTime = string.Concat(inBoundArrivalFlightTime.Text.Remove(0, 6), " ", inBoundArrivalFlightTime.Text.Remove(5));
            IWebElement inBoundFlightType = fullInBoundArrivalFlightInfo.FindElement(By.XPath("div[3]"));
            FlightResultsPage.Data.InboundFlightType = inBoundFlightType.Text;

            IWebElement inBoundFlightJourneyDetails = inBoundFlightInfo.FindElement(By.XPath("div[4]"));
            IWebElement inBoundFlightStops = inBoundFlightJourneyDetails.FindElement(By.CssSelector("span"));
            FlightResultsPage.Data.InboundJourney = inBoundFlightStops.Text;
            IWebElement inBoundFlightTime = inBoundFlightJourneyDetails.FindElement(By.XPath("div/span"));
            FlightResultsPage.Data.InboundJourneyTime = inBoundFlightTime.Text;

            var fullpriceinformation = fullresult.FindElement(By.CssSelector("div.select-btn-container"));

            IWebElement flightPrice = fullpriceinformation.FindElement(By.CssSelector("div.row-fluid div.price"));
            FlightResultsPage.Data.Price = decimal.Parse(flightPrice.Text.Substring(1));
        }

        private static void SaveFlightPriceInfo(ReadOnlyCollection<IWebElement> flightPrice)
        {
            FlightResultsPage.Data.Price = decimal.Parse(flightPrice[0].Text.Substring(1));
        }

        public static bool AreResultsDisplayed()
        {
            try
            {
                SaveSearchGUID();
                return GetFlightResultPanels().Count > 0;
            }
            catch
            {
                return false;
            }
        }

        private static void SaveSearchGUID()
        {
            string url = Driver.Instance.Url;
            string searchID = url.Split(new char[] { '=', '&' })[1];
            FlightResultsPage.Data.FlightSearchGuid = searchID;
        }

        public static bool ValidateSearchCriteria()
        {
            IWebElement flightResultsHeader = Driver.Instance.FindElement(By.CssSelector("div.row-fluid div.span12 strong"));
            string text = flightResultsHeader.Text;
            if (text.IndexOf(HomePage.Data.DepartureAirport.Split(',')[0], StringComparison.CurrentCultureIgnoreCase) != 0)
            {
                return false;
            }
            if (text.IndexOf(HomePage.Data.Destination.Split(',')[0], StringComparison.CurrentCultureIgnoreCase) <= 0)
            {
                return false;
            }
            string textDate = text.Split('-')[1].Trim();
            if (!string.Equals(textDate.Substring(0, textDate.Length / 2), HomePage.Data.CheckInDate, StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }
            if (!string.Equals(textDate.Substring(textDate.Length / 2).Trim(), HomePage.Data.CheckOutDate, StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }

            return true;
        }
    }
}
