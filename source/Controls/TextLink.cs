using OpenQA.Selenium;

namespace Simple.Selenium.Controls
{

    /// <summary>
    /// Class for representing and interacting with text links on a web page.
    /// </summary>
    public class TextLink : Control
    {

        /// <summary>
        /// Constructs an object to represent and interact with a hypertext link on a web page. </summary>
        /// <param name="locator">  The <seealso cref="By"/> locator for locating this text link;
        ///                  it should select the HTML {@code <a href="...">} tag of the text link. </param>
        /// <exception cref="NullPointerException"> if the specified <code>locator</code> is <code>null</code> </exception>
        public TextLink(By locator, IWebDriver webDriver = null) : base(locator, webDriver)
        {
        }

        /// <summary>
        /// Gets the actual text of this text link, without any leading or trailing whitespace,
        /// and with other whitespace collapsed.
        /// 
        /// If the text link is not visible (default) or does not exist, this method will keep waiting until it appears or until
        /// the <seealso cref="WebUI.DefaultImplicitWaitTimeout default implicit wait timeout"/> is reached.  
        /// </summary>
        /// <exception cref="NoSuchElementException"> if this text link is still not visible (default) or does not exist
        ///     after the <seealso cref="WebUI.DefaultImplicitWaitTimeout default implicit wait timeout"/> is reached </exception>
        /// <exception cref="StaleElementReferenceException">Thrown when the <seealso cref="IWebElement"/> of this text link becomes invalid
        ///     (unlikely unless the HTML tag of this text link is refreshed while this property is retrieved).</exception>
        public virtual string Text
        {
            get
            {
                // Get the web element, possibly with the implicit wait, and then get its text.
                return GetWebElement().Text;
            }
        }

        /// <summary>
        /// Simulates the user interaction of clicking this text link on UI.
        /// 
        /// If the text link is not visible (default) or does not exist, this method will keep waiting until it appears or until
        /// the <seealso cref="WebUI.DefaultImplicitWaitTimeout default implicit wait timeout"/> is reached.  
        /// </summary>
        /// <exception cref="NoSuchElementException"> if this text link is still not visible (default) or does not exist
        ///     after the <seealso cref="WebUI.DefaultImplicitWaitTimeout default implicit wait timeout"/> is reached </exception>
        /// <exception cref="StaleElementReferenceException">Thrown when the <seealso cref="IWebElement"/> of this text link becomes invalid
        ///            (unlikely unless the HTML tag of this text link is refreshed while this method is invoked).</exception>
        public virtual void Click()
        {
            // Get the web element, possibly with the implicit wait, and then click it.
            GetWebElement().Click();
        }

    }

}