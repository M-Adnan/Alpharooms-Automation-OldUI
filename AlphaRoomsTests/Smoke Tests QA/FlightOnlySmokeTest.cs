using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaRooms.AutomationFramework;
using Nunit

namespace AlphaRooms.AutomationFramework.Tests.Smoke_Test.FlightOnly
{
    [TestFixture]
    public class SmokeTest : AlpharoomsTestBase
    {
        [Test]
        [Category("SmokeTest")]
        public void ShouldBookFlight_ForDestination_Benidorm()
        {
            HomePage.SearchFor().FlightOnly().ToDestination("Benidorm").FromCheckIn("01/09/2014").ToCheckOut("08/09/2014")
                .FromDepartureAirport("Manchester Airport, Manchester, United Kingdom (MAN)").ForAdults(2).Search();

            Assert.That(FlightResultsPage.IsDisplayed(), Is.True, "Flights results not displaying");

            FlightResultsPage.SelectFlight().ByFlightNumber(FlightResultsPageRnd.PickRandomFlight()).Continue();
            
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not showing");

            ExtrasPage.Save(Information.Flight);

            Assert.That(ExtrasPage.Data.OutboundAirline == FlightResultsPage.Data.OutboundAirline, Is.True, "OutboundAirline differ from flight search results to extras");
            Assert.That(ExtrasPage.Data.OutboundArrivalAirport == FlightResultsPage.Data.OutboundArrivalAirport, Is.True, "OutboundArrivalAirport differ from flight search results to extras");
            Assert.That(ExtrasPage.Data.OutboundArrivalTime == FlightResultsPage.Data.OutboundArrivalTime, Is.True, "OutboundArrivalTime differ from flight search results to extras");
            Assert.That(ExtrasPage.Data.OutboundDepartureAiport == FlightResultsPage.Data.OutboundDepartureAirport, Is.True, "OutboundDepartureAiport differ from flight search results to extras");
            Assert.That(ExtrasPage.Data.OutboundDepartureTime == FlightResultsPage.Data.OutboundDepartureTime, Is.True, "OutboundDepartureTime differ from flight search results to extras");
            Assert.That(ExtrasPage.Data.OutboundFlightNo == FlightResultsPage.Data.OutboundFlightNo, Is.True, "OutboundFlightNo differ from flight search results to extras");
            Assert.That(ExtrasPage.Data.InboundAirline == FlightResultsPage.Data.InboundAirline, Is.True, "InboundAirline differ from flight search results to extras");
            Assert.That(ExtrasPage.Data.InboundArrivalAirport == FlightResultsPage.Data.InboundArrivalAirport, Is.True, "InboundArrivalAirport differ from flight search results to extras");
            Assert.That(ExtrasPage.Data.InboundArrivalTime == FlightResultsPage.Data.InboundArrivalTime, Is.True, "InboundArrivalTime differ from flight search results to extras");
            Assert.That(ExtrasPage.Data.InbounddDepartureAiport == FlightResultsPage.Data.InboundDepartureAirport, Is.True, "InbounddDepartureAiport differ from flight search results to extras");
            Assert.That(ExtrasPage.Data.InboundDepartureTime == FlightResultsPage.Data.InboundDepartureTime, Is.True, "InboundDepartureTime differ from flight search results to extras");
            Assert.That(ExtrasPage.Data.InboundFlightNo == FlightResultsPage.Data.InboundFlightNo, Is.True, "InboundFlightNo differ from flight search results to extras");

            ExtrasPage.ClickBookNowAndCapture();

            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not showing");

            PaymentPage.Save(Information.Flight);

            Assert.That(PaymentPage.Data.TotalPrice == ExtrasPage.Data.TotalPrice, Is.True, "Total price differs between extras and payment");
            Assert.That(PaymentPage.Data.OutboundJourney == ExtrasPage.Data.OutboundJourney, Is.True, "Outbound Journey differs from extras to payment");
            Assert.That(PaymentPage.Data.OutboundFlightNo == ExtrasPage.Data.OutboundFlightNo, Is.True, "Outbound flight number differs from extras to payment");
            Assert.That(PaymentPage.Data.OutboundDepartureTime == ExtrasPage.Data.OutboundDepartureTime, Is.True, "Outbound departure time differs from extras to payment");
            Assert.That(PaymentPage.Data.InboundJourney == ExtrasPage.Data.InboundJourney, Is.True, "Inbound Journey differs from extras to payment");
            Assert.That(PaymentPage.Data.OutboundFlightNo == ExtrasPage.Data.InboundFlightNo, Is.True, "Inbound flight number differs from extras to payment");
            Assert.That(PaymentPage.Data.OutboundDepartureTime == ExtrasPage.Data.InboundDepartureTime, Is.True, "Inbound departure time differs from extras to payment");

            PaymentPage.SelectGuestTitle(1, Title.Mr);
            PaymentPage.TypeGuestFirstName(1, "Test");
            PaymentPage.TypeGuestLastName(1, "Test");
            PaymentPage.SelectGuestDoB(1, "01/01/1985");
            PaymentPage.SelectGuestTitle(1, Title.Mr);
            PaymentPage.TypeGuestFirstName(1, "Test");
            PaymentPage.TypeGuestLastName(1, "Test");
            PaymentPage.SelectGuestDoB(1, "01/02/1985");

            PaymentPage.TypeContactFirstName("Test");
            PaymentPage.TypeContactLastName("Test");
            PaymentPage.TypeContactEmail("simon.w@alpharooms.com");
            PaymentPage.TypeContactNumber("08719110030");

            PaymentPage.SelectPaymentType(Card.Mastercard);
            PaymentPage.TypeCardHolderName("Test Test");
            PaymentPage.TypeCardNumber("5569510003306537");
            PaymentPage.TypeExpiryDate("03/15");
            PaymentPage.TypeSecurityCode("123");
            PaymentPage.TypePostCode("S11 8NX");

            PaymentPage.TypeStaffReference("smw test");
            PaymentPage.TypeStaffCustomerPhone("1");

            PaymentPage.ClickConfirmAndCapture();


        }
    }
}
