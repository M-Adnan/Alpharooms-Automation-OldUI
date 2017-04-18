using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.Home.Interfaces
{
    public interface ISearchFlightAndHotel
    {
        ISearchFlightAndHotel ToDestination(string dest);
        ISearchFlightAndHotel FromDepartureAirport(string airport);
        ISearchFlightAndHotel FromCheckIn(string From);
        ISearchFlightAndHotel ToCheckOut(string to);
        ISearchFlightAndHotelRoom AddAnotherRoom();
        ISearchFlightAndHotel ForAdults(int adu);
        ISearchFlightAndHotelChildren WithChildren(int child);
        void Search();
        void SearchAndCapture();
    }
}
