using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AlphaRooms.AutomationFramework;
//using Excel = Microsoft.Office.Interop.Excel;

namespace AlphaRooms.AutomationFramework.Tests.Smoke_Tests
{
    [TestFixture]
    public class FlightOnlyBookingTests : AlpharoomsTestBase
    {
        [Test]
        public void ShouldMakeFlightOnlyBooking()
        {
            //Select united kingdom as the user location
            HomePage.TopPanel.ClickLocation(Location.UnitedKingdom);

            HomePage.SearchFor().FlightOnly().ToDestination("AYT").FromCheckIn("10/10/2014")
                .ToCheckOut("17/10/2014").FromDepartureAirport("Leeds-Bradford, Leeds, United Kingdom (LBA)")
                .ForAdults(2).SearchAndCapture();

            //Check if result page is displayed within 60 sec
            Assert.That(FlightResultsPage.IsDisplayed(), Is.True, "Flight Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(FlightResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the Flight search");

            //Check if search criteria match flight search header
            //Assert.That(FlightResultsPage.ValidateSearchCriteria(), Is.True, "Home Search criteria");

            //Confirm all flights depature and return dates are same as the search criteria
            //Assert.That(FlightResultsPage.ValidateResultDates(),Is.True,"Search criteria dates don’t match with the flight results");

            //Confirm all inbond outbond departure airport are correct
            //Assert.That(FlightResultsPage.ValidateResultDestinations(), Is.True, "Search criteria destination don’t match with the flight results");

            //Find supplier on first result page
            //Assert.IsTrue(FlightResultsPage.ContainsFlightFromSupplier(script.TestName),"Supplier name not found in staff 'Supplier' and 'Airline' information");

            //select the first result from first page
            FlightResultsPage.SelectFlight().ByFlightNumber(FlightResultsPageRnd.PickRandomFlight()).ContinueAndCapture();
            
            //check extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras Page isn't displayed");

            //Price checks
            Assert.That(ExtrasPage.ValidatePrice(), Is.True, "Difference in price comparision - Flight price on results page: {0} - Change in Price on Extra Page {1} - Orignal price on extra page: {2}",
                FlightResultsPage.Data.Price
                , ExtrasPage.Data.ChangedPrice
                , ExtrasPage.Data.OrignalPrice);

            //Check total price against changed price
            if (ExtrasPage.Data.TotalPrice == 0)
            {
                ExtrasPage.Save(Information.TotalPrice);
                Assert.AreEqual(ExtrasPage.Data.ChangedPrice, ExtrasPage.Data.TotalPrice, "Difference in price comparision - Changed price on extras page: {0} - Total Price on Extras page {1}"
                    , ExtrasPage.Data.ChangedPrice
                    , ExtrasPage.Data.TotalPrice);
            }

            //save flight information
            ExtrasPage.Save(Information.Flight);

            //Compare selected flight information
            Assert.AreEqual(FlightResultsPage.Data.OutboundDepartureTime, ExtrasPage.Data.OutboundDepartureTime, "FlightResult page Outbound DepartureTime doesn't match with Extra Page");
            Assert.AreEqual(FlightResultsPage.Data.OutboundArrivalTime, ExtrasPage.Data.OutboundArrivalTime, "FlightResult page Outbound ArrivalTime doesn't match with Extra Page");
            Assert.AreEqual(FlightResultsPage.Data.OutboundFlightNo, ExtrasPage.Data.OutboundFlightNo, "FlightResult page Outbound FlightNumber doesn't match with Extra Page");
            Assert.AreEqual(FlightResultsPage.Data.OutboundDepartureAirport, ExtrasPage.Data.OutboundDepartureAiport, "FlightResult page Outbound DepartureAirport doesn't match with Extra Page");
            Assert.AreEqual(FlightResultsPage.Data.OutboundArrivalAirport, ExtrasPage.Data.OutboundArrivalAirport, "FlightResult page Outbound ArrivalAirport doesn't match with Extra Page");
            Assert.AreEqual(FlightResultsPage.Data.OutboundJourneyTime, ExtrasPage.Data.OutboundJourneyTime, "FlightResult page Outbound JourneyTime doesn't match with Extra Page");

            //Assert.AreEqual(FlightResultsPage.Data.OutboundJourney, ExtrasPage.Data.OutboundJourney);


            Assert.AreEqual(FlightResultsPage.Data.InboundFlightNo, ExtrasPage.Data.InboundFlightNo, "FlightResult page Inbound FlightNumber doesn't match with Extra Page");
            Assert.AreEqual(FlightResultsPage.Data.InboundDepartureTime, ExtrasPage.Data.InboundDepartureTime, "FlightResult page Inbound DepartureTime doesn't match with Extra Page");
            Assert.AreEqual(FlightResultsPage.Data.InboundDepartureAirport, ExtrasPage.Data.InbounddDepartureAiport, "FlightResult page Inbound DepartureAirport doesn't match with Extra Page");
            Assert.AreEqual(FlightResultsPage.Data.InboundArrivalAirport, ExtrasPage.Data.InboundArrivalAirport, "FlightResult page Inbound ArrivalAirport doesn't match with Extra Page");
            Assert.AreEqual(FlightResultsPage.Data.InboundArrivalTime, ExtrasPage.Data.InboundArrivalTime, "FlightResult page Inbound ArrivalTime doesn't match with Extra Page");
            //Assert.AreEqual(FlightResultsPage.Data.InboundJourney, ExtrasPage.Data.InboundJourney);
            Assert.AreEqual(FlightResultsPage.Data.InboundJourneyTime, ExtrasPage.Data.InboundJourneyTime, "FlightResult page Inbound JourneyTime doesn't match with Extra Page");

            ExtrasPage.BookFlight().Continue();

            PaymentPage.Save(Information.Flight);

            Assert.AreEqual(ExtrasPage.Data.OutboundDepartureTime, PaymentPage.Data.OutboundDepartureTime, "Extra's page Outbound DepartureTime doesn't match with Payment Page");
            Assert.AreEqual(ExtrasPage.Data.OutboundArrivalTime, PaymentPage.Data.OutboundArrivalTime, "Extra's page Outbound ArrivalTime doesn't match with Payment Page");
            Assert.AreEqual(ExtrasPage.Data.OutboundJourneyTime, PaymentPage.Data.OutboundJourneyTime, "Extra's page Outbound JourneyTime doesn't match with Payment Page");
            Assert.AreEqual(ExtrasPage.Data.OutboundJourney, PaymentPage.Data.OutboundJourney, "Extra's page Outbound Journey doesn't match with Payment Page");
            Assert.AreEqual(ExtrasPage.Data.OutboundAirline, PaymentPage.Data.OutboundAirline, "Extra's page Outbound Airline doesn't match with Payment Page");
            Assert.AreEqual(ExtrasPage.Data.OutboundFlightNo, PaymentPage.Data.OutboundFlightNo, "Extra's page Outbound FlightNumber doesn't match with Payment Page");



            Assert.AreEqual(ExtrasPage.Data.InboundDepartureTime, PaymentPage.Data.InboundDepartureTime, "Extra's page Inbound DepartureTime doesn't match with Payment Page");
            Assert.AreEqual(ExtrasPage.Data.InboundArrivalTime, PaymentPage.Data.InboundArrivalTime, "Extra's page Inbound ArrivalTime doesn't match with Payment Page");
            Assert.AreEqual(ExtrasPage.Data.InboundJourneyTime, PaymentPage.Data.InboundJourneyTime, "Extra's page Inbound JourneyTime doesn't match with Payment Page");
            Assert.AreEqual(ExtrasPage.Data.InboundJourney, PaymentPage.Data.InboundJourney, "Extra's page Inbound Journey doesn't match with Payment Page");
            Assert.AreEqual(ExtrasPage.Data.InboundAirline, PaymentPage.Data.InboundAirline, "Extra's page Inbound Airline doesn't match with Payment Page");
            Assert.AreEqual(ExtrasPage.Data.InboundFlightNo, PaymentPage.Data.InboundFlightNo, "Extra's page Inbound FlightNumber doesn't match with Payment Page");

            PaymentPage.Save(Information.TotalPrice);

            Assert.AreEqual(ExtrasPage.Data.TotalPrice, PaymentPage.Data.TotalPrice);

            PaymentPage.MakeAFlightBooking()
                .ForGuestDetailsNumber(1).AutoFill()
                .ForGuestDetailsNumber(2).AutoFill()
                .ForContactDetails()
                .WithEmail("test@test.com").WithPhoneNumber("08719110030")
                .ForPaymentDetails()
                .WithPaymentType(Card.Mastercard)
                .WithCardNo("5454545454545454")
                .WithExpiryDate("10/20")
                .WithSecurityCode("123")
                .WithPostCode("S11 8NX")
                .ForStaffDetails().WithReference("test").WithCustomerPhone("Test")
                .Confirm();

            Assert.IsTrue(ConfirmationPage.IsDisplayed(), "Confirmation page isn't displayed within 60 sec");

            Assert.IsTrue(ConfirmationPage.PNRNoIsDisplayed(), "PNR Number is empty");

            ConfirmationPage.SaveItineraryNo();
            ConfirmationPage.SaveBookedByDetails();
            ConfirmationPage.SavePNRNo();
            ConfirmationPage.SaveFlightDetails();
            ConfirmationPage.SaveTotalPrice();

            Assert.AreEqual(PaymentPage.Data.TotalPriceWithCardFees, ConfirmationPage.Data.TotalPrice, "Total Price with Card Fee don't match with total price on booking page");
            
            AdminPanelPage.Navigate(AdminPanelEnviorment.Test);

            Assert.IsTrue(AdminPanelPage.IsDisplayed(), "AdminPanel Page isn't displayed");

            AdminPanelPage.SearchBookingByRefrence(ConfirmationPage.Data.ItineraryNo);

            Assert.IsTrue(AdminPanelPage.IsBookingFound(), "Booking is not found on admin panel");

            Assert.IsTrue(AdminPanelPage.IsBookingValid(ConfirmationPage.Data.ItineraryNo), "Itinerary number of the booking found donot match with the itinerary number provided");

            AdminPanelPage.SaveBookingDetails();

            //ConfirmationPage
            //HomePage.TopPanel.ClickLocation(Location.UnitedKingdom);

            ////check if landing page is displayed
            //Assert.That(HomePage.IsDisplayed, Is.True, "Home Page isn't displayed");

            ////check if landing page is displayed
            ////Assert.That(HomePage.IsDisplayed, Is.True, "Home Page isn't displayed");
            ////HotelResultsPage.SelectRoom().ByHotel("Hilton").OnlyOneRoomWithAvailableRoom(1).Continue();

            ////Enter flight only search data
            //HomePage.SearchFor().FlightOnly().ToDestination("AYT").FromCheckIn(Calendar.PickRandomCheckInDate())
            //    .ToCheckOut(Calendar.PickRandomCheckOutDate()).FromDepartureAirport("London Gatwick, London, United Kingdom (LGW)")
            //    .ForAdults(2).SearchAndCapture();
            ////Enter flight only search data
            ///*HomePage.SearchFor().FlightOnly().ToDestination("Paris (All Airports)").FromCheckIn("23/04/2014")
            //    .ToCheckOut("30/04/2014").FromDepartureAirport("London (All Airports), London, United Kingdom (LON)")
            //    .ForAdults(2).SearchAndCapture();*/
            ///*HomePage.SearchFor().FlightOnly().ToDestination(script.Destination).FromCheckIn(HomePageRnd.PickRandomCheckInDate())
            //    .ToCheckOut(HomePageRnd.PickRandomCheckOutDate()).FromDepartureAirport(script.DepartureAirport)
            //    .ForAdults(script.Adults).SearchAndCapture();*/

            ////Check if result page is displayed within 60 sec
            //Assert.That(FlightResultsPage.IsDisplayed(), Is.True, "Flight Search Result Page isn't displayed within 60 sec");

            //FlightResultsPage.ExpandFlexible();
            //FlightResultsPage.ClickSearchDateRange(SearchDateRanges.Minus3Days);

            //FlightResultsPage.ChangeSearchPanel.ChangeSearch().FlightOnly().ToDestination("PMI").FromCheckIn(Calendar.PickRandomCheckInDate())
            //    .ToCheckOut(Calendar.PickRandomCheckOutDate()).FromDepartureAirport("London Heathrow, London, United Kingdom (LHR)").ForAdults(3).WithChildren(2).OfAges(1, 2).ShowPricesAndCapture();

            ////Check if any hote results are displayed for the search
            //Assert.That(FlightResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the Flight search");

            ////Check if search criteria match flight search header
            ////Assert.That(FlightResultsPage.ValidateSearchCriteria(), Is.True, "Home Search criteria");

            ////Confirm all flights depature and return dates are same as the search criteria
            //FlightResultsPage.ValidateResultDates();

            ////Confirm all inbond outbond departure airport are correct
            //FlightResultsPage.ValidateResultDestinations();

            ////select the first result from first page
            //FlightResultsPage.SelectFlight().ByFlightNumber(1).Continue();

            ////check extra page is displayed
            //Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras Page isn't displayed");

            ////save flight information on extra's page
            //Assert.That(ExtrasPage.ValidatePrice(), Is.True, "Price on flight result page and extra page doesn't match");

            ////Check total price against changed price
            //if (ExtrasPage.Data.TotalPrice == 0)
            //{
            //    ExtrasPage.SaveTotalPrice();
            //    Assert.AreEqual(ExtrasPage.Data.ChangedPrice, ExtrasPage.Data.TotalPrice, "Changed price and total price don't match");
            //}

            ////save flight information
            //ExtrasPage.Save(Information.Flight);

            ////Compare selected flight information
            //Assert.AreEqual(FlightResultsPage.Data.OutboundFlightNo, ExtrasPage.Data.OutboundFlightNo);
            //Assert.AreEqual(FlightResultsPage.Data.OutboundDepartureTime, ExtrasPage.Data.OutboundDepartureTime);
            //Assert.AreEqual(FlightResultsPage.Data.OutboundDepartureAirport, ExtrasPage.Data.OutboundDepartureAiport);
            //Assert.AreEqual(FlightResultsPage.Data.OutboundArrivalAirport, ExtrasPage.Data.OutboundArrivalAirport);
            //Assert.AreEqual(FlightResultsPage.Data.OutboundArrivalTime, ExtrasPage.Data.OutboundArrivalTime);
            ////Assert.AreEqual(FlightResultsPage.Data.OutboundJourney, ExtrasPage.Data.OutboundJourney);
            //Assert.AreEqual(FlightResultsPage.Data.OutboundJourneyTime, ExtrasPage.Data.OutboundJourneyTime);

            //Assert.AreEqual(FlightResultsPage.Data.InboundFlightNo, ExtrasPage.Data.InboundFlightNo);
            //Assert.AreEqual(FlightResultsPage.Data.InboundDepartureTime, ExtrasPage.Data.InboundDepartureTime);
            //Assert.AreEqual(FlightResultsPage.Data.InboundDepartureAirport, ExtrasPage.Data.InbounddDepartureAiport);
            //Assert.AreEqual(FlightResultsPage.Data.InboundArrivalAirport, ExtrasPage.Data.InboundArrivalAirport);
            //Assert.AreEqual(FlightResultsPage.Data.InboundArrivalTime, ExtrasPage.Data.InboundArrivalTime);
            ////Assert.AreEqual(FlightResultsPage.Data.InboundJourney, ExtrasPage.Data.InboundJourney);
            //Assert.AreEqual(FlightResultsPage.Data.InboundJourneyTime, ExtrasPage.Data.InboundJourneyTime);

            //ExtrasPage.BookFlight().Continue();

            //PaymentPage.Save(Information.Flight);

            //Assert.AreEqual(ExtrasPage.Data.OutboundFlightNo, PaymentPage.Data.OutboundFlightNo);
            //Assert.AreEqual(ExtrasPage.Data.OutboundDepartureTime, PaymentPage.Data.OutboundDepartureTime);
            //Assert.AreEqual(ExtrasPage.Data.OutboundJourney, PaymentPage.Data.OutboundJourney);

            //Assert.AreEqual(ExtrasPage.Data.InboundFlightNo, PaymentPage.Data.InboundFlightNo);
            //Assert.AreEqual(ExtrasPage.Data.InboundDepartureTime, PaymentPage.Data.InboundDepartureTime);
            //Assert.AreEqual(ExtrasPage.Data.InboundJourney, PaymentPage.Data.InboundJourney);

            //PaymentPage.Save(Information.TotalPrice);

            //Assert.AreEqual(ExtrasPage.Data.TotalPrice, PaymentPage.Data.TotalPrice);

            ////Assert.AreEqual(FlightResultsPage.Data.OutboundArrivalAirport, ExtrasPage.Data.OutboundArrivalAirport);
            ////Assert.AreEqual(FlightResultsPage.Data.OutboundFlightArrivalTime, ExtrasPage.Data.OutboundArrivalTime);
            ////Assert.AreEqual(FlightResultsPage.Data.OutboundJourney, ExtrasPage.Data.OutboundJourney);
            ////Assert.AreEqual(FlightResultsPage.Data.OutboundJourneyTime, ExtrasPage.Data.OutboundJourneyTime);
            ////ExtrasPage.BookFlight().Continue();
            //////check if result page is displayed
            ////Assert.That(FlightResultsPage.IsDisplayed(), Is.True, "Flight Search Result Page isn't displayed");
            ////Assert.That(FlightResultsPage.AreResultsDisplayed(), Is.True, "Flight Search Results aren't displayed");
            ////Assert.That(FlightResultsPage.ValidateSearchCriteria(), Is.True, "Validate Search Criteria with Flight Results Page Header");
            //////throw new Exception();
            //////throw new Exception();

            ////Assert.That(FlightResultsPage.ValidateResultDates(), Is.True, "Search dates donot match");
            ////Assert.That(FlightResultsPage.ValidateResultDestinations(), Is.True, "Search dates donot match");
            //////search for Airline
            //////Assert.That(FlightResultsPage.ContainsFlightFromAirline("Easyjet"), Is.True, "EasyJet Airline is available in results");
            ////FlightResultsPage.SelectFlight().ByFlightNumber(4).ContinueAndCapture();

            ////select flight by airline 
            ////FlightResultsPage.ClickFlight(FlightResultsPage.FindFlightForFirstAirline("Easyjet"));

            //////FlightResultsPage.FindFlightForFirstAirline("EasyJet")

            //////Search for supplier
            //////Assert.That(FlightResultsPage.ContainsFlightFromSupplier("TP"), Is.True, "Supplier not found on current select page");
            ////////Search for supplier
            //////Assert.That(FlightResultsPage.ContainsFlightFromSupplier("TP"), Is.True, "Supplier not found on current select page");

            ////FlightResultsPage.SelectFlight().ByFirstSupplier("TP").Continue();
            //////FlightResultsPage.SelectFlight().ByFirstSupplier("TP").Continue();

            //////confirm if flight summary page is displayed
            ////Assert.That(ExtrasPage.IsDisplayed, Is.True, "Flight summary page isn't displayed");
            ////////confirm if flight summary page is displayed
            //////Assert.That(ExtrasPage.IsDisplayed, Is.True, "Flight summary page isn't displayed");

            ////ExtrasPage.CheckHoldLuggage();
            //////ExtrasPage.CheckHoldLuggage();

            ////ExtrasPage.SelectHoldLuggagePassengers(1);
            //////ExtrasPage.SelectHoldLuggagePassengers(1);

            ////ExtrasPage.CheckFlightExtraNumber(2);
            //////ExtrasPage.CheckFlightExtrasOption(2);

            ////ExtrasPage.SelectFlightExtraPassengers(2, 1);
            //////ExtrasPage.SelectFlightExtraPassengers(2, 1);

            ////ExtrasPage.ClickExtraLink(Extras.Airport_Transfer);
            //////ExtrasPage.ClickExtraLink(Extras.Airport_Transfer);
            ////ExtrasPage.SelectAirportTransferHotelLocation("Charles De Gaulle Airport");
            //////ExtrasPage.SelectAirportTransferHotelLocation("Charles De Gaulle Airport");

            ////ExtrasPage.TypeAirportTransferHotel("Kyriad Prestige Roissy");
            //////ExtrasPage.TypeAirportTransferHotel("Kyriad Prestige Roissy");

            ////ExtrasPage.ClickAirportTransterUpdate();
            //////ExtrasPage.ClickAirportTransterUpdate();

            ////ExtrasPage.CheckTransferNumber(1);
            //////ExtrasPage.CheckTransferNumber(1);

            //////ExtrasPage.ClickExtraLink(Extras.Car_Hire);

            //////ExtrasPage.SelectCarHirePickupLocation("Beauvais Airport");

            //////ExtrasPage.SelectCarHireMainDriverAge(22);

            //////ExtrasPage.SelectCarHirePickupTime("22:00");

            //////ExtrasPage.SelectCarHireDropoffTime("07:00");

            //////ExtrasPage.CheckCarHireNumber(2);

            ////ExtrasPage.ClickExtraLink(Extras.Airport_Parking);
            //////ExtrasPage.ClickExtraLink(Extras.Airport_Parking);

            ////ExtrasPage.SelectAirportParkingDropoffTime("20:00");
            //////ExtrasPage.SelectAirportParkingDropoffTime("20:00");

            ////ExtrasPage.SelectAirportParkingPickupTime("11:00");
            //////ExtrasPage.SelectAirportParkingPickupTime("11:00");

            ////ExtrasPage.ClickAirportParkingUpdate();
            //////ExtrasPage.ClickAirportParkingUpdate();

            ////ExtrasPage.CheckAirportParkingNumber(2);
            ////ExtrasPage.CheckAirportParkingNumber(2);

            ////ExtrasPage.IsVisible(Extras.Hold_luggage);
            ////ExtrasPage.Visible(Extras.Hold_luggage);
            
        }


