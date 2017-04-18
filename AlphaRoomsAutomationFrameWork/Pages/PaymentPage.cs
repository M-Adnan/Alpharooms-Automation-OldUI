using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using AlphaRooms.AutomationFramework;
using AlphaRooms.AutomationFramework.PageData;
using AlphaRooms.AutomationFramework.Functions;
using AlphaRooms.AutomationFramework.Panels;
using AlphaRooms.AutomationFramework.Processes.Payment;
using AlphaRooms.AutomationFramework.Selenium;

namespace AlphaRooms.AutomationFramework
{
    public static class PaymentPage
    {
        private static PaymentPageData data = new PaymentPageData();
        public static PaymentPageData Data { get { return data; } }

        private static TopPanel topPanel = new TopPanel();
        public static TopPanel TopPanel { get { return topPanel; } }

        public static bool IsDisplayed()
        {
            return Driver.Instance.IsElementDisplayedById("paymentpage");
        }

        internal static void WaitForLoad()
        {
            PaymentPage.ResetData();
            NonFunctionalReq.CaptureTime(() => Driver.Instance.FindElementWithTimeout(By.Id("submitPaymentButton"), 60, "Payment Page  not loaded within 60 sec"), "Payment page load time is");
        }

        internal static void ResetData()
        {
            PaymentPage.data = new PaymentPageData();
            PaymentPage.topPanel = new TopPanel();
        }
        
        public static void SelectGuestTitle(int guestNo, Title title)
        {
            Logger.AddSelectAction("GuestTitle", "GuestNumber", guestNo, "Title", title);
            SelectGuestTitle(1, guestNo, title);
        }

        public static void SelectGuestTitle(int roomNo, int guestNo, Title title)
        {
            Logger.AddSelectAction("GuestTitle", "RoomNumber", roomNo, "GuestNumber", guestNo, "Title", title);
            //Select a random title
            var titleInput = Driver.Instance.FindElement(By.Id(String.Format("BookingExtraDetails_People_{0}__{1}__Title",roomNo-1,guestNo-1)));
            var titleDiv = titleInput.FindElement(By.XPath(".."));
            SelectElement dropDown = new SelectElement(titleDiv.FindElement(By.Id("title")));
            dropDown.SelectByText(title.ToString());
        }

        public static void TypeGuestFirstName(int guestNo, string value)
        {
            Logger.AddTypeAction("GuestFirstName", "GuestNumber", guestNo, "FirstName", value);
            TypeGuestFirstName(1, guestNo, value);
        }

        public static void TypeGuestFirstName(int roomNo, int guestNo, string value)
        {
            Logger.AddTypeAction("GuestFirstName", "RoomNumber", roomNo, "GuestNumber", guestNo, "FirstName", value);
            IWebElement firstnameTxtField = Driver.Instance.FindElement(By.Id(string.Format("BookingExtraDetails_People_{0}__{1}__FirstName",roomNo-1,guestNo-1)));
            firstnameTxtField.SendKeys(value);
        }
        
        public static void TypeGuestLastName(int guestNo, string value)
        {
            Logger.AddTypeAction("GuestLastName", "GuestNumber", guestNo, "LastName", value);
            TypeGuestLastName(1, guestNo, value);
        }

        public static void TypeGuestLastName(int roomNo, int guestNo, string value)
        {
            Logger.AddTypeAction("GuestLastName", "RoomNumber", roomNo, "GuestNumber", guestNo, "LastName", value);
            IWebElement lastnameTxtField = Driver.Instance.FindElement(By.Id(string.Format("BookingExtraDetails_People_{0}__{1}__Lastname", roomNo - 1, guestNo - 1)));
            lastnameTxtField.SendKeys(value);
        }

        public static void SelectGuestDoB(int guestNo, string value)
        {
            Logger.AddTypeAction("GuestDoB", "GuestNumber", guestNo, "DoB", value);
            SelectGuestDoB(1, guestNo, value);
        }

