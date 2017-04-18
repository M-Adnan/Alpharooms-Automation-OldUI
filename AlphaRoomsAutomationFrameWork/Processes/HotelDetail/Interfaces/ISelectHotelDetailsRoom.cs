using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.HotelDetail.Interfaces
{
    public interface ISelectHotelDetailsRoom
    {
        ISelectHotelDetails WithAvailableRoom(int room);
    }
}
