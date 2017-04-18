using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using AlphaRooms.AutomationFramework.Functions;
using AlphaRooms.AutomationFramework.Selenium;

namespace AlphaRooms.AutomationFramework
{
    public static class FlightResultsPageRnd
    {
        private static int GetNumberOfResults()
        {
            var allSearchResults = Driver.Instance.FindElementWithTimeout(By.CssSelector("div.span9 div.results-wrap div"), 5, "No results found for the search");
            ReadOnlyCollection<IWebElement> divallSearchResults = allSearchResults.FindElements(By.CssSelector("div.flights-result-box"));
            return divallSearchResults.Count;
        }

        private static int PickRandomFlightNo(int totalFlights)
        {
            if (totalFlights > 0)
            {
                Random r = new Random();
                int flightNo = r.Next(1, totalFlights);
                return flightNo;
            }
            else
                throw new Exception("No search results available");
        }

        public static int PickRandomResultPage()
        {
            int totalResultPages = TopResultPagination.GetNumberOfPages();
            int randPageNo = TopResultPagination.GenerateRandPageNo(totalResultPages);
            return randPageNo;
        }

        public static int PickRandomFlight()
        {
            int totalFlightResults = GetNumberOfResults();
            int randFlight = PickRandomFlightNo(totalFlightResults);
            return randFlight;
        }
    }
}
