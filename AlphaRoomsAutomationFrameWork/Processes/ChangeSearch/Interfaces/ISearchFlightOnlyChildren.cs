using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.ChangeSearch.Interfaces
{
    public interface ISearchFlightOnlyChildren
    {
        ISearchFlightOnly OfAges(params int[] ChildrenAges);
        void ShowPrices();
        void ShowPricesAndCapture();
    }
}
