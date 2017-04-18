using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using AlphaRooms.AutomationFramework.PageData;
using AlphaRooms.AutomationFramework.Functions;
using AlphaRooms.AutomationFramework.Panels;
using AlphaRooms.AutomationFramework.Processes.HotelResults;
using AlphaRooms.AutomationFramework.Infos;
using AlphaRooms.AutomationFramework.Selenium;

namespace AlphaRooms.AutomationFramework
{
    public static class HotelResultsPage
    {
        private static HotelResultsPageData data = new HotelResultsPageData();
        public static HotelResultsPageData Data { get { return data; } }

        private static TopPanel topPanel = new TopPanel();
        public static TopPanel TopPanel { get { return topPanel; } }

        private static ChangeSearchPanel changeSearchPanel = new ChangeSearchPanel();
        public static ChangeSearchPanel ChangeSearchPanel { get { return changeSearchPanel; } }

        private static int currentRoomNumber = 1;

        internal static void WaitToLoad()
        {
            HotelResultsPage.ResetData();
            HotelResultsPage.Data.LoadingTime = NonFunctionalReq.CaptureTime(() => Driver.Instance.WaitForAjax(), "Hotel Result Page Rendering Time");
        }

        internal static void ResetData()
        {
            HotelResultsPage.data = new HotelResultsPageData();
            HotelResultsPage.topPanel = new TopPanel();
            HotelResultsPage.changeSearchPanel = new ChangeSearchPanel();
            currentRoomNumber = 1;
        }

        public static bool IsDisplayed()
        {
            return Driver.Instance.IsElementDisplayedById("accommodationresults");
        }

        private static ReadOnlyCollection<IWebElement> GetHotelResultPanels()
        {
            var allSearchResults = Driver.Instance.FindElement(By.CssSelector("div.span9 div.results div.results-wrap"));
            return allSearchResults.FindElements(By.CssSelector("div.result-box"));
        }

        private static IWebElement GetHotelPanel(int hotelNumber)
        {
            return GetHotelResultPanels()[hotelNumber - 1];
        }

        private static IWebElement GetHotelRoomPanel(int hotelNumber)
        {
            var tabContent = GetHotelPanel(hotelNumber).FindElement(By.CssSelector("div[class='tab-content']"));
            ReadOnlyCollection<IWebElement> roomsTBody = tabContent.FindElements(By.CssSelector("table.table-condensed tbody"));
            return roomsTBody.First(i => i.Displayed);
        }

        private static IWebElement GetAvailableRoomPanel(int hotelNumber, int availableRoom)
        {
            IWebElement panel = GetHotelRoomPanel(hotelNumber);
            ReadOnlyCollection<IWebElement> roomsTrs = panel.FindElements(By.CssSelector("tr:not([class*='staff-info'])"));
            if (roomsTrs.Count < availableRoom) throw new Exception(string.Format("Available room {0} not available.", availableRoom));
            return roomsTrs[availableRoom - 1];
        }

        private static IWebElement GetAvailableRoomButton(int hotelNumber, int availableRoom)
        {
            var roomTr = GetAvailableRoomPanel(hotelNumber, availableRoom);
            return roomTr.FindElement(By.XPath("td[4]/a"));
        }

        private static IWebElement GetContinueButton()
        {
            ReadOnlyCollection<IWebElement> continues = Driver.Instance.FindElements(By.XPath(".//input[@id='continue-0']"));
            return continues.First(i => i.Displayed);
        }

        public static void ClickResultPage(int pageNo)
        {
            if (pageNo < 1) throw new ArgumentOutOfRangeException("pageNo", pageNo, "The page number must be 1 or higher.");
            Logger.AddClickAction("ResultPage", "PageNumber", pageNo);
            FlightResultsPage.ClickResultPage(pageNo);
        }

        public static void ClickHotelNumber(int hotelNumber)
        {
            SaveSearchGUID();
            SaveTotalSearchResults();
            if (hotelNumber < 1) throw new ArgumentOutOfRangeException("hotelNumber", hotelNumber, "The hotel number must be 1 or higher.");
            Logger.AddClickAction("HotelNumber", "HotelNumber", hotelNumber);
            HotelResultsPage.SaveHotelData(hotelNumber);
            var hotelPanel = GetHotelPanel(hotelNumber);
            var hotelNameLink = hotelPanel.FindElement(By.CssSelector("h3.hotel-name a"));
            hotelNameLink.Click();
            HotelDetailPage.WaitForLoad();
        }

