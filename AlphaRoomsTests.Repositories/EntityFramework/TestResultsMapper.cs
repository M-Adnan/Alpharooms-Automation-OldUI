using AlphaRooms.AutomationFramework.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Repositories.EntityFramework
{
    public class TestResultMapper : EntityTypeConfiguration<TestResult>
    {
        public TestResultMapper()
        {
            this.ToTable("TestResults");
            this.HasKey(t => t.Id);
            this.Property(i => i.TestFixture).HasColumnName("TestFixture").IsOptional();
            this.Property(i => i.TestName).HasColumnName("TestName").IsOptional();
            this.Property(i => i.IsSuccess).HasColumnName("IsSuccess").IsOptional();
            this.Property(i => i.IsResultError).HasColumnName("IsResultError").IsOptional();
            this.Property(i => i.Time).HasColumnName("Time").IsOptional();
            this.Property(i => i.AssertCount).HasColumnName("AssertCount").IsOptional();
            this.Property(i => i.Message).HasColumnName("Message").IsOptional();
            this.Property(i => i.HotelSearchGuid).HasColumnName("HotelSearchGuid").IsOptional();
            this.Property(i => i.HotelName).HasColumnName("HotelName").IsOptional();
            this.Property(i => i.BasketGuid).HasColumnName("BasketGuid").IsOptional();
            this.Property(i => i.IsExecuted).HasColumnName("IsExecuted").IsOptional();
            this.Property(i => i.StackTrace).HasColumnName("StackTrace").IsOptional();
            this.Property(i => i.StepToReproduce).HasColumnName("StepToReproduce").IsOptional();
            this.Property(i => i.SearchTime).HasColumnName("SearchTime").IsOptional();
            this.Property(i => i.BasketTime).HasColumnName("BasketTime").IsOptional();
            this.Property(i => i.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            this.Property(i => i.HotelResultPageLoadingTime).HasColumnName("HotelResultPageLoadingTime").IsOptional();
            this.Property(i => i.FlightResultPageLoadingTime).HasColumnName("FlightResultPageLoadingTime").IsOptional();
            this.Property(i => i.TotalHotelSearchResults).HasColumnName("TotalHotelSearchResults").IsOptional();
            this.Property(i => i.OutboundAirline).HasColumnName("OutboundAirline").IsOptional();
            this.Property(i => i.OutboundFlightNo).HasColumnName("OutboundFlightNo").IsOptional();
            this.Property(i => i.InboundAirline).HasColumnName("InboundAirline").IsOptional();
            this.Property(i => i.InboundFlightNo).HasColumnName("InboundFlightNo").IsOptional();
            this.Property(i => i.FlightSearchGuid).HasColumnName("FlightSearchGuid").IsOptional();
            this.Property(i => i.TestingEnvironment).HasColumnName("TestingEnvironment").IsOptional();
            this.Property(i => i.TestingBrowser).HasColumnName("TestingBrowser").IsOptional();
        }
    }
}
