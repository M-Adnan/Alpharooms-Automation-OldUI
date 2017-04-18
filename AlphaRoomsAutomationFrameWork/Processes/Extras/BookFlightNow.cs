using AlphaRooms.AutomationFramework.Processes.Extras.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Processes.Extras
{
    public class BookFlightNow : 
        IBookFlightNowAirportTransfer, IBookFlightNowAirportTransferItem, IBookFlightNowAirportTransferEnd
        , IBookFlightNowAirportParking, IBookFlightNowAirportParkingEnd
    {

        private class AirportTransfer
        {
            public string Airport { get; set; }
            public int? TransferNumber { get; set; }
        }

        private class AirportParking
        {
            public string DepartureAirport { get; set; }
            public string PickupTime { get; set; }
            public string DropoffTime { get; set; }
            public int? ParkingNumber { get; set; }
        }

        private AirportTransfer airportTransfer;
        private AirportParking airportParking;

        public BookFlightNow()
        {
            airportTransfer = new AirportTransfer();
            airportParking = new AirportParking();
        }

        public IBookFlightNowAirportTransfer AddAirportTransfer()
        {
            return this;
        }

        public IBookFlightNowAirportTransferItem WithAirport(string p)
        {
            this.airportTransfer.Airport = p;
            return this;
        }

        public IBookFlightNowAirportTransferEnd WithTransferNumber(int p)
        {
            this.airportTransfer.TransferNumber = p;
            return this;
        }

        public IBookFlightNowAirportParking AddAirportParking()
        {
            return this;
        }

        public IBookFlightNowAirportParking WithDepartureAirport(string p)
        {
            this.airportParking.DepartureAirport = p;
            return this;
        }

        IBookFlightNowAirportParking IBookFlightNowAirportParking.WithDropoffTime(string p)
        {
            this.airportParking.DropoffTime = p;
            return this;
        }

        IBookFlightNowAirportParking IBookFlightNowAirportParking.WithPickupTime(string p)
        {
            this.airportParking.PickupTime = p;
            return this;
        }

        public IBookFlightNowAirportParkingEnd WithParkingNumber(int p)
        {
            this.airportParking.ParkingNumber = p;
            return this;
        }

        private void ContinueProcess()
        {
            if (this.airportTransfer.Airport != null) ExtrasPage.SelectAirportTransferAirport(this.airportTransfer.Airport);
            if (this.airportTransfer.TransferNumber != null)
            {
                ExtrasPage.ClickAirportTransterUpdate();
                ExtrasPage.CheckTransferNumber(this.airportTransfer.TransferNumber.Value);
            }

            if (this.airportParking.DepartureAirport != null) ExtrasPage.SelectAirportParkingDepartureAirport(this.airportParking.DepartureAirport);
            if (this.airportParking.DropoffTime != null) ExtrasPage.SelectAirportParkingDropoffTime(this.airportParking.DropoffTime);
            if (this.airportParking.PickupTime != null) ExtrasPage.SelectAirportParkingPickupTime(this.airportParking.PickupTime);
            if (this.airportParking.ParkingNumber != null) ExtrasPage.CheckAirportParkingNumber(this.airportParking.ParkingNumber.Value);
        }

        public void Continue()
        {
            this.ContinueProcess();
            ExtrasPage.ClickBookNow();
        }

        public void ContinueAndCapture()
        {
            this.ContinueProcess();
            ExtrasPage.ClickBookNowAndCapture();
        }
    }
}
