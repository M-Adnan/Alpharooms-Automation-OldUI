using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AlphaRooms.AutomationFramework.Selenium;
using AlphaRooms.AutomationFramework.Tests.Properties;

namespace AlphaRooms.AutomationFramework.Tests.UnitTests
{
    [TestFixture]
    public class HotelResultsPageTester
    {
        [SetUp]
        public void Initialise()
        {
            Driver.Initialize(Settings.Default.SeleniumExecuteLocally, Settings.Default.SeleniumRemoteServerURL, Settings.Default.SeleniumBrowser);
            HomePage.Navigate((TestEnvironment)Enum.Parse(typeof(TestEnvironment), Settings.Default.TestingEnvironment));
            Assert.That(HomePage.IsDisplayed, Is.True, "Home Page isn't displayed");
        }

        [TearDown]
        public void CleanUp()
        {
            Driver.Close();
        }
        
        [Test]
        [Category("FrameworkTests")]
        public void HotelResultsPageOnlyOneRoomShouldReturnValidData()
        {
            string destination = "London, United Kingdom";
            string checkIn = DateTime.Today.AddMonths(5).ToShortDateString();
            string checkOut = DateTime.Today.AddMonths(5).AddDays(7).ToShortDateString();
            int adults = 4;
            int children = 2;
            int[] childrenAges = new int[] { 5, 7 };

            HomePage.SearchFor().HotelOnly().ToHotelName(destination).FromCheckIn(checkIn).ToCheckOut(checkOut)
                .ForAdults(adults).WithChildren(children).OfAges(childrenAges).SearchAndCapture();

            //check if result page is displayed
            Assert.AreEqual(true, HotelResultsPage.IsDisplayed(), "Hotel Search Result Page isn't displayed");

            int hotelNumber = 1, availableRoom = 2;
            HotelResultsPage.SelectRoom().ByHotelNumber(hotelNumber).OnlyOneRoomWithAvailableRoom(availableRoom).ContinueAndCapture();

            //check if result page is displayed
            Assert.AreEqual(true, ExtrasPage.IsDisplayed(), "Hotel Extras Page isn't displayed");

            Assert.IsTrue(!String.IsNullOrEmpty(HotelResultsPage.Data.HotelSearchGuid));
            Assert.IsTrue(HotelResultsPage.Data.LoadingTime.TotalMilliseconds > 0);
            Assert.AreEqual(hotelNumber, HotelResultsPage.Data.HotelNumber);
            Assert.AreEqual(1, HotelResultsPage.Data.Rooms.Length);
            Assert.AreEqual(availableRoom, HotelResultsPage.Data.Rooms[0].AvailableRoom);
        }

        [Test]
        [Category("FrameworkTests")]
        public void HotelResultsPageOnlyOneRoomByHotelNameShouldReturnValidData()
        {
            string destination = "London, United Kingdom";
            string checkIn = DateTime.Today.AddMonths(5).ToShortDateString();
            string checkOut = DateTime.Today.AddMonths(5).AddDays(7).ToShortDateString();
            int adults = 4;
            int children = 2;
            int[] childrenAges = new int[] { 5, 7 };

            HomePage.SearchFor().HotelOnly().ToHotelName(destination).FromCheckIn(checkIn).ToCheckOut(checkOut)
                .ForAdults(adults).WithChildren(children).OfAges(childrenAges).SearchAndCapture();

            //check if result page is displayed
            Assert.AreEqual(true, HotelResultsPage.IsDisplayed(), "Hotel Search Result Page isn't displayed");

            string hotelName = "Somerset Hotel";
            int availableRoom = 2;
            HotelResultsPage.SelectRoom().ByHotelName(hotelName).OnlyOneRoomWithAvailableRoom(availableRoom).ContinueAndCapture();

            //check if result page is displayed
            Assert.AreEqual(true, ExtrasPage.IsDisplayed(), "Hotel Extras Page isn't displayed");
            
            Assert.IsTrue(!String.IsNullOrEmpty(HotelResultsPage.Data.HotelSearchGuid));
            Assert.IsTrue(HotelResultsPage.Data.LoadingTime.TotalMilliseconds > 0);
            Assert.AreEqual(hotelName, HotelResultsPage.Data.HotelName);
            Assert.AreEqual(1, HotelResultsPage.Data.Rooms.Length);
            Assert.AreEqual(availableRoom, HotelResultsPage.Data.Rooms[0].AvailableRoom);
        }

