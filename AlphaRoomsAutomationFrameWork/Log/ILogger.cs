using System;

namespace AlphaRooms.AutomationFramework.Log
{
    public interface ILogger
    {
        void AddCheckAction(string description);
        void AddCheckAction(string property, object value);
        void AddClickAction(string description);
        void AddClickAction(string description, string property, object value);
        void AddClickAction(string description, string property, object value, string property2, object value2);
        void AddSelectAction(string description, string property, object value, string property2, object value2);
        void AddSelectAction(string description, string property, object value, string property2, object value2, string property3, object value3);
        void AddSelectAction(string property, object value);
        void AddTypeAction(string description, string property, object value, string property2, object value2);
        void AddTypeAction(string description, string property, object value, string property2, object value2, string property3, object value3);
        void AddTypeAction(string property, object value);
        void AddUncheckAction(string description);
        void Clear();
        string GetStepToReproduce();
        void Log(string message, Exception exception);
    }
}