        public static void ClickHotelNumberAndCapture(int hotelNumber)
        {
            SaveSearchGUID();
            if (hotelNumber < 1) throw new ArgumentOutOfRangeException("hotelNumber", hotelNumber, "The hotel number must be 1 or higher.");
            Logger.AddClickAction("HotelNumberAndCapture", "HotelNumber", hotelNumber);
            HotelResultsPage.SaveHotelData(hotelNumber);
            var hotelPanel = GetHotelPanel(hotelNumber);
            var hotelNameLink = hotelPanel.FindElement(By.CssSelector("h3.hotel-name a"));
            hotelNameLink.Click();

            HotelDetailPage.WaitForLoad();

            //capture screenshot
            //NonFunctionalReq.GetScreenShot("Hotel Booking Summary Page");
        }

        public static void ClickRoomNumber(int hotelNumber, int roomNumber)
        {
            if (hotelNumber < 1) throw new ArgumentOutOfRangeException("hotelNumber", hotelNumber, "The hotel number must be 1 or higher.");
            if (roomNumber < 1) throw new ArgumentOutOfRangeException("roomNumber", roomNumber, "The room number must be 1 or higher.");
            Logger.AddClickAction("RoomNumber", "HotelNumber", hotelNumber, "RoomNumber", roomNumber);
            currentRoomNumber = roomNumber;
            HotelResultsPage.Data.EnsureCapacity(roomNumber);
            HotelResultsPage.SaveHotelData(hotelNumber);
            var hotelPanel = GetHotelPanel(hotelNumber);
            var selectedRoom = hotelPanel.FindElement(By.Id(string.Format("estab{0}-room-tab{1}", hotelNumber - 1, roomNumber)));
            selectedRoom.Click();
            Driver.Instance.WaitForAjax();
            Thread.Sleep(500);
        }
        
        public static void ClickAvailableRoom(int hotelNumber, int availableRoom)
        {
            if (hotelNumber < 1) throw new ArgumentOutOfRangeException("hotelNumber", hotelNumber, "The hotel number must be 1 or higher.");
            if (availableRoom < 1) throw new ArgumentOutOfRangeException("availableRoom", availableRoom, "The available room must be 1 or higher.");
            Logger.AddClickAction("AvailableRoom", "HotelNumber", hotelNumber, "AvailableRoom", availableRoom);
            var selectRoomLink = GetAvailableRoomButton(hotelNumber, availableRoom);
            SaveSearchGUID();
            SaveTotalSearchResults();
            SaveRoomData(hotelNumber, currentRoomNumber, availableRoom);
            selectRoomLink.Click();

            if (HomePage.Data.Rooms.Length == 1)
            {
                try
                {
                    //capture time that takes to load summary page
                    ExtrasPage.WaitForLoad();
                }
                catch (Exception ex)
                {
                    if (Driver.Instance.IsElementDisplayedBy(By.CssSelector("div.alert h4[class='alert alert-error hidden']")))
                        throw new Exception(string.Format("Hotel Number {0} selected room is no longer available.", hotelNumber));
                    if (Driver.Instance.IsElementDisplayedBy(By.CssSelector("div.alert h4[class='alert alert-warning hidden']")))
                        throw new Exception(string.Format("Hotel Number {0} is fully booked for the dates selected.", hotelNumber));
                    throw ex;
                }   
            }
        }

