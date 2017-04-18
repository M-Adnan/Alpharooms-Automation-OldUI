using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.Extras.Interfaces
{
    public interface IBookFlightNowAirportParking
    {
        IBookFlightNowAirportParking WithDepartureAirport(string p);
        IBookFlightNowAirportParking WithDropoffTime(string p);
        IBookFlightNowAirportParking WithPickupTime(string p);
        IBookFlightNowAirportParkingEnd WithParkingNumber(int p);
        void Continue();
        void ContinueAndCapture();
    }
}
