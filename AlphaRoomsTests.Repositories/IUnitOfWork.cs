using AlphaRoomsTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRoomsTests.Repositories
{
    public interface IUnitOfWork:IDisposable
    {
        BaseRepository<TestResult> TestResultRepository { get; }
        void Save();
    }
}
