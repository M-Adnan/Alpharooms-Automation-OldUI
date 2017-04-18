using AlphaRooms.AutomationFramework.Models;
using AlphaRooms.AutomationFramework.Repositories.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Repositories
{
    public class TestResultRepository
    {
        private string connectionString;

        public TestResultRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Save(TestResult testResult)
        {
            using (AutomationFrameworkDbContext context = new AutomationFrameworkDbContext(connectionString))
            {
                context.TestResults.Add(testResult);
                context.SaveChanges();
            }
        }
    }
}
