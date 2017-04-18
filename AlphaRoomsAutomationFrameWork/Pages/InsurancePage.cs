using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaRooms.AutomationFramework.PageData;
using AlphaRooms.AutomationFramework.Processes.Insurance.Interfaces;
using AlphaRooms.AutomationFramework.Processes.Insurance;
using AlphaRooms.AutomationFramework.Panels;

namespace AlphaRooms.AutomationFramework
{
    public static class InsurancePage
    {
        private static InsurancePageData data = new InsurancePageData();
        public static InsurancePageData Data { get { return data; } }

        private static TopPanel topPanel = new TopPanel();
        public static TopPanel TopPanel { get { return topPanel; } }

        public static bool IsDisplayed()
        {
            throw new NotImplementedException();
            //return Driver.Instance.IsElementDisplayedById("paymentpage");
        }

        internal static void WaitForLoad()
        {
            PaymentPage.ResetData();
            throw new NotImplementedException();
            //NonFunctionalReq.CaptureTime(() => Driver.Instance.FindElementWithTimeout(By.Id("submitPaymentButton"), 60, "Payment Page  not loaded within 60 sec"), "Payment page load time is");
        }

        internal static void ResetData()
        {
            InsurancePage.data = new InsurancePageData();
            InsurancePage.topPanel = new TopPanel();
        }

        public static IUpdatePrices UpdatePrices()
        {
            return new UpdatePricesStarter();
        }

        public static void TypeDestination(string p)
        {
            Logger.AddTypeAction("Destination", p);
            throw new NotImplementedException();
        }

        public static void SelectAdults(int p)
        {
            Logger.AddSelectAction("Adults", p);
            throw new NotImplementedException();
        }

        public static void SelectAdultsAge(int[] p)
        {
            Logger.AddSelectAction("AdultsAge", p);
            throw new NotImplementedException();
        }

        public static void SelectChildren(int p)
        {
            Logger.AddSelectAction("Children", p);
            throw new NotImplementedException();
        }

        public static void SelectChildrenAges(int[] p)
        {
            Logger.AddSelectAction("ChildrenAges", p);
            throw new NotImplementedException();
        }

        public static void SelectRelationship(string p)
        {
            Logger.AddSelectAction("Relationship", p);
            throw new NotImplementedException();
        }

        public static void SelectStartDate(string p)
        {
            Logger.AddSelectAction("StartDate", p);
            throw new NotImplementedException();
        }

        public static void SelectEndDate(string p)
        {
            Logger.AddSelectAction("EndDate", p);
            throw new NotImplementedException();
        }

        public static void ClickUpdatePrices()
        {
            Logger.AddClickAction("UpdatePrices");
            throw new NotImplementedException();
        }

        public static void ClickUpdatePricesAndCapture()
        {
            Logger.AddClickAction("kUpdatePricesAndCapture");
            throw new NotImplementedException();
        }
    }
}
