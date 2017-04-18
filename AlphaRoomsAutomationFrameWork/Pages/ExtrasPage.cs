using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using AlphaRooms.AutomationFramework.Processes.Extras;
using AlphaRooms.AutomationFramework.PageData;
using AlphaRooms.AutomationFramework.Functions;
using AlphaRooms.AutomationFramework.Panels;
using AlphaRooms.AutomationFramework.Selenium;

namespace AlphaRooms.AutomationFramework
{
    public static class ExtrasPage
    {
        private static ExtrasPageData data = new ExtrasPageData();
        public static ExtrasPageData Data { get { return data; } }

        private static TopPanel topPanel = new TopPanel();
        public static TopPanel TopPanel { get { return topPanel; } }

        public static bool IsDisplayed()
        {
            return Driver.Instance.IsElementDisplayedById("extraspage");
        }

        internal static void WaitForLoad()
        {
            ExtrasPage.ResetData();
            ExtrasPage.Data.LoadingTime = NonFunctionalReq.CaptureTime(() => Driver.Instance.FindElementWithTimeout(By.Id("extraspage"), 60, "Extras Page  not loaded within 60 sec"), "Booking Summary page load time is");
        }

        internal static void ResetData()
        {
            ExtrasPage.data = new ExtrasPageData();
            ExtrasPage.topPanel = new TopPanel();
        }

        public static void ClickBookNow()
        {
            Logger.AddClickAction("BookNow");
            Driver.Instance.WaitForAjax();
            Thread.Sleep(1000);
            //find and click the 'Book Now!" button
            IWebElement bookNowBtn = Driver.Instance.FindElement(By.LinkText("Continue"));
            bookNowBtn.Click();

            //capture time that takes to load summary page
            PaymentPage.WaitForLoad();
        }


        public static void ClickBookNowAndCapture()
        {
            Logger.AddClickAction("BookNowAndCapture");
            SaveBasketGUID();
            Driver.Instance.WaitForAjax();
            //find and click the 'Book Now!" button
            IWebElement bookNowBtn = Driver.Instance.FindElement(By.LinkText("Continue"));
            bookNowBtn.Click();

            //capture time that takes to load summary page
            PaymentPage.WaitForLoad();

            //capture screenshot
            //NonFunctionalReq.GetScreenShot("Payment Page");
        }

        public static BookFlightNowStarter BookFlight()
        {
            return new BookFlightNowStarter();
        }

        public static BookNowStarter BookHotel()
        {
            return new BookNowStarter();
        }

        public static BookNowStarter BookFlightAndHotel()
        {
            return new BookNowStarter();
        }

        public static void Save(Information info)
        {
            ReadOnlyCollection<IWebElement> fullResultInfo = Driver.Instance.FindElements(By.CssSelector("div#extraspage.container div.row-fluid"));

            switch (info)
            {
                case Information.Flight:
                    SaveFlightOnlyInformation(fullResultInfo[2]);
                    break;

                case Information.Hotel:
                    CreateRoomArray();
                    SaveHotelOnlyInformation(fullResultInfo[2]);
                    break;

                case Information.FlightAndHotel:
                    SaveFlightOnlyInformation(fullResultInfo[2]);
                    CreateRoomArray();
                    SaveHotelOnlyInformation(fullResultInfo[3]);
                    break;

                case Information.TotalPrice:
                    SaveTotalPrice();
                    break;
            }
        }

        private static void CreateRoomArray()
        {
            int i = 1;
            while (i <= HomePage.Data.Rooms.Count() - 1)
            {
                ExtrasPage.Data.AddRoom();
                i++;
            }
        }

        public static bool Confirm(Information info)
        {
            List<bool> results = new List<bool>();
            switch (info)
            {
                case Information.Flight:
                    results.Add(CompareFlightInfo());
                    break;

                case Information.Hotel:
                    results.Add(CompareHotelInfo());
                    for (int i = 0; i < HomePage.Data.Rooms.Count(); i++)
                    {
                        results.Add(CompareRoomInfo(i));
                    }
                    break;

                case Information.FlightAndHotel:
                    results.Add(CompareFlightInfo());
                    results.Add(CompareHotelInfo());
                    for (int i = 0; i < HomePage.Data.Rooms.Count(); i++)
                    {
                        results.Add(CompareRoomInfo(i));
                    }
                    break;
            }

            return results.Aggregate((i, j) => i && j);
        }

