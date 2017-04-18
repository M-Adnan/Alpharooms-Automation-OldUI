using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.PageData
{
    public class PaymentPageData
    {
        private PaymentPageRoomData[] rooms = new PaymentPageRoomData[] { new PaymentPageRoomData() };

        public void AddRoom()
        {
            List<PaymentPageRoomData> list = new List<PaymentPageRoomData>(rooms);
            list.Add(new PaymentPageRoomData());
            rooms = list.ToArray();
        }

        public string OutboundDepartureTime { get; set; }
        public string OutboundArrivalTime { get; set; }
        public string OutboundJourneyTime { get; set; }
        public string OutboundJourney { get; set; }
        public string OutboundAirline { get; set; }
        public string OutboundFlightNo { get; set; }
               
        public string InboundDepartureTime { get; set; }
        public string InboundArrivalTime { get; set; }
        public string InboundJourneyTime { get; set; }
        public string InboundJourney { get; set; }
        public string InboundAirline { get; set; }
        public string InboundFlightNo { get; set; }
                
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public string HotelStay { get; set; }
        public string HotelTotalStay { get; set; }
        public string TotalAdults { get; set; }
        public string TotalChildren { get; set; }

        public decimal TotalPrice { get; set; }
        public decimal TotalPriceWithCardFees { get; set; }

        public PaymentPageRoomData[] Rooms { get { return rooms; } }
        
    }
}
