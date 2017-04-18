using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaRooms.AutomationFramework.Processes.Extras.Interfaces;

namespace AlphaRooms.AutomationFramework.Processes.Extras
{
    public class BookFlightNowStarter
    {
        public IBookFlightNowAirportTransfer AddAirportTransfer()
        {
            return new BookFlightNow();
        }

        public IBookFlightNowAirportParking AddAirportParking()
        {
            return new BookFlightNow();
        }

        public void Continue()
        {
            ExtrasPage.ClickBookNow();
        }

        public void ContinueAndCapture()
        {
            ExtrasPage.ClickBookNowAndCapture();
        }
    }
}
