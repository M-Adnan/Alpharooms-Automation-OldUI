using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaRooms.AutomationFramework.Tests.SOA_Flight_Tests
{
    public class TestScript
    {
        public string TestName { get; set; }
        public string Destination { get; set; }
        public string DepartureDate { get; set; }
        public string ReturnDate { get; set; }
        public string DepartureAirport { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int[] ChildrenAges { get; set; }
    }
}
