using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.PageData
{
    public class ConfirmationPageData
    {
        public string ItineraryNo { get; set; }
        public string BookedByName { get; set; }
        public string BookedByPhone { get; set; }
        public string BookedByEmail { get; set; }
        public string PNRNo { get; set; }

        public string OutboundFlightNo { get; set; }
        public string OutboundDepartureAirport { get; set; }
        public string OutboundDepartureTime { get; set; }
        public string OutboundArrivalAirport { get; set; }
        public string OutboundArrivalTime { get; set; }

        public string InboundFlightNo { get; set; }
        public string InboundDepartureAirport { get; set; }
        public string InboundDepartureTime { get; set; }
        public string InboundArrivalAirport { get; set; }
        public string InboundArrivalTime { get; set; }

        public Decimal TotalPrice { get; set; }

        public TimeSpan LoadingTime { get; set; }
    }
}
