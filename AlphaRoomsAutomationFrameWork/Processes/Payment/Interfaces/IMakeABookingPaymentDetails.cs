using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.Payment.Interfaces
{
    public interface IMakeABookingPaymentDetails
    {
        IMakeABookingPaymentDetails WithPaymentType(Card card);
        IMakeABookingPaymentDetails WithCardNo(string number);
        IMakeABookingPaymentDetails WithExpiryDate(string date);
        IMakeABookingPaymentDetails WithSecurityCode(string sCode);
        IMakeABookingPaymentDetails WithCardHolderName(string name);
        IMakeABookingPaymentDetails WithPostCode(string postCode);
        IMakeABookingPaymentDetailsAuto AutoFill();
        IMakeABookingStaffDetails ForStaffDetails();
        void Confirm();
        void ConfirmAndCapture();
    }
}
