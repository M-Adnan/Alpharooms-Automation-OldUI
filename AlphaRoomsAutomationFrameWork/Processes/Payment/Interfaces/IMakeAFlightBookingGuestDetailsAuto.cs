using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaRooms.AutomationFramework.Processes.Payment.Interfaces
{
    public interface IMakeAFlightBookingGuestDetailsAuto
    {
        IMakeAFlightBookingGuestDetails ForGuestDetailsNumber(int guest);
        IMakeABookingContactDetails ForContactDetails();
        IMakeABookingPaymentDetails ForPaymentDetails();
        IMakeABookingStaffDetails ForStaffDetails();
        void Confirm();
        void ConfirmAndCapture();
    }
}
