using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.Extras.Interfaces
{
    public interface IBookNowFlightExtras
    {
        IBookNowFlightExtrasEnd WithPassengers(int pass);
        IBookNowFlightExtras AddFlightExtras(int p);
        IBookNowAirportTransfer AddAirportTransfer();
        IBookNowCarHire AddCarHire();
        IBookNowAirportParking AddAirportParking();
        void Continue();
        void ContinueAndCapture();
    }
}