        public static void SelectGuestDoB(int roomNo, int guestNo, string value)
        {
            Logger.AddTypeAction("GuestDoB", "RoomNumber", roomNo, "GuestNumber", guestNo, "DoB", value);
            DateTime valueDateTime = DateTime.Parse(value);
            IWebElement inputelementDOB = Driver.Instance.FindElement(By.Id(string.Format("BookingExtraDetails_People_{0}__{1}__DateOfBirthDay",roomNo-1, guestNo-1)));
            IWebElement DOBDropDown = inputelementDOB.FindElement(By.XPath(".."));
            var DOB = DOBDropDown.FindElements(By.TagName("select"));

            //Select random day
            var selectDate = new SelectElement(DOB[0]);
            selectDate.SelectByIndex(valueDateTime.Day);

            //select random month
            var selectMonth = new SelectElement(DOB[1]);
            selectMonth.SelectByIndex(valueDateTime.Month);

            //select random year (Refine this to select year within a particular range)
            var selectYear = new SelectElement(DOB[2]);
            int currentYear = DateTime.Now.Year;
            selectYear.SelectByIndex(currentYear - valueDateTime.Year + 1);
        }

        public static void TypePostCode(string postCode)
        {
            Logger.AddTypeAction("PostCode", postCode);
            var postCodeTxt = Driver.Instance.FindElement(By.Id("postcode-lookup"));
            //var findPostCodeBtn = Driver.Instance.FindElement(By.Id("find-address-btn"));

            postCodeTxt.SendKeys(postCode);
            //findPostCodeBtn.Click();
        }


        public static MakeAHotelBookingStarter MakeAHotelBooking()
        {
            return new MakeAHotelBookingStarter();
        }

        public static MakeAFlightBookingStarter MakeAFlightBooking()
        {
            return new MakeAFlightBookingStarter();
        }

        public static MakeAFlightAndHotelBookingStarter MakeAFlightAndHotelBooking()
        {
            return new MakeAFlightAndHotelBookingStarter();
        }

        //public static void CheckAndConfirm()
        //{
        //    Logger.AddTypeAction("PostCode", postCode);
        //    var paymentOptionDiv = Driver.Instance.FindElement(By.CssSelector("div#paymentpage.container form div.row-fluid div.span10"));
        //    var paymentOptionChkBox = paymentOptionDiv.FindElement(By.TagName("span"));
        //    paymentOptionChkBox.Click();
        //    var confirmBtn = Driver.Instance.FindElement(By.Id("submitPaymentButton"));
        //    confirmBtn.Click();
        //}

        //public static void CheckConfirmAndCapture()
        //{
        //    var paymentOptionDiv = Driver.Instance.FindElement(By.CssSelector("div#paymentpage.container form div.row-fluid div.span10"));
        //    var paymentOptionChkBox = paymentOptionDiv.FindElement(By.TagName("span"));
        //    paymentOptionChkBox.Click();


        //    //click search button
        //    var confirmBtn = Driver.Instance.FindElement(By.Id("submitPaymentButton"));
        //    confirmBtn.Click();

        //    if (Check3DSPage())
        //    {}
        //    else if (CheckCardDecline())
        //    {}
        //    else if (CheckConfirmationMsg())
        //    { }
        //    else throw new Exception("None of the following outcomes are presented as a result of the booking (3DS Page, Card Decline or Payment Confirmation)");

        //        NonFunctionalReq.GetScreenShot("Booking Confirmation Page");
        //}

        
        public static bool Validate3DSPage()
        {
            try
            {
                //capture the page load time
                NonFunctionalReq.CaptureTime(() => Driver.Instance.FindElementWithTimeout(By.Id("threedspage"), 40, "3Ds Page is not loaded within 60 Sec"), "3DS Page load time is");

                Driver.Instance.SwitchTo().Frame("iframe3DS");

                return true;
                //Driver.Instance.FindElement(By.Name("continue")).Click();

            }
            catch { return false; } 
        }

