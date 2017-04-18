using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Infos
{
    public class AvailableHotelRoomInfo
    {
        private int roomNumber;
        private int availableRoomNumber;

        public AvailableHotelRoomInfo(int roomNumber, int availableRoomNumber)
        {
            this.roomNumber = roomNumber;
            this.availableRoomNumber = availableRoomNumber;
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
