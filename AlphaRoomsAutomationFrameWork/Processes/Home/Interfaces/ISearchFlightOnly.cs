using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.Home.Interfaces
{
    public interface ISearchFlightOnly
    {
        ISearchFlightOnly ToDestination(string dest);
        ISearchFlightOnly FromDepartureAirport(string airport);
        ISearchFlightOnly FromCheckIn(string From);
        ISearchFlightOnly ToCheckOut(string To);
        ISearchFlightOnly ForAdults(int Adu);
        ISearchFlightOnlyChildren WithChildren(int Child);
        void Search();
        void SearchAndCapture();
    }
}
