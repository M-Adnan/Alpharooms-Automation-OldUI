using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaRooms.AutomationFramework.Processes.FlightResults.Interfaces;

namespace AlphaRooms.AutomationFramework.Processes.FlightResults
{
    public class SelectFlightStarter
    {
        public ISelectFlight ByFlightNumber(int number)
        {
            return new SelectFlight(number);
        }

        public ISelectFlightByFirstSupplier ByFirstSupplier(string supplier)
        {
            int number = FlightResultsPage.FindFlightForFirstSupplierorProvider(supplier, 1);
            return new SelectFlight(number);
        }

        public ISelectFlightByFirstSupplier ByAirline(string airline)
        {
            int number = FlightResultsPage.FindFlightForFirstAirline(airline);
            return new SelectFlight(number);
        }
    }
}
