using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaRooms.AutomationFramework.Processes.HotelDetail.Interfaces
{
    public interface ISelectHotelDetails
    {
        ISelectHotelDetailsRoom ForRoomNumber(int roomNumber);
        void Continue();
        void ContinueAndCapture();
    }
}
