using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.PageData
{
    public class ExtrasPageData
    {
        private ExtrasPageRoomData[] rooms = new ExtrasPageRoomData[] { new ExtrasPageRoomData() };

        public void AddRoom()
        {
            List<ExtrasPageRoomData> list = new List<ExtrasPageRoomData>(rooms);
            list.Add(new ExtrasPageRoomData());
            rooms = list.ToArray();
        }

        public string OutboundDepartureTime { get; set; }
        public string OutboundArrivalTime { get; set; }
        public string OutboundJourney { get; set; }
        public string OutboundJourneyTime { get; set; }
        public string OutboundFlightNo { get; set; }
        public string OutboundAirline { get; set; }
        public string OutboundDepartureAiport { get; set; }
        public string OutboundArrivalAirport { get; set; }

        public string InboundDepartureTime { get; set; }
        public string InboundArrivalTime { get; set; }
        public string InboundJourney { get; set; }
        public string InboundJourneyTime { get; set; }
        public string InboundFlightNo { get; set; }
        public string InboundAirline { get; set; }
        public string InbounddDepartureAiport { get; set; }
        public string InboundArrivalAirport { get; set; }

        public Decimal OrignalPrice { get; set; }
        public Decimal ChangedPrice { get; set; }
        public Decimal TotalPrice { get; set; }

        public decimal? flightHoldLuggagePrice { get; set; }

        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public string BasketGUID { get; set; }
        public string HotelCheckinDate { get; set; }
        public string HotelCheckoutDate { get; set; }
        public string HotelTotalStay { get; set; }


        public ExtrasPageRoomData[] Rooms { get { return rooms; } }

        public TimeSpan LoadingTime { get; set; }
    }
}
