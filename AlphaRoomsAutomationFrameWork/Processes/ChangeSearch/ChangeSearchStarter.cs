using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaRooms.AutomationFramework.Processes.ChangeSearch.Interfaces;

namespace AlphaRooms.AutomationFramework.Processes.ChangeSearch
{
    public class ChangeSearchStarter
    {
        private Panels.ChangeSearchPanel panel;

        public ChangeSearchStarter(Panels.ChangeSearchPanel panel)
        {
            this.panel = panel;
        }

        public ISearchFlightOnly FlightOnly()
        {
            return new ChangeSearchFlightOnly(this.panel);
        }

        public ISearchHotelOnly HotelOnly()
        {
            return new ChangeSearchHotelOnly(this.panel);
        }

        public ISearchFlightAndHotel FlightAndHotel()
        {
            return new ChangeSearchFlightAndHotel(this.panel);
        }
    }
}
