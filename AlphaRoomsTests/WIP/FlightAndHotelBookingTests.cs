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
    public class FlightAndHotelBookingTests : AlpharoomsTestBase
    {
        [Test]
        public void ShouldMakeFlightAndHotelBooking()
        {
            //Enter flight only search data
            HomePage.SearchFor().FlightAndHotel().ToDestination("Tenerife (Main), Tenerife, Canaries (TFS)").FromCheckIn("20/06/2014")
                .ToCheckOut("27/07/2014").FromDepartureAirport("London Heathrow, London, United Kingdom (LHR)")
                .ForAdults(2).SearchAndCapture();

            //check if result page is displayed
            Assert.That(FlightResultsPage.IsDisplayed(), Is.True, "Flight Search Result Page isn't displayed");
            Assert.That(FlightResultsPage.AreResultsDisplayed(), Is.True, "No results are displayed");

            //Search for supplier
            //Assert.That(FlightResultsPage.ContainsFlightFromSupplier("TP"), Is.True, "Supplier not found on current select page");

            FlightResultsPage.SelectFlight().ByFlightNumber(1).ContinueAndCapture();

            //Check if result page is displayed within 60 sec
            Assert.That(HotelResultsPage.IsDisplayed(), Is.True, "Hotel Search Result Page isn't displayed within 60 sec");

            Assert.That(HotelResultsPage.AreResultsDisplayed(), Is.True, "No hotel results page are displayed");

            //check if ItalCamel supplier is available from first results page
            //Assert.That(HotelResultsPage.ContainsRoomFromSupplier("ItalCamel (6)"), Is.True, "ItalCamel supplier isn't displayed on first result page");

            //Select first room option of the first hotel displayed on the very first result page
            HotelResultsPage.ClickAvailableRoom(1, 1);

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed");

            ExtrasPage.Save(Information.FlightAndHotel);

            //ExtrasPage.Confirm(Information.FlightAndHotel);

            ExtrasPage.ClickBookNow();

            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment Page not displayed");

            PaymentPage.Save(Information.FlightAndHotel);
        }


        [Test]
        public void HotelBookingTestPaths()
        {
            //Enter flight only search data
            HomePage.SearchFor().HotelOnly().ToHotelName("Tenerife (Main), Tenerife, Canaries (TFS)").FromCheckIn("20/06/2014")
                .ToCheckOut("27/07/2014").ForAdults(2).AddAnotherRoom().ForAdults(3).WithChildren(2).OfAges(1,3).SearchAndCapture();
        
            //Check if result page is displayed within 60 sec
            Assert.That(HotelResultsPage.IsDisplayed(), Is.True, "Hotel Search Result Page isn't displayed within 60 sec");

            Assert.That(HotelResultsPage.AreResultsDisplayed(), Is.True, "No hotel results page are displayed");

            //Select first room option of the first hotel displayed on the very first result page
            HotelResultsPage.SelectRoom().ByHotelName("Iberostar Grand Hotel Mencey").ForRoomNumber(1).WithAvailableRoom(1)
                .ForRoomNumber(2).WithAvailableRoom(2).ContinueAndCapture();

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed");

            ExtrasPage.Save(Information.FlightAndHotel);

            //ExtrasPage.Confirm(Information.FlightAndHotel);

            ExtrasPage.ClickBookNow();

            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment Page not displayed");

            PaymentPage.Save(Information.FlightAndHotel);
        }

        //    //Click Booknow button
        //    ExtrasPage.BookNow().Continue();

        //    //Check Payment Page is displayed
        //    Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");
            
        //    PaymentPage.MakeAFlightAndHotelBooking()
        //        .ForGuestNumber(1).OfRoomNo(1).SelectTitle(Title.Mr).TypeFirstName("John").TypeLastName("Test").SelectDoB("2/3/2000")
        //        .ForGuestNumber(2).OfRoomNo(1).SelectTitle(Title.Miss).TypeFirstName("Mary").TypeLastName("Depaul").SelectDoB("2/3/2000")
        //        .ForContactDetails().TypeFirstName("Contact").TypeLastName("ContactLast").WithEmail("c@yahoo.com").WithNumber("1234566789")
        //        .ForPaymentDetails().SelectPaymentType(Card.Mastercard).TypeCardNo("5454545454545454").TypeExpiryDate("02/18").WithSecurityCode("503")
        //        .WithCardHolderName("ThisGuy").WithPostCode("SL1 2NQ")
        //        .ForStaffDetails().TypeReference("222").TypeCustomerPhone("3432")
        //        .ConfirmAndCapture();

            //PaymentPage.MakeAFlightAndHotelBooking().GuestDetails().AutoFill().ForContactDetails().AutoFill()..ForPaymentDetails().Autofill()..ForStaffDetails().AutoFill()..ConfirmAndCapture();

            //check if landing page is displayed
            //Assert.That(HomePage.IsDisplayed, Is.True, "Home Page isn't displayed");

//            //Enter flight only search data
//            HomePage.SearchFor().FlightAndHotel().ToDestination("Mallorca (Majorca), Spain").FromCheckIn("20/04/2014")
//                .ToCheckOut("05/05/2014").FromDepartureAirport("London Heathrow, London, United Kingdom (LHR)")
//                .ForAdults(1).WithChildren(1).OfAges(1)
//                .AddAnotherRoom().ForAdults(2).WithChildren(2).OfAges(2, 3).Search();

//            //check if result page is displayed
//            Assert.That(FlightResultsPage.IsDisplayed(), Is.True, "Search Result Page isn't displayed");

//            //select a page
//            FlightResultsPage.ClickResultPage(1);

//            //select flight
//            FlightResultsPage.ClickFlight(1);

//            //check if the hotel result page is displayed
//            Assert.That(HotelResultsPage.IsDisplayed(), Is.True, "Hotel Result page is not displayed");

//            //select the hotel
//            HotelResultsPage.ClickResultPage(1);

//            // Select room options
//            //HotelResultsPage.SelectHotel(2).WithRoomOption(1);
//            //HotelResultsPage.SelectRoomNo(2).WithRoomOption(1);

//            //click continue
//            // HotelResultsPage.ContinueAndCapture();

//            //check if flight and hotel summary page is displayed
//            Assert.That(ExtrasPage.IsDisplayed, Is.True, "Hotel Summary page is not been displayed");

//            ExtrasPage.Save(Information.FlightAndHotel);

//            //confirm information
//            Assert.That(ExtrasPage.Confirm(Information.FlightAndHotel), Is.True, "Selected flightAndHotel information doesn't match with flight information on Extras page");

//            //click book now
//            ExtrasPage.BookNow().Continue();

//            //check if payment page is displayed
//            Assert.That(PaymentPage.IsDisplayed, Is.True, "Payment page isn't displayed");

//            //save information
//            PaymentPage.Save(Information.FlightAndHotel);

//            //confirm information
//            Assert.That(PaymentPage.Confirm(Information.FlightAndHotel), Is.True, "Selected flight information doesn't match with flight information on Extras page");

//            //fill in guesst details




            //PaymentPage.FillFlightAndHotelGuestDetails().ForGuestNo(1).OfRoomNo(1).SelectGuestTitle(Title.Miss).TypeGuestFirstName("John").TypeGuestLastName("Test").SelectGuestDOB("01/01/1980");
            //PaymentPage.FillFlightAndHotelGuestDetails().ForGuestNo(2).OfRoomNo(1).SelectGuestTitle(Title.Mr).TypeGuestFirstName("John").TypeGuestLastName("Test").SelectGuestDOB("19/03/2013");
            //PaymentPage.FillFlightAndHotelGuestDetails().ForGuestNo(1).OfRoomNo(2).SelectGuestTitle(Title.Master).TypeGuestFirstName("John").TypeGuestLastName("Test").SelectGuestDOB("01/01/1980");
            //PaymentPage.FillFlightAndHotelGuestDetails().ForGuestNo(2).OfRoomNo(2).SelectGuestTitle(Title.Dr).TypeGuestFirstName("John").TypeGuestLastName("Test").SelectGuestDOB("01/01/1980");
            //PaymentPage.FillFlightAndHotelGuestDetails().ForGuestNo(3).OfRoomNo(2).SelectGuestTitle(Title.Mrs).TypeGuestFirstName("John").TypeGuestLastName("Test").SelectGuestDOB("19/03/2012");
            //PaymentPage.FillFlightAndHotelGuestDetails().ForGuestNo(4).OfRoomNo(2).SelectGuestTitle(Title.Ms).TypeGuestFirstName("John").TypeGuestLastName("Test").SelectGuestDOB("19/03/2011");
            ////PaymentPage.FillFlightAndHotelGuestDetails().ForGuestNo(5).OfRoomNo(2).SelectGuestTitle(Title.Miss).TypeGuestFirstName("John").TypeGuestLastName("Test").SelectGuestDOB("19/03/2011");

            ////fill in contact details
            //PaymentPageRnd.FillinContactDetails();

            ////select and fill in card details
            //PaymentPage.FillCardDetails().SelectCard(Card.Mastercard).TypeCardHolderName("Mr Adam").TypeCardNo("5454545454545454").TypeExpiryDate("02/18").WithSecurityCode("503");

            ////type in postcode
            //PaymentPage.TypePostCode("SL1 2NQ");

            //select address
            //PaymentPageRnd.SelectAddress();

            ////enter staff details
            //PaymentPageRnd.FillStaffDetails();

            ////click confirmation button
            //PaymentPage.CheckConfirmAndCapture();
    //    }
    }
}

