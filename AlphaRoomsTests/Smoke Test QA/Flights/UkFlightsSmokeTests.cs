using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AlphaRooms.AutomationFramework;


namespace AlphaRooms.AutomationFramework.Tests.Smoke_Tests_QA.Flights
{
    [TestFixture]
    public class UK_FlightsOnlySmokeTests : AlpharoomsTestBase
    {
        [Test]
        [Category("UKQA_Flights_SmokeTest")]
        public void ShouldBookFlight_For2Adults_ForDestination_Benidorm()
        {
            //Enter search criteria on home page and click search
            HomePage.SearchFor().FlightOnly().ToDestination("ALC").FromCheckIn(Calendar.PickRandomCheckInDate()).ToCheckOut(Calendar.PickRandomCheckOutDate())
                .FromDepartureAirport("Manchester Airport, Manchester, United Kingdom (MAN)").ForAdults(2).Search();

            Assert.That(FlightResultsPage.IsDisplayed(), Is.True, "Flights results isn't displayed");

            FlightResultsPage.SelectFlight().ByFlightNumber(FlightResultsPageRnd.PickRandomFlight()).Continue();

            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page isn't displayed");

            ExtrasPage.ClickBookNowAndCapture();

            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page isn't displayed");

            PaymentPage.MakeAFlightBooking().ForAllGuestDetails().AutoFill().ForContactDetails().WithEmail("automationtest@test.com").WithPhoneNumber("07747680480")
                .ForPaymentDetails().AutoFill().ForStaffDetails().AutoFill().Confirm();

            Assert.That(ConfirmationPage.IsDisplayed(), Is.True, "Booking Confirmation page isn't displayed");
        }
    }
}
