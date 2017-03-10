using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;  // SelectElement

namespace Simple.Selenium.Controls
{

    /// <summary>
    /// Class for representing and interacting with a list box or drop-down list on a web page.
    /// A list box is defined by an HTML <![CDATA[<select>]]> tag with its size greater than 1.
    /// A drop-down list is defined by an HTML <![CDATA[<select>]]> tag with its size equal to 1.
    /// </summary>
    public class SelectBox : Control
    {

        /// <summary>
        /// Constructs an object to represent and interact with a list box or drop-down list on a web page.
        /// </summary>
        /// <param name="locator">  The <seealso cref="By"/> locator for locating this list box;
        ///                  it should select the HTML <![CDATA[<select>]]> tag of the list box. </param>
        /// <exception cref="ArgumentNullException"> if the specified <code>locator</code> is <code>null</code> </exception>
        public SelectBox(By locator, IWebDriver webDriver = null) : base(locator, webDriver)
        {
        }

        public virtual SelectElement SelectElement
        {
            get
            {
                // Get the web element with the default implicit timeout.
                IWebElement webElement = GetWebElement();
                return new SelectElement(webElement);
            }
        }

    }

}