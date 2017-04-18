using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaRooms.AutomationFramework.Processes.Payment.Interfaces;

namespace AlphaRooms.AutomationFramework.Processes.Payment
{
    public class MakeAHotelBooking : IMakeAHotelBookingGuestDetails, IMakeAHotelBookingGuestDetailsAuto, IMakeAHotelBookingGuestRoom, IMakeAHotelBookingAllGuests
        , IMakeABookingContactDetails, IMakeABookingContactDetailsAuto
        , IMakeABookingPaymentDetails, IMakeABookingPaymentDetailsAuto
        , IMakeABookingStaffDetails, IMakeABookingStaffDetailsAuto
                                              
    {
        private class Guest
        {
            public Guest(int guestNumber)
            {
                this.GuestNumber = guestNumber;
            }

            public int GuestNumber { get; set; }
            public int RoomNumber { get; set; }
            public Title? Title { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        private List<Guest> guests;
        private string contactFirstName;
        private string contactLastName;
        private string contactEmail;
        private string contactPhoneNumber;
        private Card? paymentCardType;
        private string paymentCardNumber;
        private string paymentExpiryDate;
        private string paymentSecurityCode;
        private string paymentCardHolderName;
        private string paymentPostCode;
        private string staffReference;
        private string staffCustomerPhone;

        public MakeAHotelBooking(int guest)
        {
            this.guests = new List<Guest>();
            this.guests.Add(new Guest(guest));
        }

        public MakeAHotelBooking()
        {
            this.guests = new List<Guest>();
        }

        IMakeAHotelBookingGuestDetailsAuto IMakeAHotelBookingAllGuests.AutoFill()
        {
            int count = HomePage.Data.Rooms.Sum(i => i.Adults.GetValueOrDefault(0) + i.Children.GetValueOrDefault(0));
            for (int index = 0; index < count; index++)
            {
                this.guests.Add(new Guest(index + 1) {
                    Title = PaymentPageRnd.PickRandomGuestTitle()
                    , FirstName = PaymentPageRnd.PickRandomGuestFirstName()
                    , LastName = PaymentPageRnd.PickRandomGuestLastName()
                });
            }

            return this;
        }
        
        public IMakeAHotelBookingGuestRoom ForGuestDetailsNumber(int guest)
        {
            this.guests.Add(new Guest(guest));
            return this;
        }

        public IMakeAHotelBookingGuestDetails OfRoomNo(int room)
        {
            this.guests.Last().RoomNumber = room;
            return this;
        }

        public IMakeAHotelBookingGuestDetails WithTitle(Title title)
        {
            this.guests.Last().Title = title;
            return this;
        }

        public IMakeAHotelBookingGuestDetails WithFirstName(string firstName)
        {
            this.guests.Last().FirstName = firstName;
            return this;
        }

        public IMakeAHotelBookingGuestDetails WithLastName(string lastName)
        {
            this.guests.Last().LastName = lastName;
            return this;
        }

        IMakeAHotelBookingGuestDetailsAuto IMakeAHotelBookingGuestDetails.AutoFill()
        {
            this.guests.Last().Title = PaymentPageRnd.PickRandomGuestTitle();
            this.guests.Last().FirstName = PaymentPageRnd.PickRandomGuestFirstName();
            this.guests.Last().LastName = PaymentPageRnd.PickRandomGuestLastName();
            return this;
        }

        public IMakeABookingContactDetails ForContactDetails()
        {
            return this;
        }

        IMakeABookingContactDetails IMakeABookingContactDetails.TypeFirstName(string firstName)
        {
            this.contactFirstName = firstName;
            return this;
        }

        IMakeABookingContactDetails IMakeABookingContactDetails.WithLastName(string lastName)
        {
            this.contactLastName = lastName;
            return this;
        }

        public IMakeABookingContactDetails WithEmail(string email)
        {
            this.contactEmail = email;
            return this;
        }

        public IMakeABookingContactDetails WithPhoneNumber(string number)
        {
            this.contactPhoneNumber = number;
            return this;
        }

        IMakeABookingContactDetailsAuto IMakeABookingContactDetails.AutoFill()
        {
            this.contactFirstName = PaymentPageRnd.PickRandomContactFirstName();
            this.contactLastName = PaymentPageRnd.PickRandomContactLastName();
            this.contactEmail = PaymentPageRnd.PickRandomContactEmail();
            this.contactPhoneNumber = PaymentPageRnd.PickRandomContactPhoneNumber();
            return this;
        }

        public IMakeABookingPaymentDetails ForPaymentDetails()
        {
            return this;
        }

        public IMakeABookingPaymentDetails WithPaymentType(Card card)
        {
            this.paymentCardType = card;
            return this;
        }

        public IMakeABookingPaymentDetails WithCardNo(string number)
        {
            this.paymentCardNumber = number;
            return this;
        }

        public IMakeABookingPaymentDetails WithExpiryDate(string date)
        {
            this.paymentExpiryDate = date;
            return this;
        }

        public IMakeABookingPaymentDetails WithSecurityCode(string sCode)
        {
            this.paymentSecurityCode = sCode;
            return this;
        }

        public IMakeABookingPaymentDetails WithCardHolderName(string name)
        {
            this.paymentCardHolderName = name;
            return this;
        }

        public IMakeABookingPaymentDetails WithPostCode(string postCode)
        {
            this.paymentPostCode = postCode;
            return this;
        }

        IMakeABookingPaymentDetailsAuto IMakeABookingPaymentDetails.AutoFill()
        {
            this.paymentCardType = PaymentPageRnd.PickRandomPaymentCardType();
            this.paymentCardNumber = PaymentPageRnd.PickRandomPaymentCardNumber();
            this.paymentExpiryDate = PaymentPageRnd.PickRandomPaymentExpiryDate();
            this.paymentSecurityCode = PaymentPageRnd.PickRandomPaymentSecurityCode();
            this.paymentCardHolderName = PaymentPageRnd.PickRandomPaymentCardHolderName();
            this.paymentPostCode = PaymentPageRnd.PickRandomPaymentPostCode();
            return this;
        }

        public IMakeABookingStaffDetails ForStaffDetails()
        {
            return this;
        }

        public IMakeABookingStaffDetails WithReference(string reference)
        {
            this.staffReference = reference;
            return this;
        }

        public IMakeABookingStaffDetails WithCustomerPhone(string customerPhone)
        {
            this.staffCustomerPhone = customerPhone;
            return this;
        }

        IMakeABookingStaffDetailsAuto IMakeABookingStaffDetails.AutoFill()
        {
            this.staffReference = PaymentPageRnd.PickRandomStaffReference();
            this.staffCustomerPhone = PaymentPageRnd.PickRandomStaffCustomerPhone();
            return this;
        }

        private void ConfirmProcess()
        {
            foreach (Guest guest in this.guests)
            {
                if (guest.Title != null) PaymentPage.SelectGuestTitle(guest.RoomNumber, guest.GuestNumber, guest.Title.Value);
                if (guest.FirstName != null) PaymentPage.TypeGuestFirstName(guest.RoomNumber, guest.GuestNumber, guest.FirstName);
                if (guest.LastName != null) PaymentPage.TypeGuestLastName(guest.RoomNumber, guest.GuestNumber, guest.LastName);
            }

            if (this.contactFirstName != null) PaymentPage.TypeContactFirstName(this.contactFirstName);
            if (this.contactLastName != null) PaymentPage.TypeContactLastName(this.contactLastName);
            if (this.contactEmail != null) PaymentPage.TypeContactEmail(this.contactEmail);
            if (this.contactPhoneNumber != null) PaymentPage.TypeContactNumber(this.contactPhoneNumber);

            if (this.paymentCardType != null) PaymentPage.SelectPaymentType(this.paymentCardType.Value);
            if (this.paymentCardNumber != null) PaymentPage.TypeCardNumber(this.paymentCardNumber);
            if (this.paymentSecurityCode != null) PaymentPage.TypeSecurityCode(this.paymentSecurityCode);
            if (this.paymentExpiryDate != null) PaymentPage.TypeExpiryDate(this.paymentExpiryDate);
            if (this.paymentCardHolderName != null) PaymentPage.TypeCardHolderName(this.paymentCardHolderName);
            if (this.paymentPostCode != null)
            {
                PaymentPage.TypePostCode(this.paymentPostCode);
                PaymentPage.ClickFindAddress();
                PaymentPage.SelectAddressNumber(1);
            }

            if (this.staffReference != null) PaymentPage.TypeStaffReference(this.staffReference);
            if (this.staffCustomerPhone != null) PaymentPage.TypeStaffCustomerPhone(this.staffCustomerPhone);
        }

        public void Confirm()
        {
            this.ConfirmProcess();
            PaymentPage.ClickConfirm();
        }

        public void ConfirmAndCapture()
        {
            this.ConfirmProcess();
            PaymentPage.ClickConfirmAndCapture();
        }
    }
}
