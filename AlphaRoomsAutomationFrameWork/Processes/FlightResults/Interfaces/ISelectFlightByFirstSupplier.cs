using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaRooms.AutomationFramework.Processes.FlightResults.Interfaces
{
    public interface ISelectFlightByFirstSupplier
    {
        void Continue();
        void ContinueAndCapture();
    }
}
