using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.Home.Interfaces
{
    public interface ISearchFlightAndHotelChildren
    {
        ISearchFlightAndHotel OfAges(params int[] childrenAges);
        ISearchFlightAndHotelRoom AddAnotherRoom();
        void Search();
        void SearchAndCapture();
    }
}