        [Test]
        [Category("Live_Removed")]
        public void ShouldBookRandomSupplier_ForDestination_Majorca()
        {
            //Select the uk location
            HomePage.TopPanel.ClickLocation(Location.UnitedKingdom);

            //Enter Hotel only search data
            HomePage.SearchFor().HotelOnly().ToHotelName("Majorca").FromCheckIn("29/09/2014")
                .ToCheckOut("07/10/2014").SearchAndCapture();

            //Check if result page is displayed within 60 sec
            Assert.That(HotelResultsPage.IsDisplayed(), Is.True, "Hotel Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(HotelResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the hotel search");

            //Click on a random hotel from results page
            //HotelResultsPage.ClickHotelNumber(HotelResultsPageRnd.PickRandomHotel());

            //Check if hotel detail page is displayed
            //Assert.That(HotelDetailPage.IsDisplayed(), Is.True, "Hotel Detail Page isn't displayed within 60 sec");

            //Click the first available room from hotel detail page
            //HotelDetailPage.SelectRoom().WithAvailableRoom(1).ContinueAndCapture();

            //Select Room 1 of a random hotel from the first result page
            HotelResultsPage.SelectRoom().ByHotelNumber(12).OnlyOneRoomWithAvailableRoom(1).ContinueAndCapture();

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed");

            //Click Booknow button
            ExtrasPage.BookHotel().ContinueAndCapture();

            //Check Payment Page is displayed
            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");

        }
        //    //Extras Page Save Flight Info
        //    //ExtrasPage.Data.
        //    ExtrasPage.Save(Information.Flight);
        //    Assert.That(ExtrasPage.Save(Information.Flight), Is.True, "Selected flight information doesn't match with flight information on Extras page");
        //    //Match flight information
        //    Assert.That(ExtrasPage.Confirm(Information.Flight), Is.True, "Selected flight information doesn't match with flight information on Extras page");
        //    ////Extras Page Save Flight Info
        //    ////ExtrasPage.Data.
        //    //ExtrasPage.Save(Information.Flight);

