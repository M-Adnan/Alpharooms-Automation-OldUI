using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AlphaRooms.AutomationFramework;
using AlphaRooms.AutomationFramework.Tests.Properties;
using AlphaRooms.AutomationFramework.Tests.Utilities;

namespace AlphaRooms.AutomationFramework.Tests.SOA_Flight_Tests
{
    [TestFixture]
    public class MulticomSupplierTesting : AlpharoomsTestBase
    {
        private IEnumerable<TestCaseData> GetTestData()
        {
            List<TestCaseData> tests = new List<TestCaseData>();
            CSVReader reader = new CSVReader("Test Data\\FlightSOA_Availability_TestData.csv", true);
            while (reader.Read())
            {
                TestScript data = new TestScript() { 
                    TestName = reader["Test_Name"]
                    , Destination = reader["Destination"]
                    , DepartureDate = reader["Departure_Date"]
                    , ReturnDate = reader["Return_Date"]
                    , DepartureAirport = reader["Departure_Airport"]
                    , Adults = int.Parse(reader["Adults"])
                    , Children = int.Parse(reader["Children"])
                    , ChildrenAges = (reader["Children_Ages"] != null ? reader["Children_Ages"].Split(',').Select(i => int.Parse(i)).ToArray() : new int[0])
                };
                tests.Add(NUnitModule.CreateTestCaseData(data, data.TestName, ""));
            }
            return tests;
        }

        [Test]
        [TestCaseSource("GetTestData")]
        [Category("Flight_SOA")]
        public void ShouldSearch_MostPopularRoutes_ForAllMultiCom_Suppliers(TestScript script)
        {


            //Select united kingdom as the user location
            HomePage.TopPanel.ClickLocation(Location.UnitedKingdom);

            Assert.That(HomePage.IsDisplayed(), Is.True, "Home Page isn't displayed within 60 sec");

            HomePage.SearchFor().FlightOnly().ToDestination(script.Destination).FromCheckIn(Calendar.PickRandomCheckInDate())
                .ToCheckOut(Calendar.PickRandomCheckOutDate()).FromDepartureAirport(script.DepartureAirport)
                .ForAdults(script.Adults).SearchAndCapture();

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
            Assert.That(ExtrasPage.ValidatePrice(),Is.True,"Difference in price comparision - Flight price on results page: {0} - Change in Price on Extra Page {1} - Orignal price on extra page: {2}",
                FlightResultsPage.Data.Price
                ,ExtrasPage.Data.ChangedPrice
                ,ExtrasPage.Data.OrignalPrice);
            
            //Check total price against changed price
            if (ExtrasPage.Data.TotalPrice == 0)
            {
                ExtrasPage.Save(Information.TotalPrice);
                Assert.AreEqual(ExtrasPage.Data.ChangedPrice, ExtrasPage.Data.TotalPrice, "Difference in price comparision - Changed price on extras page: {0} - Total Price on Extras page {1}"
                    ,ExtrasPage.Data.ChangedPrice
                    ,ExtrasPage.Data.TotalPrice);
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

            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment Page isn't displayed");
            //PaymentPage.Save(Information.Flight);

            //Assert.AreEqual(ExtrasPage.Data.OutboundDepartureTime, PaymentPage.Data.OutboundDepartureTime, "Extra's page Outbound DepartureTime doesn't match with Payment Page");
            //Assert.AreEqual(ExtrasPage.Data.OutboundArrivalTime, PaymentPage.Data.OutboundArrivalTime, "Extra's page Outbound ArrivalTime doesn't match with Payment Page");
            //Assert.AreEqual(ExtrasPage.Data.OutboundJourneyTime, PaymentPage.Data.OutboundJourneyTime, "Extra's page Outbound JourneyTime doesn't match with Payment Page");
            //Assert.AreEqual(ExtrasPage.Data.OutboundJourney, PaymentPage.Data.OutboundJourney, "Extra's page Outbound Journey doesn't match with Payment Page");
            //Assert.AreEqual(ExtrasPage.Data.OutboundAirline, PaymentPage.Data.OutboundAirline, "Extra's page Outbound Airline doesn't match with Payment Page");
            //Assert.AreEqual(ExtrasPage.Data.OutboundFlightNo, PaymentPage.Data.OutboundFlightNo, "Extra's page Outbound FlightNumber doesn't match with Payment Page");



            //Assert.AreEqual(ExtrasPage.Data.InboundDepartureTime, PaymentPage.Data.InboundDepartureTime, "Extra's page Inbound DepartureTime doesn't match with Payment Page");
            //Assert.AreEqual(ExtrasPage.Data.InboundArrivalTime, PaymentPage.Data.InboundArrivalTime, "Extra's page Inbound ArrivalTime doesn't match with Payment Page");
            //Assert.AreEqual(ExtrasPage.Data.InboundJourneyTime, PaymentPage.Data.InboundJourneyTime, "Extra's page Inbound JourneyTime doesn't match with Payment Page");
            //Assert.AreEqual(ExtrasPage.Data.InboundJourney, PaymentPage.Data.InboundJourney, "Extra's page Inbound Journey doesn't match with Payment Page");
            //Assert.AreEqual(ExtrasPage.Data.InboundAirline, PaymentPage.Data.InboundAirline, "Extra's page Inbound Airline doesn't match with Payment Page");
            //Assert.AreEqual(ExtrasPage.Data.InboundFlightNo, PaymentPage.Data.InboundFlightNo, "Extra's page Inbound FlightNumber doesn't match with Payment Page");

            //PaymentPage.Save(Information.TotalPrice);

            //Assert.AreEqual(ExtrasPage.Data.TotalPrice, PaymentPage.Data.TotalPrice);

            //PaymentPage.MakeAFlightBooking()
            //    .ForGuestDetailsNumber(1).AutoFill()
            //    .ForGuestDetailsNumber(2).AutoFill()
            //    .ForContactDetails()
            //    .WithEmail("test@test.com").WithPhoneNumber("08719110030")
            //    .ForPaymentDetails()
            //    .WithPaymentType(Card.Mastercard)
            //    .WithCardHolderName("Test")
            //    .WithCardNo("5454545454545454")
            //    .WithExpiryDate("10/20")
            //    .WithSecurityCode("123")
            //    .WithPostCode("S11 8NX")
            //    .ForStaffDetails().WithReference("test").WithCustomerPhone("Test")
            //    .Confirm();

            //Assert.IsTrue(ConfirmationPage.IsDisplayed(), "Confirmation page isn't displayed within 60 sec");

            //Assert.IsTrue(ConfirmationPage.PNRNoIsDisplayed(), "PNR Number is empty");

            //Assert.Pass(script.TestName + " Sucessful");
        }
    }
}
