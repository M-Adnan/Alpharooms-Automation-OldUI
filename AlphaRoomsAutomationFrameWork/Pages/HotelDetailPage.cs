using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Threading;
using AlphaRooms.AutomationFramework.PageData;
using AlphaRooms.AutomationFramework.Functions;
using AlphaRooms.AutomationFramework.Panels;
using AlphaRooms.AutomationFramework.Processes.HotelDetail;
using AlphaRooms.AutomationFramework.Infos;
using AlphaRooms.AutomationFramework.Selenium;

namespace AlphaRooms.AutomationFramework
{
    public static class HotelDetailPage
    {
        private static HotelDetailPageData data = new HotelDetailPageData();
        public static HotelDetailPageData Data { get { return data; } }

        private static TopPanel topPanel = new TopPanel();
        public static TopPanel TopPanel { get { return topPanel; } }

        private static ChangeSearchPanel changeSearchPanel = new ChangeSearchPanel();
        public static ChangeSearchPanel ChangeSearchPanel { get { return changeSearchPanel; } }

        private static int currentRoomNumber;

        internal static void WaitForLoad()
        {
            HotelDetailPage.ResetData();
            HotelDetailPage.Data.LoadingTime = NonFunctionalReq.CaptureTime(() => Driver.Instance.FindElementWithTimeout(By.Id("establishmentpage"), 60, "Hotel Details Page  not loaded within 60 sec"), "Hotel details page load time is");
        }

        internal static void ResetData()
        {
            HotelDetailPage.data = new HotelDetailPageData();
            HotelDetailPage.topPanel = new TopPanel();
            HotelDetailPage.changeSearchPanel = new ChangeSearchPanel();
            currentRoomNumber = 1;
        }

        public static bool IsDisplayed()
        {
            try
            {
                //Findout if hotel page is available
                Driver.Instance.FindElementWithTimeout(By.Id("establishmentpage"),0, "Hotel Details Page  not loaded within 60 sec");
                return true;
                //return Driver.Instance.IsElementDisplayedById("establishmentpage");
            }
            catch (Exception ex)
            {
                if (Driver.Instance.IsElementDisplayedBy(By.CssSelector("div div[class='alert alert-warning']")))
                    throw new Exception(String.Format("Hotel {0} unavailable for checkin date:{1} to checkoutdate:{2}",HotelDetailPage.Data.HotelName ,HomePage.Data.CheckInDate, HomePage.Data.CheckOutDate));
                if (Driver.Instance.IsElementDisplayedBy(By.CssSelector("div div[class='alert alert-error']")))
                    throw new Exception(String.Format("Room unavailable for checkin date:{1} to checkoutdate:{2} for hotel {0}",HotelDetailPage.Data.HotelName, HomePage.Data.CheckInDate, HomePage.Data.CheckOutDate));
                    //throw new Exception(String.Format("A {0} Room of Board Type: {1} from supplier: {2} is unavailable for checkin date:{3} to checkoutdate:{4}", HotelDetailPage.[room - 1].BoardType, HotelDetailPage.Data.Rooms[room - 1].Supplier, HomePage.Data.CheckInDate, HomePage.Data.CheckOutDate));          
                if (Driver.Instance.IsElementDisplayedBy(By.Id("accommodationresults")))
                    throw new Exception("Hotel Result page is displayed instead of Hotel Detail page");
                throw ex;
            }
        }

        private static IWebElement GetHotelRoomPanel()
        {
            var tabContent = Driver.Instance.FindElement(By.CssSelector("div[class='tab-content']"));
            ReadOnlyCollection<IWebElement> rooms = tabContent.FindElements(By.CssSelector("table.table-condensed tbody"));
            return rooms.First(i => i.Displayed);
        }

        private static IWebElement GetAvailableRoomPanel(int availableRoom)
        {
            IWebElement panel = GetHotelRoomPanel();
            ReadOnlyCollection<IWebElement> roomsTrs = panel.FindElements(By.CssSelector("tr:not([class*='staff-info'])"));
            if (roomsTrs.Count < availableRoom) throw new Exception(string.Format("Available room {0} not available.", availableRoom));
            return roomsTrs[availableRoom - 1];
        }

