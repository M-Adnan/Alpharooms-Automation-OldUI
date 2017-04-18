using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.PageData
{
    public class HotelResultsPageRoomData
    {
        public string RoomType { get; set; }
        public string RoomPrice { get; set; }
        public Decimal TotalPrice { get; set; }
        public string BoardType { get; set; }
        public string Cost { get; set; }
        public string Supplier { get; set; }
        public int AvailableRoom { get; set; }
    }
}
