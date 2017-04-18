using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaRooms.AutomationFramework.Processes.Home.Interfaces;

namespace AlphaRooms.AutomationFramework.Processes.Home
{
    public class SearchHotelOnly : ISearchHotelOnlyRoom, ISearchHotelOnly, ISearchHotelOnlyChildren
    {
        private class Room
        {
            public int? Adults { get; set; }
            public int? Children { get; set; }
            public int[] ChildrenAges { get; set; }
        }

        private string destination;
        private bool isHotel;
        private string checkIn;
        private string checkOut;
        private List<Room> rooms;

        public SearchHotelOnly()
        {
            this.rooms = new List<Room>();
            this.rooms.Add(new Room());
        }

        public ISearchHotelOnly ToDestination(string dest)
        {
            destination = dest;
            isHotel = false;
            return this;
        }

        public ISearchHotelOnly ToHotelName(string hot)
        {
            destination = hot;
            isHotel = true;
            return this;
        }

        public ISearchHotelOnly FromCheckIn(string From)
        {
            checkIn = From;
            return this;
        }

        public ISearchHotelOnly ToCheckOut(string To)
        {
            checkOut = To;
            return this;
        }

        public ISearchHotelOnly ForAdults(int Adu)
        {
            this.rooms.Last().Adults = Adu;
            return this;
        }

        public ISearchHotelOnlyChildren WithChildren(int Child)
        {
            this.rooms.Last().Children = Child;
            return this;
        }

        public ISearchHotelOnly OfAges(params int[] ChildrenAges)
        {
            this.rooms.Last().ChildrenAges = ChildrenAges;
            return this;
        }

        public ISearchHotelOnlyRoom AddAnotherRoom()
        {
            this.rooms.Add(new Room());
            return this;
        }

        ISearchHotelOnly ISearchHotelOnlyRoom.AutoFill()
        {
            this.rooms.Last().Adults = HomePageRnd.PickRandomAdults();
            this.rooms.Last().Children = HomePageRnd.PickRandomChildren();
            this.rooms.Last().ChildrenAges = HomePageRnd.PickRandomChildrenAges();
            return this;
        }

        private void SearchProcess()
        {
            HomePage.ClickHotelOnly();
            if (destination != null) HomePage.TypeHotelDestination(destination, isHotel);
            if (checkIn != null) HomePage.SelectCheckIn(checkIn);
            if (checkOut != null) HomePage.SelectCheckOut(checkOut);
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
