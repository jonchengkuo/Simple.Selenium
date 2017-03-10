using System;

namespace Simple.Selenium.Pages
{
    /// <summary>
    /// This class provides the <seealso cref="TimeSpanHelper.Within"/> static method that can
    /// increase the readability in the Web UI automation code when explicit timeout needs to be specified.
    /// 
    /// With C# 6.0 or higher, you can replace code like <code>TimeSpan.FromSeconds(3)</code>
    /// with <code>Within(3).Seconds</code>. See the example below about how it increases the code readability.
    /// </summary>
    /// 
    /// <example>
    /// <code>
    ///   using static Simple.Selenium.Helpers.TimeSpanHelper;
    ///   ...
    ///   if (LoginPage.IsVisible(Within(5).Seconds)) {
    ///   }
    ///   ...
    ///   LoginPage.WaitUntilAvailable(Within(5).Seconds);
    /// </code>
    public static class TimeSpanHelper
    {
        public static TimeSpanMaker Within(double timeAmount)
        {
            return new TimeSpanMaker(timeAmount);
        }
    }

    public class TimeSpanMaker
    {
        private double timeAmount;
        public TimeSpanMaker(double timeAmount)
        {
            this.timeAmount = timeAmount;
        }

        public TimeSpan Seconds { get { return TimeSpan.FromSeconds(this.timeAmount); } }
        public TimeSpan Milliseconds { get { return TimeSpan.FromMilliseconds(this.timeAmount); } }
        public TimeSpan Minutes { get { return TimeSpan.FromMinutes(this.timeAmount); } }
    }

}
