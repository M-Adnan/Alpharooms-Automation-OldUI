using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaRooms.AutomationFramework.Processes.FlightResults.Interfaces;

namespace AlphaRooms.AutomationFramework.Processes.FlightResults
{
    public class SelectFlight : ISelectFlightByFirstSupplier, ISelectFlight
    {
        private int number;

        public SelectFlight(int number)
        {
            this.number = number;
        }

        public void Continue()
        {
            FlightResultsPage.ClickFlight(this.number);
        }

        public void ContinueAndCapture()
        {
            FlightResultsPage.ClickFlightAndCapture(this.number);
        }
    }
}
