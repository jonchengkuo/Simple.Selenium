using OpenQA.Selenium;

namespace Simple.Selenium.Controls
{

    /// <summary>
    /// Class for representing and interacting with text fields (or called text boxes) on a web page.
    /// </summary>
    public class TextField : Control
    {

        /// <summary>
        /// Constructs an object to represent and interact with a text field (or called a text box) on a web page.
        /// 
        /// Note that the label of a text field is outside the text field element and should be handled separately.
        /// </summary>
        /// <param name="locator">  The <seealso cref="By"/> locator for locating this text field;
        ///                  it should select the HTML {@code <input type="text">} tag of the text field. </param>
        /// <exception cref="NullReferenceException"> if the specified <code>locator</code> is <code>null</code> </exception>
        public TextField(By locator, IWebDriver webDriver = null) : base(locator, webDriver)
        {
        }

        /// <summary>
        /// Returns the actual text in this text field or simulates the user interaction of
        /// clearing this text field and entering the given text into this text field.
        /// 
        /// If the text field is not visible (default) or does not exist, this method will keep waiting until it appears or until
        /// the <seealso cref="WebUI.DefaultImplicitWaitTimeout default implicit wait timeout"/> is reached.  
        /// </summary>
        /// <exception cref="NoSuchElementException"> if this text field is still not visible (default) or does not exist
        ///     after the <seealso cref="WebUI.DefaultImplicitWaitTimeout default implicit wait timeout"/> is reached </exception>
        /// <exception cref="StaleElementReferenceException">Thrown when the <seealso cref="IWebElement"/> of this text field becomes invalid
        ///            (unlikely unless the HTML tag of this text field is refreshed while this property is retrieved or set).</exception>
        public virtual string Text
        {
            get
            {
                // Get the web element, possibly with the implicit wait.
                IWebElement webElement = GetWebElement();
                return webElement.GetAttribute("value");
            }
        }

        public virtual void EnterText(string text)
        {
            // Get the web element, possibly with the implicit wait.
            IWebElement webElement = GetWebElement();

            // Clear the text field and then enter the desired text.
            webElement.Clear();
            webElement.SendKeys(text);
        }

        public virtual void AppendText(string text)
        {
            // Get the web element, possibly with the implicit wait, and then enter more text to it.
            GetWebElement().SendKeys(text);
        }

        /// <summary>
        /// Simulates the user interaction of clicking this text field on UI.
        /// 
        /// If the text field is not visible (default) or does not exist, this method will keep waiting until it appears or until
        /// the <seealso cref="WebUI.DefaultImplicitWaitTimeout default implicit wait timeout"/> is reached.  
        /// </summary>
        /// <exception cref="NoSuchElementException"> if this text field is still not visible (default) or does not exist
        ///     after the <seealso cref="WebUI.DefaultImplicitWaitTimeout default implicit wait timeout"/> is reached </exception>
        /// <exception cref="StaleElementReferenceException">Thrown when the <seealso cref="IWebElement"/> of this text field becomes invalid
        ///     (unlikely unless the HTML tag of this text field is refreshed while this method is invoked).</exception>
        public virtual void Click()
        {
            // Get the web element, possibly with the implicit wait, and then click it.
            GetWebElement().Click();
        }

        /// <summary>
        /// Simulates the user interaction of submitting the web form that contains this text field.
        /// 
        /// If the text field is not visible (default) or does not exist, this method will keep waiting until it appears or until
        /// the <seealso cref="WebUI.DefaultImplicitWaitTimeout default implicit wait timeout"/> is reached.  
        /// </summary>
        /// <exception cref="NoSuchElementException"> if this text field is still not visible (default) or does not exist
        ///     after the <seealso cref="WebUI.DefaultImplicitWaitTimeout default implicit wait timeout"/> is reached </exception>
        /// <exception cref="StaleElementReferenceException">Thrown when the <seealso cref="IWebElement"/> of this text field becomes invalid
        ///            (unlikely unless the web page of this text field is refreshed while this method is invoked).</exception>
        public virtual void Submit()
        {
            // Get the web element, possibly with the implicit wait, and then
            // submit the form that contains this text field.
            GetWebElement().Submit();
        }

    }

}