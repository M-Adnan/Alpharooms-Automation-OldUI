using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaRooms.AutomationFramework.Processes.Home.Interfaces;

namespace AlphaRooms.AutomationFramework.Processes.Home
{
    public class SearchFlightOnly : ISearchFlightOnly, ISearchFlightOnlyChildren
    {
        private string destination;
        private string checkIn;
        private string checkOut;
        private string deptAirport;
        private int? adults;
        private int? children;
        private int[] childrenAges;

        public SearchFlightOnly()
        {
        }

        public ISearchFlightOnly ToDestination(string dest)
        {
            destination = dest;
            return this;
        }

        public ISearchFlightOnly FromCheckIn(string From)
        {
            checkIn = From;
            return this;
        }

        public ISearchFlightOnly ToCheckOut(string To)
        {
            checkOut = To;
            return this;
        }

        public ISearchFlightOnly ForAdults(int Adu)
        {
            adults = Adu;
            return this;
        }

        public ISearchFlightOnly FromDepartureAirport(string airport)
        {
            deptAirport = airport;
            return this;
        }

        public ISearchFlightOnlyChildren WithChildren(int Child)
        {
            children = Child;
            return this;
        }

        public ISearchFlightOnly OfAges(params int[] ChildrenAges)
        {
            this.childrenAges = ChildrenAges;
            return this;
        }

        private void SearchProcess()
        {
            HomePage.ClickFlightOnly();
            if (destination != null) HomePage.TypeFlightDestination(destination);
            if (checkIn != null) HomePage.SelectCheckIn(checkIn);
            if (checkOut != null) HomePage.SelectCheckOut(checkOut);
            if (deptAirport != null) HomePage.SelectAirport(deptAirport);
            if (adults != null) HomePage.SelectAdults(adults.Value);
            if (children != null) HomePage.SelectChildren(children.Value, childrenAges);
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
