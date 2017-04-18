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
    public class HotelOnlyTests : AlpharoomsTestBase
    {
        [Test]
        [Category("Live_Removed")]
        public void ShouldBook_1st_MostPopularHotel_Mallorca()
        {
            //Enter Hotel only search data
            HomePage.SearchFor().HotelOnly().ToHotelName("Mallorca (Majorca)").FromCheckIn(Calendar.PickRandomCheckInDate())
                .ToCheckOut(Calendar.PickRandomCheckOutDate()).Search();

            //Check if result page is displayed within 60 sec
            Assert.That(HotelResultsPage.IsDisplayed(), Is.True, "Hotel Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(HotelResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the hotel search");

            //Select first room option of the first hotel displayed on the very first result page
            HotelResultsPage.SelectRoom().ByHotelNumber(HotelResultsPageRnd.PickRandomHotel()).OnlyOneRoomWithAvailableRoom(1).Continue();

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed");

            //Click Booknow button
            ExtrasPage.BookHotel().Continue();

            //Check Payment Page is displayed
            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");
        }

        [Test]
        [Category("Live_Removed")]
        public void ShouldBook_2nd_MostPopularHotel_Benidorm()
        {
            //Enter Hotel only search data
            HomePage.SearchFor().HotelOnly().ToHotelName("Sol Pelicanos Ocas,Sol Pelicanos Ocas").FromCheckIn(Calendar.PickRandomCheckInDate())
                .ToCheckOut(Calendar.PickRandomCheckOutDate()).AddAnotherRoom().Search();

            //Check if result page is displayed within 60 sec
            //Assert.That(HotelDetailPage.IsDisplayed(), Is.True, "Hotel Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            //Assert.That(HotelDetailPage., Is.True, "No Results are available for the hotel search");

            //Select first room option of the first hotel displayed on the very first result page
            HotelDetailPage.SelectRoom().ForRoomNumber(1).WithAvailableRoom(2).ForRoomNumber(2).WithAvailableRoom(1).Continue();

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed");

            //Click Booknow button
            ExtrasPage.BookHotel().Continue();

            //Check Payment Page is displayed
            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");

        }

        [Test]
        [Category("Live_Removed")]
        public void ShouldBook_3rd_MostPopularHotel_Costa_Del_Sol()
        {
            //Enter Hotel only search data
            HomePage.SearchFor().HotelOnly().ToHotelName("Costa Del Sol").FromCheckIn(Calendar.PickRandomCheckInDate())
                .ToCheckOut(Calendar.PickRandomCheckOutDate()).Search();

            //Check if result page is displayed within 60 sec
            Assert.That(HotelResultsPage.IsDisplayed(), Is.True, "Hotel Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(HotelResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the hotel search");

            //Select first room option of the first hotel displayed on the very first result page
            HotelResultsPage.SelectRoom().ByHotelNumber(1).OnlyOneRoomWithAvailableRoom(1).Continue();

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed");

            //Click Booknow button
            ExtrasPage.BookHotel().Continue();

            //Check Payment Page is displayed
            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");

        }

        [Test]
        [Category("Live_Removed")]
        public void ShouldBook_4th_MostPopularHotel_Tenerife()
        {
            //Enter Hotel only search data
            HomePage.SearchFor().HotelOnly().ToHotelName("Tenerife").FromCheckIn(Calendar.PickRandomCheckInDate())
                .ToCheckOut(Calendar.PickRandomCheckOutDate()).Search();

            //Check if result page is displayed within 60 sec
            Assert.That(HotelResultsPage.IsDisplayed(), Is.True, "Hotel Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(HotelResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the hotel search");

            //Select first room option of the first hotel displayed on the very first result page
            HotelResultsPage.SelectRoom().ByHotelNumber(1).OnlyOneRoomWithAvailableRoom(1).Continue();

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed");

            //Click Booknow button
            ExtrasPage.BookHotel().Continue();

            //Check Payment Page is displayed
            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");

        }

        [Test]
        [Category("Live_Removed")]
        public void ShouldBook_5th_MostPopularHotel_Algarve()
        {
            //Enter Hotel only search data
            HomePage.SearchFor().HotelOnly().ToHotelName("Algarve").FromCheckIn(Calendar.PickRandomCheckInDate())
                .ToCheckOut(Calendar.PickRandomCheckOutDate()).Search();

            //Check if result page is displayed within 60 sec
            Assert.That(HotelResultsPage.IsDisplayed(), Is.True, "Hotel Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(HotelResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the hotel search");

            //Select first room option of the first hotel displayed on the very first result page
            HotelResultsPage.SelectRoom().ByHotelNumber(1).OnlyOneRoomWithAvailableRoom(1).Continue();

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed");

            //Click Booknow button
            ExtrasPage.BookHotel().Continue();

            //Check Payment Page is displayed
            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");

        }

        [Test]
        [Category("Live_Removed")]
        public void ShouldBookFromSupplier_Sol_ForDestination_Benidorm()
        {
            //Enter Hotel only search data
            HomePage.SearchFor().HotelOnly().ToHotelName("Benidorm").FromCheckIn(Calendar.PickRandomCheckInDate())
                .ToCheckOut(Calendar.PickRandomCheckOutDate()).Search();

            //Check if result page is displayed within 60 sec
            Assert.That(HotelResultsPage.IsDisplayed(), Is.True, "Hotel Search Result Page isn't displayed within 60 sec");

            //Check if any hote results are displayed for the search
            Assert.That(HotelResultsPage.AreResultsDisplayed(), Is.True, "No Results are available for the hotel search");

            //check if sol supplier is available from first results page
            Assert.That(HotelResultsPage.ContainsRoomFromSupplier("SOL (PELICANOS OCAS) (A)"), Is.True, "Sol supplier isn't displayed on first result page");

            //Select first room option of the first hotel displayed on the very first result page
            HotelResultsPage.SelectRoom().ByFirstSupplier("SOL (PELICANOS OCAS) (A)").Continue();

            //Check if the extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras page is not displayed");

            //Click Booknow button
            ExtrasPage.BookHotel().Continue();

            //Check Payment Page is displayed
            Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page is not displayed");

        }
    }
}