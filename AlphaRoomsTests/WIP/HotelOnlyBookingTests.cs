using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AlphaRooms.AutomationFramework;

namespace AlphaRooms.AutomationFramework.Tests.Smoke_Tests
{
    [TestFixture]
    public class HotelOnlyBookingTests : AlpharoomsTestBase
    {
        [Test]
        public void ShouldMakeHotelOnlyBooking()
        {
            //check if homepage is displayed
            Assert.That(HomePage.IsDisplayed(), Is.True, "Homepage isn't displayed");

            //Select united kingdom as the user location
            HomePage.TopPanel.ClickLocation(Location.UnitedKingdom);

            //Enter Hotel only search data
            HomePage.SearchFor().HotelOnly().ToHotelName("Playa Real Resort").FromCheckIn("24/09/2014")
                .ToCheckOut("30/09/2014").Search();

            //Check HotelDetailPage is displayed
            Assert.That(HotelDetailPage.IsDisplayed(), Is.True, "Hotel Detail page is not displayed");

            //Select the first available room from the list
            HotelDetailPage.SelectRoom().OnlyOneRoomWithAvailableRoom(1).ContinueAndCapture();

            //check extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed, Is.True, "Extra page is not been displayed");

            //check if carhire is displayed
            Assert.That(ExtrasPage.IsExtraDisplayed(Extras.CarHire), Is.True, "Car Hire is not Visible on extra page");

            //Expand Car Hire
            ExtrasPage.ExpandExtraLink(Extras.CarHire);

            //Confirm if any results are available
            Assert.That(ExtrasPage.AreResultsDisplayed(Extras.CarHire), Is.True, "Car Hire results not available by default");

            //check if homepage is displayed
            Assert.That(HomePage.IsDisplayed(), Is.True, "Homepage isn't displayed");

            //Enter Hotel only search data
            HomePage.SearchFor().HotelOnly().ToHotelName("Africa El Mouradi Hotel").FromCheckIn(Calendar.PickRandomCheckInDate())
                .ToCheckOut(Calendar.PickRandomCheckOutDate())
                /*.AddAnotherRoom().ForAdults(2).WithChildren(1).OfAges(2)*/.Search();

            //HotelResultsPage.ClickHotelNumber(1);
            Assert.That(HotelDetailPage.IsDisplayed(), Is.True, "Hotel Result page is not displayed");

            //
            HotelDetailPage.SelectRoom().OnlyOneRoomWithAvailableRoom(1).ContinueAndCapture();

            //extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed, Is.True, "Hotel Summary page is not been displayed");


            //
            Assert.That(ExtrasPage.IsExtraDisplayed(Extras.CarHire),Is.True,"Car Hire is not Visible on extra page");

            Assert.That(ExtrasPage.IsExtraDisplayed(Extras.AirportTransfer), Is.True, "Airport Transfer is not Visible on extra page");

            Assert.That(ExtrasPage.IsExtraDisplayed(Extras.HoldLuggage), Is.True, "Airport Transfer is not Visible on extra page");

            //FlightResultsPage.FindFlightForFirstSupplier()
            //HotelResultsPage.ClickHotelNumber(HotelResultsPage.FindHotel("TRH Jardin Del Mar, Santa Ponsa"));

            //HotelResultsPage.SelectRoom().ByHotelNumber(1).ForRoomNumber(1).WithAvailableRoom(1).Continue();


            //HotelDetailPage.SelectRoom().WithAvailableRoom(2).ContinueAndCapture();


            //HotelDetailPage.SelectRoom().ByFirstSupplier("JT (Y)").ContinueAndCapture();

            //HotelDetailPage.SelectRoom().WithAvailableRoom(4).Continue();


            //HotelDetailPage.SelectRoom().WithAvailableRoom(1).Continue();

            //HotelDetailPage.SelectRoom().ForRoomNumber(1).WithAvailableRoom(1).ForRoomNumber(2).WithAvailableRoom(2).Continue();

            //HotelDetailPage.SelectRoom().ByFirstSupplier("JT (Y)").Continue();
        }
    }


    //        //check if homepage is displayed
    //        Assert.That(HomePage.IsDisplayed(), Is.True, "Homepage isn't displayed");

    //        //Enter Hotel only search data
    //        HomePage.SearchFor().HotelOnly().ToDestination("Mallorca (Majorca), Spain").FromCheckIn(HomePageRnd.CheckInDatePickRandom())
    //            .ToCheckOut(HomePageRnd.CheckOutDatePickRandom()).Search();
    //            //.ForAdults(1).WithChildren(2).OfAges(2,3)
    //            //.AddAnotherRoom().ForAdults(3)
    //            //.AddAnotherRoom().ForAdults(1).WithChildren(3).OfAges(3, 5, 2).Search();

    //        Assert.That(HotelResultsPage.IsDisplayed(), Is.True, "Hotel Result page is not displayed");

    //        Assert.That(HotelResultsPage.ContainsHotelByName("Viviendas Margarita II"), Is.True, "No hotel available with this name");

    //        //HotelResultsPage.ClickHotel(HotelResultsPage.FindHotel("Viviendas Margarita II"));
    //        HotelResultsPage.SelectRoom().ByHotel("Viviendas Margarita II").WithAvailableRoom(2).Continue();

    //        //HotelResultsPage.ClickHotel(1);
    //        //HotelResultsPage.SelectRoomByFirstSupplier("").

    //        //HotelResultsPage.SelectRoomByHotel("Vista Sol Aparthotel").ForRoomNumber(1).SetAvailableRoom(2)
    //        //    .ForRoomNumber(2).SetAvailableRoom(1)
    //        //    .ForRoomNumber(3).SetAvailableRoom(1).Continue();