        private static void SaveFlightOnlyInformation(IWebElement fullFlightInfo)
        {
            ReadOnlyCollection<IWebElement> outboundFlightUl = fullFlightInfo.FindElements(By.CssSelector("div.span6 ul[class='inline-list half']"));

            ReadOnlyCollection<IWebElement> outboundFlightLis = outboundFlightUl[0].FindElements(By.TagName("li"));
            ExtrasPage.Data.OutboundDepartureTime = outboundFlightLis[0].Text.Trim();
            ExtrasPage.Data.OutboundAirline = outboundFlightLis[1].Text.Split(' ').First().Trim();
            ExtrasPage.Data.OutboundArrivalTime = outboundFlightLis[2].Text.Split('(').First().Trim();
            ExtrasPage.Data.OutboundJourneyTime = (outboundFlightLis[2].Text.Split('(').Last()).TrimEnd(')').Trim();
            ExtrasPage.Data.OutboundFlightNo = outboundFlightLis[3].Text.Trim();
            ExtrasPage.Data.OutboundJourney = outboundFlightLis[4].Text.Trim();
            string[] OutboundDepartureAiportArray = outboundFlightLis[4].Text.Split('-');
            if (OutboundDepartureAiportArray.Length == 2)
            {
                ExtrasPage.Data.OutboundDepartureAiport = OutboundDepartureAiportArray[0].Trim();
                ExtrasPage.Data.OutboundArrivalAirport = OutboundDepartureAiportArray[1].Trim();
            }
            else
            {
                ExtrasPage.Data.OutboundDepartureAiport = OutboundDepartureAiportArray[0].Trim() + " - " + OutboundDepartureAiportArray[1].Trim();
                ExtrasPage.Data.OutboundArrivalAirport = OutboundDepartureAiportArray[2].Trim();
            }
           
            ReadOnlyCollection<IWebElement> inboundFlightLis = outboundFlightUl[1].FindElements(By.TagName("li"));
            ExtrasPage.Data.InboundDepartureTime = inboundFlightLis[0].Text.Trim();
            ExtrasPage.Data.InboundAirline = inboundFlightLis[1].Text.Split(' ').First().Trim();
            ExtrasPage.Data.InboundArrivalTime = inboundFlightLis[2].Text.Split('(').First().Trim();
            ExtrasPage.Data.InboundJourneyTime = (inboundFlightLis[2].Text.Split('(').Last()).TrimEnd(')').Trim();
            ExtrasPage.Data.InboundFlightNo = inboundFlightLis[3].Text.Trim();
            ExtrasPage.Data.InboundJourney = inboundFlightLis[4].Text.Trim();
            string[] InbounddDepartureAiportArray = inboundFlightLis[4].Text.Split('-');
            if (InbounddDepartureAiportArray.Length == 2)
            {
                ExtrasPage.Data.InbounddDepartureAiport = InbounddDepartureAiportArray[0].Trim();
                ExtrasPage.Data.InboundArrivalAirport = InbounddDepartureAiportArray[1].Trim();
            }
            else
            {
                ExtrasPage.Data.InbounddDepartureAiport = InbounddDepartureAiportArray[0].Trim();
                ExtrasPage.Data.InboundArrivalAirport = InbounddDepartureAiportArray[1].Trim() + " - " + InbounddDepartureAiportArray[2].Trim();
            }
        }

        private static void SaveHotelOnlyInformation(IWebElement fullFlightInfo)
        {
            ExtrasPage.Data.HotelName = fullFlightInfo.FindElement(By.CssSelector("div.span12 h3")).Text.Trim();
            var hotelInfoDetail = fullFlightInfo.FindElement(By.CssSelector("div.row-fluid div.span5"));
            SaveHotelDetails(hotelInfoDetail);
            var roomsDetail = fullFlightInfo.FindElement(By.CssSelector("div.span7"));              
            ReadOnlyCollection<IWebElement> elements = roomsDetail.FindElements(By.XPath("./p"));
            int i = 0, count = elements.Count / 2;
            while (i < count)
            {
                string custRoomInfo = elements[i * 2].Text;
                //string staffInfo = elements[i * 2 + 1].Text;
                string[] custRoomInfoArray = custRoomInfo.Split(new string[] { "," }, StringSplitOptions.None);
                if (custRoomInfoArray.Count() == 3)
                {
                    ExtrasPage.Data.Rooms[i].Adults = custRoomInfoArray[0].Trim();
                    ExtrasPage.Data.Rooms[i].Children = custRoomInfoArray[1].Trim();
                    ExtrasPage.Data.Rooms[i].RoomType = custRoomInfoArray[2].Split(new string[] { " with " }, StringSplitOptions.None).First().Trim();
                    ExtrasPage.Data.Rooms[i].BoardType = custRoomInfoArray[2].Split(new string[] { " with " }, StringSplitOptions.None).Last().Trim();
                    //SaveHotelStaffInfo(i, staffInfo);
                    i++;
                }
                else
                {
                    ExtrasPage.Data.Rooms[i].Adults = custRoomInfoArray[0].Trim();
                    ExtrasPage.Data.Rooms[i].Children = "0 Children";
                    ExtrasPage.Data.Rooms[i].RoomType = custRoomInfoArray[1].Split(new string[] { " with " }, StringSplitOptions.None).First().Trim();
                    ExtrasPage.Data.Rooms[i].BoardType = custRoomInfoArray[1].Split(new string[] { " with " }, StringSplitOptions.None).Last().Trim();
                    //SaveHotelStaffInfo(i, staffInfo);
                    i++;
                }
            }
        }

