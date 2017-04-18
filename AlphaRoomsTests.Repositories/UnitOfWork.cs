using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaRoomsTests.DbContext;
using AlphaRoomsTests.Models;

namespace AlphaRoomsTests.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly IDbContext context;
        private BaseRepository<TestResult> testResultRepository;
        private bool disposed = false;

        public UnitOfWork()
        {
            if(this.context==null)
                this.context=new AlphaRoomsTestsContext();
        }

        public UnitOfWork(IDbContext context)
        {
            this.context = context;
        }
        public BaseRepository<Models.TestResult> TestResultRepository
        {
            get
            {
                if (testResultRepository == null)
                {
                    this.testResultRepository=new BaseRepository<TestResult>(context);
                }
                return testResultRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
