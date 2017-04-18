using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.PageData
{
    public class AdminPanelPageData
    {
        public string ItineraryNo { get; set; }
        public string BookedByName { get; set; }
        public string BookedByPhone { get; set; }
        public string BookedByEmail { get; set; }
        public string PNRNo { get; set; }

        public Decimal TotalPrice { get; set; }

        public TimeSpan LoadingTime { get; set; }
    }
}
