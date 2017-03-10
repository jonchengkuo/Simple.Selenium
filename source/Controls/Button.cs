using OpenQA.Selenium;

namespace Simple.Selenium.Controls
{

    /// <summary>
    /// Class for representing and interacting with buttons on a web page.
    /// </summary>
    public class Button : Control
    {

        /// <summary>
        /// Constructs an object to represent and interact with a button on a web page. </summary>
        /// <param name="locator">  The <seealso cref="By"/> locator for locating the <seealso cref="WebElement"/> of this button;
        ///                  it should select the HTML {@code <input type="button">} tag of the button. </param>
        /// <exception cref="NullPointerException"> if the specified <code>locator</code> is <code>null</code> </exception>
        public Button(By locator, IWebDriver webDriver = null) : base(locator, webDriver)
        {
        }

        /// <summary>
        /// Simulates the user interaction of clicking this button on UI.
        /// 
        /// If the button is not visible (default) or does not exist, this method will keep waiting until it appears or until
        /// the <seealso cref="WebUI.DefaultImplicitWaitTimeout default implicit wait timeout"/> is reached.  
        /// </summary>
        /// <exception cref="NoSuchElementException"> if this button is still not visible (default) or does not exist
        ///     after the <seealso cref="WebUI.DefaultImplicitWaitTimeout default implicit wait timeout"/> is reached </exception>
        /// <exception cref="StaleElementReferenceException">Thrown when the <seealso cref="IWebElement"/> of this button becomes invalid
        ///     (unlikely unless the HTML tag of this button is refreshed while this method is invoked).</exception>
        public virtual void Click()
        {
            // Get the web element, possibly with the implicit wait, and then click it.
            GetWebElement().Click();
        }

        /// <summary>
        /// Returns the actual text on this button.
        /// 
        /// If the button does not exist, this method will keep waiting until it appears or until
        /// the <seealso cref="WebUI.DefaultImplicitWaitTimeout default implicit wait timeout"/> is reached.  
        /// </summary>
        /// <returns> a String value representing the actual text on this button </returns>
        /// <exception cref="NoSuchElementException"> if this button is still not visible (default) or does not exist
        ///     after the <seealso cref="WebUI.DefaultImplicitWaitTimeout default implicit wait timeout"/> is reached </exception>
        public virtual string Text
        {
            get
            {
                // Get the web element with the implicit timeout.
                IWebElement webElement = GetWebElement();
                // Get and check the web element text. If it is an empty string,
                // get the text from the value attribute of the button
                string text = webElement.Text;
                if (text.Length == 0)
                {
                    text = webElement.GetAttribute("value");
                }
                return text;
            }
        }

    }

}