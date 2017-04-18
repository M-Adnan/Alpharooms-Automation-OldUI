using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaRooms.AutomationFramework.Infos
{
    public class AvailableRoomInfo
    {
        private int hotelNumber;
        private int roomNumber;
        private int availableRoomNumber;

        public AvailableRoomInfo(int hotelNumber, int roomNumber, int availableRoomNumber)
        {
            this.hotelNumber = hotelNumber;
            this.roomNumber = roomNumber;
            this.availableRoomNumber = availableRoomNumber;
        }

        public int HotelNumber 
        {
            get { return this.hotelNumber; } 
        }

        public int RoomNumber
        {
            get { return this.roomNumber; } 
        }

        public int AvailableRoomNumber
        {
            get { return this.availableRoomNumber; } 
        }
    }
}
