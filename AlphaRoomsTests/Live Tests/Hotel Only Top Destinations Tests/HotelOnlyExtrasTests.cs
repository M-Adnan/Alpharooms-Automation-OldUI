using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AlphaRooms.AutomationFramework;
using AlphaRooms.AutomationFramework.Tests.Properties;
using AlphaRooms.AutomationFramework.Tests.Utilities;



namespace AlphaRooms.AutomationFramework.Tests.Live_Tests.Hotel_Only_Top_Destinations_Tests
{
    [TestFixture]
    public class HotelOnlyExtrasTests : AlpharoomsTestBase
    {
        private IEnumerable<TestCaseData> GetHotelNames()
        {
            List<TestCaseData> tests = new List<TestCaseData>();
            CSVReader reader = new CSVReader("Test Data\\Top2000HotelNames.csv", true);
            while (reader.Read())
            {
                HotelExtrasTestScript data = new HotelExtrasTestScript()
                {
                    TestName = reader["Test_Name"]
                    , HotelName = reader["Hotel_Name"]
                };
                tests.Add(NUnitModule.CreateTestCaseData(data, data.TestName, ""));
            }
            return tests;
        }

        [Test]
        [TestCaseSource("GetHotelNames")]
        [Category("Live_Removed")]
        public void ShouldSearch_MostPopularRoutes_ForAllMultiCom_Suppliers(HotelExtrasTestScript script)
        {
            //check if homepage is displayed
            Assert.That(HomePage.IsDisplayed(), Is.True, "Homepage isn't displayed");

            //Select united kingdom as the user location
            HomePage.TopPanel.ClickLocation(Location.UnitedKingdom);

            //Enter Hotel only search data
            HomePage.SearchFor().HotelOnly().ToHotelName(script.HotelName).FromCheckIn(Calendar.PickRandomCheckInDate())
                .ToCheckOut(Calendar.PickRandomCheckOutDate()).Search();

            //Check HotelDetailPage is displayed
            Assert.That(HotelDetailPage.IsDisplayed(), Is.True, "Hotel Detail page is not displayed");

            //Select the first available room from the list
            HotelDetailPage.SelectRoom().OnlyOneRoomWithAvailableRoom(1).ContinueAndCapture();

            //check extra page is displayed
            Assert.That(ExtrasPage.IsDisplayed, Is.True, "Extra page is not been displayed");

            //check if carhire is displayed
            Assert.That(ExtrasPage.IsExtraDisplayed(Extras.CarHire), Is.True, "Car Hire is not displayed on extra page");

            //check if Airport Transfer is displayed
            //Assert.That(ExtrasPage.IsVisible(Extras.Airport_Transfer), Is.True, "Airport Transport is not displayed on extra page");

            //Expand Car Hire
            ExtrasPage.ExpandExtraLink(Extras.CarHire);

            //Confirm if any results are available
            Assert.That(ExtrasPage.AreResultsDisplayed(Extras.CarHire), Is.True, "Car Hire results not available by default");

            //Confirm if any results are available
            //Assert.That(ExtrasPage.CheckResultsAvailabilityFor(Extras.Airport_Transfer), Is.True, "Airport Transfer results not available");

            //Assert.That(ExtrasPage.IsVisible(Extras.Airport_Transfer), Is.True, "Airport Transfer is not Visible on extra page");

            //Assert.That(ExtrasPage.IsVisible(Extras.Hold_luggage), Is.True, "Airport Transfer is not Visible on extra page");

            Assert.Pass(script.TestName + " Sucessful");
        }
    }
}
