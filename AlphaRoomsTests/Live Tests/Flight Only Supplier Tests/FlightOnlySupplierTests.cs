using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AlphaRooms.AutomationFramework;

namespace AlphaRooms.AutomationFramework.Tests.Live_Tests
{
    [TestFixture]
    public class FlightOnlySupplierTests : AlpharoomsTestBase
    {
        [Test]
        [Category("Live_Removed")]
        public void ShouldBookFromSupplier_TP_ForFlightTo_Porto()
        {
            //Select the uk location
            HomePage.TopPanel.ClickLocation(Location.UnitedKingdom);

            //Enter Hotel only search data
            HomePage.SearchFor().FlightOnly().ToDestination("Porto, Portugal").FromCheckIn(Calendar.PickRandomCheckInDate())
                .ToCheckOut(Calendar.PickRandomCheckOutDate()).FromDepartureAirport("London Heathrow, London, United Kingdom (LHR)")
                .ForAdults(2).WithChildren(2).OfAges(2, 4).Search();

            //Check if result page is displayed within 60 sec
            Assert.That(FlightResultsPage.IsDisplayed(), Is.True, "Flight Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(FlightResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the flight search.");

            //Search for supplier TP
            Assert.That(FlightResultsPage.ContainsFlightFromSupplier("TP"), Is.True, "TP supplier is not displayed on first result page");

            //Select first room option of the first hotel displayed on the very first result page
            FlightResultsPage.SelectFlight().ByFirstSupplier("TP").Continue();

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed within 60 sec");

            //Click Booknow button
            ExtrasPage.BookFlight().Continue();

            //Check Payment Page is displayed
            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");
        }

        [Test]
        [Category("Live_Removed")]
        public void ShouldBookFromSupplier_M_ForFlightTo_Athens()
        {
            //Select the uk location
            HomePage.TopPanel.ClickLocation(Location.UnitedKingdom);

            //Enter Hotel only search data
            HomePage.SearchFor().FlightOnly().ToDestination("Athens, Greece").FromCheckIn(Calendar.PickRandomCheckInDate())
                .ToCheckOut(Calendar.PickRandomCheckOutDate()).FromDepartureAirport("London Heathrow, London, United Kingdom (LHR)")
                .ForAdults(2).WithChildren(2).OfAges(2, 4).Search();

            //Check if result page is displayed within 60 sec
            Assert.That(FlightResultsPage.IsDisplayed(), Is.True, "Flight Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(FlightResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the flight search.");

            //Search for supplier M
            Assert.That(FlightResultsPage.ContainsFlightFromSupplier("M"), Is.True, "M supplier is not displayed on first result page");

            //Select first room option of the first hotel displayed on the very first result page
            FlightResultsPage.SelectFlight().ByFirstSupplier("M").Continue();

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed within 60 sec");

            //Click Booknow button
            ExtrasPage.BookFlight().Continue();

            //Check Payment Page is displayed
            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");
        }

        [Test]
        [Category("Live_Removed")]
        public void ShouldBookRandomFlightSupplier_ForDestination_Tenerife()
        {
            //Select the uk location
            HomePage.TopPanel.ClickLocation(Location.UnitedKingdom);

            //Enter Hotel only search data
            HomePage.SearchFor().FlightOnly().ToDestination("Tenerife").FromCheckIn(Calendar.PickRandomCheckInDate())
                .ToCheckOut(Calendar.PickRandomCheckOutDate()).FromDepartureAirport(HomePageRnd.PickRandomFlightDepartureAirport()).SearchAndCapture();

            //Check if result page is displayed within 60 sec
            Assert.That(FlightResultsPage.IsDisplayed(), Is.True, "Flight Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(FlightResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the flight search.");

            //Select Room 1 of a random hotel from the first result page
            FlightResultsPage.SelectFlight().ByFlightNumber(FlightResultsPageRnd.PickRandomFlight()).ContinueAndCapture();

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed");

            //Click Booknow button
            ExtrasPage.BookFlight().ContinueAndCapture();

            //Check Payment Page is displayed
            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");
        }

