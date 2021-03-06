﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.Extras.Interfaces
{
    public interface IBookNowHoldLuggageEnd
    {
        IBookNowFlightExtras AddFlightExtras(int p);
        IBookNowAirportTransfer AddAirportTransfer();
        IBookNowCarHire AddCarHire();
        IBookNowAirportParking AddAirportParking();
        void Continue();
        void ContinueAndCapture();
    }
}
