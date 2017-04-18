using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaRooms.AutomationFramework.Processes.HotelResults.Interfaces;
using AlphaRooms.AutomationFramework.Infos;

namespace AlphaRooms.AutomationFramework.Processes.HotelResults
{
    public class SelectRoomByHotel : ISelectRoomByHotelFirst, ISelectRoomByHotelSingleRoom, ISelectRoomByFirstSupplier, ISelectRoomByHotel, ISelectRoomByHotelRoom
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

        private int hotelNumber;
        private List<Room> rooms = new List<Room>();
        private bool isSingleRoom;

        public SelectRoomByHotel(int hotelNumber)
        {
            this.hotelNumber = hotelNumber;
            this.rooms = new List<Room>();
            this.isSingleRoom = false;
        }

        public SelectRoomByHotel(AvailableRoomInfo room)
        {
            this.hotelNumber = room.HotelNumber;
            this.rooms = new List<Room>();
            this.rooms.Add(new Room(room.RoomNumber) { AvailableRoom = room.AvailableRoomNumber });
            this.isSingleRoom = true;
        }

        public ISelectRoomByHotelRoom ForRoomNumber(int roomNumber)
        {
            this.rooms.Add(new Room(roomNumber));
            return this;
        }

        public ISelectRoomByHotel WithAvailableRoom(int room)
        {
            this.rooms.Last().AvailableRoom = room;
            return this;
        }
        
        ISelectRoomByHotelSingleRoom ISelectRoomByHotelFirst.OnlyOneRoomWithAvailableRoom(int room)
        {
            this.rooms.Add(new Room(1) { AvailableRoom = room });
            this.isSingleRoom = true;
            return this;
        }

        private void ContinueProcess(bool isCapture)
        {
            if (this.rooms.Count == 1)
            {
                if (!this.isSingleRoom) HotelResultsPage.ClickRoomNumber(hotelNumber, this.rooms[0].RoomNumber);
                if (isCapture) HotelResultsPage.ClickAvailableRoomAndCapture(hotelNumber, this.rooms[0].AvailableRoom);
                else HotelResultsPage.ClickAvailableRoom(hotelNumber, this.rooms[0].AvailableRoom);
                return;
            }
            foreach (Room room in this.rooms)
            {
                HotelResultsPage.ClickRoomNumber(hotelNumber, room.RoomNumber);
                HotelResultsPage.ClickAvailableRoom(hotelNumber, room.AvailableRoom);
            }
            if (isCapture) HotelResultsPage.ClickContinueAndCapture();
            else HotelResultsPage.ClickContinue();
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
