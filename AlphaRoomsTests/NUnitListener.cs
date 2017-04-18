using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SqlClient;
using NUnit.Core;
using NUnit.Core.Extensibility;
using AlphaRooms.AutomationFramework;
using AlphaRooms.AutomationFramework.Tests.Properties;
using AlphaRooms.AutomationFramework.Repositories;
using AlphaRooms.AutomationFramework.Log;

namespace AlphaRooms.AutomationFramework.Tests
{
    [NUnitAddinAttribute(Type = ExtensionType.Core, Name = "Database Addin", Description = "Writes test results to the database.")]
    public class NUnitListener : IAddin, EventListener
    {
        private TestResultRepository testResultRepository;

        public NUnitListener()
        {
            this.testResultRepository = new TestResultRepository(Settings.Default.AutomationFrameworkConnectionString);
        }

        public bool Install(IExtensionHost host)
        {
            IExtensionPoint listeners = host.GetExtensionPoint("EventListeners");
            if (listeners == null) return false;
            listeners.Install(this);
            return true;
        }

        public void RunFinished(Exception exception)
        {
        }

        public void RunFinished(TestResult result)
        {
        }

        public void RunStarted(string name, int testCount)
        {
        }

        public void SuiteFinished(TestResult result)
        {
        }

        public void SuiteStarted(TestName testName)
        {
        }

        public void TestFinished(TestResult result)
        {
            SaveTestResult(result);
        }

        public void TestOutput(TestOutput testOutput)
        {
        }

        public void TestStarted(TestName testName)
        {
        }

        public void UnhandledException(Exception exception)
        {
        }

        private void SaveTestResult(TestResult result)
        {
            try
            {
                string stepToReproduce = null;
                if (!result.IsSuccess) stepToReproduce = Logger.GetStepToReproduce();
                Models.TestResult testResult = new Models.TestResult
                {
                    IsResultError = result.IsError
                    , IsSuccess = result.IsSuccess
                    , TestFixture = result.Test.ClassName
                    , TestName = result.Name
                    , Time = result.Time
                    , AssertCount = result.AssertCount
                    , Message = result.Message
                    , IsExecuted = result.Executed
                    , StackTrace = result.StackTrace
                    , CreatedDate = DateTime.Now
                    , StepToReproduce = stepToReproduce
                    , SearchTime = HomePage.Data.SearchTime.TotalMilliseconds
                    , HotelResultPageLoadingTime = HotelResultsPage.Data.LoadingTime
                    , HotelName = HotelResultsPage.Data.HotelName
                    , HotelSearchGuid = HotelResultsPage.Data.HotelSearchGuid
                    , BasketTime = ExtrasPage.Data.LoadingTime.TotalMilliseconds
                    , TotalHotelSearchResults = HotelResultsPage.Data.TotalHotelSearchResults
                    , FlightResultPageLoadingTime = FlightResultsPage.Data.LoadingTime
                    , FlightSearchGuid = FlightResultsPage.Data.FlightSearchGuid
                    , OutboundAirline = FlightResultsPage.Data.OutboundAirline
                    , OutboundFlightNo = FlightResultsPage.Data.OutboundFlightNo
                    , InboundAirline = FlightResultsPage.Data.InboundAirline
                    , InboundFlightNo = FlightResultsPage.Data.InboundFlightNo
                    , BasketGuid = ExtrasPage.Data.BasketGUID
                    , TestingEnvironment = Settings.Default.TestingEnvironment
                    , TestingBrowser = Settings.Default.SeleniumBrowser
                };
                testResultRepository.Save(testResult);
            }
            catch (Exception exception)
            {
                Logger.Log(string.Format("Unable to save {0}.{1} results to the database due to an exception.", result.Test.ClassName.Split('.').Last(), result.Name), exception);
                throw exception;
            }
        }
    }
}
