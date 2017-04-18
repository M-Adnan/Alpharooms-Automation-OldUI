using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.Payment.Interfaces
{
    public interface IMakeAHotelBookingGuestRoom
    {
        IMakeAHotelBookingGuestDetails OfRoomNo(int room);
    }
}
