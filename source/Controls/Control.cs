using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;  // ExpectedConditions
using Simple.Selenium.Extensions;  // WebDriverExtensions

namespace Simple.Selenium.Controls
{

    /// <summary>
    /// Base class for representing and interacting with UI controls shown on a web page.
    /// It provides common utility methods for subclasses.
    /// A subclass should be defined for each specific UI control type (e.g., buttons, check boxes, etc.)
    /// to model specific UI behaviors (e.g., clicking a button or checking a check box).
    /// 
    /// <para>Every UI control shown on a web page is defined by an HTML element, which is defined
    /// by a pair of HTML start and end tags.</para>
    /// 
    /// <para>Instances of UI controls of this class are using a <seealso cref="By"/> locator.
    /// A locator is a mechanism by which the HTML element of a UI control can be located.
    /// Because every UI control shown on a web page is defined by a particular HTML element,
    /// this framework sometimes uses the term <em>UI control</em> and <em>HTML element</em> interchangeably.</para>
    /// 
    /// <para>Every time your code interacts with a UI control
    /// (by calling its public methods), it will always try to locate its HTML element on the web page with its locator.
    /// This design is less efficient because it does not cache a previously located HTML element (i.e.,
    /// it does not reuse a previously available IWebElement object). However, this design does guarantee that
    /// it will always interact with the current, up-to-date web page. From the framework's perspective, it has no way
    /// to know whether a previously located HTML element still exists or not.</para>
    /// </summary>
    /// 
    /// <example>
    /// <code>
    ///   using OpenQA.Selenium;
    ///   using Simple.Selenium.Controls;
    /// 
    ///   public class Button : BaseControl {
    ///       public Button(By locator) : base (locator, webDriver) { }
    ///
    ///       public void Click() {
    ///           GetWebElement().Click();
    ///       }
    ///   }
    /// 
    ///   Button button = new Button(By.id("buttonId"));
    /// <code>
    /// </example>
    public class Control
    {
        private bool m_expectVisible = true;
        protected virtual bool ExpectVisible
        {
            set
            {
                m_expectVisible = value;
            }
        }

        /// <summary>
        /// Returns the <seealso cref="By"/> locator of this UI control.
        /// If this UI control is created with a <seealso cref="IWebElement"/> instead of a locator, this property will return <code>null</code>. </summary>
        /// <returns> the <seealso cref="By"/> locator of this UI control;
        ///         <code>null</code> if this UI control is created with a <seealso cref="IWebElement"/> instead of a locator </returns>
        public readonly By Locator;

        private readonly IWebDriver m_webDriver;
        /// <summary>
        /// Returns the WebDriver instance used by this UI control.
        /// It is default to <seealso cref="WebUIGlobals.DefaultWebDriver"/> if this UI control is constructed without given a WebDriver instance.
        /// </summary>
        /// <returns> the <seealso cref="IWebDriver"/> instance used by this UI control </returns>
        public virtual IWebDriver WebDriver
        {
            get
            {
                // If m_webDriver is not set, use the global default WebDriver.
                return m_webDriver ?? DefaultWebDriverHelper.GetDefaultWebDriver();
            }
        }

        /// <summary>
        /// The implicit wait timeout for this UI control.
        /// It is default to <seealso cref="WebUISettings.DefaultImplicitWaitTimeoutInSeconds"/>.
        /// </summary>
        public virtual TimeSpan ImplicitWaitTimeout { get; set; }

        /// <summary>
        /// Constructs the base object of a concrete UI control object that represents a specific UI control on a web page.
        /// The given <code>locator</code> will be used to locate the <seealso cref="IWebElement"/> of the UI control. </summary>
        /// <param name="locator">  The <seealso cref="By"/> locator for locating the UI control on a web page. </param>
        /// <param name="webDriver">  The WebDriver (i.e., browser) that hosts the web page which this UI control is on.
        ///                           If it is null, the global default WebDriver, defined by <seealso cref="Control.DefaultWebDriver"/> will be used (when this UI control is used). </param>
        /// <exception cref="ArgumentNullException"> if the specified <code>locator</code> is <code>null</code> </exception>
        protected Control(By locator, IWebDriver webDriver)
        {
            if (locator == null)
            {
                throw new ArgumentNullException("The locator given to the UI control is null.");
            }
            Locator = locator;
            m_webDriver = webDriver;
            ImplicitWaitTimeout = TimeSpan.FromSeconds(WebUISettings.DefaultImplicitWaitTimeoutInSeconds);
        }

        /// <summary>
        /// Returns the name of this UI control.
        /// It is default to the simple class name of this UI control, such as "Button", "CheckBox", etc.,
        /// appended with the string representation of either the locator (if this UI control has a locator),
        /// or the <seealso cref="IWebElement"/> given to the constructor of this UI control. </summary>
        /// <returns> the name of this UI control </returns>
        public virtual string Name
        {
            get
            {
                return this.GetType().Name + "(" + Locator.ToString() + ")";
            }
        }

