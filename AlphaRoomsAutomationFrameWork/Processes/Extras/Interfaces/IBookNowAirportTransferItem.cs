﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.Extras.Interfaces
{
    public interface IBookNowAirportTransferItem
    {
        IBookNowAirportTransferEnd WithTransferNumber(int p);
        IBookNowCarHire AddCarHire();
        IBookNowAirportParking AddAirportParking();
        void Continue();
        void ContinueAndCapture();
    }
}
