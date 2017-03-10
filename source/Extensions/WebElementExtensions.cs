using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Simple.Selenium.Extensions
{
    public static class WebElementExtensions
    {
        public static void DoubleClick(this IWebElement webElement)
        {
            // TODO: Get WebDriver from webElement.
            IWebDriver webDriver = null;
            new Actions(webDriver)
                .MoveToElement(webElement)
                .DoubleClick()
                .Build()
                .Perform();
        }
    }
}