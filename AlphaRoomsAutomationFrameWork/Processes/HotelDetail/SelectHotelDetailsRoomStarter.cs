using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaRooms.AutomationFramework.Processes.HotelDetail.Interfaces;
using AlphaRooms.AutomationFramework.Infos;

namespace AlphaRooms.AutomationFramework.Processes.HotelDetail
{
    public class SelectHotelDetailsRoomStarter
    {
        public ISelectHotelDetailsRoom ForRoomNumber(int number)
        {
            return new SelectHotelDetails(number);
        }

        public ISelectHotelDetailsSingleRoom OnlyOneRoomWithAvailableRoom(int room)
        {
            return new SelectHotelDetails(room, true);
        }

        public ISelectHotelDetailsByFirstSupplier ByFirstSupplier(string supplier)
        {
            AvailableHotelRoomInfo room = HotelDetailPage.FindRoomForFirstSupplier(supplier);
            return new SelectHotelDetails(room);
        }
    }
}