        //    //Match flight information
        //    //Assert.That(ExtrasPage.Confirm(Information.Flight), Is.True, "Selected flight information doesn't match with flight information on Extras page");

        //    /*ExtrasPage.BookNow().AddHoldLuggage().ForPassengers(2)
        //        .AddFlightExtras(1).WithPassengers(2)
        //        .AddFlightExtras(4).WithPassengers(1)
        //        .AddAirportTransfer().SetHotelLocation("").WithHotel("").SelectTransferNumber(1)
        //        .AddCarHire().SetPickupLocation("").WithMainDriverAge(21).WithPickupTime("08:00").WithDropoffTime("08:00")
        //        .AddAirportParking().WithDropoffTime("08:00").WithPickupTime("08:00").SetParkingNumber(1).Continue();*/

        //    //click book now and capture
        //    //ExtrasPage.BookNow().Continue();

        //    //check if payment page is displayed
        //    //Assert.That(PaymentPage.IsDisplayed, Is.True, "Payment page isn't displayed");

        //    /*PaymentPage.MakeAFlightBooking()
        //       .ForGuestNumber(1).SelectTitle(Title.Mr).TypeFirstName("John").TypeLastName("Test").SelectDoB("2/3/2000")
        //       .ForGuestNumber(2).SelectTitle(Title.Miss).TypeFirstName("Mary").TypeLastName("Depaul").SelectDoB("2/3/2000")
        //       .ForContactDetails().TypeFirstName("Contact").TypeLastName("ContactLast").WithEmail("c@yahoo.com").WithNumber("1234566789")
        //       .ForPaymentDetails().SelectPaymentType(Card.Mastercard).TypeCardNo("5454545454545454").TypeExpiryDate("02/18").WithSecurityCode("503")
        //       .WithCardHolderName("ThisGuy").WithPostCode("SL1 2NQ")
        //       .ForStaffDetails().TypeReference("222").TypeCustomerPhone("3432")
        //       .ConfirmAndCapture();*/

