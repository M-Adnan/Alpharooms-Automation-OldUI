using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AlphaRooms.AutomationFramework.Functions;
using AlphaRooms.AutomationFramework.Selenium;
using AlphaRooms.AutomationFramework.Tests.Properties;

namespace AlphaRooms.AutomationFramework.Tests.UnitTests
{
    [TestFixture]
    public class HomePageTester
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

        //[Test]
        //[Category("FrameworkTests")]
        //[ExpectedException(typeof(Exception))]
        //public void HomePageShouldThrowExceptionWhenFlightDestinationIsNotValid()
        //{
        //    HomePage.ClickFlightOnly();
        //    HomePage.TypeFlightDestination("My Home");
        //}

        //[Test]
        //[Category("FrameworkTests")]
        //[ExpectedException(typeof(Exception))]
        //public void HomePageShouldThrowExceptionWhenHotelDestinationIsNotValid()
        //{
        //    HomePage.ClickHotelOnly();
        //    HomePage.TypeHotelDestination("My Flight My Flight");
        //}

        [Test]
        [Category("FrameworkTests")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HomePageShouldThrowExceptionWhenCheckInIsInPast()
        {
            HomePage.ClickFlightOnly();
            //HomePage.SelectCheckIn(Calendar.FormatDate(DateTime.Now.AddDays(-1).ToString()));
        }

        [Test]
        [Category("FrameworkTests")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HomePageShouldThrowExceptionWhenCheckOutIsInPast()
        {
            HomePage.ClickFlightOnly();
            //HomePage.SelectCheckOut(Calendar.FormatDate(DateTime.Now.AddDays(-1).ToString()));
        }

        [Test]
        [Category("FrameworkTests")]
        [ExpectedException(typeof(ArgumentException))]
        public void HomePageShouldThrowExceptionWhenChildrenAndChildrenAgesDontMatch()
        {
            HomePage.ClickFlightOnly();
            HomePage.SelectChildren(1, new int[] { 1, 2 });
        }

        [Test]
        [Category("FrameworkTests")]
        public void HomePageFlightOnlyShouldReturnValidData()
        {
            string destination = "Alicante, Benidorm, Spain (ALC)";
            string departure = "London Gatwick, London, United Kingdom (LGW)";
            string checkIn = DateTime.Today.AddMonths(5).ToShortDateString();
            string checkOut  = DateTime.Today.AddMonths(5).AddDays(7).ToShortDateString();
            int adults = 4;
            int children = 2;
            int[] childrenAges = new int[]{5,7};

            HomePage.SearchFor().FlightOnly().ToDestination(destination).FromCheckIn(checkIn).ToCheckOut(checkOut).FromDepartureAirport(departure)
                .ForAdults(adults).WithChildren(children).OfAges(childrenAges).SearchAndCapture();

            //check if result page is displayed
            Assert.AreEqual(true, FlightResultsPage.IsDisplayed(), "Flight Search Result Page isn't displayed");

            Assert.AreEqual(SearchOption.FlightOnly, HomePage.Data.SearchOption);
            Assert.IsTrue(HomePage.Data.SearchTime.TotalMilliseconds > 0);
            Assert.AreEqual(destination, HomePage.Data.Destination);
            Assert.AreEqual(departure, HomePage.Data.DepartureAirport);
            Assert.AreEqual(DateTime.Parse(checkIn).ToString("dd MMM yyyy"), HomePage.Data.CheckInDate);
            Assert.AreEqual(DateTime.Parse(checkOut).ToString("dd MMM yyyy"), HomePage.Data.CheckOutDate);
            Assert.AreEqual(1, HomePage.Data.Rooms.Length);
            Assert.AreEqual(adults, HomePage.Data.Rooms[0].Adults);
            Assert.AreEqual("4 Adults", HomePage.Data.Rooms[0].AdultsLabel);
            Assert.AreEqual(children, HomePage.Data.Rooms[0].Children);
            Assert.AreEqual("2 Children", HomePage.Data.Rooms[0].ChildrenLabel);
            Assert.AreEqual(childrenAges, HomePage.Data.Rooms[0].ChildrenAges);
        }
        
        [Test]
        [Category("FrameworkTests")]
        public void HomePageHotelOnlyOneRoomShouldReturnValidData()
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

            Assert.AreEqual(SearchOption.HotelOnly, HomePage.Data.SearchOption);
            Assert.IsTrue(HomePage.Data.SearchTime.TotalMilliseconds > 0);
            Assert.AreEqual(destination, HomePage.Data.Destination);
            Assert.AreEqual(DateTime.Parse(checkIn).ToString("dd MMM yyyy"), HomePage.Data.CheckInDate);
            Assert.AreEqual(DateTime.Parse(checkOut).ToString("dd MMM yyyy"), HomePage.Data.CheckOutDate);
            Assert.AreEqual(1, HomePage.Data.Rooms.Length);
            Assert.AreEqual(adults, HomePage.Data.Rooms[0].Adults);
            Assert.AreEqual("4 Adults", HomePage.Data.Rooms[0].AdultsLabel);
            Assert.AreEqual(children, HomePage.Data.Rooms[0].Children);
            Assert.AreEqual("2 Children", HomePage.Data.Rooms[0].ChildrenLabel);
            Assert.AreEqual(childrenAges, HomePage.Data.Rooms[0].ChildrenAges);
        }

        [Test]
        [Category("FrameworkTests")]
        public void HomePageHotelOnlyMultipleRoomsShouldReturnValidData()
        {
            string destination = "London, United Kingdom";
            string checkIn = DateTime.Today.AddMonths(5).ToShortDateString();
            string checkOut = DateTime.Today.AddMonths(5).AddDays(7).ToShortDateString();
            int adults = 4;
            int children = 2;
            int[] childrenAges = new int[] { 5, 7 };
            int adults2 = 1;
            int children2 = 4;
            int[] childrenAges2 = new int[] { 1, 2, 3, 4 };
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

            Assert.AreEqual(SearchOption.HotelOnly, HomePage.Data.SearchOption);
            Assert.IsTrue(HomePage.Data.SearchTime.TotalMilliseconds > 0);
            Assert.AreEqual(destination, HomePage.Data.Destination);
            Assert.AreEqual(DateTime.Parse(checkIn).ToString("dd MMM yyyy"), HomePage.Data.CheckInDate);
            Assert.AreEqual(DateTime.Parse(checkOut).ToString("dd MMM yyyy"), HomePage.Data.CheckOutDate);
            Assert.AreEqual(3, HomePage.Data.Rooms.Length);
            Assert.AreEqual(adults, HomePage.Data.Rooms[0].Adults);
            Assert.AreEqual("4 Adults", HomePage.Data.Rooms[0].AdultsLabel);
            Assert.AreEqual(children, HomePage.Data.Rooms[0].Children);
            Assert.AreEqual("2 Children", HomePage.Data.Rooms[0].ChildrenLabel);
            Assert.AreEqual(childrenAges, HomePage.Data.Rooms[0].ChildrenAges);
            Assert.AreEqual(adults2, HomePage.Data.Rooms[1].Adults);
            Assert.AreEqual("1 Adult", HomePage.Data.Rooms[1].AdultsLabel);
            Assert.AreEqual(children2, HomePage.Data.Rooms[1].Children);
            Assert.AreEqual("4 Children", HomePage.Data.Rooms[1].ChildrenLabel);
            Assert.AreEqual(childrenAges2, HomePage.Data.Rooms[1].ChildrenAges);
            Assert.AreEqual(adults3, HomePage.Data.Rooms[2].Adults);
            Assert.AreEqual("3 Adults", HomePage.Data.Rooms[2].AdultsLabel);
            Assert.AreEqual(children3, HomePage.Data.Rooms[2].Children);
            Assert.AreEqual("1 Child", HomePage.Data.Rooms[2].ChildrenLabel);
            Assert.AreEqual(childrenAges3, HomePage.Data.Rooms[2].ChildrenAges);
        }
        
        [Test]
        [Category("FrameworkTests")]
        public void HomePageFlightAndHotelOneRoomShouldReturnValidData()
        {
            string destination = "London, United Kingdom";
            string departure = "London Gatwick, London, United Kingdom (LGW)";
            string checkIn = DateTime.Today.AddMonths(5).ToShortDateString();
            string checkOut = DateTime.Today.AddMonths(5).AddDays(7).ToShortDateString();
            int adults = 4;
            int children = 2;
            int[] childrenAges = new int[] { 5, 7 };

            HomePage.SearchFor().FlightAndHotel().ToDestination(destination).FromCheckIn(checkIn).ToCheckOut(checkOut)
                .FromDepartureAirport(departure)
                .ForAdults(adults).WithChildren(children).OfAges(childrenAges).SearchAndCapture();

            //check if result page is displayed
            Assert.AreEqual(true, FlightResultsPage.IsDisplayed(), "Flight Search Result Page isn't displayed");
            
            Assert.AreEqual(SearchOption.FlightAndHotel, HomePage.Data.SearchOption);
            Assert.IsTrue(HomePage.Data.SearchTime.TotalMilliseconds > 0);
            Assert.AreEqual(destination, HomePage.Data.Destination);
            Assert.AreEqual(DateTime.Parse(checkIn).ToString("dd MMM yyyy"), HomePage.Data.CheckInDate);
            Assert.AreEqual(DateTime.Parse(checkOut).ToString("dd MMM yyyy"), HomePage.Data.CheckOutDate);
            Assert.AreEqual(1, HomePage.Data.Rooms.Length);
            Assert.AreEqual(adults, HomePage.Data.Rooms[0].Adults);
            Assert.AreEqual("4 Adults", HomePage.Data.Rooms[0].AdultsLabel);
            Assert.AreEqual(children, HomePage.Data.Rooms[0].Children);
            Assert.AreEqual("2 Children", HomePage.Data.Rooms[0].ChildrenLabel);
            Assert.AreEqual(childrenAges, HomePage.Data.Rooms[0].ChildrenAges);
        }

        [Test]
        [Category("FrameworkTests")]
        public void HomePageFlightAndHotelMultipleRoomsShouldReturnValidData()
        {
            string destination = "London, United Kingdom";
            string departure = "London Gatwick, London, United Kingdom (LGW)";
            string checkIn = DateTime.Today.AddMonths(5).ToShortDateString();
            string checkOut = DateTime.Today.AddMonths(5).AddDays(7).ToShortDateString();
            int adults = 4;
            int children = 2;
            int[] childrenAges = new int[] { 5, 7 };
            int adults2 = 1;
            int children2 = 4;
            int[] childrenAges2 = new int[] { 1, 2, 3, 4 };
            int adults3 = 3;
            int children3 = 1;
            int[] childrenAges3 = new int[] { 3 };

            HomePage.SearchFor().FlightAndHotel().ToDestination(destination).FromCheckIn(checkIn).ToCheckOut(checkOut)
                .FromDepartureAirport(departure)
                .ForAdults(adults).WithChildren(children).OfAges(childrenAges)
                .AddAnotherRoom().ForAdults(adults2).WithChildren(children2).OfAges(childrenAges2)
                .AddAnotherRoom().ForAdults(adults3).WithChildren(children3).OfAges(childrenAges3)
                .SearchAndCapture();

            //check if result page is displayed
            Assert.AreEqual(true, FlightResultsPage.IsDisplayed(), "Flight Search Result Page isn't displayed");
            
            Assert.AreEqual(SearchOption.FlightAndHotel, HomePage.Data.SearchOption);
            Assert.IsTrue(HomePage.Data.SearchTime.TotalMilliseconds > 0);
            Assert.AreEqual(destination, HomePage.Data.Destination);
            Assert.AreEqual(DateTime.Parse(checkIn).ToString("dd MMM yyyy"), HomePage.Data.CheckInDate);
            Assert.AreEqual(DateTime.Parse(checkOut).ToString("dd MMM yyyy"), HomePage.Data.CheckOutDate);
            Assert.AreEqual(3, HomePage.Data.Rooms.Length);
            Assert.AreEqual(adults, HomePage.Data.Rooms[0].Adults);
            Assert.AreEqual("4 Adults", HomePage.Data.Rooms[0].AdultsLabel);
            Assert.AreEqual(children, HomePage.Data.Rooms[0].Children);
            Assert.AreEqual("2 Children", HomePage.Data.Rooms[0].ChildrenLabel);
            Assert.AreEqual(childrenAges, HomePage.Data.Rooms[0].ChildrenAges);
            Assert.AreEqual(adults2, HomePage.Data.Rooms[1].Adults);
            Assert.AreEqual("1 Adult", HomePage.Data.Rooms[1].AdultsLabel);
            Assert.AreEqual(children2, HomePage.Data.Rooms[1].Children);
            Assert.AreEqual("4 Children", HomePage.Data.Rooms[1].ChildrenLabel);
            Assert.AreEqual(childrenAges2, HomePage.Data.Rooms[1].ChildrenAges);
            Assert.AreEqual(adults3, HomePage.Data.Rooms[2].Adults);
            Assert.AreEqual("3 Adults", HomePage.Data.Rooms[2].AdultsLabel);
            Assert.AreEqual(children3, HomePage.Data.Rooms[2].Children);
            Assert.AreEqual("1 Child", HomePage.Data.Rooms[2].ChildrenLabel);
            Assert.AreEqual(childrenAges3, HomePage.Data.Rooms[2].ChildrenAges);
        }
    }
}
