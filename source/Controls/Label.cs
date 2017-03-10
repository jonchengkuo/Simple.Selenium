using OpenQA.Selenium;

namespace Simple.Selenium.Controls
{

    /// <summary>
    /// Class for representing and interacting with an HTML Label element on a web page.
    /// An HTML Label element (&lt;label&gt;) represents a caption for an item in a user interface and
    /// can be associated with a control either by placing the control element inside the &lt;label&gt; element,
    /// or by using the for attribute.
    /// </summary>
    public class Label : Control
    {

        /// <summary>
        /// Constructs an object to represent and interact with a label on a web page.
        /// </summary>
        /// <param name="locator">  The <seealso cref="By"/> locator for locating this label;
        ///                  it should select an HTML &lt;label&gt; tag. </param>
        /// <exception cref="NullPointerException"> if the specified <code>locator</code> is <code>null</code> </exception>
        public Label(By locator, IWebDriver webDriver = null) : base(locator, webDriver)
        {
        }

        /// <summary>
        /// Gets the actual text of this text element, without any leading or trailing whitespace,
        /// and with other whitespace collapsed.
        /// 
        /// If the text element is not visible (default) or does not exist, this method will keep waiting until it appears or until
        /// the <seealso cref="WebUI.DefaultImplicitWaitTimeout default implicit wait timeout"/> is reached.  
        /// </summary>
        /// <exception cref="NoSuchElementException"> if this text element is still not visible (default) or does not exist
        ///     after the <seealso cref="WebUI.DefaultImplicitWaitTimeout default implicit wait timeout"/> is reached </exception>
        /// <exception cref="StaleElementReferenceException">Thrown when the <seealso cref="IWebElement"/> of this text element becomes invalid
        ///     (unlikely unless the HTML tag of this text element is refreshed while this property is retrieved).</exception>
        public virtual string Text
        {
            get
            {
                // Get the web element, possibly with the implicit wait, and then get its text.
                return GetWebElement().Text;
            }
        }

        /// <summary>
        /// Simulates the user interaction of clicking this label.
        /// If this label has an associated control element, clicking the label will have the same
        /// effect as clicking the associated control element.
        ///
        /// If the label is not visible (default) or does not exist, this method will keep waiting until it appears or until
        /// the <seealso cref="WebUI.DefaultImplicitWaitTimeout default implicit wait timeout"/> is reached.  
        /// </summary>
        /// <exception cref="NoSuchElementException"> if this label is still not visible (default) or does not exist
        ///     after the <seealso cref="WebUI.DefaultImplicitWaitTimeout default implicit wait timeout"/> is reached </exception>
        /// <exception cref="StaleElementReferenceException">Thrown when the <seealso cref="IWebElement"/> of this label becomes invalid
        ///     (unlikely unless the HTML tag of this label is refreshed while this method is invoked).</exception>
        public virtual void Click()
        {
            // Get the web element, possibly with the implicit wait, and then click it.
            GetWebElement().Click();
        }

    }

}