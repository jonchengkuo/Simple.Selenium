using OpenQA.Selenium;

namespace Simple.Selenium.Controls
{

    /// <summary>
    /// Class for representing and interacting with check boxes on a web page.
    /// </summary>
    public class CheckBox : Control
    {

        /// <summary>
        /// Constructs an object to represent and interact with a check box on a web page.
        /// 
        /// Note that the label of the check box is usually a label or text element outside the check box element.
        /// Thus, it needs to be handled separately.
        /// </summary>
        /// <param name="locator">  The <seealso cref="By"/> locator for locating this check box;
        ///                  it should select the HTML {@code <input type="checkbox">} tag of the check box. </param>
        /// <exception cref="NullPointerException"> if the specified <code>locator</code> is <code>null</code> </exception>
        public CheckBox(By locator, IWebDriver webDriver = null) : base(locator, webDriver)
        {
        }

        /// <summary>
        /// Simulates the user interaction of checking this check box.
        /// 
        /// If the check box is not visible (default) or does not exist, this method will keep waiting until it appears or until
        /// the <seealso cref="WebUI.DefaultImplicitWaitTimeout default implicit wait timeout"/> is reached.  
        /// </summary>
        /// <exception cref="NoSuchElementException"> if this check box is still not visible (default) or does not exist
        ///     after the <seealso cref="WebUI.DefaultImplicitWaitTimeout default implicit wait timeout"/> is reached </exception>
        /// <exception cref="StaleElementReferenceException">Thrown when the <seealso cref="IWebElement"/> of this check box becomes invalid
        ///     (unlikely unless the HTML tag of this check box is refreshed while this method is invoked).</exception>
        public virtual void Check()
        {
            // Get the web element, possibly with the implicit wait.
            IWebElement webElement = GetWebElement();

            if (!webElement.Selected)
            {
                webElement.Click();
            }
        }

        /// <summary>
        /// Simulates the user interaction of unchecking this check box.
        /// 
        /// If the check box is not visible (default) or does not exist, this method will keep waiting until it appears or until
        /// the <seealso cref="WebUI.DefaultImplicitWaitTimeout default implicit wait timeout"/> is reached.  
        /// </summary>
        /// <exception cref="NoSuchElementException"> if this check box is still not visible (default) or does not exist
        ///     after the <seealso cref="WebUI.DefaultImplicitWaitTimeout default implicit wait timeout"/> is reached </exception>
        /// <exception cref="StaleElementReferenceException">Thrown when the <seealso cref="IWebElement"/> of this check box becomes invalid
        ///     (unlikely unless the HTML tag of this check box is refreshed while this method is invoked).</exception>
        public virtual void Uncheck()
        {
            // Get the web element, possibly with the implicit wait.
            IWebElement webElement = GetWebElement();

            if (webElement.Selected)
            {
                webElement.Click();
            }
        }

    }

}