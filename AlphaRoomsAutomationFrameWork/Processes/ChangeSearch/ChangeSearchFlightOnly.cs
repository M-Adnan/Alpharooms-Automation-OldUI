using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaRooms.AutomationFramework.Processes.ChangeSearch.Interfaces;

namespace AlphaRooms.AutomationFramework.Processes.ChangeSearch
{
    public class ChangeSearchFlightOnly : ISearchFlightOnly, ISearchFlightOnlyChildren
    {
        private string destination;
        private string checkIn;
        private string checkOut;
        private string deptAirport;
        private int? adults;
        private int? children;
        private int[] childrenAges;
        private Panels.ChangeSearchPanel changeSearchPanel;

        public ChangeSearchFlightOnly()
        {
        }

        public ChangeSearchFlightOnly(Panels.ChangeSearchPanel changeSearchPanel)
        {
            this.changeSearchPanel = changeSearchPanel;
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
            this.changeSearchPanel.ClickChangeSearch();
            this.changeSearchPanel.ClickFlightOnly();
            if (destination != null) this.changeSearchPanel.TypeFlightDestination(destination);
            if (checkIn != null) this.changeSearchPanel.SelectCheckIn(checkIn);
            if (checkOut != null) this.changeSearchPanel.SelectCheckOut(checkOut);
            if (deptAirport != null) this.changeSearchPanel.SelectAirport(deptAirport);
            if (adults != null) this.changeSearchPanel.SelectAdults(adults.Value);
            if (children != null) this.changeSearchPanel.SelectChildren(children.Value, childrenAges);
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
