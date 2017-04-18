using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.PageData
{
    public class HotelResultsPageData
    {
        private HotelResultsPageRoomData[] rooms = new HotelResultsPageRoomData[0];

        public HotelResultsPageData()
        {
        }

        public void AddRoom()
        {
            List<HotelResultsPageRoomData> list = new List<HotelResultsPageRoomData>(rooms);
            list.Add(new HotelResultsPageRoomData());
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

        //public int? counter = 0;
        public int? ResultPageNo { get; set; }
        public int? HotelNumber { get; set; }
        public int? SupplierHotelIndex { get; set; }
        public int? SupplierRoomIndex { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public string HotelLocation { get; set; }
        public string HotelSearchGuid { get; set; }
        public TimeSpan BasketTime { get; set; }
        public HotelResultsPageRoomData[] Rooms { get { return this.rooms; } }
        public TimeSpan LoadingTime { get; set; }
        public int TotalHotelSearchResults { get; set; }
    }
}
