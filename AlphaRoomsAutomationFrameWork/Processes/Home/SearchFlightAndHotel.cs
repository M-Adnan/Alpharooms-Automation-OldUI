using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaRooms.AutomationFramework.Processes.Home.Interfaces;

namespace AlphaRooms.AutomationFramework.Processes.Home
{
    public class SearchFlightAndHotel : ISearchFlightAndHotelRoom, ISearchFlightAndHotel, ISearchFlightAndHotelChildren
    {
        private class Room
        {
            public int? Adults { get; set; }
            public int? Children { get; set; }
            public int[] ChildrenAges { get; set; }
        }

        private bool isHotel;
        private string destination;
        private string checkIn;
        private string checkOut;
        private string deptAirport;
        private List<Room> rooms;

        public SearchFlightAndHotel()
        {
            this.rooms = new List<Room>();
            this.rooms.Add(new Room());
        }

        public ISearchFlightAndHotel ToDestination(string dest)
        {
            destination = dest;
            isHotel = false;
            return this;
        }

        public ISearchFlightAndHotel ToHotelName(string hot)
        {
            destination = hot;
            isHotel = true;
            return this;
        }

        public ISearchFlightAndHotel FromCheckIn(string From)
        {
            checkIn = From;
            return this;
        }

        public ISearchFlightAndHotel ToCheckOut(string To)
        {
            checkOut = To;
            return this;
        }
        
        public ISearchFlightAndHotel FromDepartureAirport(string airport)
        {
            deptAirport = airport;
            return this;
        }
        
        public ISearchFlightAndHotel ForAdults(int Adu)
        {
            this.rooms.Last().Adults = Adu;
            return this;
        }

        public ISearchFlightAndHotelChildren WithChildren(int Child)
        {
            this.rooms.Last().Children = Child;
            return this;
        }

        public ISearchFlightAndHotel OfAges(params int[] ChildrenAges)
        {
            this.rooms.Last().ChildrenAges = ChildrenAges;
            return this;
        }

        public ISearchFlightAndHotelRoom AddAnotherRoom()
        {
            this.rooms.Add(new Room());
            return this;
        }

        ISearchFlightAndHotel ISearchFlightAndHotelRoom.AutoFill()
        {
            this.rooms.Last().Adults = HomePageRnd.PickRandomAdults();
            this.rooms.Last().Children = HomePageRnd.PickRandomChildren();
            this.rooms.Last().ChildrenAges = HomePageRnd.PickRandomChildrenAges();
            return this;
        }

        private void SearchProcess()
        {
            HomePage.ClickFlightAndHotel();
            if (destination != null) HomePage.TypeHotelDestination(destination,isHotel);
            if (checkIn != null) HomePage.SelectCheckIn(checkIn);
            if (checkOut != null) HomePage.SelectCheckOut(checkOut);
            if (deptAirport != null) HomePage.SelectAirport(deptAirport);
            foreach (Room room in this.rooms)
            {
                if (this.rooms.IndexOf(room) > 0) HomePage.ClickAddAnotherRoom();
                if (room.Adults != null) HomePage.SelectAdults(room.Adults.Value);
                if (room.Children != null) HomePage.SelectChildren(room.Children.Value, room.ChildrenAges);
            }
        }

        public void Search()
        {
            this.SearchProcess();
            HomePage.ClickSearch();
        }

        public void SearchAndCapture()
        {
            this.SearchProcess();
            HomePage.ClickSearchAndCapture();
        }
    }
}
