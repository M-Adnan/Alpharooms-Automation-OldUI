using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.PageData
{
    public class ChangeSearchPanelData
    {
        private ChangeSearchPanelRoomData[] rooms = new ChangeSearchPanelRoomData[0];

        public ChangeSearchPanelData()
        {
            AddRoom();
        }

        public void AddRoom()
        {
            List<ChangeSearchPanelRoomData> list = new List<ChangeSearchPanelRoomData>(rooms);
            list.Add(new ChangeSearchPanelRoomData());
            rooms = list.ToArray();
        }

        public Location? SiteLocation { get; set; }
        public SearchOption? SearchOption { get; set; }
        public string Destination { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public string DepartureAirport { get; set; }
        public TimeSpan SearchTime { get; set; }
        public ChangeSearchPanelRoomData[] Rooms { get { return this.rooms; } }
    }
}