        private static IWebElement GetAvailableRoomButton(int availableRoom)
        {
            var roomTr = GetAvailableRoomPanel(availableRoom);
            return roomTr.FindElement(By.XPath("td[4]/a"));
        }

        private static IWebElement GetContinueButton()
        {
            ReadOnlyCollection<IWebElement> continues = Driver.Instance.FindElements(By.XPath(".//input[@id='continue-0']"));
            return continues.First(i => i.Displayed);
        }

        public static void ClickRoomNumber(int roomNumber)
        {
            //Logger.AddClickAction("RoomNumber", "RoomNumber", roomNumber);
            var selectedRoom = Driver.Instance.FindElement(By.Id(string.Format("estab0-room-tab{0}", roomNumber)));
            selectedRoom.Click();
            currentRoomNumber = roomNumber;
            Driver.Instance.WaitForAjax();
            Thread.Sleep(500);
        }

        public static void ClickAvaliableRoom(int availableRoom)
        {
            Logger.AddClickAction("AvailableRoom", "AvailableRoom", availableRoom);
            IWebElement availableRoomButton = GetAvailableRoomButton(availableRoom);
            SaveHotelData();
            SaveRoomData(currentRoomNumber, availableRoom);
            availableRoomButton.Click();

            if (HomePage.Data.Rooms.Length == 1)
            {
                //capture time that takes to load summary page
                ExtrasPage.WaitForLoad();
            }
        }
        
        public static void ClickAvaliableRoomAndCapture(int availableRoom)
        {
            Logger.AddClickAction("AvailableRoomAndCapture", "AvailableRoom", availableRoom);
            IWebElement availableRoomButton = GetAvailableRoomButton(availableRoom);
            SaveHotelData();
            SaveRoomData(currentRoomNumber, availableRoom);
            availableRoomButton.Click();

            if (HomePage.Data.Rooms.Length == 1)
            {
                //capture time that takes to load summary page
                //ExtrasPage.WaitForLoad();
                try
                {
                    //capture time that takes to load summary page
                    ExtrasPage.WaitForLoad();
                }
                catch (Exception ex)
                {
                    if (Driver.Instance.IsElementDisplayedBy(By.CssSelector("div div[class='alert alert-error']")))
                        throw new Exception(String.Format("Room unavailable for checkin date:{1} to checkoutdate:{2} for Hotel {0}", HotelDetailPage.Data.HotelName, HomePage.Data.CheckInDate, HomePage.Data.CheckOutDate));
                    if (Driver.Instance.IsElementDisplayedBy(By.CssSelector("div div[class='alert alert-warning']")))
                        throw new Exception(String.Format("Hotel {0} unavailable for checkin date:{1} to checkoutdate:{2}", HotelDetailPage.Data.HotelName, HomePage.Data.CheckInDate, HomePage.Data.CheckOutDate));                    
                    throw ex;
                }
            }
        }

        public static void ClickContinue()
        {
            Logger.AddClickAction("Continue");
            GetContinueButton().Click();

            //capture time that takes to load summary page
            ExtrasPage.WaitForLoad();
        }

        public static void ClickContinueAndCapture()
        {
            Logger.AddClickAction("ContinueAndCapture");
            GetContinueButton().Click();

            //capture time that takes to load summary page
            ExtrasPage.WaitForLoad();

            //capture screenshot
            //NonFunctionalReq.GetScreenShot("Extras Page");
        }

        public static SelectHotelDetailsRoomStarter SelectRoom()
        {
            return new SelectHotelDetailsRoomStarter();
        }

