using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using AlphaRooms.AutomationFramework.Log;

namespace AlphaRooms.AutomationFramework
{
    public static class Logger
    {
        private static List<LogAction> actions = new List<LogAction>();

        public static void AddClickAction(string description)
        {
            Logger.actions.Add(new LogAction(ActionTypes.Click, description));
        }

        public static void AddClickAction(string description, string property, object value)
        {
            Logger.actions.Add(new LogAction(ActionTypes.Click, description, new Parameter[] { new Parameter(property, value)}));
        }

        public static void AddClickAction(string description, string property, object value, string property2, object value2)
        {
            Logger.actions.Add(new LogAction(ActionTypes.Click, description, new Parameter[] { new Parameter(property, value), new Parameter(property2, value2)}));
        }

        public static void AddCheckAction(string description)
        {
            Logger.actions.Add(new LogAction(ActionTypes.Check, description));
        }

        public static void AddCheckAction(string property, object value)
        {
            Logger.actions.Add(new LogAction(ActionTypes.Check, null, new Parameter[] { new Parameter(property, value) }));
        }

        public static void AddUncheckAction(string description)
        {
            Logger.actions.Add(new LogAction(ActionTypes.Uncheck, description));
        }

        public static void AddTypeAction(string property, object value)
        {
            Logger.actions.Add(new LogAction(ActionTypes.Type, null, new Parameter[] { new Parameter(property, value) }));
        }

        public static void AddTypeAction(string description, string property, object value, string property2, object value2)
        {
            Logger.actions.Add(new LogAction(ActionTypes.Type, null, new Parameter[] { new Parameter(property, value), new Parameter(property2, value2) }));
        }

        public static void AddTypeAction(string description, string property, object value, string property2, object value2, string property3, object value3)
        {
            Logger.actions.Add(new LogAction(ActionTypes.Type, null, new Parameter[] { new Parameter(property, value), new Parameter(property2, value2), new Parameter(property3, value3) }));
        }

        public static void AddSelectAction(string property, object value)
        {
            Logger.actions.Add(new LogAction(ActionTypes.Select, null, new Parameter[] { new Parameter(property, value) }));
        }

        public static void AddSelectAction(string description, string property, object value, string property2, object value2)
        {
            Logger.actions.Add(new LogAction(ActionTypes.Select, null, new Parameter[] { new Parameter(property, value), new Parameter(property2, value2) }));
        }

        public static void AddSelectAction(string description, string property, object value, string property2, object value2, string property3, object value3)
        {
            Logger.actions.Add(new LogAction(ActionTypes.Select, null, new Parameter[] { new Parameter(property, value), new Parameter(property2, value2), new Parameter(property3, value3) }));
        }

        public static void Clear()
        {
            actions.Clear();
        }

        public static string GetStepToReproduce()
        {
            StringBuilder stepToReproduce = new StringBuilder();
            foreach (LogAction action in actions)
            {
                action.WriteTo(stepToReproduce);
                stepToReproduce.Append("\r\n");
            }
            if (actions.Count > 0) stepToReproduce.Length -= 2;
            return stepToReproduce.ToString();
        }
        
        public static void Log(string message, Exception exception)
        {
            string logName = "AlphaRooms AutomationFramework";
            string source = logName;
            string msg = string.Format("{0}\r\nError: {1}\r\nStack Trace: {2}", message, exception.Message, exception.StackTrace);
            if (!EventLog.SourceExists(source)) EventLog.CreateEventSource(source, logName);
            EventLog.WriteEntry(source, msg, EventLogEntryType.Error);
        }
    }
}
