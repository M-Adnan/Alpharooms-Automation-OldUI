using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.HotelResults.Interfaces
{
    public interface ISelectRoomByHotelFirst
    {
        ISelectRoomByHotelRoom ForRoomNumber(int roomNumber);
        ISelectRoomByHotelSingleRoom OnlyOneRoomWithAvailableRoom(int room);
        void Continue();
        void ContinueAndCapture();
    }
}