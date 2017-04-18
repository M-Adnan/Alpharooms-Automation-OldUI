using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.Payment.Interfaces
{
    public interface IMakeAFlightAndHotelBookingAllGuests
    {
        IMakeAFlightAndHotelBookingGuestDetailsAuto AutoFill();
    }
}