        /// <summary>
        /// Locates and returns the <seealso cref="IWebElement"/> of this UI control.
        /// If the web element of this UI control cannot be found (i.e., not in the DOM tree),
        /// or if it exists but not visible,
        /// it will immediately throw a <exception cref="NoSuchElementException">.
        /// </summary>
        /// <returns> the <seealso cref="IWebElement"/> located using the locator of this UI control </returns>
        /// <exception cref="NoSuchElementException"> if this UI control still does not exist </exception>
        public virtual IWebElement GetWebElementNoWait()
        {
            IWebElement webElement = WebDriver.FindElement(Locator);
            if (this.m_expectVisible && !webElement.Displayed)
            {
                throw new NoSuchElementException(this.Name + " exists in the DOM tree but not visible.");
            }
            return webElement;
        }

        /// <summary>
        /// Locates and returns the <seealso cref="IWebElement"/> of this UI control.
        /// If it cannot find the web element, it will periodically (every half second) try to find it again until the implicit wait timeout is reached.
        /// The default implicit wait timeout is defined by <seealso cref="WebUISettings.DefaultImplicitWaitTimeoutInSeconds"/>.
        /// </summary>
        /// <returns> the <seealso cref="IWebElement"/> located using the locator of this UI control </returns>
        /// <exception cref="NoSuchElementException"> if this UI control still does not exist after the default implicit wait timeout is reached </exception>
        public virtual IWebElement GetWebElement()
        {
            return GetWebElement(ImplicitWaitTimeout);
        }

        /// <summary>
        /// Locates and returns the <seealso cref="IWebElement"/> of this UI control.
        /// If the specified timeout is greater than 0, it will periodically (every half second)
        /// locate it until the specified timeout is reached.
        /// </summary>
        /// <param name="timeout">  timeout in waiting for the web element </param>
        /// <returns> the <seealso cref="IWebElement"/> located using the locator of this UI control </returns>
        /// <exception cref="NoSuchElementException"> if this UI control still does not exist after the specified timeout is reached </exception>
        public virtual IWebElement GetWebElement(TimeSpan timeout)
        {
            // Firstly, check whether the web element exists/visible or not without waiting.
            try
            {
                return GetWebElementNoWait();
            }
            catch (NoSuchElementException) { }

            // The web element does not yet exist/visible; do implicit fluent wait.
            IWebElement webElement = null;
            try
            {
                if (this.m_expectVisible)
                {
                    webElement = WaitUntilVisible(timeout);
                }
                else
                {
                    webElement = WaitUntilExists(timeout);
                }
            }
            catch (WebDriverTimeoutException e)
            {
                // Convert a WebDriverTimeoutException into a NoSuchElementException.
                throw new NoSuchElementException(e.Message);
            }
            return webElement;
        }


