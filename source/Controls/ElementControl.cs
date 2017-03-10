using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;  // ExpectedConditions
using Simple.Selenium.Extensions;  // WebDriver extensions

namespace Simple.Selenium.Controls
{

    /// <summary>
    /// Base class for representing and interacting with UI controls shown on a web page.
    /// Unlike the <seealso cref="Control"/> class, the constructor of this class requires
    /// a reference to a web element (<seealso cref="IWebElement"/>) instead of a <seealso cref="By"/> locator.
    /// A web element represents a located HTML element.
    /// It is designed to be used as a transient UI control that lives for a short period of time,
    /// e.g., a row (<![CDATA[<tr>]]>) element within a table.
    /// </summary>
    /// 
    public class ElementControl
    {
        /// <summary>
        /// </summary>
        public readonly IWebElement WebElement;

        /// <summary>
        /// Constructs the base object of a concrete UI control object that represents a specific UI control on a web page.
        /// This UI control is directly tied to the given <seealso cref="IWebElement"/> located by other means. </summary>
        /// <param name="webElement">  The <seealso cref="IWebElement"/> of the UI control on a web page. </param>
        /// <exception cref="ArgumentNullException"> if the specified <code>webElement</code> is <code>null</code> </exception>
        protected ElementControl(IWebElement webElement)
        {
            if (webElement == null)
            {
                throw new System.ArgumentNullException("The WebElement given to the UI control is null.");
            }
            WebElement = webElement;
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
                return this.GetType().Name + "(<" + WebElement.TagName + ">)";
            }
        }

    }

}