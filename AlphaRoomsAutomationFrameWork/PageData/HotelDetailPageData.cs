using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.PageData
{
    public class HotelDetailPageData
    {
        private HotelDetailPageRoomData[] rooms = new HotelDetailPageRoomData[0];

        public HotelDetailPageData()
        {
        }

        public void AddRoom()
        {
            List<HotelDetailPageRoomData> list = new List<HotelDetailPageRoomData>(rooms);
            list.Add(new HotelDetailPageRoomData());
            rooms = list.ToArray();
        }

        public void EnsureCapacity(int roomNumber)
        {
            int index = rooms.Length, count = roomNumber;
            while (rooms.Length < roomNumber)
            {
                AddRoom();
                index++;
            }
        }

        //public int counter = 0;
        public int? ResultPageNo { get; set; }
        public int? SupplierHotelIndex { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public string HotelLocation { get; set; }
        public HotelDetailPageRoomData[] Rooms { get { return this.rooms; } }
        public TimeSpan LoadingTime { get; set; }
    }
}
