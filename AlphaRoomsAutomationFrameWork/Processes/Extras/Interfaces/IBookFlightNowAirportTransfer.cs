using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.Extras.Interfaces
{
    public interface IBookFlightNowAirportTransfer
    {
        IBookFlightNowAirportTransferItem WithAirport(string p);
        IBookFlightNowAirportParking AddAirportParking();
        void Continue();
        void ContinueAndCapture();
    }
}
