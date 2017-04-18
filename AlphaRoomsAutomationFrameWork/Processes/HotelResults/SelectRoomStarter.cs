using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaRooms.AutomationFramework.Processes.HotelResults.Interfaces;
using AlphaRooms.AutomationFramework.Infos;

namespace AlphaRooms.AutomationFramework.Processes.HotelResults
{
    public class SelectRoomStarter
    {
        public ISelectRoomByHotelFirst ByHotelName(string hotel)
        {
            int hotelNumber = HotelResultsPage.FindHotel(hotel);
            return new SelectRoomByHotel(hotelNumber);
        }

        public ISelectRoomByHotelFirst ByHotelNumber(int hotel)
        {
            return new SelectRoomByHotel(hotel);
        }

        public ISelectRoomByFirstSupplier ByFirstSupplier(string supplier)
        {
            AvailableRoomInfo room = HotelResultsPage.FindRoomForFirstSupplier(supplier);
            return new SelectRoomByHotel(room);
        }
    }
}
