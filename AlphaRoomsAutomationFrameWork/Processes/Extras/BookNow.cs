using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaRooms.AutomationFramework.Processes.Extras.Interfaces;

namespace AlphaRooms.AutomationFramework.Processes.Extras
{
    public class BookNow : IBookNowHoldLuggage, IBookNowHoldLuggageEnd
        , IBookNowFlightExtras, IBookNowFlightExtrasEnd
        , IBookNowAirportTransfer, IBookNowAirportTransferHotel, IBookNowAirportTransferItem, IBookNowAirportTransferEnd
        , IBookNowCarHire, IBookNowCarHireEnd 
        , IBookNowAirportParking, IBookNowAirportParkingEnd
    {
        private class HoldLuggage
        {
            public int? Passengers { get; set; }
        }

        private class FlightExtras
        {
            public FlightExtras(int extra)
            {
                this.Number = Number;
            }
            public int? Number { get; set; }
            public int? Passengers { get; set; }
        }

        private class AirportTransfer
        {
            public string HotelLocation { get; set; }
            public string Hotel { get; set; }
            public int? TransferNumber { get; set; }
        }

        private class CarHire
        {
            public string PickupLocation { get; set; }
            public int? MainDriverAge { get; set; }
            public string PickupTime { get; set; }
            public string DropoffTime { get; set; }
            public int? CarHireNumber { get; set; }
        }

        private class AirportParking
        {
            public string PickupTime { get; set; }
            public string DropoffTime { get; set; }
            public int? ParkingNumber { get; set; }
        }

        private HoldLuggage holdLuggage;
        private List<FlightExtras> flightExtras;
        private AirportTransfer airportTransfer;
        private CarHire carHire;
        private AirportParking airportParking;

        public BookNow()
        {
            holdLuggage = new HoldLuggage();
            flightExtras = new List<FlightExtras>();
            airportTransfer = new AirportTransfer();
            carHire = new CarHire();
            airportParking = new AirportParking();
        }

        public IBookNowHoldLuggage SelectCheckInHoldLuggage()
        {
            return this;
        }

        public IBookNowHoldLuggageEnd ForPassengers(int pass)
        {
            this.holdLuggage.Passengers = pass;
            return this;
        }

        public IBookNowFlightExtras AddFlightExtras(int p)
        {
            this.flightExtras.Add(new FlightExtras(p));
            return this;
        }

        public IBookNowFlightExtrasEnd WithPassengers(int pass)
        {
            this.flightExtras.Last().Passengers = pass;
            return this;
        }

        public IBookNowAirportTransfer AddAirportTransfer()
        {
            return this;
        }

        public IBookNowAirportTransferHotel WithHotelLocation(string p)
        {
            this.airportTransfer.HotelLocation = p;
            return this;
        }

        public IBookNowAirportTransferItem WithHotel(string p)
        {
            this.airportTransfer.Hotel = p;
            return this;
        }

        public IBookNowAirportTransferEnd WithTransferNumber(int p)
        {
            this.airportTransfer.TransferNumber = p;
            return this;
        }

        public IBookNowCarHire AddCarHire()
        {
            return this;
        }

        public IBookNowCarHire WithPickupLocation(string p)
        {
            this.carHire.PickupLocation = p;
            return this;
        }

        public IBookNowCarHire WithMainDriverAge(int p)
        {
            this.carHire.MainDriverAge = p;
            return this;
        }

        public IBookNowCarHire WithPickupTime(string p)
        {
            this.carHire.PickupTime = p;
            return this;
        }

        public IBookNowCarHire WithDropoffTime(string p)
        {
            this.carHire.DropoffTime = p;
            return this;
        }

        public IBookNowCarHireEnd WithCarHireNumber(int p)
        {
            this.carHire.CarHireNumber = p;
            return this;
        }

        public IBookNowAirportParking AddAirportParking()
        {
            return this;
        }

        IBookNowAirportParking IBookNowAirportParking.WithDropoffTime(string p)
        {
            this.airportParking.DropoffTime = p;
            return this;
        }

        IBookNowAirportParking IBookNowAirportParking.WithPickupTime(string p)
        {
            this.airportParking.PickupTime = p;
            return this;
        }

        public IBookNowAirportParkingEnd WithParkingNumber(int p)
        {
            this.airportParking.ParkingNumber = p;
            return this;
        }

        private void ContinueProcess()
        {
            if (this.holdLuggage.Passengers != null)
            {
                ExtrasPage.CheckHoldLuggage();
                if (this.holdLuggage.Passengers != null) ExtrasPage.SelectHoldLuggagePassengers(this.holdLuggage.Passengers.Value);
            }

            foreach (FlightExtras flightExtra in this.flightExtras)
            {
                ExtrasPage.CheckFlightExtraNumber(flightExtra.Number.Value);
                if (flightExtra.Passengers != null) ExtrasPage.SelectFlightExtraPassengers(flightExtra.Number.Value, flightExtra.Passengers.Value);
            }

            if (this.airportTransfer.HotelLocation != null) ExtrasPage.SelectAirportTransferHotelLocation(this.airportTransfer.HotelLocation);
            if (this.airportTransfer.Hotel != null) ExtrasPage.TypeAirportTransferHotel(this.airportTransfer.Hotel);
            if (this.airportTransfer.TransferNumber != null)
            {
                ExtrasPage.ClickAirportTransterUpdate();
                ExtrasPage.CheckTransferNumber(this.airportTransfer.TransferNumber.Value);
            }

            if (this.carHire.PickupLocation != null) ExtrasPage.SelectCarHirePickupLocation(this.carHire.PickupLocation);
            if (this.carHire.MainDriverAge != null) ExtrasPage.SelectCarHireMainDriverAge(this.carHire.MainDriverAge.Value);
            if (this.carHire.PickupTime != null) ExtrasPage.SelectCarHirePickupTime(this.carHire.PickupTime);
            if (this.carHire.DropoffTime != null) ExtrasPage.SelectCarHireDropoffTime(this.carHire.DropoffTime);
            if (this.carHire.CarHireNumber != null) ExtrasPage.CheckCarHireNumber(this.carHire.CarHireNumber.Value);

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