        //    //Extras Page Save Flight Info
        //    //PaymentPage.Save(Information.Flight);

        //    //confirm flight details
        //    //Assert.That(PaymentPage.Confirm(Information.Flight), Is.True, "Selected flight information doesn't match with flight information on Payment page");

        //    //fill in guest details
        //    //PaymentPageRnd.AutoFillinGuestDetails();
        //    //PaymentPage.FillGuestDetails().ForGuestNo(1).SelectGuestTitle(Title.Mr).TypeGuestFirstName("John").TypeGuestLastName("Test").SelectGuestDOB("20/10/1980");
        //    //PaymentPage.FillGuestDetails().ForGuestNo(2).SelectGuestTitle(Title.Miss).TypeGuestFirstName("John").TypeGuestLastName("Test").SelectGuestDOB("20/10/1980");
        //    //PaymentPage.FillGuestDetails().ForGuestNo(3).SelectGuestTitle(Title.Mr).TypeGuestFirstName("John").TypeGuestLastName("Test").SelectGuestDOB("20/10/1980");
        //    //PaymentPage.FillGuestDetails().ForGuestNo(4).SelectGuestTitle(Title.Miss).TypeGuestFirstName("John").TypeGuestLastName("Test").SelectGuestDOB("20/10/1980");
        //    //PaymentPage.FillGuestDetails().ForGuestNo(5).SelectGuestTitle(Title.Master).TypeGuestFirstName("John").TypeGuestLastName("Test").SelectGuestDOB("10/03/2012");
        //    //PaymentPage.FillGuestDetails().ForGuestNo(6).SelectGuestTitle(Title.Master).TypeGuestFirstName("John").TypeGuestLastName("Test").SelectGuestDOB("10/03/2010");

