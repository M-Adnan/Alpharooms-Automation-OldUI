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
    public class FlightResultsPageTester
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
        public void FlightResultsPageShouldReturnValidData()
        {
            string destination = "Alicante, Benidorm, Spain (ALC)";
            string departure = "London Gatwick, London, United Kingdom (LGW)";
            string checkIn = DateTime.Today.AddMonths(5).ToShortDateString();
            string checkOut = DateTime.Today.AddMonths(5).AddDays(7).ToShortDateString();
            int adults = 4;
            int children = 2;
            int[] childrenAges = new int[] { 5, 7 };

            HomePage.SearchFor().FlightOnly().ToDestination(destination).FromCheckIn(checkIn).ToCheckOut(checkOut).FromDepartureAirport(departure)
                .ForAdults(adults).WithChildren(children).OfAges(childrenAges).SearchAndCapture();

            //check if result page is displayed
            Assert.AreEqual(true, FlightResultsPage.IsDisplayed(), "Flight Search Result Page isn't displayed");

            FlightResultsPage.SelectFlight().ByFlightNumber(1).ContinueAndCapture();

            //check if result page is displayed
            Assert.AreEqual(true, ExtrasPage.IsDisplayed(), "Flight Extras Page isn't displayed");

            Assert.IsTrue(!String.IsNullOrEmpty(FlightResultsPage.Data.FlightSearchGuid));
            Assert.IsTrue(FlightResultsPage.Data.LoadingTime.TotalMilliseconds > 0);
            Assert.AreEqual("(ALC) Alicante", FlightResultsPage.Data.OutboundArrivalAirport);
            Assert.AreEqual("(LGW) London Gatwick", FlightResultsPage.Data.OutboundDepartureAirport);
            Assert.AreEqual("(LGW) London Gatwick", FlightResultsPage.Data.InboundArrivalAirport);
            Assert.AreEqual("(ALC) Alicante", FlightResultsPage.Data.InboundDepartureAirport);
            Assert.AreEqual(DateTime.Parse(checkIn).ToString("dd MMM yyyy"), FlightResultsPage.Data.OutboundArrivalTime.Substring(0, 11));
            Assert.AreEqual(DateTime.Parse(checkIn).ToString("dd MMM yyyy"), FlightResultsPage.Data.OutboundDepartureTime.Substring(0, 11));
            Assert.AreEqual(DateTime.Parse(checkOut).ToString("dd MMM yyyy"), FlightResultsPage.Data.InboundArrivalTime.Substring(0, 11));
            Assert.AreEqual(DateTime.Parse(checkOut).ToString("dd MMM yyyy"), FlightResultsPage.Data.InboundDepartureTime.Substring(0, 11));
        }

        [Test]
        [Category("FrameworkTests")]
        public void FlightResultsPageFirstSupplierShouldReturnValidData()
        {
            string destination = "Alicante, Benidorm, Spain (ALC)";
            string departure = "London Gatwick, London, United Kingdom (LGW)";
            string checkIn = DateTime.Today.AddMonths(5).ToShortDateString();
            string checkOut = DateTime.Today.AddMonths(5).AddDays(7).ToShortDateString();
            int adults = 4;
            int children = 2;
            int[] childrenAges = new int[] { 5, 7 };

            HomePage.SearchFor().FlightOnly().ToDestination(destination).FromCheckIn(checkIn).ToCheckOut(checkOut).FromDepartureAirport(departure)
                .ForAdults(adults).WithChildren(children).OfAges(childrenAges).SearchAndCapture();

            //check if result page is displayed
            Assert.AreEqual(true, FlightResultsPage.IsDisplayed(), "Flight Search Result Page isn't displayed");

            FlightResultsPage.SelectFlight().ByFirstSupplier("TP").ContinueAndCapture();

            //check if result page is displayed
            Assert.AreEqual(true, ExtrasPage.IsDisplayed(), "Flight Extras Page isn't displayed");

            Assert.AreEqual("Travel Port", FlightResultsPage.Data.Supplier);
        }
    }
}
