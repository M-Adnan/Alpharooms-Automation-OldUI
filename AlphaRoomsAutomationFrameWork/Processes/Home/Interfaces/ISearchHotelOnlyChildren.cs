using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.Home.Interfaces
{
    public interface ISearchHotelOnlyChildren
    {
        ISearchHotelOnly OfAges(params int[] ChildrenAges);
        ISearchHotelOnlyRoom AddAnotherRoom();
        void Search();
        void SearchAndCapture();
    }
}