        //    ////fill in contact details
        //    //PaymentPageRnd.FillinContactDetails();

        //    ////select and fill in card details
        //    //PaymentPage.FillCardDetails().SelectCard(Card.Mastercard).TypeCardHolderName("Mr Adam").TypeCardNo("5454545454545454").TypeExpiryDate("02/18").WithSecurityCode("503");

        //    ////type in postcode
        //    //PaymentPage.TypePostCode("SL1 2NQ");

        //    ////select address
        //    //PaymentPageRnd.SelectAddress();

        //    ////enter staff details
        //    //PaymentPageRnd.FillStaffDetails();

        //    ////click confirmation button
        //    //PaymentPage.CheckConfirmAndCapture();
        //}

        //[Test]
        //public void ShouldMakeFlightOnlyBookingDataDriver()
        //{
        //    Excel.Application xlApp;
        //    Excel.Workbook xlWorkBook;
        //    Excel.Worksheet xlWorkSheet;
        //    Excel.Range range;

        //    string Destination,Departure_Date,Return_Date, Departure_Airport;
        //    int   Adults,Children=0;



        //    int rCnt = 0;


        //    xlApp = new Excel.Application();
        //    //open excel file
        //    xlWorkBook = xlApp.Workbooks.Open(@"C:\AlphaRoomsAutomationFramework\AlphaRoomsTests\Test Data\FlightSOA_Availability_TestData.xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

