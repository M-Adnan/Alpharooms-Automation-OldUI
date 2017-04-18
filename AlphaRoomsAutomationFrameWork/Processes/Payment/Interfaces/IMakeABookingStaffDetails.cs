using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.Payment.Interfaces
{
    public interface IMakeABookingStaffDetails
    {
        IMakeABookingStaffDetails WithReference(string reference);
        IMakeABookingStaffDetails WithCustomerPhone(string customerPhone);
        IMakeABookingStaffDetailsAuto AutoFill();
        void Confirm();
        void ConfirmAndCapture();
    }
}
