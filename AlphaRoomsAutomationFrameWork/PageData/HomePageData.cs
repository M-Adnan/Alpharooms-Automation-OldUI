using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.PageData
{
    public class HomePageData
    {
        private HomePageRoomData[] rooms = new HomePageRoomData[0];

        public HomePageData()
        {
            AddRoom();
        }

        public void AddRoom()
        {
            List<HomePageRoomData> list = new List<HomePageRoomData>(rooms);
            list.Add(new HomePageRoomData());
            rooms = list.ToArray();
        }

        public Location? SiteLocation { get; set; }
        public SearchOption? SearchOption { get; set; }
        public string Destination { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public string DepartureAirport { get; set; }
        public TimeSpan SearchTime { get; set; }
        public HomePageRoomData[] Rooms { get { return this.rooms; } }
    }
}
