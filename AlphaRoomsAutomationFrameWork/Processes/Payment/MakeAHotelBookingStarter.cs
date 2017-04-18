using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaRooms.AutomationFramework.Processes.Payment.Interfaces;

namespace AlphaRooms.AutomationFramework.Processes.Payment
{
    public class MakeAHotelBookingStarter
    {
        public IMakeAHotelBookingGuestRoom ForGuestDetailsNumber(int guest)
        {
            return new MakeAHotelBooking(guest);
        }

        public IMakeAHotelBookingAllGuests ForAllGuestDetails()
        {
            return new MakeAHotelBooking();
        }
    }
}
