using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Models
{
    public class TestResult
    {
        public int Id { get; set; }
        public string TestFixture { get; set; }
        public string TestName { get; set; }
        public bool? IsSuccess { get; set; }
        public bool? IsResultError { get; set; }
        public double? Time { get; set; }
        public int? AssertCount { get; set; }
        public string Message { get; set; }
        public string HotelSearchGuid { get; set; }
        public string HotelName { get; set; }
        public string BasketGuid { get; set; }
        public bool? IsExecuted { get; set; }
        public  string StackTrace { get; set; }
        public string StepToReproduce { get; set; }
        public double? SearchTime { get; set; }
        public double? BasketTime { get; set; }
        public DateTime? CreatedDate { get; set; }
        public TimeSpan? HotelResultPageLoadingTime { get; set; }
        public TimeSpan? FlightResultPageLoadingTime { get; set; }
        public int? TotalHotelSearchResults { get; set; }
        public string OutboundAirline { get; set; }
        public string OutboundFlightNo { get; set; }
        public string InboundAirline { get; set; }
        public string InboundFlightNo { get; set; }
        public string FlightSearchGuid { get; set; }
        public string TestingEnvironment { get; set; }
        public string TestingBrowser { get; set; }
    }
}
