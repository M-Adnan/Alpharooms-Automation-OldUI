using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AlphaRooms.AutomationFramework;

namespace AlphaRooms.AutomationFramework.Tests.Live_Tests
{
    public class FlightAndHotelTests : AlpharoomsTestBase
    {
        [Test]
        [Category("Live")]
        public void ShouldBook_1st_MostPopularFlightAndHotel_Benidorm()
        {
            //Select the uk location
            HomePage.TopPanel.ClickLocation(Location.UnitedKingdom);

            //Enter Hotel only search data
            HomePage.SearchFor().FlightAndHotel().ToDestination("Benidorm, Spain").FromCheckIn(Calendar.PickRandomCheckInDate())
                .ToCheckOut(Calendar.PickRandomCheckOutDate()).FromDepartureAirport(HomePageRnd.PickRandomFlightDepartureAirport()).SearchAndCapture();

            //Check if result page is displayed within 60 sec
            Assert.That(FlightResultsPage.IsDisplayed(), Is.True, "Flight Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(FlightResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the flight search.");

            //Select Room 1 of a random hotel from the first result page
            FlightResultsPage.SelectFlight().ByFlightNumber(FlightResultsPageRnd.PickRandomFlight()).ContinueAndCapture();

            //Check if result page is displayed within 60 sec
            Assert.That(HotelResultsPage.IsDisplayed(), Is.True, "Hotel Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(HotelResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the hotel search");

            //Select random hotel from the first result page
            HotelResultsPage.ClickHotelNumber(HotelResultsPageRnd.PickRandomHotel());

            //Check HotelDetailPage is displayed
            Assert.That(HotelDetailPage.IsDisplayed(), Is.True, "Hotel Detail page is not displayed");

            //Select the first available room from the list
            HotelDetailPage.SelectRoom().OnlyOneRoomWithAvailableRoom(1).ContinueAndCapture();

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed");

            //Click Booknow button
            ExtrasPage.BookFlightAndHotel().ContinueAndCapture();

            //Check Payment Page is displayed
            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");
        }

        [Test]
        [Category("Live")]
        public void ShouldBook_2nd_MostPopularFlightAndHotel_Tenerife()
        {
            //Select the uk location
            HomePage.TopPanel.ClickLocation(Location.UnitedKingdom);

            //Enter Hotel only search data
            HomePage.SearchFor().FlightAndHotel().ToDestination("Tenerife, Canaries").FromCheckIn(Calendar.PickRandomCheckInDate())
                .ToCheckOut(Calendar.PickRandomCheckOutDate()).FromDepartureAirport(HomePageRnd.PickRandomFlightDepartureAirport()).SearchAndCapture();

            //Check if result page is displayed within 60 sec
            Assert.That(FlightResultsPage.IsDisplayed(), Is.True, "Flight Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(FlightResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the flight search.");

            //Select Room 1 of a random hotel from the first result page
            FlightResultsPage.SelectFlight().ByFlightNumber(FlightResultsPageRnd.PickRandomFlight()).ContinueAndCapture();

            //Check if result page is displayed within 60 sec
            Assert.That(HotelResultsPage.IsDisplayed(), Is.True, "Hotel Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(HotelResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the hotel search");

            //Select random hotel from the first result page
            HotelResultsPage.ClickHotelNumber(HotelResultsPageRnd.PickRandomHotel());

            //Check HotelDetailPage is displayed
            Assert.That(HotelDetailPage.IsDisplayed(), Is.True, "Hotel Detail page is not displayed");

            //Select the first available room from the list
            HotelDetailPage.SelectRoom().OnlyOneRoomWithAvailableRoom(1).ContinueAndCapture();

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed");

            //Click Booknow button
            ExtrasPage.BookFlightAndHotel().ContinueAndCapture();

            //Check Payment Page is displayed
            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");

        }

