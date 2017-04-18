using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaRooms.AutomationFramework.Processes.Extras.Interfaces;

namespace AlphaRooms.AutomationFramework.Processes.Extras
{
    public class BookNowStarter
    {
        public IBookNowHoldLuggage AddHoldLuggage()
        {
            return new BookNow();
        }
        
        public IBookNowFlightExtras AddFlightExtras(int p)
        {
            BookNow bookNow = new BookNow();
            bookNow.AddFlightExtras(p);
            return bookNow;
        }

        public IBookNowAirportTransfer AddAirportTransfer()
        {
            return new BookNow();
        }

        public IBookNowCarHire AddCarHire()
        {
            return new BookNow();
        }

        public IBookNowAirportParking AddAirportParking()
        {
            return new BookNow();
        }

        public void Continue()
        {
            ExtrasPage.ClickBookNow();
        }

        public void ContinueAndCapture()
        {
            ExtrasPage.ClickBookNowAndCapture();
        }
    }
}