        public static void ClickAvailableRoomAndCapture(int hotelNumber, int availableRoom)
        {
            if (hotelNumber < 1) throw new ArgumentOutOfRangeException("hotelNumber", hotelNumber, "The hotel number must be 1 or higher.");
            if (availableRoom < 1) throw new ArgumentOutOfRangeException("availableRoom", availableRoom, "availableRoom must be 1 or higher.");
            Logger.AddClickAction("AvailableRoomAndCapture", "HotelNumber", hotelNumber, "AvailableRoom", availableRoom);
            HotelResultsPage.SaveHotelData(hotelNumber);
            var selectRoomLink = GetAvailableRoomButton(hotelNumber, availableRoom);
            SaveSearchGUID();
            SaveTotalSearchResults();
            SaveRoomData(hotelNumber, currentRoomNumber, availableRoom);
            selectRoomLink.Click();

            if (HomePage.Data.Rooms.Length == 1)
            {
                try
                {
                    //capture time that takes to load summary page
                    ExtrasPage.WaitForLoad();
                }
                catch (Exception ex)
                {
                    if (Driver.Instance.IsElementDisplayedBy(By.CssSelector("div.alert h4[class='alert alert-error hidden']")))
                        throw new Exception(string.Format("Hotel Number {0} selected room is no longer available.", hotelNumber));
                    if (Driver.Instance.IsElementDisplayedBy(By.CssSelector("div.alert h4[class='alert alert-warning hidden']")))
                        throw new Exception(string.Format("Hotel Number {0} is fully booked for the dates selected.", hotelNumber));
                    throw ex;
                }   
                //capture screenshot
                //NonFunctionalReq.GetScreenShot("Hotel Booking Summary Page");
            }
        }

        public static void ClickContinue()
        {
            Logger.AddClickAction("Continue");
            SaveSearchGUID();
            GetContinueButton().Click();

            try
            {
                //capture time that takes to load summary page
                ExtrasPage.WaitForLoad();
            }
            catch (Exception ex)
            {
                if (Driver.Instance.IsElementDisplayedBy(By.CssSelector("div.alert h4[class='alert alert-error hidden']")))
                    throw new Exception(string.Format("Hotel Number {0} selected room is no longer available.", ExtrasPage.Data.HotelName));
                if (Driver.Instance.IsElementDisplayedBy(By.CssSelector("div.alert h4[class='alert alert-warning hidden']")))
                    throw new Exception(string.Format("Hotel Number {0} is fully booked for the dates selected.", ExtrasPage.Data.HotelName));
                throw ex;
            }   
        }

        public static void ClickContinueAndCapture()
        {
            Logger.AddClickAction("ContinueAndCapture");
            SaveSearchGUID();
            GetContinueButton().Click();

            try
            {
                //capture time that takes to load summary page
                ExtrasPage.WaitForLoad();
            }
            catch (Exception ex)
            {
                if (Driver.Instance.IsElementDisplayedBy(By.CssSelector("div.alert h4[class='alert alert-error hidden']")))
                    throw new Exception(string.Format("Hotel Number {0} selected room is no longer available.", ExtrasPage.Data.HotelName));
                if (Driver.Instance.IsElementDisplayedBy(By.CssSelector("div.alert h4[class='alert alert-warning hidden']")))
                    throw new Exception(string.Format("Hotel Number {0} is fully booked for the dates selected.", ExtrasPage.Data.HotelName));
                throw ex;
            }   
            //capture screenshot
            //NonFunctionalReq.GetScreenShot("Hotel Booking Summary Page");
        }

        public static SelectRoomStarter SelectRoom()
        {
            return new SelectRoomStarter();
        }

        public static int FindHotel(string hotel)
        {
            ReadOnlyCollection<IWebElement> divallSearchResults = GetHotelResultPanels();
            for (int i = 0; i < divallSearchResults.Count; i++)
            {
                var fullresult = divallSearchResults[i].FindElement(By.CssSelector("h3.hotel-name"));
                if (string.Equals(fullresult.Text, hotel, StringComparison.CurrentCultureIgnoreCase)) return i + 1;
            }
            return -1;
        }

        public static object ContainsRoomFromSupplier(string supplier)
        {
            return FindRoomForFirstSupplier(supplier) != null;
        }

        public static object ContainsHotelByName(string supplier)
        {
            return FindHotel(supplier) != -1;
        }