        private static void SaveHotelDetails(IWebElement hotelDetail)
        {
            ExtrasPage.Data.HotelAddress = hotelDetail.FindElement(By.TagName("address")).Text;
            ExtrasPage.Data.HotelCheckinDate = hotelDetail.FindElement(By.TagName("p")).Text.Split('-').First().Trim();
            ExtrasPage.Data.HotelCheckoutDate = hotelDetail.FindElement(By.TagName("p")).Text.Split('-').Last().Trim().Split('(').First().Trim();
            ExtrasPage.Data.HotelTotalStay = hotelDetail.FindElement(By.TagName("p")).Text.Split('-').Last().Trim().Split('(').Last().Trim().Split(')').First();
        }
        private static void SaveHotelStaffInfo(int i, string staffInfo)
        {
            string[] staffInfoArray = staffInfo.Split(';');
            ExtrasPage.Data.Rooms[i].Supplier = staffInfoArray[0];
            ExtrasPage.Data.Rooms[i].Cost = staffInfoArray[1];
        }

        private static bool CompareFlightInfo()
        {
            if (FlightResultsPage.Data.OutboundDepartureAirport == ExtrasPage.Data.OutboundDepartureAiport && FlightResultsPage.Data.OutboundDepartureTime == ExtrasPage.Data.OutboundDepartureTime 
                && FlightResultsPage.Data.OutboundFlightNo == ExtrasPage.Data.OutboundFlightNo && FlightResultsPage.Data.OutboundArrivalAirport == ExtrasPage.Data.OutboundArrivalAirport 
                && FlightResultsPage.Data.OutboundArrivalTime == ExtrasPage.Data.OutboundArrivalTime && FlightResultsPage.Data.OutboundJourneyTime == ExtrasPage.Data.OutboundJourneyTime
                && FlightResultsPage.Data.OutboundAirline == ExtrasPage.Data.OutboundAirline
                
                && FlightResultsPage.Data.InboundDepartureAirport == ExtrasPage.Data.InbounddDepartureAiport && FlightResultsPage.Data.InboundDepartureTime == ExtrasPage.Data.InboundDepartureTime 
                && FlightResultsPage.Data.InboundFlightNo == ExtrasPage.Data.InboundFlightNo && FlightResultsPage.Data.InboundArrivalAirport == ExtrasPage.Data.InboundArrivalAirport 
                && FlightResultsPage.Data.InboundArrivalTime == ExtrasPage.Data.InboundArrivalTime && FlightResultsPage.Data.InboundJourneyTime == ExtrasPage.Data.InboundJourneyTime
                && FlightResultsPage.Data.InboundAirline == ExtrasPage.Data.InboundAirline)
                return true;
            return false;
        }

        private static bool CompareHotelInfo()
        {
            if ((string.Equals(HotelResultsPage.Data.HotelName, ExtrasPage.Data.HotelName, StringComparison.OrdinalIgnoreCase)))
                return true;
            return false;
        }

