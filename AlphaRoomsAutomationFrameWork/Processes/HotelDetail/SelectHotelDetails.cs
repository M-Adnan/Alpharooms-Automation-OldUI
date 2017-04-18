using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pages = AlphaRooms.AutomationFramework;
using AlphaRooms.AutomationFramework.Processes.HotelDetail.Interfaces;
using AlphaRooms.AutomationFramework.Infos;

namespace AlphaRooms.AutomationFramework.Processes.HotelDetail
{
    public class SelectHotelDetails : ISelectHotelDetailsSingleRoom, ISelectHotelDetailsByFirstSupplier, ISelectHotelDetails, ISelectHotelDetailsRoom
    {
        private class Room
        {
            public Room(int roomNumber)
            {
                this.RoomNumber = roomNumber;
            }
            public int RoomNumber { get; set; }
            public int AvailableRoom { get; set; }
        }

        private List<Room> rooms;
        private bool isSingleRoom;

        public SelectHotelDetails(int roomNumber)
        {
            this.rooms = new List<Room>();
            this.ForRoomNumber(roomNumber);
        }

        public SelectHotelDetails(int availableRoom, bool isSingleRoom)
        {
            this.rooms = new List<Room>();
            this.rooms.Add(new Room(1) { AvailableRoom = availableRoom });
            this.isSingleRoom = isSingleRoom;
        }

        public SelectHotelDetails(AvailableHotelRoomInfo room)
        {
            this.rooms = new List<Room>();
            this.rooms.Add(new Room(room.RoomNumber) { AvailableRoom = room.AvailableRoomNumber });
            this.isSingleRoom = true;
        }

        public ISelectHotelDetailsRoom ForRoomNumber(int roomNumber)
        {
            this.rooms.Add(new Room(roomNumber));
            return this;
        }

        public ISelectHotelDetails WithAvailableRoom(int room)
        {
            this.rooms.Last().AvailableRoom = room;
            return this;
        }

        private void ContinueProcess(bool isCapture)
        {
            if (this.rooms.Count == 1)
            {
                if (!this.isSingleRoom) Pages.HotelDetailPage.ClickRoomNumber(this.rooms[0].RoomNumber);
                if (!isCapture) Pages.HotelDetailPage.ClickAvaliableRoom(this.rooms[0].AvailableRoom);
                else Pages.HotelDetailPage.ClickAvaliableRoomAndCapture(this.rooms[0].AvailableRoom);
                return;
            }
            foreach (Room room in this.rooms)
            {
                Pages.HotelDetailPage.ClickRoomNumber(room.RoomNumber);
                Pages.HotelDetailPage.ClickAvaliableRoom(room.AvailableRoom);
            }
            if (!isCapture) Pages.HotelDetailPage.ClickContinue();
            else Pages.HotelDetailPage.ClickContinueAndCapture();
        }

        public void Continue()
        {
            this.ContinueProcess(false);
        }

        public void ContinueAndCapture()
        {
            this.ContinueProcess(true);
        }
    }
}
