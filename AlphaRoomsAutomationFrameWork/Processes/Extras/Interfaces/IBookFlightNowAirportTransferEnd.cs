using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.Extras.Interfaces
{
    public interface IBookFlightNowAirportTransferEnd
    {
        IBookFlightNowAirportParking AddAirportParking();
        void Continue();
        void ContinueAndCapture();
    }
}
