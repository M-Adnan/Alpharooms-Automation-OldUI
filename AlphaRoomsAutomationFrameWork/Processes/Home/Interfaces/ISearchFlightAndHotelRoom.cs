using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.Home.Interfaces
{
    public interface ISearchFlightAndHotelRoom
    {
        ISearchFlightAndHotel ForAdults(int Adu);
        ISearchFlightAndHotelChildren WithChildren(int Child);
        ISearchFlightAndHotelRoom AddAnotherRoom();
        ISearchFlightAndHotel AutoFill();
        void Search();
        void SearchAndCapture();
    }
}
