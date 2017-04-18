using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AlphaRooms.AutomationFramework;

namespace AlphaRooms.AutomationFramework.Tests.Functional.Tests.Flight_Only_Tests.Destination_Tests
{
    [TestFixture]
    public class TopFiveFlightOnlyDestinations : AlpharoomsTestBase
    {
        [Test]
        [Category("Functional")]
        public void ShouldBook_1st_MostPopularFlight_Tenerife()
        {
            //Enter Hotel only search data
            HomePage.SearchFor().FlightOnly().ToDestination("Tenerife").FromDepartureAirport("All London (STN,SEN,LTN,LHR,LGW,LCY), London, United Kingdom (LON)")
                .FromCheckIn(Calendar.PickRandomCheckInDate()).ToCheckOut(Calendar.PickRandomCheckOutDate())
                .ForAdults(2).WithChildren(2).OfAges(2, 4).Search();

            //Check if result page is displayed within 60 sec
            Assert.That(FlightResultsPage.IsDisplayed(), Is.True, "Flight Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(FlightResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the flight search");

            //Select first room option of the first hotel displayed on the very first result page
            FlightResultsPage.SelectFlight().ByFlightNumber(FlightResultsPageRnd.PickRandomFlight()).Continue();

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed within 60 sec");

            //Click Booknow button
            ExtrasPage.BookFlight().Continue();

            //Check Payment Page is displayed
            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");

            //Fill in Payment Page Details

        }

        [Test]
        [Category("Functional")]
        public void ShouldBook_2nd_MostPopularFlight_Alicante()
        {
            //Enter Hotel only search data
            HomePage.SearchFor().FlightOnly().ToDestination("Alicante, Benidorm, Spain").FromCheckIn(Calendar.PickRandomCheckInDate())
            .ToCheckOut(Calendar.PickRandomCheckOutDate()).FromDepartureAirport("London Heathrow, London, United Kingdom (LHR)")
            .ForAdults(2).WithChildren(2).OfAges(2, 4).Search();

            //Check if result page is displayed within 60 sec
            Assert.That(FlightResultsPage.IsDisplayed(), Is.True, "Flight Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(FlightResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the flight search");

            //Select first room option of the first hotel displayed on the very first result page
            FlightResultsPage.SelectFlight().ByFlightNumber(FlightResultsPageRnd.PickRandomFlight()).Continue();

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed within 60 sec");

            //Click Booknow button
            ExtrasPage.BookFlight().Continue();

            //Check Payment Page is displayed
            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");

        }

        [Test]
        [Category("Functional")]
        public void ShouldBook_3rd_MostPopularFlight_Malaga()
        {
            //Enter Hotel only search data
            HomePage.SearchFor().FlightOnly().ToDestination("Malaga, Costa Del Sol, Spain").FromCheckIn(Calendar.PickRandomCheckInDate())
            .ToCheckOut(Calendar.PickRandomCheckOutDate()).FromDepartureAirport("London Heathrow, London, United Kingdom (LHR)")
            .ForAdults(2).WithChildren(2).OfAges(2, 4).Search();

            //Check if result page is displayed within 60 sec
            Assert.That(FlightResultsPage.IsDisplayed(), Is.True, "Flight Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(FlightResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the flight search");

            //Select first room option of the first hotel displayed on the very first result page
            FlightResultsPage.SelectFlight().ByFlightNumber(FlightResultsPageRnd.PickRandomFlight()).Continue();

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed within 60 sec");

            //Click Booknow button
            ExtrasPage.BookFlight().Continue();

            //Check Payment Page is displayed
            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");

        }

        [Test]
        [Category("Functional")]
        public void ShouldBook_4th_MostPopularFlight_Barcelona()
        {
            //Enter Hotel only search data
            HomePage.SearchFor().FlightOnly().ToDestination("Barcelona, Spain").FromCheckIn(Calendar.PickRandomCheckInDate())
            .ToCheckOut(Calendar.PickRandomCheckOutDate()).FromDepartureAirport("London Heathrow, London, United Kingdom (LHR)")
            .ForAdults(2).WithChildren(2).OfAges(2, 4).Search();

            //Check if result page is displayed within 60 sec
            Assert.That(FlightResultsPage.IsDisplayed(), Is.True, "Flight Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(FlightResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the flight search");

            //Select first room option of the first hotel displayed on the very first result page
            FlightResultsPage.SelectFlight().ByFlightNumber(FlightResultsPageRnd.PickRandomFlight()).Continue();

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed within 60 sec");

            //Click Booknow button
            ExtrasPage.BookFlight().Continue();

            //Check Payment Page is displayed
            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");

        }

        [Test]
        [Category("Functional")]
        public void ShouldBook_5th_MostPopularFlight_Palma_De_Mallorca()
        {
            //Enter Hotel only search data
            HomePage.SearchFor().FlightOnly().ToDestination("Palma De Mallorca, Mallorca").FromCheckIn(Calendar.PickRandomCheckInDate())
            .ToCheckOut(Calendar.PickRandomCheckOutDate()).FromDepartureAirport("London Heathrow, London, United Kingdom (LHR)")
            .ForAdults(2).WithChildren(2).OfAges(2, 4).Search();

            //Check if result page is displayed within 60 sec
            Assert.That(FlightResultsPage.IsDisplayed(), Is.True, "Flight Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(FlightResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the flight search");

            //Select first room option of the first hotel displayed on the very first result page
            FlightResultsPage.SelectFlight().ByFlightNumber(FlightResultsPageRnd.PickRandomFlight()).Continue();

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed within 60 sec");

            //Click Booknow button
            ExtrasPage.BookFlight().Continue();

            //Check Payment Page is displayed
            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");
        }
    }
}
