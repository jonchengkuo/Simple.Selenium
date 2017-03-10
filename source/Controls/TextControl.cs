﻿using OpenQA.Selenium;

namespace Simple.Selenium.Controls
{

    /// <summary>
    /// Class for representing and interacting with a piece of text on a web page.
    /// A piece of text on a web page may be specified in any of, but not limited to, the following HTML tags:
    /// &lt;p&gt;, &lt;div&gt;, &lt;span&gt;.
    /// If the text is defined by an HTML &lt;label&gt; tag, you should use the <seealso cref="cref="Label" element instead./>
    /// </summary>
    public class TextControl : Control
    {

        /// <summary>
        /// Constructs an object to represent and interact with a text on a web page.
        /// </summary>
        /// <param name="locator">  The <seealso cref="By"/> locator for locating this text;
        ///                  it should select the HTML tag that contains the text. </param>
        /// <exception cref="NullPointerException"> if the specified <code>locator</code> is <code>null</code> </exception>
        public TextControl(By locator, IWebDriver webDriver = null) : base(locator, webDriver)
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

    }

}