        //    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);


        //    //Gives the used cells in the sheet
        //    range = xlWorkSheet.UsedRange;

        //    for (rCnt = 2; rCnt <= range.Rows.Count; rCnt++)
        //    {

        //           //Get the string from the sheet
        //           Destination = ((Excel.Range)range.Cells[rCnt, 1]).Value2;
        //           Departure_Date = ((Excel.Range)range.Cells[rCnt, 2]).Value2;
        //           Return_Date = ((Excel.Range)range.Cells[rCnt, 3]).Value2;
        //           Departure_Airport = ((Excel.Range)range.Cells[rCnt, 4]).Value2;
        //           Adults = (int)((Excel.Range)range.Cells[rCnt, 5]).Value2;


        //           //Enter Hotel only search data
        //           HomePage.SearchFor().FlightOnly().ToDestination("Tenerife (Main), Tenerife, Canaries (TFS)").FromCheckIn(HomePageRnd.PickRandomCheckInDate())
        //               .ToCheckOut(HomePageRnd.PickRandomCheckOutDate()).FromDepartureAirport("London Heathrow, London, United Kingdom (LHR)").ForAdults(2).WithChildren(2).OfAges(1, 2).SearchAndCapture();
        //    }


        //    //check if landing page is displayed
        //    Assert.That(HomePage.IsDisplayed, Is.True, "Home Page isn't displayed");