        //[Test]
        //[Category("FrameworkTests")]
        // cannot test in live environment 
        //public void HotelResultsPageOnlyOneRoomByFirstSupplierShouldReturnValidData()
        //{
        //    string destination = "London, United Kingdom";
        //    string checkIn = DateTime.Today.AddMonths(5).ToShortDateString();
        //    string checkOut = DateTime.Today.AddMonths(5).AddDays(7).ToShortDateString();
        //    int adults = 4;
        //    int children = 2;
        //    int[] childrenAges = new int[] { 5, 7 };

        //    HomePage.SearchFor().HotelOnly().ToDestination(destination).FromCheckIn(checkIn).ToCheckOut(checkOut)
        //        .ForAdults(adults).WithChildren(children).OfAges(childrenAges).SearchAndCapture();

        //            //check if result page is displayed
        //    Assert.AreEqual(Is.True, HotelResultsPage.IsDisplayed(), "Hotel Search Result Page isn't displayed");

        //    string supplierName = "Somerset Hotel";
        //    int roomNumber = 1, availableRoom = 2;
        //    HotelResultsPage.SelectRoom().ByFirstSupplier(supplierName).ContinueAndCapture();

        //            //check if result page is displayed
        //        Assert.AreEqual(true, ExtrasPage.IsDisplayed(), "Hotel Extras Page isn't displayed");

        //    Assert.IsTrue(!String.IsNullOrEmpty(HotelResultsPage.Data.HotelSearchGuid));
        //    Assert.IsTrue(HotelResultsPage.Data.LoadingTime.TotalMilliseconds > 0);
        //    Assert.AreEqual(1, HotelResultsPage.Data.Rooms.Length);
        //    Assert.AreEqual(availableRoom, HotelResultsPage.Data.Rooms[0].AvailableRoom);
        //}


        [Test]
        [Category("FrameworkTests")]
        public void HotelResultsPageMultipleRoomsShouldReturnValidData()
        {
            string destination = "London, United Kingdom";
            string checkIn = DateTime.Today.AddMonths(5).ToShortDateString();
            string checkOut = DateTime.Today.AddMonths(5).AddDays(7).ToShortDateString();
            int adults = 2;
            int children = 1;
            int[] childrenAges = new int[] { 5 };
            int adults2 = 1;
            int children2 = 2;
            int[] childrenAges2 = new int[] { 1, 2 };
            int adults3 = 3;
            int children3 = 1;
            int[] childrenAges3 = new int[] { 3 };

            HomePage.SearchFor().HotelOnly().ToHotelName(destination).FromCheckIn(checkIn).ToCheckOut(checkOut)
                .ForAdults(adults).WithChildren(children).OfAges(childrenAges)
                .AddAnotherRoom().ForAdults(adults2).WithChildren(children2).OfAges(childrenAges2)
                .AddAnotherRoom().ForAdults(adults3).WithChildren(children3).OfAges(childrenAges3)
                .SearchAndCapture();

            //check if result page is displayed
            Assert.AreEqual(true, HotelResultsPage.IsDisplayed(), "Hotel Search Result Page isn't displayed");

            string hotelName = "Hilton London Metropole";
            int availableRoom = 1, availableRoom2 = 2, availableRoom3 = 1;
            HotelResultsPage.SelectRoom().ByHotelName(hotelName)
                .ForRoomNumber(1).WithAvailableRoom(availableRoom)
                .ForRoomNumber(2).WithAvailableRoom(availableRoom2)
                .ForRoomNumber(3).WithAvailableRoom(availableRoom3).ContinueAndCapture();

            //check if result page is displayed
            Assert.AreEqual(true, ExtrasPage.IsDisplayed(), "Hotel Extras Page isn't displayed");

            Assert.IsTrue(!String.IsNullOrEmpty(HotelResultsPage.Data.HotelSearchGuid));
            Assert.IsTrue(HotelResultsPage.Data.LoadingTime.TotalMilliseconds > 0);
            Assert.AreEqual(hotelName, HotelResultsPage.Data.HotelName);
            Assert.AreEqual(3, HotelResultsPage.Data.Rooms.Length);
            Assert.AreEqual(availableRoom, HotelResultsPage.Data.Rooms[0].AvailableRoom);
            Assert.AreEqual(availableRoom2, HotelResultsPage.Data.Rooms[1].AvailableRoom);
            Assert.AreEqual(availableRoom3, HotelResultsPage.Data.Rooms[2].AvailableRoom);
        }
    }
}
