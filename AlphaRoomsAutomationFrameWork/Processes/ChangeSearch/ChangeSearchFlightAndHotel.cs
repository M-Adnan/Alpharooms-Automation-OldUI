using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaRooms.AutomationFramework.Processes.ChangeSearch.Interfaces;

namespace AlphaRooms.AutomationFramework.Processes.ChangeSearch
{
    public class ChangeSearchFlightAndHotel : ISearchFlightAndHotelRoom, ISearchFlightAndHotel, ISearchFlightAndHotelChildren
    {
        private class Room
        {
            public int? Adults { get; set; }
            public int? Children { get; set; }
            public int[] ChildrenAges { get; set; }
        }

        private string destination;
        private string checkIn;
        private string checkOut;
        private string deptAirport;
        private List<Room> rooms;
        private Panels.ChangeSearchPanel changeSearchPanel;

        public ChangeSearchFlightAndHotel()
        {
            this.rooms = new List<Room>();
            this.rooms.Add(new Room());
        }

        public ChangeSearchFlightAndHotel(Panels.ChangeSearchPanel changeSearchPanel)
        {
            this.changeSearchPanel = changeSearchPanel;
            this.rooms = new List<Room>();
            this.rooms.Add(new Room());
        }

        public ISearchFlightAndHotel ToDestination(string dest)
        {
            destination = dest;
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
            this.changeSearchPanel.ClickFlightAndHotel();
            if (destination != null) this.changeSearchPanel.TypeHotelDestination(destination);
            if (checkIn != null) this.changeSearchPanel.SelectCheckIn(checkIn);
            if (checkOut != null) this.changeSearchPanel.SelectCheckOut(checkOut);
            if (deptAirport != null) this.changeSearchPanel.SelectAirport(deptAirport);
            foreach (Room room in this.rooms)
            {
                if (this.rooms.IndexOf(room) > 0) this.changeSearchPanel.ClickAddAnotherRoom();
                if (room.Adults != null) this.changeSearchPanel.SelectAdults(room.Adults.Value);
                if (room.Children != null) this.changeSearchPanel.SelectChildren(room.Children.Value, room.ChildrenAges);
            }
        }

        public void ShowPrices()
        {
            this.SearchProcess();
            this.changeSearchPanel.ClickShowPrices();
        }

        public void ShowPricesAndCapture()
        {
            this.SearchProcess();
            this.changeSearchPanel.ClickShowPricesAndCapture();
        }
    }
}