        public static AvailableHotelRoomInfo FindRoomForFirstSupplier(string supplier)
        {
            var fullresult = Driver.Instance.FindElement(By.CssSelector("table.table-condensed"));
            var tbody = fullresult.FindElement(By.XPath("./tbody"));
            ReadOnlyCollection<IWebElement> staffRoomsTrs = tbody.FindElements(By.CssSelector("tbody tr[class='staff-info']"));
            int j = 0;
            foreach (IWebElement StaffInfo in staffRoomsTrs)
            {
                if (string.IsNullOrWhiteSpace(staffRoomsTrs[j].Text)) throw new Exception("Staff info is not available.");
                string[] staffInfoHotel = StaffInfo.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                string hotelSupplier = staffInfoHotel[1].Split(':').Last().Trim();

                if (hotelSupplier == supplier)
                {
                    return new AvailableHotelRoomInfo(1, j + 1);
                }

                j++;
            }

            return null;
        }

        private static void SaveHotelData()
        {
            var hotelDiv = Driver.Instance.FindElement(By.ClassName("establishment-details"));
            string[] hotelDetailsArray = hotelDiv.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            //HotelResultsPage.Data.HotelNumber = hotelNumber;
            HotelDetailPage.Data.HotelName = hotelDetailsArray[0].Split(',').First().Trim();
            if (hotelDetailsArray[1] == "Unrated")
            {
                HotelDetailPage.Data.HotelAddress = hotelDetailsArray[2].Trim();
                HotelDetailPage.Data.HotelLocation = hotelDetailsArray[0].Split(',').Last().Trim();
            }
            else
            {
                HotelDetailPage.Data.HotelAddress = hotelDetailsArray[1].Split('-').First().Trim();
                HotelDetailPage.Data.HotelLocation = hotelDetailsArray[0].Split(',').Last().Trim();
            }
        }

        private static void SaveRoomData(int roomNumber, int room)
        {
            if (HotelDetailPage.Data.Rooms.Length < roomNumber)
            {
                int index = HotelDetailPage.Data.Rooms.Length;
                while (index < roomNumber)
                {
                    HotelDetailPage.Data.AddRoom();
                    index++;
                }
            }

            var tabContent = Driver.Instance.FindElement(By.CssSelector("div[class='tab-content']"));
            ReadOnlyCollection<IWebElement> roomsTBody = tabContent.FindElements(By.CssSelector("table.table-condensed tbody"));
            IWebElement roomTBody = roomsTBody.First(i => i.Displayed);
            ReadOnlyCollection<IWebElement> trs = roomTBody.FindElements(By.CssSelector("tbody tr"));
            int index2 = 0, count2 = trs.Count;
            while (index2 < count2)
            {
                if (index2 / 2 == room - 1)
                {
                    SaveCustRoomData(trs[index2], roomNumber, room);
                    return;
                }

                index2++;
            }
        }

        private static void SaveStaffRoomData(IWebElement staffRoomsTr, int room)
        {            
            string[] staffRoomDetailsArray = staffRoomsTr.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            HotelDetailPage.Data.Rooms[room - 1].Cost = staffRoomDetailsArray[0].Trim();
            HotelDetailPage.Data.Rooms[room - 1].Supplier = staffRoomDetailsArray[1].Trim();
        }

        private static void SaveCustRoomData(IWebElement roomTr, int room, int availableRoom)
        {
            Driver.Instance.WaitForAjax();
            string[] CustRoomDetailsArray = roomTr.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            HotelDetailPage.Data.Rooms[room - 1].AvailableRoom = availableRoom;
            HotelDetailPage.Data.Rooms[room - 1].RoomType = CustRoomDetailsArray[0].Trim();
            HotelDetailPage.Data.Rooms[room - 1].BoardType = CustRoomDetailsArray[1].Trim();
            //string[] priceArray = CustRoomDetailsArray[2].Split(' ');
            //HotelDetailPage.Data.Rooms[room - 1].RoomPrice = priceArray[0].Trim();
            //HotelDetailPage.Data.Rooms[room - 1].TotalPrice = Decimal.Parse(priceArray[1].Trim().Remove(0, 1));
        }
    }
}
