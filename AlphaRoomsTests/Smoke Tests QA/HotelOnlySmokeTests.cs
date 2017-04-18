using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaRooms.AutomationFramework;
using NUnit.Framework;

namespace AlphaRooms.AutomationFramework.Tests.Smoke_Test
{
    [TestFixture]
    public class SmokeTest : AlpharoomsTestBase
    {
        [Test]
        [Category("SmokeTest")]
        public void ShouldBookRandomSingleRoom_ForDestination_Benidorm()
        {
            //HomePage.ClickFlightOnly();
            //HomePage.TypeFlightDestination("Benidorm");
            //HomePage.SelectCheckIn("01/06/2014");
            //HomePage.SelectCheckOut("08/06/2014");
            //HomePage.SelectAirport("London (All Airports), London, United Kingdom (LON)");
            //HomePage.SelectAdults(2);
            //HomePage.SelectChildren(2, new int[] {2,3});
            //HomePage.ClickSearch();

            //HomePage.SearchFor().FlightOnly().ToDestination("Benidorm").FromCheckIn("01/09/2014")
            //    .ToCheckOut("08/09/2014").FromDepartureAirport("London (All Airports), London, United Kingdom (LON)")
            //    .ForAdults(2).WithChildren(2).OfAges(2, 3).Search();

            HomePage.SearchFor().HotelOnly().ToDestination("Benidorm").FromCheckIn("01/09/2014")
                .ToCheckOut("15/09/2014").ForAdults(2).Search();

            Assert.That(HotelResultsPage.IsDisplayed(), Is.True, "Hotel result page is not displayed");

            HotelResultsPage.ClickHotelNumber(1);

            HotelDetailPage.ClickAvaliableRoom(1);
            
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras Page not showing");

            //TODO
            ExtrasPage.Save(Information.Hotel);
            
            Assert.That(HotelResultsPage.Data.HotelName == ExtrasPage.Data.HotelName, Is.True, "Differing hotel names from SR to Extras");

            //Assert.That(HotelResultsPage.Data.Rooms[0].RoomPrice+==ExtrasPage.Data.

            ExtrasPage.BookHotel().Continue();

            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page not displaying");

            PaymentPage.Save(Information.Hotel);

            Assert.That(ExtrasPage.Data.HotelName == PaymentPage.Data.HotelName, Is.True, "Hotel name differs from Extras to Payment");

            PaymentPage.MakeAHotelBooking()
                 .ForGuestDetailsNumber(1).OfRoomNo(1).WithTitle(Title.Mr).WithFirstName("Test").WithLastName("Test")
                 .ForGuestDetailsNumber(2).OfRoomNo(1).WithTitle(Title.Mr).WithFirstName("Test").WithLastName("Test")
                 .ForContactDetails().TypeFirstName("Test").WithLastName("Test").WithEmail("simon.w@alpharooms.com").WithPhoneNumber("08719110030")
                 .ForPaymentDetails()
                 .WithPaymentType(Card.Mastercard)
                 .WithCardHolderName("Test Test")
                 .WithCardNo("5569510003306537")
                 .WithExpiryDate("03/15")
                 .WithSecurityCode("123")
                 .WithPostCode("S11 8NX")
                 .ForStaffDetails().WithReference("test").WithCustomerPhone("1")
                 .Confirm();

            //Assert.That(PaymentPage.Check3DSPage(), Is.True, "3DS page not displaying");
            Assert.IsTrue(ConfirmationPage.IsDisplayed(), "Confirmation page isn't displayed within 60 sec");

            Assert.IsTrue(ConfirmationPage.PNRNoIsDisplayed(), "PNR Number is empty");

            //Assert.That(PaymentPage.ValidateConfirmationMsg(), Is.True, "Confirmation page not showing");
        }

        [Test]
        [Category("SmokeTest")]
        public void ShouldBookRandomMultipleRoom_ForDestination_Mallorca()
        {
            //HomePage.TopPanel.ClickLocation(Location.UnitedKingdom);

            HomePage.SearchFor().HotelOnly().ToDestination("Mallorca").FromCheckIn("01/09/2014").ToCheckOut("08/09/2014")
                .ForAdults(2).AddAnotherRoom().ForAdults(1).WithChildren(2).OfAges(0,5).Search();

            Assert.That(HotelResultsPage.IsDisplayed(), Is.True, "Hotel result page is not displayed");

            HotelResultsPage.SelectRoom().ByHotelNumber(5).ForRoomNumber(1).WithAvailableRoom(1).ForRoomNumber(2).WithAvailableRoom(2).Continue();

            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras Page not showing");

            ExtrasPage.Save(Information.Hotel);

            Assert.That(HotelResultsPage.Data.HotelName == ExtrasPage.Data.HotelName, Is.True, "Differing hotel names from SR to Extras");

            ExtrasPage.BookHotel().Continue();

            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page not displaying");

            PaymentPage.Save(Information.Hotel);

            Assert.That(ExtrasPage.Data.HotelName == PaymentPage.Data.HotelName, Is.True, "Hotel name differs from Extras to Payment");

            PaymentPage.MakeAHotelBooking()
                .ForGuestDetailsNumber(1).OfRoomNo(1).WithTitle(Title.Mr).WithFirstName("Test").WithLastName("Test")
                .ForGuestDetailsNumber(2).OfRoomNo(1).WithTitle(Title.Mr).WithFirstName("Test").WithLastName("Test")
                .ForGuestDetailsNumber(1).OfRoomNo(2).WithTitle(Title.Mr).WithFirstName("Test").WithLastName("Test")
                .ForGuestDetailsNumber(2).OfRoomNo(2).WithTitle(Title.Mr).WithFirstName("Test").WithLastName("Test")
                .ForGuestDetailsNumber(3).OfRoomNo(2).WithTitle(Title.Mr).WithFirstName("Test").WithLastName("Test")
                .ForContactDetails().AutoFill()
                .ForPaymentDetails()
                .WithPaymentType(Card.Mastercard)
                .WithCardHolderName("Test Test")
                .WithCardNo("5569510003306537")
                .WithExpiryDate("03/15")
                .WithSecurityCode("123")
                .WithPostCode("S11 8NX")
                .ForStaffDetails().WithReference("test").WithCustomerPhone("1")
                .Confirm();        
        }
    }
}