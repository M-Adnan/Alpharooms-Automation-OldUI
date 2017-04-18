using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaRooms.AutomationFramework.Processes.Payment.Interfaces
{
    public interface IMakeAFlightBookingGuestDetails
    {
        IMakeAFlightBookingGuestDetails WithTitle(Title title);
        IMakeAFlightBookingGuestDetails WithFirstName(string firstName);
        IMakeAFlightBookingGuestDetails WithLastName(string lastName);
        IMakeAFlightBookingGuestDetails WithDoB(string doB);
        IMakeAFlightBookingGuestDetailsAuto AutoFill();
        IMakeAFlightBookingGuestDetails ForGuestDetailsNumber(int guest);
        IMakeABookingContactDetails ForContactDetails();
        IMakeABookingPaymentDetails ForPaymentDetails();
        IMakeABookingStaffDetails ForStaffDetails();
        void Confirm();
        void ConfirmAndCapture();
    }
}
