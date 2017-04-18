using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.Payment.Interfaces
{
    public interface IMakeAFlightAndHotelBookingGuestDetailsAuto
    {
        IMakeAFlightAndHotelBookingGuestRoom ForGuestDetailsNumber(int guest);
        IMakeABookingContactDetails ForContactDetails();
        IMakeABookingPaymentDetails ForPaymentDetails();
        IMakeABookingStaffDetails ForStaffDetails();
        void Confirm();
        void ConfirmAndCapture();
    }
}
