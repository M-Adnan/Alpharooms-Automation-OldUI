using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.PageData
{
    public class HomePageRoomData
    {
        public string AdultsLabel { get; set; }
        public int? Adults { get; set; }
        public string ChildrenLabel { get; set; }
        public int? Children { get; set; }
        public int[] ChildrenAges { get; set; }
    }
}
