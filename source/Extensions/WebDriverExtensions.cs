using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Simple.Selenium.Extensions
{
    public enum ReadyStateEnum
    {
        unknown,
        Loading,
        Interactive,
        Complete,
        Loaded
    }

    public static class WebDriverExtensions
    {
        private static readonly string JsReadyState = @"return document.readyState";

        /// <summary>
        /// Repeatedly calling the specified expected condition function until either it returns
        /// a non-null value or the specified timeout expires.
        /// <para>
        /// Example:
        /// <pre>
        ///     // Wait until the "username" text field is visible or timeout. 
        ///     IWebElement e = webDriver.WaitUntil(
        ///         ExpectedConditions.ElementIsVisible(By.name("username")),
        ///         TimeSpan.FromSeconds(timeoutInSeconds));
        /// </pre>
        /// </para>
        /// </summary>
        /// <param name="expectedCondition"> the expected condition to wait for </param>
        /// <param name="timeout">  the timeout when an expectation is called </param>
        /// <param name="timeoutMessage">    the message to be included in the WebDriverTimeoutException if it is thrown </param>
        /// <returns> the non-null value returned by the specified expected condition function </returns>
        /// <exception cref="InvalidOperationException"> if no web browser is currently opened </exception>
        /// <exception cref="WebDriverTimeoutException"> if the specified expected condition function still returns <code>null</code>
        ///         when the specified timeout is reached </exception>
        /// <seealso cref= WebDriverWait </seealso>
        public static T WaitUntil<T>(this IWebDriver webDriver, Func<IWebDriver, T> expectedCondition, TimeSpan timeout, string timeoutMessage = null)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, timeout);
            if (timeoutMessage != null)
                wait.Message = timeoutMessage;
            return wait.Until<T>(expectedCondition);
        }

        public static void WaitForPageLoad(this IWebDriver driver, uint timeoutInSeconds, string pageTitle = "")
        {
            string state = string.Empty;
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

                //Checks every 500 ms whether predicate returns true if returns exit otherwise keep trying till it returns ture
                wait.Until(d =>
                {
                    try
                    {
                        state = ((IJavaScriptExecutor)driver).ExecuteScript(JsReadyState).ToString();
                    }
                    catch (NoSuchWindowException)
                    {
                        //when popup is closed, switch to last windows
                        driver.SwitchTo().Window(driver.WindowHandles.Last());
                    }
                    catch (InvalidOperationException)
                    {
                        //Ignore
                    }
                    catch (WebDriverException)
                    {
                        //If we catch a webdriverexception (could be an issue communicating with the driver) we want to wait,
                        //but we don't want to throw a TimeoutException as it would swallow this so we will try to rethrow the error in the TimeoutException catch.
                    }

                    if (IsValidDocumentReadyState(state))
                    {
                        //If a title is not given just return true.
                        if (string.IsNullOrEmpty(pageTitle))
                        {
                            return true;
                        }

                        //If title is given, return result of comparison to browser title.
                        return d.Title == pageTitle;
                    }

                    return false;
                });
            }
            catch (TimeoutException)
            {
                //Sometimes Page remains in Interactive mode and never becomes Complete, then we can still try to access the controls
                //First we try to aquire state again just incase we swallowed a WebDriverException that is still occuring.
                state = ((IJavaScriptExecutor)driver).ExecuteScript(JsReadyState).ToString();

                if (IsValidInteractiveState(state))
                {
                    return;
                }

                throw new TimeoutException("Page Load Timed Out");
            }
            catch (NullReferenceException)
            {
                //Sometimes Page remains in Interactive mode and never becomes Complete, then we can still try to access the controls
                if (IsValidInteractiveState(state))
                {
                    return;
                }

                throw;
            }
            catch (WebDriverException)
            {
                if (driver != null && driver.WindowHandles.Count == 1)
                {
                    driver.SwitchTo().Window(driver.WindowHandles[0]);
                }

                state = ((IJavaScriptExecutor)driver).ExecuteScript(JsReadyState).ToString();

                if (IsValidDocumentReadyState(state))
                {
                    return;
                }

                throw;
            }
        }

        private static bool IsValidDocumentReadyState(string state)
        {
            if (state.Equals(ReadyStateEnum.Complete.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            //In IE7 there are chances we may get state as loaded instead of complete
            if (state.Equals(ReadyStateEnum.Loaded.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }

        private static bool IsValidInteractiveState(string state)
        {
            if (state.Equals(ReadyStateEnum.Interactive.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }

    }
}