    //        Assert.That(HotelDetailPage.IsDisplayed, Is.True, "Hotel Summary page is not been displayed");

    //    }

    //    [Test]
    //    public void ShouldMakeHotelOnlyBookingBySupplier()
    //    {
    //        //check if homepage is displayed
    //        Assert.That(HomePage.IsDisplayed(), Is.True, "Homepage isn't displayed");

    //        //Enter Hotel only search data
    //        HomePage.SearchHotelOnly().Destination("Mallorca (Majorca), Spain").FromCheckIn("28/03/2014")
    //            .ToCheckOut("05/04/2014")
    //            .ForAdults(1).WithChildren(2).OfAges(2,3).Search();

    //        Assert.That(HotelResultsPage.IsDisplayed(), Is.True, "Hotel Result page is not displayed");

    //        Assert.That(HotelResultsPage.ContainsRoomFromSupplier("Hotels4U (19)"), Is.True, "Supplier not found on current select page");

    //        HotelResultsPage.SelectRoomByFirstSupplier("Hotels4U (19)").Continue();

    //        Assert.That(ExtrasPage.IsDisplayed, Is.True, "Hotel Summary page is not been displayed");
    //    }


    //    //    //HotelResultsPage.SelectHotel(HotelResultsPage.Data.SupplierHotelIndex).RoomNumber(HotelResultsPage.Data.SupplierRoomIndex);


    //    //    //HotelResultsPage.SearchforSupplier("AQUASOL (A)");


    //    //    ExtrasPage.Save(Information.Hotel);

    //    //    Assert.That(ExtrasPage.Confirm(Information.Hotel), Is.True, "Selected flight information doesn't match with flight information on Extras page");

    //    //    ExtrasPage.BookNowAndCapture();


    //    //    //check if payment page is displayed
    //    //    Assert.That(PaymentPage.IsDisplayed, Is.True, "Payment page isn't displayed");

    //    //    //save hotel detailsma
    //    //    PaymentPage.Save(Information.Hotel);

    //    //    //confirm flight details
    //    //    Assert.That(PaymentPage.Confirm(Information.Hotel), Is.True, "Selected flight information doesn't match with flight information on Payment page");


    //    //    //fill in guest details
    //    //    //HotelPaymentPage.FillGuestDetails().ForGuestNo(1).SelectGuestTitle(Title.Mr).TypeGuestFirstName("John").TypeGuestLastName("Test");
    //    //    //HotelPaymentPage.FillGuestDetails().ForGuestNo(2).SelectGuestTitle(Title.Miss).TypeGuestFirstName("Johny").TypeGuestLastName("Test");
    //    //    //HotelPaymentPage.FillGuestDetails().ForGuestNo(3).SelectGuestTitle(Title.Mr).TypeGuestFirstName("John").TypeGuestLastName("Test");
    //    //    //HotelPaymentPage.FillGuestDetails().ForGuestNo(4).SelectGuestTitle(Title.Miss).TypeGuestFirstName("John").TypeGuestLastName("Test");
    //    //    //HotelPaymentPage.FillGuestDetails().ForGuestNo(5).SelectGuestTitle(Title.Master).TypeGuestFirstName("Johny").TypeGuestLastName("Test");
    //    //    //HotelPaymentPage.FillGuestDetails().ForGuestNo(6).SelectGuestTitle(Title.Master).TypeGuestFirstName("Johny").TypeGuestLastName("Test");

    //    //    PaymentPage.FillHotelOnlyGuestDetails().ForGuestNo(1).OfRoomNo(1).SelectGuestTitle(Title.Miss).TypeGuestFirstName("John").TypeGuestLastName("Test");
    //    //   // PaymentPage.FillHotelOnlyGuestDetails().ForGuestNo(2).OfRoomNo(1).SelectGuestTitle(Title.Mr).TypeGuestFirstName("John").TypeGuestLastName("Test");
    //    //    PaymentPage.FillHotelOnlyGuestDetails().ForGuestNo(1).OfRoomNo(2).SelectGuestTitle(Title.Master).TypeGuestFirstName("John").TypeGuestLastName("Test");
    //    //    PaymentPage.FillHotelOnlyGuestDetails().ForGuestNo(2).OfRoomNo(2).SelectGuestTitle(Title.Dr).TypeGuestFirstName("John").TypeGuestLastName("Test");
    //    //    PaymentPage.FillHotelOnlyGuestDetails().ForGuestNo(3).OfRoomNo(2).SelectGuestTitle(Title.Mrs).TypeGuestFirstName("John").TypeGuestLastName("Test");
    //    //    PaymentPage.FillHotelOnlyGuestDetails().ForGuestNo(4).OfRoomNo(2).SelectGuestTitle(Title.Ms).TypeGuestFirstName("John").TypeGuestLastName("Test");
    //    //    PaymentPage.FillHotelOnlyGuestDetails().ForGuestNo(5).OfRoomNo(2).SelectGuestTitle(Title.Miss).TypeGuestFirstName("John").TypeGuestLastName("Test");

    //    //    //fill in contact details
    //    //    PaymentPageRnd.FillinContactDetails();

    //    //    //select and fill in card details
    //    //    PaymentPage.FillCardDetails().SelectCard(Card.Mastercard).TypeCardHolderName("Mr Adam").TypeCardNo("5454545454545454").TypeExpiryDate("02/18").WithSecurityCode("503");

    //    //    //type in postcode
    //    //    PaymentPage.TypePostCode("SL1 2NQ");

    //    //    //select address
    //    //    PaymentPageRnd.SelectAddress();

    //    //    //enter staff details
    //    //    PaymentPageRnd.FillStaffDetails();

    //    //    //click confirmation button
    //    //    PaymentPage.CheckConfirmAndCapture();
      
    //    //}
    //}
}
