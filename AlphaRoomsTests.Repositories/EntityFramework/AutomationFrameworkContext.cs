using AlphaRooms.AutomationFramework.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Repositories.EntityFramework
{
    public class AutomationFrameworkDbContext : DbContext
    {
        static AutomationFrameworkDbContext()
        {
            Database.SetInitializer<AutomationFrameworkDbContext>(null);
        }

        public AutomationFrameworkDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<TestResult> TestResults { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TestResultMapper());
            base.OnModelCreating(modelBuilder);
        }

        public int TestResultsInsert(TestResult testResult)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("TestResultsInsert"
                , testResult.TestFixture != null ? new ObjectParameter("TestFixture", testResult.TestFixture) : new ObjectParameter("TestFixture", typeof(string))
                , testResult.TestName != null ? new ObjectParameter("TestName", testResult.TestName) : new ObjectParameter("TestName", typeof(string))
                , testResult.IsSuccess.HasValue ? new ObjectParameter("IsSuccess", testResult.IsSuccess) : new ObjectParameter("IsSuccess", typeof(bool))
                , testResult.IsResultError.HasValue ? new ObjectParameter("IsResultError", testResult.IsResultError) : new ObjectParameter("IsResultError", typeof(bool))
                , testResult.Time.HasValue ? new ObjectParameter("Time", testResult.Time) : new ObjectParameter("Time", typeof(long))
                , testResult.AssertCount.HasValue ? new ObjectParameter("AssertCount", testResult.AssertCount) : new ObjectParameter("AssertCount", typeof(int))
                , testResult.Message != null ? new ObjectParameter("Message", testResult.Message) : new ObjectParameter("Message", typeof(string))
                , testResult.HotelSearchGuid != null ? new ObjectParameter("HotelSearchGuid", testResult.HotelSearchGuid) : new ObjectParameter("HotelSearchGuid", typeof(string))
                , testResult.HotelName != null ? new ObjectParameter("HotelName", testResult.HotelName) : new ObjectParameter("HotelName", typeof(string))
                , testResult.BasketGuid != null ? new ObjectParameter("BasketGuid", testResult.BasketGuid) : new ObjectParameter("BasketGuid", typeof(string))
                , testResult.IsExecuted.HasValue ? new ObjectParameter("IsExecuted", testResult.IsExecuted) : new ObjectParameter("IsExecuted", typeof(bool))
                , testResult.StackTrace != null ? new ObjectParameter("StackTrace", testResult.StackTrace) : new ObjectParameter("StackTrace", typeof(string))
                , testResult.StepToReproduce != null ? new ObjectParameter("StepToReproduce", testResult.StepToReproduce) : new ObjectParameter("StepToReproduce", typeof(string))
                , testResult.SearchTime.HasValue ? new ObjectParameter("SearchTime", testResult.SearchTime) : new ObjectParameter("SearchTime", typeof(long))
                , testResult.BasketTime.HasValue ? new ObjectParameter("BasketTime", testResult.BasketTime) : new ObjectParameter("BasketTime", typeof(long))
                , testResult.CreatedDate.HasValue ? new ObjectParameter("CreatedDate", testResult.CreatedDate) : new ObjectParameter("CreatedDate", typeof(System.DateTime))
                , testResult.HotelResultPageLoadingTime.HasValue ? new ObjectParameter("HotelResultPageLoadingTime", testResult.HotelResultPageLoadingTime) : new ObjectParameter("HotelResultPageLoadingTime", typeof(TimeSpan))
                , testResult.FlightResultPageLoadingTime.HasValue ? new ObjectParameter("FlightResultPageLoadingTime", testResult.FlightResultPageLoadingTime) : new ObjectParameter("FlightResultPageLoadingTime", typeof(TimeSpan))
                , testResult.TotalHotelSearchResults.HasValue ? new ObjectParameter("TotalHotelSearchResults", testResult.TotalHotelSearchResults) : new ObjectParameter("TotalHotelSearchResults", typeof(int))
                , testResult.OutboundAirline != null ? new ObjectParameter("OutboundAirline", testResult.OutboundAirline) : new ObjectParameter("OutboundAirline", typeof(string))
                , testResult.OutboundFlightNo != null ? new ObjectParameter("OutboundFlightNo", testResult.OutboundFlightNo) : new ObjectParameter("OutboundFlightNo", typeof(string))
                , testResult.InboundAirline != null ? new ObjectParameter("InboundAirline", testResult.InboundAirline) : new ObjectParameter("InboundAirline", typeof(string))
                , testResult.InboundFlightNo != null ? new ObjectParameter("InboundFlightNo", testResult.InboundFlightNo) : new ObjectParameter("InboundFlightNo", typeof(string))
                , testResult.FlightSearchGuid != null ? new ObjectParameter("FlightSearchGuid", testResult.FlightSearchGuid) : new ObjectParameter("FlightSearchGuid", typeof(string))
                , testResult.TestingEnvironment != null ? new ObjectParameter("TestingEnvironment", testResult.TestingEnvironment) : new ObjectParameter("TestingEnvironment", typeof(string))
                , testResult.TestingBrowser != null ? new ObjectParameter("TestingBrowser", testResult.TestingBrowser) : new ObjectParameter("TestingBrowser", typeof(string))
                );
        }
    }
}
