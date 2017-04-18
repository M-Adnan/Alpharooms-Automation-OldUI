using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaRooms.AutomationFramework.Processes.Payment.Interfaces
{
    public interface IMakeAHotelBookingGuestDetails
    {
        IMakeAHotelBookingGuestDetails WithTitle(Title title);
        IMakeAHotelBookingGuestDetails WithFirstName(string firstName);
        IMakeAHotelBookingGuestDetails WithLastName(string lastName);
        IMakeAHotelBookingGuestDetailsAuto AutoFill();
        IMakeAHotelBookingGuestRoom ForGuestDetailsNumber(int guest);
        IMakeABookingContactDetails ForContactDetails();
        IMakeABookingPaymentDetails ForPaymentDetails();
        IMakeABookingStaffDetails ForStaffDetails();
        void Confirm();
        void ConfirmAndCapture();
    }
}
