using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AlphaRooms.AutomationFramework;
using AlphaRooms.AutomationFramework.Tests.Properties;

namespace AlphaRooms.AutomationFramework.Tests.Flight_SOA
{
    //[TestFixture]
    //class DataExtraction
    //{
    //    [Test]
    //    [Category("Data_Extraction")]
    //    public void ShouldExtract_TestCaseDataFromInssuranceProductTestSpec()
    //    {
    //        Excel.Application xlApp;
    //        Excel.Workbook xlWorkBook;
    //        Excel.Worksheet xlWorkSheet;
    //        Excel.Range range;

    //        string InputData, Destination,Travellers,AdultAges,Children,ChildrenAges,Relationship,StartDate,EndDate;
    //        string ExpectedOutput, Standard_Single, Standard_Multi, Premier_Single, Premier_Multi, PremierPlus_Single, PremierPlus_Multi;
    //        int rowCnt = 0;
      
    //        xlApp = new Excel.Application();

    //        //open excel file
    //        xlWorkBook = xlApp.Workbooks.Open(@"J:\IT Dept\QA Team\Projects\Insurance Product\Insurance Product Test Spec DataExtraction.xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

    //        //Get the worksheet for that 
    //        xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

    //        //Gives the used cells in the sheet
    //        range = xlWorkSheet.UsedRange;

    //        for (rowCnt = 3; rowCnt <= range.Rows.Count; rowCnt++)
    //        {
    //            //Driver.Initialise(Settings.Default.SeleniumExecuteLocally, Settings.Default.SeleniumRemoteServerURL);

    //            //Goto Alpharooms Homepage for a selected enviorment
    //            //HomePage.Navigate((TestEnvironment)Enum.Parse(typeof(TestEnvironment), Settings.Default.TestingEnvironment));

    //            //Check if Home page is displayed
    //            //Assert.That(HomePage.IsDisplayed, Is.True, "Home Page isn't displayed");

    //            InputData = ((Excel.Range)range.Cells[rowCnt, 2]).Value2;
    //            ExpectedOutput = ((Excel.Range)range.Cells[rowCnt, 5]).Value2;

    //            Dictionary<string,string> inputValues  = new Dictionary<string,string>();

    //            foreach (string item in InputData.Split('\n'))
    //            {
    //                string[] attribute = item.Split(':');
    //                inputValues.Add(attribute[0].Trim(), attribute[1].Trim());
    //            }

    //            //Get the string from the sheet
    //            Destination = GetValueOrNull(inputValues, "Destination");
    //            Travellers = GetValueOrNull(inputValues, "Travellers");
    //            AdultAges = GetValueOrNull(inputValues, "Adult Ages");
    //            Children = GetValueOrNull(inputValues, "Children");
    //            ChildrenAges = GetValueOrNull(inputValues, "Children Age");
    //            Relationship = GetValueOrNull(inputValues, "Relationship");
    //            StartDate = GetValueOrNull(inputValues, "Start Date");
    //            EndDate = GetValueOrNull(inputValues, "End Date");


    //            Dictionary<string, string> outputValues = new Dictionary<string, string>();

    //            foreach (string item in ExpectedOutput.Split('\n'))
    //            {
    //                string[] attribute = item.Split(':');
    //                string[] attribute1 = attribute[1].Split(' ');
    //                inputValues.Add(attribute[0].Trim(), attribute1[0].Trim());
    //            }

    //            //Get the string from the sheet
    //            Standard_Single = GetValueOrNull(outputValues, "Standard Single");
    //            Standard_Multi = GetValueOrNull(outputValues, "Standard Multi");
    //            Premier_Single = GetValueOrNull(outputValues, "Premier Single");
    //            Premier_Multi = GetValueOrNull(outputValues, "Premier Multi");
    //            PremierPlus_Single = GetValueOrNull(outputValues, "Premier Plus Single");
    //            PremierPlus_Multi = GetValueOrNull(outputValues, "Premier Plus Multi");



    //            /*HomePage.SearchFor().FlightOnly().ToDestination(Destination).FromCheckIn(Departure_Date)
    //                .ToCheckOut(Return_Date).FromDepartureAirport(Departure_Airport)
    //                .ForAdults(Adults).SearchAndCapture();*/

    //            //Driver.Close();
    //        }

    //        xlWorkBook.Close(true, null, null);
    //        xlApp.Quit();
    //    }

    //    private string GetValueOrNull(Dictionary<string, string> values, string key)
    //    {
    //        string value = null;
    //        bool exists = values.TryGetValue(key, out value);
    //        return value;
    //    }
    //}
}
