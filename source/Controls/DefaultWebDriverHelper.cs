using System;
using OpenQA.Selenium;

namespace Simple.Selenium.Controls
{

    public static class DefaultWebDriverHelper
    {
        // The Default WebDriver is thread-local, i.e., each thread has its own Default WebDriver instance.
        [ThreadStatic] private static IWebDriver s_defaultWebDriver;

        /// <summary>
        /// The default WebDriver to be used if a UI control is not given a WebDriver instance when it is constructed.
        /// It must be explicitly set before such UI controls are used.
        /// Note that a UI control that is created with a web element will not use this default WebDriver. 
        /// </summary>
        public static IWebDriver GetDefaultWebDriver()
        {
            if (s_defaultWebDriver == null)
            {
                throw new Exception("The Default WebDriver for the Simple Selenium Web UI Automation is not set.  You must use Simple.Selenium.Controls.DefaultWebDriverHelper.SetDefaultWebDriverIfNotSet() to set it before using any Web UI Controls.");
            }
            return s_defaultWebDriver;
        }

        public static void SetDefaultWebDriverIfNotSet(IWebDriver webDriver)
        {
            if (s_defaultWebDriver == null)
            {
                s_defaultWebDriver = webDriver;
            }
        }

        public static void ClearDefaultWebDriver()
        {
            s_defaultWebDriver = null;
        }

    }

}
