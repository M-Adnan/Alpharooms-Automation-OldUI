using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaRooms.AutomationFramework.Processes.Payment.Interfaces;

namespace AlphaRooms.AutomationFramework.Processes.Payment
{
    public class MakeAFlightBookingStarter
    {
        public IMakeAFlightBookingGuestDetails ForGuestDetailsNumber(int guest)
        {
            return new MakeAFlightBooking(guest);
        }

        public IMakeAFlightBookingAllGuests ForAllGuestDetails()
        {
            return new MakeAFlightBooking();
        }
    }
}