        [Test]
        [Category("Live_Removed")]
        public void ShouldBookRandomFlightSupplier_ForDestination_Alicante()
        {
            //Select the uk location
            HomePage.TopPanel.ClickLocation(Location.UnitedKingdom);

            //Enter Hotel only search data
            HomePage.SearchFor().FlightOnly().ToDestination("Alicante, Benidorm").FromCheckIn(Calendar.PickRandomCheckInDate())
                .ToCheckOut(Calendar.PickRandomCheckOutDate()).FromDepartureAirport(HomePageRnd.PickRandomFlightDepartureAirport()).SearchAndCapture();

            //Check if result page is displayed within 60 sec
            Assert.That(FlightResultsPage.IsDisplayed(), Is.True, "Flight Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(FlightResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the flight search.");

            //Select Room 1 of a random hotel from the first result page
            FlightResultsPage.SelectFlight().ByFlightNumber(FlightResultsPageRnd.PickRandomFlight()).ContinueAndCapture();

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed");

            //Click Booknow button
            ExtrasPage.BookFlight().ContinueAndCapture();

            //Check Payment Page is displayed
            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");
        }

        [Test]
        [Category("Live_Removed")]
        public void ShouldBookRandomFlightSupplier_ForDestination_Malaga()
        {
            //Select the uk location
            HomePage.TopPanel.ClickLocation(Location.UnitedKingdom);

            //Enter Hotel only search data
            HomePage.SearchFor().FlightOnly().ToDestination("Malaga, Costa Del Sol").FromCheckIn(Calendar.PickRandomCheckInDate())
                .ToCheckOut(Calendar.PickRandomCheckOutDate()).FromDepartureAirport(HomePageRnd.PickRandomFlightDepartureAirport()).SearchAndCapture();

            //Check if result page is displayed within 60 sec
            Assert.That(FlightResultsPage.IsDisplayed(), Is.True, "Flight Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(FlightResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the flight search.");

            //Select Room 1 of a random hotel from the first result page
            FlightResultsPage.SelectFlight().ByFlightNumber(FlightResultsPageRnd.PickRandomFlight()).ContinueAndCapture();

            //FlightResultsPage.SelectFlight().ByFirstNonRyanSupplier()

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed");

            //Click Booknow button
            ExtrasPage.BookFlight().ContinueAndCapture();

            //Check Payment Page is displayed
            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");
        }

        [Test]
        [Category("Live_Removed")]
        public void ShouldBookRandomFlightSupplier_ForDestination_Barcelona()
        {
            //Select the uk location
            HomePage.TopPanel.ClickLocation(Location.UnitedKingdom);

            //Enter Hotel only search data
            HomePage.SearchFor().FlightOnly().ToDestination("Barcelona").FromCheckIn(Calendar.PickRandomCheckInDate())
                .ToCheckOut(Calendar.PickRandomCheckOutDate()).FromDepartureAirport(HomePageRnd.PickRandomFlightDepartureAirport()).SearchAndCapture();

            //Check if result page is displayed within 60 sec
            Assert.That(FlightResultsPage.IsDisplayed(), Is.True, "Flight Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(FlightResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the flight search.");

            //Select Room 1 of a random hotel from the first result page
            FlightResultsPage.SelectFlight().ByFlightNumber(FlightResultsPageRnd.PickRandomFlight()).ContinueAndCapture();

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed");

            //Click Booknow button
            ExtrasPage.BookFlight().ContinueAndCapture();

            //Check Payment Page is displayed
            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");
        }

        [Test]
        [Category("Live_Removed")]
        public void ShouldBookRandomFlightSupplier_ForDestination_Palma_De_Mallorca()
        {
            //Select the uk location
            HomePage.TopPanel.ClickLocation(Location.UnitedKingdom);

            //Enter Hotel only search data
            HomePage.SearchFor().FlightOnly().ToDestination("Palma De Mallorca, Mallorca").FromCheckIn(Calendar.PickRandomCheckInDate())
                .ToCheckOut(Calendar.PickRandomCheckOutDate()).FromDepartureAirport(HomePageRnd.PickRandomFlightDepartureAirport()).SearchAndCapture();

            //Check if result page is displayed within 60 sec
            Assert.That(FlightResultsPage.IsDisplayed(), Is.True, "Flight Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(FlightResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the flight search.");
            //Select Room 1 of a random hotel from the first result page
            FlightResultsPage.SelectFlight().ByFlightNumber(FlightResultsPageRnd.PickRandomFlight()).ContinueAndCapture();

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed");

            //Click Booknow button
            ExtrasPage.BookFlight().ContinueAndCapture();

            //Check Payment Page is displayed
            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");
        }
    }
}