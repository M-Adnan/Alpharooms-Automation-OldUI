using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.ChangeSearch.Interfaces
{
    public interface ISearchHotelOnly
    {
        ISearchHotelOnly ToDestination(string dest);
        ISearchHotelOnly FromCheckIn(string From);
        ISearchHotelOnly ToCheckOut(string To);
        ISearchHotelOnlyRoom AddAnotherRoom();
        ISearchHotelOnly ForAdults(int Adu);
        ISearchHotelOnlyChildren WithChildren(int Child);
        void ShowPrices();
        void ShowPricesAndCapture();
    }
}