        //    //Enter flight only search data
        //    HomePage.SearchFor().FlightOnly().ToDestination("Paris (All Airports)").FromCheckIn("01/05/2014")
        //        .ToCheckOut("07/05/2014").FromDepartureAirport("London (All Airports), London, United Kingdom (LON)")
        //        .ForAdults(2).SearchAndCapture();
        //}

        //}

         [Test]
        public void ShouldMakeFlightOnlyBookingWithRandomData()
        {
            //Enter Hotel only search data
            HomePage.SearchFor().FlightOnly().ToDestination("Malaga").FromCheckIn(Calendar.PickRandomCheckInDate())
            .ToCheckOut(Calendar.PickRandomCheckOutDate()).FromDepartureAirport("Dublin, Republic Of Ireland (DUB)")
            .ForAdults(2).WithChildren(2).OfAges(2, 4).Search();

            //Check if result page is displayed within 60 sec
            Assert.That(FlightResultsPage.IsDisplayed(), Is.True, "Flight Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(FlightResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the flight search");

            //Search for supplier TP
            //Assert.That(FlightResultsPage.ContainsFlightFromSupplier("TP"), Is.True, "TP supplier is not displayed on first result page");

            //Select first room option of the first hotel displayed on the very first result page
            //FlightResultsPage.SelectFlight().ByFirstSupplier("TP").ContinueAndCapture();

            //Check if the extra page is displayed
            //Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed within 60 sec");
            FlightResultsPage.SelectFlight().ByFlightNumber(1).ContinueAndCapture();

            Assert.That(HotelResultsPage.IsDisplayed(), Is.True, "Hotel Result page is not displayed");

            Assert.That(HotelResultsPage.AreResultsDisplayed(), Is.True, "No hotel results are available");

            HotelResultsPage.SelectRoom().ByHotelNumber(1).ForRoomNumber(1).WithAvailableRoom(1).Continue();

            
             // save flight information on extra page
            ExtrasPage.Save(Information.Flight);

            ExtrasPage.Confirm(Information.Flight);

            //Click Booknow button
            ExtrasPage.BookFlight().Continue();

            //Check Payment Page is displayed
            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");
        }
     }
}
       

