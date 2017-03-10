using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;   // ExpectedConditions
using Simple.Selenium.Controls;     // DefaultWebDriverHelper
using Simple.Selenium.Extensions;   // WebDriverExtensions

namespace Simple.Selenium.Pages
{

    /// <summary>
    /// Base class for representing and interacting with a web page using the page object model.
    /// A subclass should be defined for each specific web page
    /// to simulate its application-specific UI behaviors (e.g., logging in).
    /// </summary>
    /// 
    /// <example>
    /// <code>
    ///   using OpenQA.Selenium;
    ///   using Simple.Selenium.Controls;
    ///   using Simple.Selenium.Pages;
    ///
    ///   public class LoginPage : BasePage<LoginPage> {
    ///       TextField UserNameTextField = new TextField(By.Id("username"));
    ///       TextField PasswordTextField = new TextField(By.Id("password"));
    ///       Button    LogInButton       = new Button(By.Id("login"));
    ///
    ///       public LoginPage()
    ///       {
    ///           // Use the visibility of the User Name text field to determine the availability of this page.
    ///           IndicatingLocator = UserNameTextField.Locator;
    ///       }
    ///
    ///       public void LogIn(string username, string password) {
    ///           UserNameTextField.EnterText(username);
    ///           PasswordTextField.EnterText(password);
    ///           LogInButton.Click();
    ///       }
    ///   }
    ///
    ///   LoginPage loginPage = new LoginPage();
    ///   loginPage.WaitUntilAvailable().LogIn("username", "password");
    /// </code>
    public abstract class BasePage
    {
        private readonly IWebDriver m_webDriver;
        /// <summary>
        /// Returns the WebDriver instance used by this page.
        /// It is default to <seealso cref="WebUIGlobals.DefaultWebDriver"/> if this page object is constructed without given a WebDriver instance.
        /// </summary>
        /// <returns> the <seealso cref="IWebDriver"/> instance used by this page </returns>
        public virtual IWebDriver WebDriver
        {
            get
            {
                // If m_webDriver is not set, use the global default WebDriver.
                return m_webDriver ?? DefaultWebDriverHelper.GetDefaultWebDriver();
            }
        }

        /// <summary>
        /// The page loading timeout used by this web page,
        /// default to <seealso cref="WebUISettings.DefaultPageLoadingTimeoutInSeconds"/>.
        /// </summary>
        public virtual TimeSpan PageLoadingTimeout { get; set; }

        /// <summary>
        /// The timeout used in waiting for this page to close;
        /// default to <seealso cref="WebUISettings.DefaultClosingTimeoutInSeconds"/>.
        /// </summary>
        public virtual TimeSpan ClosingTimeout { get; set; }

        /// <summary>
        /// Constructs a base page without a <seealso cref="By"/> locator.
        /// A derived class must set the Locator property before using a page object.
        /// </summary>
        protected BasePage(IWebDriver webDriver = null)
        {
            m_webDriver = webDriver;
            PageLoadingTimeout = TimeSpan.FromSeconds(WebUISettings.DefaultPageLoadingTimeoutInSeconds);
            ClosingTimeout = TimeSpan.FromSeconds(WebUISettings.DefaultClosingTimeoutInSeconds);
        }

        /// <summary>
        /// Returns the name of this web page.
        /// It is default to the simple class name of the most derived class.
        /// </summary>
        /// <returns> the name of this web page </returns>
        public virtual string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

    }

    public abstract class BasePage<T> : BasePage where T : BasePage<T>
    {
        /// <summary>
        /// Returns or sets the <seealso cref="By"/> locator used by this page to detremine
        /// whether or not this page exists and is visible.
        /// Note: The locator can only be set by a derived class.
        /// </summary>
        public virtual By IndicatingLocator { get; protected set; }

        /// <summary>
        /// Constructs a base web page object that uses an indicating element to determine the availability of the page.
        /// Note: If the <code>indicatingLocator</code> is not given in the constructor,
        /// the <seealso cref="IndicatingLocator"/> property needs to be set before a <code>WaitUntilXXX()</code> method can be called.
        /// <param name="indicatingLocator">  (Optional) The <seealso cref="By"/> locator for locating a web element
        /// that can indicate whether or not the page exists and is visible </param>
        /// <param name="webDriver">  (Optional) The WebDriver (i.e., browser) that hosts this web page.
        ///     If it is null, the global default WebDriver, defined by <seealso cref="WebUISettings.DefaultWebDriver"/>,
        ///     will be used (when this web page is used). </param>
        /// </summary>
        protected BasePage(By indicatingLocator = null, IWebDriver webDriver = null) : base(webDriver)
        {
            IndicatingLocator = indicatingLocator;
        }

