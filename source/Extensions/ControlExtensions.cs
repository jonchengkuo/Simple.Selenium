using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;  // ExpectedConditions
using Simple.Selenium.Controls;

namespace Simple.Selenium.Extensions
{
    public static class ControlExtensions
    {
        public static bool IsClickableNow(this Control control)
        {
            try
            {
                IWebElement webElement = control.GetWebElementNoWait();
                return webElement.Displayed && webElement.Enabled;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static bool IsClickable(this Control control)
        {
            return control.IsClickable(control.ImplicitWaitTimeout);
        }

        public static bool IsClickable(this Control control, TimeSpan timeout)
        {
            if (control.IsClickableNow())
            {
                return true;
            }

            try
            {
                control.WaitUntilClickable(timeout);
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Waits until this UI control becomes clickable, or until the specified timeout is reached.
        /// It returns the located clickable web element.
        /// </summary>
        /// <param name="timeout">  timeout in waiting for the web element </param>
        /// <returns> the located clickable web element </returns>
        /// <exception cref="WebDriverTimeoutException"> if this UI control is still not clickable after the specified timeout is reached   </exception>
        public static IWebElement WaitUntilClickable(this Control control, TimeSpan timeout)
        {
            return control.WebDriver.WaitUntil<IWebElement>(
                ExpectedConditions.ElementToBeClickable(control.Locator),
                timeout,
                "Waiting for " + control.Name + " to become clickable.");
        }

        public static bool IsNotClickableNow(this Control control)
        {
            try
            {
                IWebElement webElement = control.GetWebElementNoWait();
                return !webElement.Displayed || !webElement.Enabled;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

    }

}