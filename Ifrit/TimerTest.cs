namespace BotAgent.Ifrit
{
    using System.Diagnostics;

    /// <summary>
    /// Class created for checking how much time takes any block of code to execute
    /// </summary>
    public class TimerTest
    {
        /// <summary>
        /// Stopwatch to check the time period
        /// </summary>
        private readonly Stopwatch _stopwatch;

        /// <summary>
        /// TimerTest Start / Initialisation of the stopwatch
        /// Results will be shown in logs and in debug window
        /// </summary>
        public TimerTest()
        {
            this._stopwatch = Stopwatch.StartNew();
        }

        /// <summary>
        /// TimerTest Start / Initialisation of the stopwatch
        /// Results will be shown in debug window only
        /// </summary>
        public TimerTest(string message)
        {
            Debug.WriteLine(message);
            this._stopwatch = Stopwatch.StartNew();
        }

        /// <summary>
        /// TimerTest Start / Initialisation of the stopwatch
        /// Results will be shown in logs and in debug window
        /// </summary>
        public TimerTest(string message, IfrLog log)
        {
            log.InfoMsg(message);
            this._stopwatch = Stopwatch.StartNew();
        }

        /// <summary>
        /// Finish Timer test and show result in logs and in debug window
        /// </summary>
        /// <param name="actionNameToTest">Message to show on the finish of the test. Example: "Page was successfully loaded in" or "Page load was failed after"</param>
        public void Finish(string actionNameToTest)
        {
            string msg = string.Format("{0} {1}", actionNameToTest, this._stopwatch.Elapsed);

            Debug.WriteLine(msg);
        }

        /// <summary>
        /// Finish Timer test and show result in logs and in debug window
        /// </summary>
        /// <param name="actionNameToTest">Message to show on the finish of the test. Example: "Page was successfully loaded in" or "Page load was failed after"</param>
        /// <param name="log">Loger instance</param>
        public void Finish(string actionNameToTest, IfrLog log)
        {
            string msg = string.Format("{0} {1}", actionNameToTest, this._stopwatch.Elapsed);

            log.InfoMsg(msg);
        }

        public void FinishAndNewLap(string actionNameToTest)
        {
            Finish(actionNameToTest);
            _stopwatch.Restart();
        }

        /// <summary>
        /// </summary>
        /// <returns>Elapsed time in Ms from start of TimerTest</returns>
        public int GetElapsedTime()
        {
            int elaplsedMs = 
                _stopwatch.Elapsed.Hours * 60 * 60 * 1000 +
                _stopwatch.Elapsed.Minutes * 60 *1000 +
                _stopwatch.Elapsed.Seconds * 1000 +
                _stopwatch.Elapsed.Milliseconds;
                
            return elaplsedMs;
        }
    }
}
