using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;          // ChromeDriver
using OpenQA.Selenium.Firefox;         // FirefoxDriver
using OpenQA.Selenium.IE;              // InternetExplorerDriver
using Simple.Selenium.Controls;        // DefaultWebDriverHelper

namespace Simple.Selenium.Pages
{

    public enum BrowserType
    {
        None,
        Chrome,
        Firefox,
        IE
    }

    /// <summary>
    /// This class provides helper methods to launch and terminate web browsers.
    /// These methods also sets and clears the Default WebDriver instance used by all UI control and page classes
    /// if a WebDriver instance is not given to their constructor.
    /// </summary>
    ///
    /// <example>
    /// The following code launches and terminates a Chrome browser:
    /// <code>
    ///   using Simple.Selenium.Pages;
    ///
    ///   IWebDriver webDriver = null;
    ///   try
    ///   {
    ///       webDriver = BrowserHelper.LaunchBrowser(BrowserType.Chrome);
    ///       // Do something.
    ///   }
    ///   finally
    ///   {
    ///       if (webDriver != null)
    ///           BrowserHelper.CloseBrowser(webDriver);
    ///   }
    /// </code>
    /// </example>
    public static class BrowserHelper
    {

        /// <summary>
        /// Launches a new web browser instance/window according to the specified browser type.
        /// If it succeeds, it returns the <seealso cref="IWebDriver"/> instance that controls this open web browser instance.
        /// Note: The caller of this method is responsible for closing the opened web browser by calling the <seealso cref="IWebDriver.Quit"/> method on the returned WebDriver instance.
        /// </summary>
        /// <param name="browserType">  browser type </param>
        /// <returns> the WebDriver instance that controls the launched web browser </returns>
        /// <exception cref="WebDriverException"> if it cannot opens a new web browser instance </exception>
        public static IWebDriver LaunchBrowser(BrowserType browserType)
        {
            IWebDriver webDriver;
            switch (browserType)
            {
                case BrowserType.Chrome:
                    webDriver = new ChromeDriver();
                    break;
                case BrowserType.Firefox:
                    webDriver = new FirefoxDriver();
                    break;
                case BrowserType.IE:
                    webDriver = new InternetExplorerDriver();
                    break;
                default:
                    throw new ArgumentException("Unsupported browser type: " + browserType);
            }

            // Set the global default WebDriver if it is not set.
            DefaultWebDriverHelper.SetDefaultWebDriverIfNotSet(webDriver);

            return webDriver;
        }

        public static void CloseBrowser(IWebDriver webDriver)
        {
            webDriver.Quit();
            if (Object.ReferenceEquals(webDriver, DefaultWebDriverHelper.GetDefaultWebDriver()))
            {
                DefaultWebDriverHelper.ClearDefaultWebDriver();
            }
        }
    }

}