        private static bool ValidateCardDecline()
        {
            try
            {
                var cardDecline = Driver.Instance.FindElement(By.CssSelector("div.row-fluid div.span12 div.alert div.row-fluid div.span12 strong"));
                if (cardDecline.Displayed)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
            
        }

        public static void Save(Information info)
        {
             
             ReadOnlyCollection<IWebElement> fullBookingInfo = Driver.Instance.FindElements(By.CssSelector("div.span6 div.box"));
            

            switch (info)
            {
                case Information.Flight:
                    SaveFlightOnlyInformation(fullBookingInfo[0]);
                    break;

                case Information.Hotel:
                    SaveHotelInformation(fullBookingInfo[0]);
                    CreateRoomArray();
                    SaveRoomInformation(fullBookingInfo[1]);
                    break;

                case Information.FlightAndHotel:
                    SaveFlightOnlyInformation(fullBookingInfo[0]);
                    SaveHotelInformation(fullBookingInfo[1]);
                    CreateRoomArray();
                    SaveRoomInformation(fullBookingInfo[2]);
                    break;

                case Information.TotalPrice:
                    saveTotalPrice();
                    break;
            }
        }

        private static void saveTotalPrice()
        {
            IWebElement totalPriceTag = Driver.Instance.FindElement(By.CssSelector("div[class='total-price-value span3']"));
            IWebElement totalPriceElement = totalPriceTag.FindElement(By.CssSelector("span.pull-right"));
            PaymentPage.Data.TotalPrice = Decimal.Parse(totalPriceTag.Text.Remove(0, 1));
        }

        private static void CreateRoomArray()
        {
            int i = 1;
            while (i <= HomePage.Data.Rooms.Count() - 1)
            {
                PaymentPage.Data.AddRoom();
                i++;
            }
        }

        private static void SaveHotelInformation(IWebElement HotelInfo)
        {
            IWebElement fullHotelDetail = HotelInfo.FindElement(By.CssSelector("div.span8"));
            ReadOnlyCollection<IWebElement> hotelDetails = fullHotelDetail.FindElements(By.CssSelector("div.row-fluid"));
            PaymentPage.Data.HotelName = hotelDetails[0].FindElement(By.TagName("h3")).Text.Trim();
            PaymentPage.Data.HotelAddress = hotelDetails[0].FindElement(By.TagName("address")).Text.Trim();
            PaymentPage.Data.HotelStay = hotelDetails[1].Text.Trim().Split('(').First().Trim();
            PaymentPage.Data.HotelTotalStay = hotelDetails[1].Text.Trim().Split('(').Last().Trim().Split(')').First().Trim();
            String[] totalPassangers = hotelDetails[2].Text.Split(',');
            if (totalPassangers.Length == 2)
            {
                PaymentPage.Data.TotalAdults = hotelDetails[2].Text.Split(',').First().Trim();
                PaymentPage.Data.TotalChildren = hotelDetails[2].Text.Split(',').Last().Trim();
            }
            else
                PaymentPage.Data.TotalAdults = hotelDetails[2].Text.Split(',').First().Trim();
        }

        private static void SaveRoomInformation(IWebElement RoomInfo)
        {
            
            var roomDetail = RoomInfo.FindElement(By.CssSelector("div.standard-padding div[data-bind ='foreach: paymentNowList()']"));
            //IList<IWebElement> elements = roomDetail.FindElements(By.CssSelector("p[class = 'inline-accordion-header'],p[class = 'staff-info']"));
            IList<IWebElement> elements = roomDetail.FindElements(By.CssSelector("p[class = 'inline-accordion-header']"));
            elements = elements.Where(i => !string.IsNullOrEmpty(i.Text)).ToList();
            //(By.CssSelector("td[class='day'],td[class='day active']"));
            //int j = 0, count = (elements.Count) / 2;       
            int j = 0, count = elements.Count;
            while (j < count)
            {
                //string custRoomInfo = elements[j * 2].Text;
                string custRoomInfo = elements[j].Text;
                //string staffInfo = elements[j * 2 + 1].Text;
                PaymentPage.Data.Rooms[j].RoomType = custRoomInfo.Split(new string[] { " with " }, StringSplitOptions.None).First().Trim();
                PaymentPage.Data.Rooms[j].BoardType = custRoomInfo.Split(new string[] { " with " }, StringSplitOptions.None).Last().Trim();
                //SaveHotelStaffInfo(j, staffInfo);
                j++;
            }
        }

        private static void SaveHotelStaffInfo(int i, string staffInfo)
        {
            string[] staffInfoArray = staffInfo.Split(';');
            ExtrasPage.Data.Rooms[i].Supplier = staffInfoArray[0].Trim();
            ExtrasPage.Data.Rooms[i].Cost = staffInfoArray[1].Trim();
        }

        private static void SaveFlightOnlyInformation(IWebElement flightInfo)
        {
            ReadOnlyCollection<IWebElement> fullFlightInfo = flightInfo.FindElements(By.CssSelector("div.span6"));
            ReadOnlyCollection<IWebElement> outboundFlightInfo = fullFlightInfo[0].FindElements(By.CssSelector("div.row-fluid"));
            ReadOnlyCollection<IWebElement> inboundFlightInfo = fullFlightInfo[1].FindElements(By.CssSelector("div.row-fluid"));

            PaymentPage.Data.OutboundDepartureTime = outboundFlightInfo[0].Text.Trim();
            PaymentPage.Data.OutboundArrivalTime = outboundFlightInfo[1].Text.Split('(').First().Trim();
            PaymentPage.Data.OutboundJourneyTime = outboundFlightInfo[1].Text.Split('(').Last().TrimEnd(')').Trim();
            PaymentPage.Data.OutboundJourney = outboundFlightInfo[2].Text.Trim() + " " + outboundFlightInfo[3].Text.Trim();
            PaymentPage.Data.OutboundAirline = outboundFlightInfo[4].Text.Split(' ').First().Trim();
            PaymentPage.Data.OutboundFlightNo = outboundFlightInfo[4].Text.Split(' ').Last().Trim();


            PaymentPage.Data.InboundDepartureTime = inboundFlightInfo[0].Text.Trim();
            PaymentPage.Data.InboundArrivalTime = inboundFlightInfo[1].Text.Split('(').First().Trim();
            PaymentPage.Data.InboundJourneyTime = inboundFlightInfo[1].Text.Split('(').Last().TrimEnd(')').Trim();
            PaymentPage.Data.InboundJourney = inboundFlightInfo[2].Text.Trim() + " " + inboundFlightInfo[3].Text.Trim();
            PaymentPage.Data.InboundAirline = inboundFlightInfo[4].Text.Split(' ').First().Trim();
            PaymentPage.Data.InboundFlightNo = inboundFlightInfo[4].Text.Split(' ').Last().Trim();

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

        private static bool CompareFlightInfo()
        {
            if (FlightResultsPage.Data.OutboundDepartureAirport == PaymentPage.Data.OutboundDepartureTime && FlightResultsPage.Data.OutboundFlightNo == PaymentPage.Data.OutboundFlightNo &&
                FlightResultsPage.Data.OutboundJourney == PaymentPage.Data.OutboundJourney)
                return true;
            return false;
        }

        private static bool CompareHotelInfo()
        {
            if ((string.Equals(ExtrasPage.Data.HotelName, PaymentPage.Data.HotelName, StringComparison.OrdinalIgnoreCase)))
                return true;
            return false;
        }

        private static bool CompareRoomInfo(int index)
        {
            if ((string.Equals(ExtrasPage.Data.Rooms[index].RoomType, PaymentPage.Data.Rooms[index].RoomType, StringComparison.OrdinalIgnoreCase)) && (string.Equals(ExtrasPage.Data.Rooms[index].BoardType, PaymentPage.Data.Rooms[index].BoardType, StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }
            return false;
        }
        
        public static void ClickConfirm()
        {
            Logger.AddClickAction("Confirm");
            //click confirm button
            var confirmBtn = Driver.Instance.FindElement(By.Id("submitPaymentButton"));
            confirmBtn.Click();
            ConfirmationPage.WaitForLoad();
        }

        public static void ClickConfirmAndCapture()
        {
            Logger.AddClickAction("ConfirmAndCapture");
            //click confirm button
            var confirmBtn = Driver.Instance.FindElement(By.Id("submitPaymentButton"));
            confirmBtn.Click();
            //capture the page load time
            NonFunctionalReq.CaptureTime(() => Driver.Instance.FindElementWithTimeout(By.Id("threedspage"), 40, "3DS Page  not loaded within 60 sec"), "3DS Page load time is");
            //capture screenshot
            NonFunctionalReq.GetScreenShot("3DS Page");
        }

       
        public static void TypeContactFirstName(string firstName)
        {
            Logger.AddTypeAction("ContactFirstName", firstName);
            Driver.Instance.FindElement(By.Id("Firstname")).SendKeys(firstName);
        }

        public static void TypeContactLastName(string lastName)
        {
            Logger.AddTypeAction("ContactLastName", lastName);
            Driver.Instance.FindElement(By.Id("Lastname")).SendKeys(lastName);
        }

        public static void TypeContactEmail(string email)
        {
            Logger.AddTypeAction("ContactEmail", email);
            Driver.Instance.FindElement(By.Id("Address_Email")).SendKeys(email);
        }

        public static void TypeContactNumber(string contactNo)
        {
            Logger.AddTypeAction("ContactNumber", contactNo);
            Driver.Instance.FindElement(By.Id("Address_Telephone")).SendKeys(contactNo);
        }

        public static void SelectPaymentType(Card card)
        {
            Logger.AddSelectAction("PaymentType", card);
            var cardtypeDiv = Driver.Instance.FindElement(By.CssSelector("div.control-group div.controls div.btn-group"));
            var cardBtn = cardtypeDiv.FindElement(By.CssSelector("button[class='btn dropdown-toggle']"));
            cardBtn.Click();
            Driver.Instance.WaitForAjax();
            
            var cardTypeList = cardtypeDiv.FindElements(By.CssSelector("ul.dropdown-menu li"));

            int count = 0;
            do
            {
                Thread.Sleep(1000);
                Driver.Instance.WaitForAjax();
                if (count == 5) throw new TimeoutException("Card type dorp down not populated");
                count++;
            } while (!cardTypeList[0].FindElement(By.TagName("span")).Displayed);


            foreach (var cardTypeListItem in cardTypeList)
            {    
                var itemSpan = cardTypeListItem.FindElement(By.TagName("span"));
                if (itemSpan.Text == card.ToString().Replace("_", " "))
                {
                    cardTypeListItem.FindElement(By.TagName("a")).Click();
                    Driver.Instance.WaitForAjax();
                    SaveTotalPriceWithCardFees();
                    break;
                }
            }
        }

        private static void SaveTotalPriceWithCardFees()
        {
            IWebElement totalPriceWithCardFees = Driver.Instance.FindElement(By.CssSelector("div.row-fluid div[class='span3 total-price-value']"));
            IWebElement totalPriceWithCardFeesSoan = totalPriceWithCardFees.FindElement(By.CssSelector("span.pull-right"));
            PaymentPage.Data.TotalPriceWithCardFees = Decimal.Parse(totalPriceWithCardFeesSoan.Text.Split('£').Last().Trim());
        }

        public static void TypeCardNumber(string cardNumber)
        {
            Logger.AddTypeAction("CardNumber", cardNumber);
            //Find all payment details elements
            var cardtype = Driver.Instance.FindElement(By.Id("CardDetails_CardNumber"));
            cardtype.SendKeys(cardNumber);
        }

        public static void TypeSecurityCode(string securityCode)
        {
            Logger.AddTypeAction("SecurityCode", securityCode);
            var cardSecurityCode = Driver.Instance.FindElement(By.Id("CardDetails_SecurityCode"));
            cardSecurityCode.SendKeys(securityCode);
        }

        public static void TypeExpiryDate(string expDate)
        {
            Logger.AddTypeAction("ExpiryDate", expDate);
            DateTime expDatetime = DateTime.Parse("01/" + expDate);
            var cardExpMonth = Driver.Instance.FindElement(By.Id("CardDetails_ExpiryMonth"));
            cardExpMonth.SendKeys(expDatetime.Month.ToString());

            var cardExpYear = Driver.Instance.FindElement(By.Id("CardDetails_ExpiryYear"));
            cardExpYear.SendKeys(expDatetime.Year.ToString().Substring(2));
        }

        public static void TypeCardHolderName(string cardHolderName)
        {
            Logger.AddTypeAction("CardHolderName", cardHolderName);
            var CardHolder = Driver.Instance.FindElement(By.Id("CardDetails_CardHolder"));
            CardHolder.SendKeys(cardHolderName);
        }

        public static void ClickFindAddress()
        {
            Logger.AddClickAction("FindAddress");
            Driver.Instance.FindElement(By.Id("find-address-btn")).Click();
            Driver.Instance.WaitForAjax();
        }

        public static void SelectAddressNumber(int index)
        {
            Logger.AddSelectAction("AddressNumber", index);
            var addressDiv = Driver.Instance.FindElement(By.CssSelector("div.span12 div.form-box div div div.row-fluid div.span12 div.btn-group"));

            var selectAddBtn = addressDiv.FindElement(By.TagName("button"));
            selectAddBtn.Click();
            Driver.Instance.WaitForAjax();
            var addListul = addressDiv.FindElement(By.TagName("ul"));
            ReadOnlyCollection<IWebElement> addListli = addressDiv.FindElements(By.TagName("li"));
            addListli[index].FindElement(By.TagName("a")).Click();
            Driver.Instance.WaitForAjax();
        }

        public static void TypeStaffReference(string refr)
        {
            Logger.AddTypeAction("StaffReferenc", refr);
            var staffRefrence = Driver.Instance.FindElement(By.Id("StaffReference"));
            staffRefrence.SendKeys(refr);
        }

        public static void TypeStaffCustomerPhone(string custPhone)
        {
            Logger.AddTypeAction("StaffCustomerPhone", custPhone);
            var custPhonetxt = Driver.Instance.FindElement(By.Id("CustomerPhoneReference"));
            custPhonetxt.SendKeys(custPhone);   
        }
    }
}