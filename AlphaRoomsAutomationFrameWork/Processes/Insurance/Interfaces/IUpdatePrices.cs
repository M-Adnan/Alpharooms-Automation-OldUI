using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.Insurance.Interfaces
{
    public interface IUpdatePrices
    {
        IUpdatePrices ToDestination(string p);
        IUpdatePrices ForAdults(int p);
        IUpdatePrices OfAdultAges(params int[] p);
        IUpdatePrices WithChildren(int p);
        IUpdatePrices OfChildrenAges(params int[] p);
        IUpdatePrices WithRelationship(string p);
        IUpdatePrices FromStartDate(string p);
        IUpdatePrices ToEndDate(string p);
        void Update();
        void UpdateAndCapture();
    }
}
