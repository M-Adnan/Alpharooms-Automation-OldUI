using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.Extras.Interfaces
{
    public interface IBookNowAirportParking
    {
        IBookNowAirportParking WithDropoffTime(string p);
        IBookNowAirportParking WithPickupTime(string p);
        IBookNowAirportParkingEnd WithParkingNumber(int p);
        void Continue();
        void ContinueAndCapture();
    }
}
