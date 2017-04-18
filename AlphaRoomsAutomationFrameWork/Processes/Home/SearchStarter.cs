using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaRooms.AutomationFramework.Processes.Home.Interfaces;

namespace AlphaRooms.AutomationFramework.Processes.Home
{
    public class SearchStarter
    {
        public ISearchFlightOnly FlightOnly()
        {
            return new SearchFlightOnly();
        }

        public ISearchHotelOnly HotelOnly()
        {
            return new SearchHotelOnly();
        }

        public ISearchFlightAndHotel FlightAndHotel()
        {
            return new SearchFlightAndHotel();
        }
    }
}
