using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.PageData
{
    public class FlightResultsPageData
    {        
        public Decimal Price { get; set; }
        public string FlightSearchGuid { get; set; }

        public int? ResultPageNo { get; set; }
        public int? FlightIndex { get; set; }
        public int SupplierFlightNo { get; set; }
        
        public string OutboundDepartureAirport { get; set; }
        public string OutboundDepartureTime { get; set; }
        public string OutboundArrivalAirport { get; set; }
        public string OutboundArrivalTime { get; set; }
        public string OutboundJourney { get; set; }
        public string OutboundJourneyTime { get; set; }
        public string OutboundFlightNo { get; set; }
        public string OutboundAirline { get; set; }
        public string OutboundFlightType { get; set; }

        public string InboundDepartureAirport { get; set; }
        public string InboundDepartureTime { get; set; }
        public string InboundArrivalAirport { get; set; }
        public string InboundArrivalTime { get; set; }
        public string InboundJourney { get; set; }
        public string InboundJourneyTime { get; set; }
        public string InboundFlightNo { get; set; }
        public string InboundAirline { get; set; }
        public string InboundFlightType { get; set; }

        public string Cost { get; set; }
        public string InternalSupplier { get; set; }
        public string PaymentModel { get; set; }
        public string Supplier { get; set; }
        public string Airline { get; set; }
        public TimeSpan LoadingTime { get; set; }
    }
}
