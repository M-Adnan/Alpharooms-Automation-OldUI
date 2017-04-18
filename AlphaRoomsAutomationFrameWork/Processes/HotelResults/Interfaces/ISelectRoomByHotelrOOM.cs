using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.HotelResults.Interfaces
{
    public interface ISelectRoomByHotelRoom
    {
        ISelectRoomByHotel WithAvailableRoom(int room);
    }
}
