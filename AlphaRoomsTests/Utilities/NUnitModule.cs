using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Tests.Utilities
{
    public static class NUnitModule
    {
        public static TestCaseData CreateTestCaseData(object data, string testName, string testDescription)
        {
            TestCaseData testData = new TestCaseData(data);
            testData.SetName(testName);
            testData.SetDescription(testDescription);
            return testData;
        }
    }
}
