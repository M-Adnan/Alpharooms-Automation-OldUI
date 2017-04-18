using System;
using NUnit.Framework;
using AlphaRooms.AutomationFramework;

namespace AlphaRooms.AutomationFramework.Tests.WIP
{
    //[TestFixture]
    //class TestClass:AlpharoomsTestBase
    //{
    //    [Test]
    //    [Category("TestingTests")]
    //    public static void FrameWorkIntroduction()
    //    {
    //        HomePage.ClickFlightOnly();
    //        HomePage.TypeFlightDestination("Spain");
    //        HomePage.SelectCheckIn("23/08/2014");
    //        HomePage.SelectCheckOut("05/09/2014");
    //        HomePage.SelectAdults(3);
    //        HomePage.SelectChildren(2, new int[] {2,3});
    //        //HomePage.ClickAddAnotherRoom();
    //        HomePage.ClickSearchAndCapture();
            
    //    }

    //    [Test]
    //    [Category("TestingTests")]
    //    public static void FrameWorkIntroduction1()
    //    {
    //        HomePage.ClickFlightOnly();
    //        HomePage.TypeFlightDestination("Spain");
    //        HomePage.SelectCheckIn("23/08/2014");
    //        HomePage.SelectCheckOut("05/09/2014");
    //        HomePage.SelectAdults(3);
    //        HomePage.SelectChildren(2, new int[] { 2, 3 });
    //        //HomePage.ClickAddAnotherRoom();
    //        HomePage.ClickSearchAndCapture();

    //    }

    //    [Test]
    //    [Category("Live_Removed")]
    //    public void testingtest()
    //    {
    //        //HomePage.ClickFlightOnly();
    //        //HomePage.TypeFlightDestination("Benidorm");
    //        //HomePage.SelectCheckIn("01/06/2014");
    //        //HomePage.SelectCheckOut("08/06/2014");
    //        //HomePage.SelectAirport("London (All Airports), London, United Kingdom (LON)");
    //        //HomePage.SelectAdults(2);
    //        //HomePage.SelectChildren(2, new int[] {2,3});
    //        //HomePage.ClickSearch();

    //        //HomePage.SearchFor().FlightOnly().ToDestination("Benidorm").FromCheckIn("01/09/2014")
    //        //    .ToCheckOut("08/09/2014").FromDepartureAirport("London (All Airports), London, United Kingdom (LON)")
    //        //    .ForAdults(2).WithChildren(2).OfAges(2, 3).Search();

    //        HomePage.SearchFor().HotelOnly().ToDestination("Barcelona, Spain").FromCheckIn("01/06/2014")
    //            .ToCheckOut("15/06/2014").ForAdults(2).AddAnotherRoom().ForAdults(3).Search();

    //        Assert.That(HotelResultsPage.IsDisplayed(), Is.True, "Hotel result page is not displayed");

    //        HotelResultsPage.SelectRoom().ByHotelNumber(3).ForRoomNumber(1).WithAvailableRoom(1).ForRoomNumber(2).WithAvailableRoom(1).Continue();

    //        Assert.That(ExtrasPage.IsDisplayed(), Is.True, "Extras Page not showing");

    //        ExtrasPage.Save(Information.Hotel);

    //        Assert.That(HotelResultsPage.Data.HotelName == ExtrasPage.Data.HotelName, Is.True, "Differing hotel names from SR to Extras");

    //        //ExtrasPage.BookHotel().AddAirportTransfer().
    //        //ExtrasPage.Confirm(Information.Hotel);

    //        //Assert.That(HotelResultsPage.Data.Rooms[0].RoomPrice+==ExtrasPage.Data.

    //        ExtrasPage.BookHotel().Continue();

    //        Assert.That(PaymentPage.IsDisplayed(), Is.True, "Payment page not displaying");

    //        PaymentPage.Save(Information.Hotel);

    //        Assert.That(ExtrasPage.Data.HotelName == PaymentPage.Data.HotelName, Is.True, "Hotel name differs from Extras to Payment");

    //        /*PaymentPage.MakeAHotelBooking().ForAllGuestDetails().AutoFill().ForContactDetails().AutoFill()
    //            .ForPaymentDetails().AutoFill().Confirm();*/


    //        PaymentPage.MakeAHotelBooking().ForGuestDetailsNumber(1).OfRoomNo(1).AutoFill().ForGuestDetailsNumber(2).OfRoomNo(1).AutoFill().ForContactDetails().AutoFill()
    //            .ForPaymentDetails().AutoFill().Confirm();
           
    //    }


    //}
}