        public static AvailableRoomInfo FindRoomForFirstSupplier(string supplier)
        {
            ReadOnlyCollection<IWebElement> divallSearchResults = GetHotelResultPanels();
            for (int i = 0; i < divallSearchResults.Count; i++)
            {
                var fullresult = divallSearchResults[i].FindElement(By.CssSelector("div.tabbable div.tab-content"));
                var tbody = fullresult.FindElement(By.XPath("./div/table/tbody"));
                ReadOnlyCollection<IWebElement> staffRoomsTrs = tbody.FindElements(By.CssSelector("tbody tr[class='staff-info']"));
                for (int j = 0; j < staffRoomsTrs.Count; j++)
                {
                    if (string.IsNullOrWhiteSpace(staffRoomsTrs[j].Text)) throw new Exception("Staff info is not available.");
                    string[] staffInfoHotel = staffRoomsTrs[j].Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    string hotelSupplier = staffInfoHotel[1].Split(':').Last().Trim();

                    if (string.Equals(hotelSupplier, supplier, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return new AvailableRoomInfo(i + 1, 1, j + 1);
                    }

                    j++;
                }
            }
            return null;
        }
        
        private static void SaveHotelData(int hotelNumber)
        {
            IWebElement hotelPanel = GetHotelPanel(hotelNumber);
            HotelResultsPage.Data.HotelNumber = hotelNumber;
            IWebElement hotelName = hotelPanel.FindElement(By.CssSelector("h3[class='hotel-name']"));
            HotelResultsPage.Data.HotelName = hotelName.Text.Trim();
            IWebElement hotelAddress = hotelPanel.FindElement(By.CssSelector("div[class='address']"));
            HotelResultsPage.Data.HotelAddress = hotelAddress.Text;
            IWebElement hotelLocation = hotelPanel.FindElement(By.CssSelector("span[class='destination']"));
            HotelResultsPage.Data.HotelLocation = hotelLocation.Text;
        }
        
        private static void SaveRoomData(int hotelNumber, int roomNumber, int room)
        {
            if (HotelResultsPage.Data.Rooms.Length < roomNumber)
            {
                int index = HotelResultsPage.Data.Rooms.Length, count = roomNumber;
                while (HotelResultsPage.Data.Rooms.Length < roomNumber)
                {
                    HotelResultsPage.Data.AddRoom();
                    index++;
                }
            }

            HotelResultsPageRoomData roomData = HotelResultsPage.Data.Rooms[roomNumber - 1];

            IWebElement selectedRoomtr = GetAvailableRoomPanel(hotelNumber, room);
            ReadOnlyCollection<IWebElement> roomTds = selectedRoomtr.FindElements(By.TagName("td"));
            roomData.RoomType = roomTds[0].FindElement(By.CssSelector("div.span8 span")).Text;
            roomData.BoardType = roomTds[0].FindElement(By.CssSelector("div.span8 div.boardType")).Text;
            roomData.RoomPrice = roomTds[1].FindElement(By.CssSelector("td.price span span")).Text;
            roomData.TotalPrice = Decimal.Parse(roomTds[2].Text.Remove(0, 1));
            roomData.AvailableRoom = room;
        }

        private static void SaveStaffRoomData(IWebElement staffRoomsTr, int room)
        {
            Driver.Instance.WaitForAjax();
            Thread.Sleep(500);
            string[] staffRoomDetailsArray = staffRoomsTr.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            HotelResultsPage.Data.Rooms[room - 1].Cost = staffRoomDetailsArray[0].Trim();
            HotelResultsPage.Data.Rooms[room - 1].Supplier = staffRoomDetailsArray[1].Trim();
        }
        
        public static bool AreResultsDisplayed()
        {
            try
            {
                return (GetHotelResultPanels().Count > 0);
            }
            catch
            {
                return false;
            }
        }

        private static void SaveSearchGUID()
        {
            string url = Driver.Instance.Url;
            string searchID = url.Split(new char[] { '=', '&' })[1];
            HotelResultsPage.Data.HotelSearchGuid = searchID;
        }

        private static void SaveTotalSearchResults()
        {
            IWebElement totalResults = Driver.Instance.FindElement(By.CssSelector("div.row-fluid div.span7 h2 span[data-bind='text: numResults']"));
            HotelResultsPage.Data.TotalHotelSearchResults = int.Parse(totalResults.Text);
        }
    }
}