        private static bool CompareRoomInfo(int index)
        {
            if ((string.Equals(HomePage.Data.Rooms[index].AdultsLabel, ExtrasPage.Data.Rooms[index].Adults, StringComparison.OrdinalIgnoreCase)) && (string.Equals(HomePage.Data.Rooms[index].ChildrenLabel, ExtrasPage.Data.Rooms[index].Children, StringComparison.OrdinalIgnoreCase)) &&
                (string.Equals(HotelResultsPage.Data.Rooms[index].RoomType, ExtrasPage.Data.Rooms[index].RoomType, StringComparison.OrdinalIgnoreCase)) && (string.Equals(HotelResultsPage.Data.Rooms[index].BoardType, ExtrasPage.Data.Rooms[index].BoardType, StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }
            return false;
        }

        public static bool IsExtraDisplayed(Extras extraName)
        {
            switch (extraName)
            {
                case Extras.HoldLuggage:

                    try
                    {
                        Driver.Instance.FindElementWithTimeout(By.CssSelector("div#flightBaggageExtrasList[class='extra visible']"),0,"Flight Baggage is not displayed");
                        return true;
                    }
                    catch
                    {
                        return false;
                    }

                case Extras.FlightExtra:

                    try
                    {
                        Driver.Instance.FindElementWithTimeout(By.CssSelector("div#flightExtrasList[class='extra visible']"), 0, "Flight Extra is not displayed");
                        return true;
                    }
                    catch
                    {
                        return false;
                    }

                case Extras.AirportTransfer:

                    try
                    {
                        Driver.Instance.FindElementWithTimeout(By.CssSelector("div#extras-transfers[class='extra visible']"), 0, "Airport Transfer Extra is not displayed");
                        return true;
                    }
                    catch
                    {
                        return false;
                    }

                case Extras.CarHire:

                    try
                    {
                        Driver.Instance.FindElementWithTimeout(By.CssSelector("div#extras-carhire[class='extra visible']"), 0, "Car Hire Transfer Extra is not displayed");
                        return true;
                    }
                    catch
                    {
                        return false;
                    }

                case Extras.AirportParking:

                    try
                    {
                        Driver.Instance.FindElementWithTimeout(By.CssSelector("div#extras-airportparking[class='extra visible']"), 0, "Airport Parking Transfer Extra is not displayed");
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
            }

            return false;
        }

        public static void ExpandExtraLink(Extras extras)
        {
            Logger.AddClickAction("ExtraLink", "Extra", extras);
            switch (extras)
            {
                case Extras.HoldLuggage:
                    Driver.Instance.FindElementWithTimeout(By.CssSelector("h3.baggage"), 0, "Hold Luggage extra is not avaialable").Click();
                    Driver.Instance.WaitForAjax();
                    break;

                case Extras.FlightExtra:
                    Driver.Instance.FindElementWithTimeout(By.CssSelector("h3.food"), 0, "Flight extra is not avaialable").Click();
                    Driver.Instance.WaitForAjax();
                    break;

                case Extras.AirportTransfer:
                    Driver.Instance.FindElementWithTimeout(By.CssSelector("h3.plane"), 0, "Airport extra is not avaialable").Click();
                    Driver.Instance.WaitForAjax();
                    break;

                case Extras.CarHire:
                    Driver.Instance.FindElementWithTimeout(By.CssSelector("h3.road"), 0, "Car Hire extra is not avaialable").Click();
                    //Driver.Instance.WaitForAjax();
                    break;

                case Extras.AirportParking:
                    Driver.Instance.FindElementWithTimeout(By.CssSelector("h3.ticket"), 0, "Aiport parking extra is not avaialable").Click();
                    Driver.Instance.WaitForAjax();
                    break;
            }
        }

        public static bool VerifyHoldLuggagePrice()
        {
            IWebElement holdluggage = Driver.Instance.FindElement(By.Id("flightBaggageExtrasList"));
            var holdluggageSelectQyt = holdluggage.FindElement(By.CssSelector("select.selectpicker"));
            var selectLuggageQyt = new SelectElement(holdluggageSelectQyt);
            string luggageQytText = selectLuggageQyt.SelectedOption.Text;
            int luggageQyt = Convert.ToInt16(luggageQytText);
            ReadOnlyCollection<IWebElement> tdleHoldLuggage = holdluggage.FindElements(By.CssSelector("td[class = 'extra-price']"));
            decimal PerPersonPrice = Convert.ToDecimal(tdleHoldLuggage[0].Text.Replace('£', ' ').Trim());
            decimal TotalPrice = Convert.ToDecimal(tdleHoldLuggage[1].Text.Replace('£', ' ').Trim());

            if (luggageQyt * PerPersonPrice == TotalPrice)
            {
                var spamTotalCalPrice = holdluggage.FindElement(By.CssSelector("span[class = 'total-price pull-right']"));
                decimal TotalCalPrice = Convert.ToDecimal(spamTotalCalPrice.Text.Replace('£', ' ').Trim());
                if (TotalPrice == TotalCalPrice)
                    return true;
                else
                    return false;
                //throw new Exception("Hold Luggage total price in the table doesn't match with the Hold luggage total price in the table footer");
            }
            else
                return false;
            //throw new Exception("Hold luggage unit price and total price donot add up");            
        }

        public static void SaveHoldLuggagePrice()
        {
            IWebElement holdluggage = Driver.Instance.FindElement(By.Id("flightBaggageExtrasList"));
            var spamTotalCalPrice = holdluggage.FindElement(By.CssSelector("span[class = 'total-price pull-right']"));
            ExtrasPage.Data.flightHoldLuggagePrice = Convert.ToDecimal(spamTotalCalPrice.Text.Replace('£', ' ').Trim());
        }

        public static void CheckFlightExtrasOption(int optionnumber)
        {
            Logger.AddClickAction("FlightExtrasOption", "Option", optionnumber);
            IWebElement holdluggage = Driver.Instance.FindElement(By.Id("flightBaggageExtrasList"));
            var spamTotalCalPrice = holdluggage.FindElement(By.CssSelector("span[class = 'total-price pull-right']"));
            ExtrasPage.Data.flightHoldLuggagePrice = Convert.ToDecimal(spamTotalCalPrice.Text.Replace('£', ' ').Trim());
        }

        public static void CheckHoldLuggage()
        {
            Logger.AddCheckAction("HoldLuggage");
            IWebElement holdluggage = Driver.Instance.FindElementWithTimeout(By.Id("flightBaggageExtrasList"), 0, "Hold Luggage extra is not avaialable");
            var holdLuggageCheckBox = holdluggage.FindElement(By.CssSelector("button.check-box"));
            holdLuggageCheckBox.Click();
        }

        public static void SelectHoldLuggagePassengers(int quantity)
        {
            Logger.AddCheckAction("HoldLuggagePassengers", quantity);
            IWebElement holdluggage = Driver.Instance.FindElementWithTimeout(By.Id("flightBaggageExtrasList"), 0, "Hold Luggage extra is not avaialable");
            var holdluggageSelectQyt = holdluggage.FindElement(By.CssSelector("select.selectpicker"));
            var selectLuggageQyt = new SelectElement(holdluggageSelectQyt);
            selectLuggageQyt.SelectByIndex(quantity);
        }

        public static void CheckFlightExtraNumber(int extraNo)
        {
            Logger.AddCheckAction("FlightExtraNumber", extraNo);
            IWebElement flightExtras = Driver.Instance.FindElement(By.Id("flightExtrasList"));
            ReadOnlyCollection<IWebElement> checkboxesFlightExtras = flightExtras.FindElements(By.CssSelector("button[class = 'check-box fixed']"));
            checkboxesFlightExtras[extraNo - 1].Click();
        }

        public static void SelectFlightExtraPassengers(int extraNo, int quantity)
        {
            Logger.AddSelectAction("FlightExtraPassengers", "ExtraNumber", extraNo, "Quantity", quantity);
            IWebElement flightExtras = Driver.Instance.FindElement(By.Id("flightExtrasList"));
            ReadOnlyCollection<IWebElement> qytDropdownFlightExtras = flightExtras.FindElements(By.CssSelector("select[class = 'selectpicker combobox mini']"));
            var selectFlightExtrasQyt = new SelectElement(qytDropdownFlightExtras[extraNo - 1]);
            selectFlightExtrasQyt.SelectByIndex(quantity);
        }

        public static void SelectCarHirePickupLocation(string location)
        {
            Logger.AddSelectAction("CarHirePickupLocation", location);
            var carHireLocation = Driver.Instance.FindElement(By.CssSelector("button[data-id = 'CarHirePickupLocationViewModel']"));
            var ulCarLocation = carHireLocation.FindElement(By.XPath("../ul"));
            ReadOnlyCollection<IWebElement> lihCarLocation = ulCarLocation.FindElements(By.XPath("li"));
            int counter = 0;

            foreach (IWebElement li in lihCarLocation)
            {
                var spanlocationName = li.FindElement(By.CssSelector("a span.text"));

                if (spanlocationName.GetAttribute("innerHTML") == location)
                {
                    carHireLocation.Click();
                    li.FindElement(By.TagName("a")).Click();
                    break;

                }
                counter++;

                if (counter == lihCarLocation.Count)
                {
                    throw new Exception("Entered CarHire location isn't available from carhire drop down list");
                    //Random r = new Random();
                    //int index = r.Next(1, lihotelLocation.Count);
                    //hotelLocation.Click();
                    //lihotelLocation[index].Click();
                }
            }
        }

        public static void SelectCarHireMainDriverAge(int age)
        {
            Logger.AddSelectAction("CarHireMainDriverAge", age);
            var carHireDriverAge = Driver.Instance.FindElement(By.CssSelector("button[data-id = 'DriverAge']"));
            var ulCarDriverAge = carHireDriverAge.FindElement(By.XPath("../ul"));
            ReadOnlyCollection<IWebElement> lihCarDriverAge = ulCarDriverAge.FindElements(By.XPath("li"));
            int counter = 0;

            foreach (IWebElement li in lihCarDriverAge)
            {
                var spanlocationName = li.FindElement(By.CssSelector("a span.text"));

                if (spanlocationName.GetAttribute("innerHTML") == Convert.ToString(age))
                {
                    carHireDriverAge.Click();
                    li.FindElement(By.TagName("a")).Click();
                    break;

                }
                counter++;

                if (counter == lihCarDriverAge.Count)
                {
                    throw new Exception("Entered Driver Agaeisn't available from age drop down list");
                    //Random r = new Random();
                    //int index = r.Next(1, lihotelLocation.Count);
                    //hotelLocation.Click();
                    //lihotelLocation[index].Click();
                }
            }
        }

        public static void SelectCarHirePickupTime(string pickupTime)
        {
            Logger.AddSelectAction("CarHirePickupTime", pickupTime);
            IWebElement extrasCarhire = Driver.Instance.FindElement(By.Id("extras-carhire"));
            var carHirePickupTime = extrasCarhire.FindElement(By.CssSelector("button[data-id = 'PickupTime']"));
            var ulCarPickupTime = carHirePickupTime.FindElement(By.XPath("../ul"));
            ReadOnlyCollection<IWebElement> lihCarPickupTime = ulCarPickupTime.FindElements(By.XPath("li"));
            int counter = 0;

            foreach (IWebElement li in lihCarPickupTime)
            {
                var spanlocationName = li.FindElement(By.CssSelector("a span.text"));

                if (spanlocationName.GetAttribute("innerHTML") == pickupTime)
                {
                    carHirePickupTime.Click();
                    li.FindElement(By.TagName("a")).Click();
                    break;

                }
                counter++;

                if (counter == lihCarPickupTime.Count)
                {
                    throw new Exception("Entered CarHirePickup Time isn't available from time drop down list");
                    //Random r = new Random();
                    //int index = r.Next(1, lihotelLocation.Count);
                    //hotelLocation.Click();
                    //lihotelLocation[index].Click();
                }
            }
        }

        public static void SelectCarHireDropoffTime(string dropoffTime)
        {
            Logger.AddSelectAction("CarHireDropoffTime", dropoffTime);
            IWebElement extrasCarhire = Driver.Instance.FindElement(By.Id("extras-carhire"));
            var carHireDropoffTime = extrasCarhire.FindElement(By.CssSelector("button[data-id = 'DropoffTime']"));
            var ulCarDropoffTime = carHireDropoffTime.FindElement(By.XPath("../ul"));
            ReadOnlyCollection<IWebElement> lihCarDropOffTime = ulCarDropoffTime.FindElements(By.XPath("li"));
            int counter = 0;

            foreach (IWebElement li in lihCarDropOffTime)
            {
                var spanlocationName = li.FindElement(By.CssSelector("a span.text"));

                if (spanlocationName.GetAttribute("innerHTML") == dropoffTime)
                {
                    carHireDropoffTime.Click();
                    li.FindElement(By.TagName("a")).Click();
                    break;

                }
                counter++;

                if (counter == lihCarDropOffTime.Count)
                {
                    throw new Exception("Entered Carhire Dropoff Time isn't available from time drop down list");
                    //Random r = new Random();
                    //int index = r.Next(1, lihotelLocation.Count);
                    //hotelLocation.Click();
                    //lihotelLocation[index].Click();
                }
            }
        }

        public static void ClickCarHireUpdate()
        {
            Logger.AddClickAction("CarHireUpdate");
            var updateButton = Driver.Instance.FindElement(By.LinkText("Update"));
            updateButton.Click();
            Driver.Instance.WaitForAjax();
        }

        public static void CheckCarHireNumber(int carOption)
        {
            Logger.AddCheckAction("CarHireNumber", carOption);
            Driver.Instance.WaitForAjax();
            var airportCarHireResults = Driver.Instance.FindElement(By.CssSelector("tbody[data-bind = 'foreach: availableCarList']"));
            ReadOnlyCollection<IWebElement> selectableOptions = airportCarHireResults.FindElements(By.CssSelector("button[class = 'radio-button']"));
            selectableOptions[(Convert.ToInt16(carOption) - 1)].Click();
        }

        public static void SelectAirportParkingDropoffTime(string dropoffTime)
        {
            Logger.AddSelectAction("AirportParkingDropoffTime", dropoffTime);
            IWebElement extrasAirportparking = Driver.Instance.FindElement(By.Id("extras-airportparking"));
            var AirportDropoffTime = extrasAirportparking.FindElement(By.CssSelector("button[data-id = 'DropoffTime']"));
            var ulAirportDropoffTime = AirportDropoffTime.FindElement(By.XPath("../ul"));
            ReadOnlyCollection<IWebElement> lihAirportDropOffTime = ulAirportDropoffTime.FindElements(By.XPath("li"));
            int counter = 0;

            foreach (IWebElement li in lihAirportDropOffTime)
            {
                var spanlocationName = li.FindElement(By.CssSelector("a span.text"));

                if (spanlocationName.GetAttribute("innerHTML") == dropoffTime)
                {
                    AirportDropoffTime.Click();
                    li.FindElement(By.TagName("a")).Click();
                    break;

                }
                counter++;

                if (counter == lihAirportDropOffTime.Count)
                {
                    throw new Exception("Entered Carhire Dropoff Time isn't available from time drop down list");
                    //Random r = new Random();
                    //int index = r.Next(1, lihotelLocation.Count);
                    //hotelLocation.Click();
                    //lihotelLocation[index].Click();
                }
            }
        }

        public static void SelectAirportParkingPickupTime(string pickupTime)
        {
            Logger.AddSelectAction("AirportParkingPickupTime", pickupTime);
            IWebElement extrasAirportparking = Driver.Instance.FindElement(By.Id("extras-airportparking"));
            var AirportpickupTime = extrasAirportparking.FindElement(By.CssSelector("button[data-id = 'PickupTime']"));
            var ulAirportpickupTime = AirportpickupTime.FindElement(By.XPath("../ul"));
            ReadOnlyCollection<IWebElement> lihAirportpickupTime = ulAirportpickupTime.FindElements(By.XPath("li"));
            int counter = 0;

            foreach (IWebElement li in lihAirportpickupTime)
            {
                var spanlocationName = li.FindElement(By.CssSelector("a span.text"));

                if (spanlocationName.GetAttribute("innerHTML") == pickupTime)
                {
                    AirportpickupTime.Click();
                    li.FindElement(By.TagName("a")).Click();
                    break;

                }
                counter++;

                if (counter == lihAirportpickupTime.Count)
                {
                    throw new Exception("Entered Carhire Dropoff Time isn't available from time drop down list");
                    //Random r = new Random();
                    //int index = r.Next(1, lihotelLocation.Count);
                    //hotelLocation.Click();
                    //lihotelLocation[index].Click();
                }
            }
        }

        public static void ClickAirportParkingUpdate()
        {
            Logger.AddClickAction("AirportParkingUpdate");
            var updateButton = Driver.Instance.FindElement(By.LinkText("Update"));
            updateButton.Click();
            Driver.Instance.WaitForAjax();
        }

        public static void CheckAirportParkingNumber(int parkingOption)
        {
            Logger.AddCheckAction("AirportParkingNumber", parkingOption);
            Driver.Instance.WaitForAjax();
            var airportParkingResults = Driver.Instance.FindElement(By.CssSelector("tbody[data-bind = 'foreach: { data: availableAirportParkingList, afterAdd: showResults }"));
            ReadOnlyCollection<IWebElement> selectableOptions = airportParkingResults.FindElements(By.CssSelector("button[class = 'radio-button']"));
            selectableOptions[(parkingOption - 1)].Click();
        }

        public static void SelectAirportTransferHotelLocation(string locationName)
        {
            Logger.AddSelectAction("AirportTransferHotelLocation", locationName);
            var hotelLocation = Driver.Instance.FindElement(By.CssSelector("button[data-id = 'DestinationID']"));
            var ulhotelLocation = hotelLocation.FindElement(By.XPath("../ul"));
            ReadOnlyCollection<IWebElement> lihotelLocation = ulhotelLocation.FindElements(By.XPath("li"));
            int counter = 0;

            foreach (IWebElement li in lihotelLocation)
            {
                var spanlocationName = li.FindElement(By.CssSelector("a span.text"));

                if (spanlocationName.GetAttribute("innerHTML") == locationName)
                {
                    hotelLocation.Click();
                    li.FindElement(By.TagName("a")).Click();
                    break;

                }
                counter++;

                if (counter == lihotelLocation.Count)
                {
                    throw new Exception("Entered Hotel location isn't available from hotel drop down list");
                    //Random r = new Random();
                    //int index = r.Next(1, lihotelLocation.Count);
                    //hotelLocation.Click();
                    //lihotelLocation[index].Click();
                }
            }
        }

        public static void TypeAirportTransferHotel(string hotel)
        {
            Logger.AddTypeAction("AirportTransferHotel", hotel);
            //Enter destination in destination text field
            var hotelName = Driver.Instance.FindElement(By.CssSelector("input[class = 'standard-margin-right ui-autocomplete-input']"));
            if (!TypeAndWaitForHotelName(hotelName, hotel, false))
            {
                while (!TypeAndWaitForHotelName(hotelName, hotel, true))
                {
                }
            }
            Driver.Instance.WaitForAjax();
        }

        public static void ClickAirportTransterUpdate()
        {
            Logger.AddClickAction("AirportTransterUpdate");
            var updateButton = Driver.Instance.FindElement(By.LinkText("Update"));
            updateButton.Click();
            Driver.Instance.WaitForAjax();
        }

        private static bool TypeAndWaitForHotelName(IWebElement hotelInput, string hotel, bool useBackspace)
        {
            hotelInput.Clear();
            hotelInput.SendKeys(hotel);
            if (useBackspace) hotelInput.SendKeys(Keys.Backspace);
            int count = 0;
            do
            {
                //Thread.Sleep(1000);
                Driver.Instance.WaitForAjax();
                if (count == 3) return false;
                count++;
            } while (!Driver.Instance.FindElement(By.CssSelector("ul[class ='typeahead dropdown-menu'")).Displayed);
            hotelInput.SendKeys(Keys.Enter);
            return true;
        }

        public static void CheckTransferNumber(int tranferOption)
        {
            Logger.AddCheckAction("TransferNumber", tranferOption);
            var airportTransferResults = Driver.Instance.FindElement(By.CssSelector("tbody[data-bind = 'foreach: availableTransferList']"));
            ReadOnlyCollection<IWebElement> selectableOptions = airportTransferResults.FindElements(By.CssSelector("button[class = 'radio-button']"));
            selectableOptions[(Convert.ToInt16(tranferOption) - 1)].Click();
        }

        private static void SaveBasketGUID()
        {
            string url = Driver.Instance.Url;
            string basketID = url.Split(new char[] { '=', '&' })[1];
            ExtrasPage.Data.BasketGUID = basketID;
        }

        public static void SelectAirportTransferAirport(string p)
        {
            Logger.AddSelectAction("AirportTransferAirport", p);
            throw new NotImplementedException();
        }

        public static void SelectAirportParkingDepartureAirport(string p)
        {
            Logger.AddSelectAction("AirportParkingDepartureAirport", p);
            throw new NotImplementedException();
        }

        public static bool ValidatePrice()
        {
            try
            {
                IWebElement priceChangeTag = Driver.Instance.FindElement(By.CssSelector("div[class='extras-price padded-row border-bottom price visible']"));
                IWebElement priceChangeElement = priceChangeTag.FindElement(By.CssSelector("div.row-fluid span[class ='pull-right']"));
                String priceChangeMessage = priceChangeTag.Text;

                if (priceChangeTag.Displayed && !(priceChangeMessage.Length < 50))
                {
                    switch (HomePage.Data.SiteLocation)
                    {
                        case Location.Ireland:
                            return false;
                        case Location.UnitedKingdom:
                            return validateFlightPrice('£', priceChangeMessage);
                        case Location.UnitedStates:
                            return false;
                        default:
                            return false;
                    }
                }
                else
                    throw new Exception("Price has not been changed");
            }
            catch (Exception e)
            {
                SaveTotalPrice();

                if (Convert.ToInt16(FlightResultsPage.Data.Price * (HomePage.Data.Rooms[0].Adults + HomePage.Data.Rooms[0].Children.GetValueOrDefault())) == Convert.ToInt16(ExtrasPage.Data.TotalPrice))
                    return true;
                return false;
            }
        }

            private static bool validateFlightPrice(char currencySymbol, string priceChangeMessage)
            {
                string splitWord = " to ";
                int start = priceChangeMessage.IndexOf(currencySymbol) + 1;
                int end = priceChangeMessage.IndexOf(splitWord, start);
                ExtrasPage.Data.OrignalPrice = Decimal.Parse(priceChangeMessage.Substring(start, end - start));
                int start1 = priceChangeMessage.IndexOf(currencySymbol, end)+1;
                    //priceChangeMessage.IndexOf(splitWord,end);
                ExtrasPage.Data.ChangedPrice = Decimal.Parse(priceChangeMessage.Substring(start1, priceChangeMessage.Length - start1));
                            
                if (Convert.ToInt16((FlightResultsPage.Data.Price * (HomePage.Data.Rooms[0].Adults + HomePage.Data.Rooms[0].Children.GetValueOrDefault()))) == Convert.ToInt16(ExtrasPage.Data.OrignalPrice)
                    && (FlightResultsPage.Data.Price * (HomePage.Data.Rooms[0].Adults + HomePage.Data.Rooms[0].Children.GetValueOrDefault()) - ExtrasPage.Data.ChangedPrice < 50 ||
                    FlightResultsPage.Data.Price * (HomePage.Data.Rooms[0].Adults + HomePage.Data.Rooms[0].Children.GetValueOrDefault()) - ExtrasPage.Data.ChangedPrice > 50))
                    return true;
                return false;
            }

        public static void SaveTotalPrice()
        {
            IWebElement totalPriceElement = Driver.Instance.FindElement(By.CssSelector("div[class='extras-price padded-row border-bottom total-price visible']"));
            IWebElement totalPriceTag = totalPriceElement.FindElement(By.CssSelector("div.row-fluid span.pull-right"));
            ExtrasPage.Data.TotalPrice = Decimal.Parse(totalPriceTag.Text.Remove(0, 1));
        }

        public static bool AreResultsDisplayed(Extras extras)
        {
            switch (extras)
            {
                case Extras.HoldLuggage:
                    try
                    {
                        Driver.Instance.FindElementWithTimeout(By.CssSelector("div#extras-transfers.extra div#extrasTransfers.collapse div.results table[style='']"), 0, "No results displayed for Airport extra").Click();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }

                case Extras.FlightExtra:
                    try
                    {
                        Driver.Instance.FindElementWithTimeout(By.CssSelector("div#extras-transfers.extra div#extrasTransfers.collapse div.results table[style='']"), 0, "No results displayed for Airport extra").Click();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }

                case Extras.AirportTransfer:
                    try
                    {
                        Driver.Instance.FindElementWithTimeout(By.CssSelector("div#extras-transfers.extra div#extrasTransfers.collapse div.results table[style='']"), 0, "No results were available for Airport Transfer extra").Click();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }

                case Extras.CarHire:           
                        try
                        {
                            Driver.Instance.FindElementWithTimeout(By.CssSelector("div#extras-carhire.extra div#extrasCarHire.in div.results p[style='']"), 0, "Car Hire extra is not avaialable");
                            return false;                                                  
                            
                        }
                        catch(Exception)
                        {
                            NonFunctionalReq.CaptureTime(() => Driver.Instance.FindElementWithTimeout(By.CssSelector("div#extrasCarHire.in div.results table[style='']"), 30, "No results were available for Car Hire extra in 30 secs"), "Car Hire load time is");
                            return true;
                            
                        }                

                case Extras.AirportParking:
                    try
                    {
                        Driver.Instance.FindElementWithTimeout(By.CssSelector("div#extrasAirportParking.in div.results table[style='']"), 0, "No results were available for Airport Parking extra");
                        return true;
                    }
                    catch
                    {
                        return false;
                    }

                default:
                    return false;
            }
        }
    }
}