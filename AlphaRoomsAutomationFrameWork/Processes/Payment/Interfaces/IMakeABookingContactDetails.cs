using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.Payment.Interfaces
{
    public interface IMakeABookingContactDetails
    {
        IMakeABookingContactDetails TypeFirstName(string firstName);
        IMakeABookingContactDetails WithLastName(string lastName);
        IMakeABookingContactDetails WithEmail(string email);
        IMakeABookingContactDetails WithPhoneNumber(string number);
        IMakeABookingContactDetailsAuto AutoFill();
        IMakeABookingPaymentDetails ForPaymentDetails();
        IMakeABookingStaffDetails ForStaffDetails();
        void Confirm();
        void ConfirmAndCapture();
    }
}