        [Test]
        [Category("Live")]
        public void ShouldBook_3rd_MostPopularFlightAndHotel_Mallorca()
        {
            //Select the uk location
            HomePage.TopPanel.ClickLocation(Location.UnitedKingdom);

            //Enter Hotel only search data
            HomePage.SearchFor().FlightAndHotel().ToDestination("Mallorca (Majorca), Spain").FromCheckIn(Calendar.PickRandomCheckInDate())
                .ToCheckOut(Calendar.PickRandomCheckOutDate()).FromDepartureAirport(HomePageRnd.PickRandomFlightDepartureAirport()).SearchAndCapture();

            //Check if result page is displayed within 60 sec
            Assert.That(FlightResultsPage.IsDisplayed(), Is.True, "Flight Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(FlightResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the flight search.");

            //Select Room 1 of a random hotel from the first result page
            FlightResultsPage.SelectFlight().ByFlightNumber(FlightResultsPageRnd.PickRandomFlight()).ContinueAndCapture();

            //Check if result page is displayed within 60 sec
            Assert.That(HotelResultsPage.IsDisplayed(), Is.True, "Hotel Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(HotelResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the hotel search");

            //Select random hotel from the first result page
            HotelResultsPage.ClickHotelNumber(HotelResultsPageRnd.PickRandomHotel());

            //Check HotelDetailPage is displayed
            Assert.That(HotelDetailPage.IsDisplayed(), Is.True, "Hotel Detail page is not displayed");

            //Select the first available room from the list
            HotelDetailPage.SelectRoom().OnlyOneRoomWithAvailableRoom(1).ContinueAndCapture();

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed");

            //Click Booknow button
            ExtrasPage.BookFlightAndHotel().ContinueAndCapture();

            //Check Payment Page is displayed
            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");

        }

        [Test]
        [Category("Live")]
        public void ShouldBook_4th_MostPopularFlightAndHotel_Algarve()
        {
            //Select the uk location
            HomePage.TopPanel.ClickLocation(Location.UnitedKingdom);

            //Enter Hotel only search data
            HomePage.SearchFor().FlightAndHotel().ToDestination("Algarve, Portugal").FromCheckIn(Calendar.PickRandomCheckInDate())
                .ToCheckOut(Calendar.PickRandomCheckOutDate()).FromDepartureAirport(HomePageRnd.PickRandomFlightDepartureAirport()).SearchAndCapture();

            //Check if result page is displayed within 60 sec
            Assert.That(FlightResultsPage.IsDisplayed(), Is.True, "Flight Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(FlightResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the flight search.");

            //Select Room 1 of a random hotel from the first result page
            FlightResultsPage.SelectFlight().ByFlightNumber(FlightResultsPageRnd.PickRandomFlight()).ContinueAndCapture();

            //Check if result page is displayed within 60 sec
            Assert.That(HotelResultsPage.IsDisplayed(), Is.True, "Hotel Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(HotelResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the hotel search");

            //Select random hotel from the first result page
            HotelResultsPage.ClickHotelNumber(HotelResultsPageRnd.PickRandomHotel());

            //Check HotelDetailPage is displayed
            Assert.That(HotelDetailPage.IsDisplayed(), Is.True, "Hotel Detail page is not displayed");

            //Select the first available room from the list
            HotelDetailPage.SelectRoom().OnlyOneRoomWithAvailableRoom(1).ContinueAndCapture();

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed");

            //Click Booknow button
            ExtrasPage.BookFlightAndHotel().ContinueAndCapture();

            //Check Payment Page is displayed
            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");
        }

        [Test]
        [Category("Live")]
        public void ShouldBook_5th_MostPopularFlightAndHotel_Lanzarote()
        {
            //Select the uk location
            HomePage.TopPanel.ClickLocation(Location.UnitedKingdom);

            //Enter Hotel only search data
            HomePage.SearchFor().FlightAndHotel().ToDestination("Lanzarote, Canaries").FromCheckIn(Calendar.PickRandomCheckInDate())
                .ToCheckOut(Calendar.PickRandomCheckOutDate()).FromDepartureAirport(HomePageRnd.PickRandomFlightDepartureAirport()).SearchAndCapture();

            //Check if result page is displayed within 60 sec
            Assert.That(FlightResultsPage.IsDisplayed(), Is.True, "Flight Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(FlightResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the flight search.");

            //Select Room 1 of a random hotel from the first result page
            FlightResultsPage.SelectFlight().ByFlightNumber(FlightResultsPageRnd.PickRandomFlight()).ContinueAndCapture();

            //Check if result page is displayed within 60 sec
            Assert.That(HotelResultsPage.IsDisplayed(), Is.True, "Hotel Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(HotelResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the hotel search");

            //Select random hotel from the first result page
            HotelResultsPage.ClickHotelNumber(HotelResultsPageRnd.PickRandomHotel());

            //Check HotelDetailPage is displayed
            Assert.That(HotelDetailPage.IsDisplayed(), Is.True, "Hotel Detail page is not displayed");

            //Select the first available room from the list
            HotelDetailPage.SelectRoom().OnlyOneRoomWithAvailableRoom(1).ContinueAndCapture();

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed");

            //Click Booknow button
            ExtrasPage.BookFlightAndHotel().ContinueAndCapture();

            //Check Payment Page is displayed
            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");
        }
    }
}
