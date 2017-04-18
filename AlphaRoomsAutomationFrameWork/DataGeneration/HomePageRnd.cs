using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework
{
    public static class HomePageRnd
    {
        internal static Random Random = new Random();

        public static int PickRandomAdults()
        {
            int adultNo = Random.Next(1, 4);
            return adultNo;
        }

        private static int? lastChildren;

        public static int PickRandomChildren()
        {
            int child = Random.Next(1, 4);
            lastChildren = child;
            return child;
        }

        public static int[] PickRandomChildrenAges()
        {
            if (lastChildren == null) throw new Exception("Cannot generate random children ages without generating a children.");
            int[] ages = new int[lastChildren.Value];
            int index = 0;
            while (index < lastChildren)
            {
                ages[index] = Random.Next(1, 12);
                index++;
            }
            return ages;   
        }

        public static string PickRandomFlightDestination()
        {
            //Most popular flight only destination repository
            List<String> FlightOnlyDest = new List<string>
            {
                "Tenerife (Main), Tenerife, Canaries (TFS)","Alicante, Benidorm, Spain (ALC)","Malaga, Costa Del Sol, Spain (AGP)","Barcelona, Spain (BCN)",
                "Palma De Mallorca, Mallorca (Majorca), Spain (PMI)","Schiphol Amsterdam, Amsterdam, The Netherlands (AMS)","Cairns, Australia (CNS)",
                "Dalaman, Turkey (DLM)","Faro, Portugal (FAO)","Lanzarote, Canaries (ACE)","Ibiza, Spain (IBZ)","Charles De Gaulle, Paris, France (CDG)",
                "Malaga, Costa Del Sol, Spain (AGP)", "Las Palmas, Gran Canaria, Canaries (LPA)", "Edinburgh, United Kingdom (EDI)", "Paphos, Cyprus (PFO)",
                "Lanzarote, Canaries (ACE)", "Larnaca, Cyprus (LCA)", "Charles De Gaulle, Paris, France (CDG)", "Sharm El Sheikh, Egypt (SSH)",
                "Malta International, Malta, Malta (MLA)", "Milas Bodrum, Bodrum, Turkey (BJV)"
            };

            int index = Random.Next(FlightOnlyDest.Count);
            string randFlightDest = FlightOnlyDest[index];
            return randFlightDest;
        }

        public static string PickRandomFlightDepartureAirport()
        {
            List<string> DepartureAirportList = new List<String>
                                     {
                                         "Manchester Airport, Manchester, United Kingdom (MAN)", "London Gatwick, London, United Kingdom (LGW)", 
                                         "Birmingham, United Kingdom (BHX)",                                       
                                     };
            /*"Dublin, Republic Of Ireland (DUB)", "London Luton, London, United Kingdom (LTN)", "Leeds-Bradford, Leeds, United Kingdom (LBA)", 
            "Glasgow, United Kingdom (GLA)", "Istanbul Ataturk, Istanbul, Turkey (IST)", 
            "Liverpool John Lennon, Liverpool, United Kingdom (LPL)", "George Best Belfast City, Belfast, United Kingdom (BHD)",
            "Newcastle, Newcastle (UK), United Kingdom (NCL)","Belfast International, Belfast, United Kingdom (BFS)",
            "Bristol, United Kingdom (BRS)","London Southend, Southend, United Kingdom (SEN)","Edinburgh, United Kingdom (EDI)", "East Midlands, Derby, United Kingdom (EMA)" */

            int index = Random.Next(DepartureAirportList.Count);
            string randDeptAirport = DepartureAirportList[index];
            return randDeptAirport;
        }
    }
}
