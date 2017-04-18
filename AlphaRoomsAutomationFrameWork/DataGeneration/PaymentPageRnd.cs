using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace AlphaRooms.AutomationFramework
{
    public class PaymentPageRnd
    {
        public static Title PickRandomGuestTitle()
        {
            Array Titles = Enum.GetValues(typeof(Title));
            Title randomTitle = (Title)Titles.GetValue(HomePageRnd.Random.Next(Titles.Length));
            return randomTitle;
        }

        public static string PickRandomGuestFirstName()
        {
            //Most popular english first names
            List<String> FirstNames = new List<string>
            {
                "Emily","James","Chole","Jack","Sophie","Daniel","Charlotte","Alex","Lauren","Sam","Hannah","Ben","Emma","Tom","Amy","Ryan","Megan","Callum",
                "Ellie","Adam","Katie","Joe","Lucy","Josh","Jessica","Matthew","Olivia","David","Rebbeca","Thomas","Georgia","Jake","Laura","Harry","Sarah","Luke",
                "Holly", "Liam", "Beth", "William","Shannon","Jordan","Jade","Jamie","Rachel","Chris","Bethany","Lewis","Alice","Scott","Jess","Connor",
            };
            int index = HomePageRnd.Random.Next(FirstNames.Count);
            return FirstNames[index];
        }
        
        public static string PickRandomGuestLastName()
        {
            return "Smith";
        }

        public static string PickRandomGuestDoB(int age)
        {
            return DateTime.Now.AddYears(-age).ToShortDateString();
        }

        public static string PickRandomContactFirstName()
        {
            return "ContactFirstName";
        }

        public static string PickRandomContactLastName()
        {
            return "ContactLastName";
        }

        public static string PickRandomContactEmail()
        {
            return "automationtest@alpharooms.com";
        }

        public static string PickRandomContactPhoneNumber()
        {
            return "+447707496130";
        }

        private static Card lastCard;

        public static Card PickRandomPaymentCardType()
        {
            Array cards = Enum.GetValues(typeof(Card));
            Card randomCard = (Card)cards.GetValue(HomePageRnd.Random.Next(cards.Length));
            lastCard = randomCard;
            return randomCard;
        }

        public static string PickRandomPaymentCardNumber()
        {
            switch (lastCard)
            {
                case Card.American_Express:
                    return "378282246310005";

                case Card.Diners_Club:
                    return "36148900647913";

                //case "Maestro":
                //    cardNo = "6759649826438453";
                //    break;

                case Card.Mastercard:
                    return "5454545454545454";

                //case "Solo":
                //    cardNo = "4444333322221111";
                //    break;

                //case "Switch":
                //    cardNo = "4444333322221111";
                //    break;

                case Card.Visa_Credit:
                    return "4484070000000000";

                case Card.Visa_Debit:
                    return "4462030000000000";

                case Card.Visa_Delta:
                    return "4444333322221111";

                case Card.Visa_Electron:
                    return "4917300800000000";
            }
            return "378282246310005";
        }

        public static string PickRandomPaymentExpiryDate()
        {
            return "01/18";
        }

        public static string PickRandomPaymentSecurityCode()
        {
            return "503";
        }

        public static string PickRandomPaymentCardHolderName()
        {
            return "CardHolderName";
        }

        public static string PickRandomPaymentPostCode()
        {
            //Britain's 20 worst postcodes for burglaries
            String[] postCodes = new String[]
            {
                "M21","SE24","N10","RM8","N2","L18","L22","N18","UB6","SE27","B73","E8","DN2","N20","E9","N11","N16","LS16","B24","HP10"
            };
            int index = HomePageRnd.Random.Next(postCodes.Length);
            return postCodes[index];
        }

        public static string PickRandomStaffReference()
        {
            return "AutomationTest";
        }

        public static string PickRandomStaffCustomerPhone()
        {
            return "07747854123";
        }
    }
}
