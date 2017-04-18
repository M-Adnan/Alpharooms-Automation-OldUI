using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.Payment.Interfaces
{
    public interface IMakeAFlightAndHotelBookingGuestDetails
    {
        IMakeAFlightAndHotelBookingGuestDetails WithTitle(Title title);
        IMakeAFlightAndHotelBookingGuestDetails WithFirstName(string firstName);
        IMakeAFlightAndHotelBookingGuestDetails WithLastName(string lastName);
        IMakeAFlightAndHotelBookingGuestDetails WithDoB(string doB);
        IMakeAFlightAndHotelBookingGuestDetailsAuto AutoFill();
        IMakeAFlightAndHotelBookingGuestRoom ForGuestDetailsNumber(int guest);
        IMakeABookingContactDetails ForContactDetails();
        IMakeABookingPaymentDetails ForPaymentDetails();
        IMakeABookingStaffDetails ForStaffDetails();
        void Confirm();
        void ConfirmAndCapture();
    }
}
