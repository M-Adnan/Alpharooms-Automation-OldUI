using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
//using NUnit.Framework;

namespace AlphaRooms.AutomationFramework.Selenium
{
    public static class Driver
    {
        public static RemoteWebDriver Instance { get; private set; }
        public static bool WaitOff { get; internal set; }

        public static void Initialize(bool isExecuteLocally, string remoteServerURL, string browser)
        {
            if (isExecuteLocally)
            {
                switch (browser.Trim().ToLower()) {
                    case "firefox":
                        Instance = new FirefoxDriver();
                        /*DesiredCapabilities capabilities = new DesiredCapabilities();
                        //capabilities = DesiredCapabilities.Firefox();
                        //capabilities.SetCapability(CapabilityType.BrowserName, "firefox");
                        //capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));
                        //capabilities.SetCapability(CapabilityType.Version, "29.0.1");

                        //Instance = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), capabilities);
                        //driver = new FirefoxDriver();
                        //baseURL = "https://www.google.co.in/";
                        //verificationErrors = new StringBuilder();*/
                        break;
                    case "chrome":
                        Instance = new ChromeDriver("C:\\Drivers\\");
                        break;
                    case "ie":
                        Instance = new InternetExplorerDriver("C:\\Drivers\\");
                        break;

                    default:
                        throw new Exception(string.Format("Browser {0} unknown", browser));
                }
            }
            else
            {
                switch (browser.Trim().ToLower())
                {
                    case "firefox":
                        Instance = new RemoteWebDriver(new Uri(remoteServerURL), DesiredCapabilities.Firefox());
                        /*DesiredCapabilities capabilities = new DesiredCapabilities();
                        capabilities = DesiredCapabilities.Firefox();
                        capabilities.SetCapability(CapabilityType.BrowserName, "firefox");
                        capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));
                        capabilities.SetCapability(CapabilityType.Version, "15.0");

                        Instance = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), capabilities);
                        //driver = new FirefoxDriver();
                        /*baseURL = "https://www.google.co.in/";*/
                        //verificationErrors = new StringBuilder();*/
                        break;
                    case "chrome":
                        Instance = new RemoteWebDriver(new Uri(remoteServerURL), DesiredCapabilities.Chrome());
                        break;
                    case "ie":
                        Instance = new RemoteWebDriver(new Uri(remoteServerURL), DesiredCapabilities.InternetExplorer());
                        break;

                    default:
                        throw new Exception(string.Format("Browser {0} unknown", browser));
                }
            }

            //Turn on explicit wait
            Instance.Manage().Window.Maximize();
            WaitOff = true;
            Instance.TurnOnWait();
        }

        public static void Close()
        {
            //close the browser
            Instance.Quit();
        }
    }
}
