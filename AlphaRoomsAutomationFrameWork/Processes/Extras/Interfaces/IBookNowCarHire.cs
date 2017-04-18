using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.Extras.Interfaces
{
    public interface IBookNowCarHire
    {
        IBookNowCarHire WithPickupLocation(string p);
        IBookNowCarHire WithMainDriverAge(int p);
        IBookNowCarHire WithPickupTime(string p);
        IBookNowCarHire WithDropoffTime(string p);
        IBookNowCarHireEnd WithCarHireNumber(int p);
        IBookNowAirportParking AddAirportParking();
        void Continue();
        void ContinueAndCapture();
    }
}
