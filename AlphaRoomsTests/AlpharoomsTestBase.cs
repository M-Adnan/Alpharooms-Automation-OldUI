using System;
using System.Data.Common;
using System.Data.SqlClient;
using NUnit.Framework;
using AlphaRooms.AutomationFramework;
using AlphaRooms.AutomationFramework.Selenium;
using AlphaRooms.AutomationFramework.Tests.Properties;

namespace AlphaRooms.AutomationFramework.Tests
{
    public class AlpharoomsTestBase
    {
        [SetUp]
        public void Initialise()
        {
            //Initialise the driver
            Driver.Initialize(Settings.Default.SeleniumExecuteLocally, Settings.Default.SeleniumRemoteServerURL, Settings.Default.SeleniumBrowser);

            //Goto Alpharooms Homepage for a selected enviorment
            HomePage.Navigate((TestEnvironment)Enum.Parse(typeof(TestEnvironment), Settings.Default.TestingEnvironment));

            //Check if Home page is displayed
            Assert.That(HomePage.IsDisplayed, Is.True, "Home Page isn't displayed");
        }

        [TearDown]
        public void CleanUp()
        {
            //Close the browser
            Driver.Close();
        }
    }
}
