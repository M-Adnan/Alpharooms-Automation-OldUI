using System;
using OpenQA.Selenium;
using System.Drawing.Imaging;
using System.Diagnostics;
using AlphaRooms.AutomationFramework.Selenium;
using System.Threading;

namespace AlphaRooms.AutomationFramework.Functions
{
    public class NonFunctionalReq
    {
        private static int imageCounter = 1;

        public static void GetScreenShot(string pageName)
        {
            //capture the screenshot
            Screenshot SS = ((ITakesScreenshot)Driver.Instance).GetScreenshot();
            //save the screen shot
            SS.SaveAsFile(string.Format("C:\\Automated Testing\\ScreenShots\\{0} {1}.jpg", NonFunctionalReq.imageCounter, pageName), ImageFormat.Jpeg);
            NonFunctionalReq.imageCounter++;

        }
        
        public static TimeSpan CaptureTime(Action action, String logMsg)
        {
            Stopwatch watch = StartTime();

            action();
            
            StopTime(watch);

            //Logger.Write(string.Format("{0} {1} ",logMsg, watch.Elapsed));

            return watch.Elapsed;
        }

        //public static void CaptureTimeAndGUID(Action action, String logMsg1, string logMsg2)
        //{
        //    Stopwatch watch = StartTime();

        //    action();

        //    StopTime(watch);

        //    string url = Driver.Instance.Url;
        //    string searchID = url.Split(new char[] { '=', '&' })[1];

        //    Logger.Write(string.Format("{0} {1} ", logMsg1, watch.Elapsed));
        //    Logger.Write(string.Format("{0} {1} ", logMsg2, searchID));
        //}

        public static Stopwatch StartTime()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            return watch;
        }

        public static void StopTime(Stopwatch watch)
        {
            watch.Stop();
        }

        public static void ExecuteAndRetry(Action action, string message)
        {
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    action.Invoke();
                    return;
                }
                catch
                {
                }
                Thread.Sleep(1000);
            }
            throw new Exception(message);
        }
    }
}
