using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.HotelResults.Interfaces
{
    public interface ISelectRoomByHotel
    {
        ISelectRoomByHotelRoom ForRoomNumber(int roomNumber);
        void Continue();
        void ContinueAndCapture();
    }
}