        /// <summary>
        /// Returns whether this UI control exists (i.e., present in the DOM tree) or not, as of now.
        /// Unlike getting the <seealso cref="BaseElement.GetWebElement"/> method,
        /// getting this property does not involve implicit waiting.
        /// </summary>
        /// <returns> <code>true</code> if this UI control exists; <code>false</code> otherwise </returns>
        public virtual bool ExistsNow()
        {
                try
                {
                    WebDriver.FindElement(Locator);
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
        }

        /// <summary>
        /// Returns whether this UI control exists (i.e., present in the DOM tree) or not,
        /// within the <seealso cref="ImplicitWaitTimeout"/>, which can be configured and defaults to
        /// <seealso cref="WebUISettings.DefaultImplicitWaitTimeoutInSeconds"/>.
        /// </summary>
        /// <returns> <code>true</code> if this UI control exists within the default implicit wait timeout;
        ///         <code>false</code> otherwise </returns>
        public virtual bool Exists()
        {
            return Exists(ImplicitWaitTimeout);
        }

        /// <summary>
        /// Returns whether this UI control exists (i.e., present in the DOM tree) or not
        /// within the specified timeout.
        /// If the specified timeout is 0, it will check the current existence of this UI control.
        /// If the specified timeout is greater than 0, it will periodically (every half second)
        /// check until the specified timeout is reached.
        /// </summary>
        /// <param name="timeout">  timeout in waiting for the web element </param>
        /// <returns> <code>true</code> if this UI control exists within the specified timeout;
        ///         <code>false</code> otherwise </returns>
        public virtual bool Exists(TimeSpan timeout)
        {
            if (ExistsNow())
            {
                return true;
            }

            try
            {
                WaitUntilExists(timeout);
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns whether this UI control is visible or not, as of now.
        /// </summary>
        /// <returns> <code>true</code> if this UI control is visible; <code>false</code> otherwise </returns>
        public virtual bool IsVisibleNow()
        {
            try
            {
                IWebElement webElement = WebDriver.FindElement(Locator);
                return webElement.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Property indicating whether this UI control is visible or not,
        /// within the <seealso cref="ImplicitWaitTimeout"/>, which can be configured and defaults to
        /// <seealso cref="WebUISettings.DefaultImplicitWaitTimeoutInSeconds"/>.
        /// </summary>
        /// <returns> <code>true</code> if this UI control exists and is visible within the default implicit wait timeout;
        ///         <code>false</code> otherwise </returns>
        public virtual bool IsVisible()
        {
            return IsVisible(ImplicitWaitTimeout);
        }

        /// <summary>
        /// Returns whether this UI control is visible or not within the specified timeout.
        /// If the specified timeout is 0, it will check the current visibility of this UI control.
        /// If the specified timeout is greater than 0, it will periodically (every half second)
        /// check until the specified timeout is reached.
        /// </summary>
        /// <param name="timeout">  timeout in waiting for the web element </param>
        /// <returns> <code>true</code> if this UI control is visible within the specified timeout;
        ///         <code>false</code> otherwise </returns>
        public virtual bool IsVisible(TimeSpan timeout)
        {
            if (IsVisibleNow())
            {
                return true;
            }

            try
            {
                WaitUntilVisible(timeout);
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Property indicating whether this UI control is invisible or not,
        /// within the <seealso cref="ImplicitWaitTimeout"/>, which can be configured and defaults to
        /// <seealso cref="WebUISettings.DefaultImplicitWaitTimeoutInSeconds"/>.
        /// </summary>
        /// <returns> <code>true</code> if this UI control does not exists or is invisible within the default implicit wait timeout;
        ///         <code>false</code> otherwise </returns>
        public virtual bool IsNotVisible()
        {
            return IsNotVisible(ImplicitWaitTimeout);
        }

        /// <summary>
        /// Returns whether this UI control is invisible or not within the specified timeout.
        /// If the specified timeout is 0, it will check the current visibility of this UI control.
        /// If the specified timeout is greater than 0, it will periodically (every half second)
        /// check until the specified timeout is reached.
        /// </summary>
        /// <param name="timeout">  timeout in waiting for the web element </param>
        /// <returns> <code>true</code> if this UI control does not exist or is invisible within the specified timeout;
        ///         <code>false</code> otherwise </returns>
        public virtual bool IsNotVisible(TimeSpan timeout)
        {
            if (!IsVisibleNow())
            {
                return true;
            }

            try
            {
                WaitUntilNotVisible(timeout);
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Waits until this UI control becomes exist (i.e., present in the DOM tree), or until the specified timeout is reached.
        /// It returns the located web element.
        /// </summary>
        /// <param name="timeout">  timeout in waiting for the web element </param>
        /// <returns> the located web element </returns>
        /// <exception cref="WebDriverTimeoutException"> if this UI control is still not present after the specified timeout is reached   </exception>
        public virtual IWebElement WaitUntilExists(TimeSpan timeout)
        {
            return WebDriver.WaitUntil<IWebElement>(
                ExpectedConditions.ElementExists(Locator),
                timeout,
                "Waiting for " + this.Name + " to become exists.");
        }

        /// <summary>
        /// Waits until this UI control becomes present and visible, or until the specified timeout is reached.
        /// It returns the located visible web element.
        /// </summary>
        /// <param name="timeout">  timeout in waiting for the web element </param>
        /// <returns> the located visible web element </returns>
        /// <exception cref="WebDriverTimeoutException"> if this UI control is still not visible after the specified timeout is reached   </exception>
        public virtual IWebElement WaitUntilVisible(TimeSpan timeout)
        {
            return WebDriver.WaitUntil<IWebElement>(
                ExpectedConditions.ElementIsVisible(Locator),
                timeout,
                "Waiting for " + this.Name + " to become exist and visible.");
        }

        /// <summary>
        /// Waits until this UI control becomes invisible, or until the specified timeout is reached.
        /// </summary>
        /// <param name="timeout">  timeout in waiting for the web element </param>
        /// <returns> the return value from <seealso cref="WebDriverWait.Until"/> </returns>
        /// <exception cref="WebDriverTimeoutException"> if this UI control is still visible after the specified timeout is reached   </exception>
        public virtual bool WaitUntilNotVisible(TimeSpan timeout)
        {
            return WebDriver.WaitUntil<bool>(
                ExpectedConditions.InvisibilityOfElementLocated(Locator),
                timeout,
                "Waiting for " + this.Name + " to become not visible.");
        }

    }

}