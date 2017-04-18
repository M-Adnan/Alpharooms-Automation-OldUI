using System;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using AlphaRooms.AutomationFramework.Selenium;

namespace AlphaRooms.AutomationFramework.Functions
{
    public class TopResultPagination
    {

        //static ReadOnlyCollection<IWebElement> resultPages;

        //static TopResultPagination()
        //{
        //    ReadOnlyCollection<IWebElement> searchResultpagination;
        //    searchResultpagination = Driver.Instance.FindElements(By.CssSelector("div.span12 div.row-fluid div.span9 div.row-fluid div div.pagination ul li"));
        //    //refactor this later and add driver.findelements method
        //    Driver.TurnOffWait();
        //    resultPages = searchResultpagination[1].FindElements(By.CssSelector("a[class='']"));
        //    Driver.TurnOnWait();
        //    //resultPages = searchResultpagination[2].FindElements(By.CssSelector("a[class='']"));
        //    //resultPages = searchResultpagination[1].FindElements(By.CssSelector("a[class='']"));

        //}
        
        public static int GenerateRandPageNo(int totalPages)
        {
            if (totalPages > 0)
            {
                Random r = new Random();
                int pageNo = r.Next(1, totalPages);
                return pageNo + 1;
            }
            else
                return totalPages;
        }

        public static int GetNumberOfPages()
        {
            ReadOnlyCollection<IWebElement> searchResultpagination = Driver.Instance.FindElements(By.CssSelector("div.span12 div.row-fluid div.span9 div.row-fluid div div.pagination ul li"));
            Driver.Instance.TurnOffWait();
            ReadOnlyCollection<IWebElement> resultPages = searchResultpagination[1].FindElements(By.CssSelector("a[class='']"));
            Driver.Instance.TurnOnWait();
            return resultPages.Count;
        }

        public static void SelectPage(int pageNo)
        {
            //ReadOnlyCollection<IWebElement> searchResultpagination = Driver.Instance.FindElements(By.CssSelector("div.span12 div.row-fluid div.span9 div.row-fluid div div.pagination ul li"));
            //ReadOnlyCollection<IWebElement> resultPages = searchResultpagination[1].FindElements(By.CssSelector("a[class='']"));
            if (pageNo > 0)
            {
                var Pagelink = Driver.Instance.FindElement(By.LinkText(Convert.ToString(pageNo)));
                FlightResultsPage.Data.ResultPageNo = pageNo;
                Pagelink.Click();
            }
            //foreach (IWebElement page in resultPages)
            //{
            //    if (page.Text == Convert.ToString(pageNo))
            //    {
            //        page.Click();
            //        break;
            //    }
            //}
        }
            
    }
}
