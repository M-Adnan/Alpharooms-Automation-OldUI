using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaRooms.AutomationFramework.Processes.Insurance.Interfaces;

namespace AlphaRooms.AutomationFramework.Processes.Insurance
{
    public class UpdatePricesStarter : IUpdatePrices
    {
        private string destination;
        private int? adults;
        private int[] adultsAges;
        private int? children;
        private int[] childrenAges;
        private string relationship;
        private string startDate;
        private string endDate;

        public IUpdatePrices ToDestination(string p)
        {
            this.destination = p;
            return this;
        }

        public IUpdatePrices ForAdults(int p)
        {
            this.adults = p;
            return this;
        }

        public IUpdatePrices OfAdultAges(params int[] p)
        {
            this.adultsAges = p;
            return this;
        }

        public IUpdatePrices WithChildren(int p)
        {
            this.children = p;
            return this;
        }

        public IUpdatePrices OfChildrenAges(params int[] p)
        {
            this.childrenAges = p;
            return this;
        }

        public IUpdatePrices OfAges(params int[] p)
        {
            this.childrenAges = p;
            return this;
        }

        public IUpdatePrices WithRelationship(string p)
        {
            this.relationship = p;
            return this;
        }

        public IUpdatePrices FromStartDate(string p)
        {
            this.startDate = p;
            return this;
        }

        public IUpdatePrices ToEndDate(string p)
        {
            this.endDate = p;
            return this;
        }

        private void UpdateProcess()
        {
            if (this.destination != null) InsurancePage.TypeDestination(this.destination);
            if (this.adults != null) InsurancePage.SelectAdults(this.adults.Value);
            if (this.adultsAges != null) InsurancePage.SelectAdultsAge(this.adultsAges);
            if (this.children != null) InsurancePage.SelectChildren(this.children.Value);
            if (this.childrenAges != null) InsurancePage.SelectChildrenAges(this.childrenAges);
            if (this.relationship != null) InsurancePage.SelectRelationship(this.relationship);
            if (this.startDate != null) InsurancePage.SelectStartDate(this.startDate);
            if (this.endDate != null) InsurancePage.SelectEndDate(this.endDate);
        }

        public void Update()
        {
            this.UpdateProcess();
            InsurancePage.ClickUpdatePrices();
        }

        public void UpdateAndCapture()
        {
            this.UpdateProcess();
            InsurancePage.ClickUpdatePricesAndCapture();
        }
    }
}
