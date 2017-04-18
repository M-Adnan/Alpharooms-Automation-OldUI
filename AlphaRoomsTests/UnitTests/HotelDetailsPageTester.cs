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
    public class HotelDetailsPageTester
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
        public void HotelDetailsPageOnlyOneRoomShouldReturnValidData()
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

            int hotelNumber = 1;
            HotelResultsPage.ClickHotelNumberAndCapture(hotelNumber);
            
            //check if result page is displayed
            Assert.AreEqual(true, HotelDetailPage.IsDisplayed(), "Hotel Details Page isn't displayed");

            int availableRoom = 2;
            HotelDetailPage.SelectRoom().OnlyOneRoomWithAvailableRoom(availableRoom).ContinueAndCapture();

            Assert.IsTrue(HotelDetailPage.Data.LoadingTime.TotalMilliseconds > 0);
            Assert.AreEqual(1, HotelDetailPage.Data.Rooms.Length);
            Assert.AreEqual(availableRoom, HotelDetailPage.Data.Rooms[0].AvailableRoom);
        }
    }
}
