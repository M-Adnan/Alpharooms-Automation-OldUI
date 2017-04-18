using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.Home.Interfaces
{
    public interface ISearchHotelOnlyRoom
    {
        ISearchHotelOnly ForAdults(int Adu);
        ISearchHotelOnlyChildren WithChildren(int Child);
        ISearchHotelOnlyRoom AddAnotherRoom();
        ISearchHotelOnly AutoFill(); 
        void Search();
        void SearchAndCapture();
    }
}
