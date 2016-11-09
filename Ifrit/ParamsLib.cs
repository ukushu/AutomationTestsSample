namespace BotAgent.Ifrit
{
    public class ParamsLib
    {
        public class BrwsrOptions
        {
            public const string BrwsrPath = @"\BrwsrDrivers";

            public const string IePath = @"C:\Users\lmbtuh0\Documents\Visual Studio 2012\Projects\Demo_test\Demo_test\Ifrit\Utils\BrwsrDrivers\ie";

            /// Sets the amount of time to wait for a page load to complete before throwing an error.
            /// + Sets the amount of time to wait for an asynchronous script to finish execution before throwing an error.
            /// Set to 0 for disable
            public const int PageLoadTimeOut = 0;

            /// Specifies the amount of time the driver should wait when searching for an element 
            /// if it is not immediately present.
            /// Set to 0 for disable
            public const int PagePingTimeOut = 0;

            /// <summary>
            /// This is default time to fail some Loops. Its bad idea to set it more than 3000 ms.
            /// </summary>
            public const int DefaultWaitPeriod = 2000;

            /// <summary>
            /// If you getting errors like: "The status of the exception was ConnectFailure","Unable to connect to the remote server;"
            /// try to change this port to fix. Looks like some application got control of the port.
            /// Also you can change it to 0 to take port randomly.
            /// 
            /// At the moment Firefox and PhantomJS only
            /// </summary>
            public const int Port = 0;

            /// <summary>
            /// ONLY FOR PhantomJS block
            /// </summary>
            public const string BrowserCookiesFile = @"\cookies.xml";

            public const string UserAgent = @"Mozilla/5.0 (Windows NT 6.1; WOW64; rv:38.0) Gecko/20100101 Firefox/38.0";
            
            public const bool ProxyOn = false;
            public const string ProxyHost = "";
            public const int ProxyPort = 3128;
            public const string ProxyLogin = "proxy";
            public const string ProxyPassword = "proxy";

        }

        /// <summary>
        /// Настройки по сбору скриншотов с невидимого браузера
        /// </summary>
        public class IfritOptions
        {
            public const bool LoggingEnabled = false;
            public const string LogsDirPath = @"c:\Ifrit\";
            public const string ScreenShotName = @"ScreenShot_";
        }
    }
}
