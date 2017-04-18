using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using AlphaRooms.AutomationFramework.Selenium;

namespace AlphaRooms.AutomationFramework
{
    public class Calendar
    {
        private static string lastCheckInDate;
        internal static Random random = new Random();

        internal static string FormatMonthYear(string date)
        {
            return DateTime.Parse(date).ToString("MMMM yyyy");
        }

        internal static string FormatDate(string date)
        {
            return DateTime.Parse(date).ToString("dd MMM yyyy");
        } 

        public static string PickRandomCheckInDate()
        {
            DateTime StartDate = DateTime.Now.AddMonths(3);
            DateTime EndDate = DateTime.Now.AddMonths(7);
            int range = (EndDate - StartDate).Days;
            DateTime rDate = StartDate.AddDays(random.Next(range));
            lastCheckInDate = rDate.ToShortDateString();
            return lastCheckInDate;
        }

        public static string PickRandomCheckOutDate()
        {
            DateTime StartDate = Convert.ToDateTime(lastCheckInDate).AddDays(1);
            DateTime EndDate = StartDate.AddDays(random.Next(5, 9));
            return EndDate.ToShortDateString();
        }
    }
}