        protected void ThrowIfIndicatingLocatorNull()
        {
            if (IndicatingLocator == null)
            {
                throw new System.NullReferenceException("The IndicatingLocator of this page object is not set." + " You must set it (preferred in your page class constructor) before using this page.");
            }
        }

        /// <summary>
        /// Returns whether this web page becomes available or not within the default page loading timeout,
        /// which is specified and can be configured by <seealso cref="WebUISettings.DefaultPageLoadingTimeoutInSeconds"/>.
        /// A page is considered available if the web browser can use the locator of this page
        /// to locate a visible <seealso cref="IWebElement"/> within the specified timeout.
        /// </summary>
        /// <returns> <code>true</code> if this web page is available within the default page loading timeout;
        ///         <code>false</code> otherwise </returns>
        public virtual bool IsAvailable()
        {
            return IsAvailable(PageLoadingTimeout);
        }

        /// <summary>
        /// Returns whether this web page is available or not within the specified timeout.
        /// A page is considered available if the web browser can use the locator of this page
        /// to locate a visible <seealso cref="IWebElement"/> within the specified timeout.
        /// If the specified timeout is 0, it will check the current availability of this web page.
        /// If the specified timeout is greater than 0, it will periodically (every half second by default)
        /// check the existence of this web page until the specified timeout is reached.
        /// </summary>
        /// <param name="timeout">  timeout in waiting for the page to become available </param>
        /// <returns> <code>true</code> if this web page is available within the specified timeout;
        ///         <code>false</code> otherwise </returns>
        public virtual bool IsAvailable(TimeSpan timeout)
        {
            try
            {
                WaitUntilAvailable(timeout);
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }


        public virtual bool IsNotAvailable()
        {
            return IsNotAvailable(ClosingTimeout);
        }

        public virtual bool IsNotAvailable(TimeSpan timeout)
        {
            try
            {
                WaitUntilNotAvailable(timeout);
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Waits until this page is available (i.e., its key element is visible), or until the default page-loading timeout is reached.
        /// The default page-loading timeout is determined and can be configured by <seealso cref="WebUISettings.DefaultPageLoadingTimeoutInSeconds"/>.
        /// 
        /// A page is considered available if the browser can use the locator of this page to locate a visible <seealso cref="WebElement"/>.
        /// </summary>
        /// <returns> this page itself (for supporting the fluid interface) </returns>
        /// <exception cref="WebDriverTimeoutException"> if the page is still not available after the default page-loading timeout expires  </exception>
        public virtual T WaitUntilAvailable()
        {
            return WaitUntilAvailable(PageLoadingTimeout);
        }

        /// <summary>
        /// Waits until this page becomes available (i.e., its key element is visible), or until the specified timeout is reached.
        /// 
        /// A page is considered available if the browser can use the locator of this page to locate a visible <seealso cref="WebElement"/>.
        /// </summary>
        /// <param name="timeoutInSeconds">  time out in seconds </param>
        /// <returns> this page itself (for supporting the fluid interface) </returns>
        /// <exception cref="WebDriverTimeoutException"> if the page is still not available after the specified timeout is reached  </exception>
        public virtual T WaitUntilAvailable(TimeSpan timeout)
        {
            ThrowIfIndicatingLocatorNull();
            string timeOutMessage = "Waiting for " + this.Name + " to become available (visible).";
            WebDriver.WaitUntil(
                ExpectedConditions.ElementIsVisible(IndicatingLocator),
                timeout,
                timeOutMessage);
            return (T)this;
        }

        public virtual T WaitUntilNotAvailable()
        {
            return WaitUntilNotAvailable(ClosingTimeout);
        }

        public virtual T WaitUntilNotAvailable(TimeSpan timeout)
        {
            ThrowIfIndicatingLocatorNull();
            string timeOutMessage = "Waiting for " + this.Name + " to become unavailable (invisible).";
            WebDriver.WaitUntil(
                ExpectedConditions.InvisibilityOfElementLocated(IndicatingLocator),
                timeout,
                timeOutMessage);
            return (T)this;
        }

    }

}