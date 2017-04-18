using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.Home.Interfaces
{
    public interface ISearchFlightOnlyChildren
    {
        ISearchFlightOnly OfAges(params int[] ChildrenAges);
        void Search();
        void SearchAndCapture();
    }
}
