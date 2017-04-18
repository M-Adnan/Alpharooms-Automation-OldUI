using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaRooms.AutomationFramework.Processes.Payment.Interfaces;

namespace AlphaRooms.AutomationFramework.Processes.Payment
{
    public class MakeAFlightAndHotelBookingStarter
    {
        public IMakeAFlightAndHotelBookingGuestRoom ForGuestDetailsNumber(int guest)
        {
            return new MakeAFlightAndHotelBooking(guest);
        }

        public IMakeAFlightAndHotelBookingAllGuests ForAllGuestDetails()
        {
            return new MakeAFlightAndHotelBooking();
        }
    }
}
