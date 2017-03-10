using System;
using OpenQA.Selenium;

namespace Simple.Selenium.Pages
{

    /// <summary>
    /// Base class for representing and interacting with a web application using the page object model.
    /// A subclass should be defined for each specific web application
    /// to simulate its application-specific UI behaviors (e.g., logging in).
    /// </summary>
    ///
    /// <example>
    /// <code>
    ///   using Simple.Selenium.Pages;
    ///
    ///   public class MyApp : BaseApp {
    ///       static string appURL = "http://mywebapp";
    ///       LoginPage LoginPage = new LoginPage();
    ///       MainPage MainPage = new MainPage();
    /// 
    ///       public MyApp(BrowserType browserType) : base(browserType, appURL)
    ///       {
    ///       }
    ///
    ///       public bool LogIn(string username, string password) {
    ///           if (LoginPage.IsAvailable()) {
    ///               LoginPage.LogIn(username, password);
    ///               if (MainPage.IsAvailable()) {
    ///                   return true;
    ///               } else {
    ///                   // Error: Login failed.
    ///                   return false;
    ///               }
    ///           } else {
    ///               // Error: Login page not available.
    ///               return false;
    ///           }
    ///       }
    ///
    ///       public void DoSomething() {
    ///           ...
    ///       }
    ///   }
    ///
    ///   // See the <seealso cref="BasePage"/> documentation for the LoginPage class example.
    ///
    ///   // Launch a web browser and navigate to the home page of MyApp.
    ///   using (MyApp app = new MyApp(BrowserType.IE)) {
    ///       if (app.LogIn("username", "password")) {
    ///           app.DoSomething();
    ///           ....
    ///       }
    ///       // The web browser will be automatically closed after this line.
    ///   }
    /// </code> 
    /// </example>
    public class BaseApp : IDisposable
    {
        private readonly bool m_webDriverOwnedByMe;

        /// <summary>
        /// Returns the WebDriver instance used by this web application.
        /// </summary>
        /// <returns> the <seealso cref="IWebDriver"/> instance used by this web application </returns>
        public readonly IWebDriver WebDriver;

        /// <summary>
        /// Constructs a base web application object with a WebDriver instance and starting URL.
        /// It will drive the web browser referenced by the given WebDriver to navigate to the starting page of the web application.
        /// If the web browser is already opened, it will reuse the currently opened web browser instance.
        /// </summary>
        /// <param name="webDriver">  The WebDriver (i.e., browser) instance used by this web application; it must not be null. </param>
        /// <param name="startingUrl">  (Optional) The URL to navigate to when this web application is launched </param>
        /// <exception cref="ArgumentNullException"> if the specified <code>webDriver</code> is <code>null</code> </exception>
        /// <exception cref="WebDriverException"> if it cannot communicate with the browser instance or navigate to the given starting URL. </exception>
        protected BaseApp(IWebDriver webDriver, string startingUrl = null)
        {
            WebDriver = webDriver;
            m_webDriverOwnedByMe = false;
            if (!String.IsNullOrWhiteSpace(startingUrl))
                NavigateTo(startingUrl);
        }

        /// <summary>
        /// Constructs a base web application object with the browser type to use and the starting URL.
        /// It will open a new web browser instance/window and navigates to the starting page of the web application.
        /// If this is the first time a web browser is launched through the WebUI Automation Framework,
        /// it will also set the global default WebDriver to point to this web browser.
        /// </summary>
        /// <param name="browserType">  The type of the web browser to be used to launch this web application. </param>
        /// <param name="startingUrl">  (Optional) The URL to navigate to when this web application is launched </param>
        /// <exception cref="ArgumentException"> if the given browser type is invalid or is not supported </exception>
        /// <exception cref="WebDriverException"> if it cannot opens a new browser instance, communicate with the browser instance, or navigate to the given starting URL. </exception>
        protected BaseApp(BrowserType browserType, string startingUrl = null)
        {
            WebDriver = BrowserHelper.LaunchBrowser(browserType);
            m_webDriverOwnedByMe = true;
            if (!String.IsNullOrWhiteSpace(startingUrl))
                NavigateTo(startingUrl);
        }

        /// <summary>
        /// Disposes this web application.
        /// If this web application had launched its own web browser instance, it will close its launched web browser.
        /// </summary>
        public virtual void Dispose()
        {
            if (m_webDriverOwnedByMe)
            {
                BrowserHelper.CloseBrowser(WebDriver);
            }
        }

        /// <summary>
        /// Returns the name of this web application.
        /// It is default to the simple class name of a derived class.
        /// </summary>
        /// <returns> the name of this web application </returns>
        public virtual string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public virtual void NavigateTo(string url)
        {
            WebDriver.Navigate().GoToUrl(url);
        }
    }

}