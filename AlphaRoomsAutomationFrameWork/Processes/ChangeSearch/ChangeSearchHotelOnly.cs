using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaRooms.AutomationFramework.Processes.ChangeSearch.Interfaces;

namespace AlphaRooms.AutomationFramework.Processes.ChangeSearch
{
    public class ChangeSearchHotelOnly : ISearchHotelOnlyRoom, ISearchHotelOnly, ISearchHotelOnlyChildren
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
        private List<Room> rooms;
        private Panels.ChangeSearchPanel changeSearchPanel;

        public ChangeSearchHotelOnly()
        {
            this.rooms = new List<Room>();
            this.rooms.Add(new Room());
        }

        public ChangeSearchHotelOnly(Panels.ChangeSearchPanel changeSearchPanel)
        {
            this.changeSearchPanel = changeSearchPanel;
            this.rooms = new List<Room>();
            this.rooms.Add(new Room());
        }

        public ISearchHotelOnly ToDestination(string dest)
        {
            destination = dest;
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
            this.changeSearchPanel.ClickHotelOnly();
            if (destination != null) this.changeSearchPanel.TypeHotelDestination(destination);
            if (checkIn != null) this.changeSearchPanel.SelectCheckIn(checkIn);
            if (checkOut != null) this.changeSearchPanel.SelectCheckOut(checkOut);
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
