namespace Simple.Selenium
{

    public static class WebUISettings
    {

        /// <summary>
        /// The default implicit waiting timeout used by UI elements
        /// when no explicit timeout is specified (as an argument to a method call).
        /// The default timeout value is 3 (seconds).
        /// <para>
        /// Note that this implicit wait timeout has nothing to do with the Selenium
        /// implicit wait. The UI elements are smart in that they will periodically
        /// (default to every half second) check the availability of themselves. 
        /// </para>
        /// </summary>
        public static int DefaultImplicitWaitTimeoutInSeconds = 3;

        /// <summary>
        /// The default page loading timeout used by UI pages; default to 30 seconds.
        /// </summary>
        public static int DefaultPageLoadingTimeoutInSeconds = 30;

        /// <summary>
        /// The default timeout used in waiting for a dialog or page to close; default to 3 seconds.
        /// </summary>
        public static int DefaultClosingTimeoutInSeconds = 3;

    }

}
