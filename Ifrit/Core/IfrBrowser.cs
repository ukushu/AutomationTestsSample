using System.Collections.Generic;
using System.IO;
using BotAgent.Ifrit.Exceptions;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.Extensions;

namespace BotAgent.Ifrit.Core
{
    using System;
    using System.Linq;
    using OpenQA.Selenium;
    
    public class IfrBrowser
    {
        /// <summary>
        /// Instanse of the original Selenium Web driver. 
        /// </summary>
        private IWebDriver Instance { get; set; }

        private static int _timeOutWaitPeriod = ParamsLib.BrwsrOptions.DefaultWaitPeriod;

        private List<string> _windowsList;
        private int _lastWindowIndex;
        private int _currentWindowIndex;

        private IfrPage _currentPage;

        public IfrLog Log;

        public NavigationPanel Nav;

        /// <summary>
        /// Returns current active page methods of current active window
        /// </summary>
        public IfrPage Page
        {
            get
            {
                return _currentPage;
            }
        }

        
        /// <summary>
        /// returns Browser session ID. This parameter needed for internal usage in most cases
        /// </summary>
        public string BrwsrSessionId { get; private set; }

        private void GenerateSessionId()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var randomId = new string(
                Enumerable.Repeat(chars, 8)
                            .Select(s => s[random.Next(s.Length)])
                            .ToArray());

            BrwsrSessionId = string.Format("{0}.{1}.{2}_{3}.{4}---{5}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, randomId);
        }

        /// <summary>
        /// Starts Browser session. By default is firefox.
        /// </summary>
        public IfrBrowser(Brwsr browser = Brwsr.Firefox)
        {
            InitLocal(browser);
        }
        
        private void InitLocal(Brwsr brwsr)
        {
            GenerateSessionId();
            this.Log = new IfrLog(BrwsrSessionId, Page);

            switch (brwsr)
            {
                case Brwsr.Firefox:
                    InitLocalFf();
                    break;
                case Brwsr.Chrome:
                    InitLocalChrome();
                    break;
                case Brwsr.FhantomJs:
                    InitLocalPhantomJs();
                    break;
                case Brwsr.Ie:
                    InitLocalIe();
                    break;
            }

            this._currentPage = new IfrPage(Instance, this);
            this.Nav = new NavigationPanel(Instance, this);

            TimeOutWaitPeriod(ParamsLib.BrwsrOptions.DefaultWaitPeriod);
            TimeOutPageLoad(ParamsLib.BrwsrOptions.PageLoadTimeOut);
            TimeOutPagePing(ParamsLib.BrwsrOptions.PagePingTimeOut);
        }

        /// <summary>
        /// Init local InternetExplorer Browser
        /// </summary>
        private void InitLocalIe()
        {
            Log.InfoMsg("IE Browser session started.\r\n-----------------------------------------");

            var options = new InternetExplorerOptions();

            options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;

            Instance = new InternetExplorerDriver(options);
        }

        private void InitLocalChrome()
        {
            Log.InfoMsg("Chrome Browser session started.\r\n-----------------------------------------");

            Instance = new ChromeDriver();
        }

        private void InitLocalFf()
        {
            Log.InfoMsg("FireFox Browser session started.\r\n-----------------------------------------");

            FirefoxProfile ffProfile = new FirefoxProfile();
            var port = ParamsLib.BrwsrOptions.Port;
            if (port != 0)
            {
                ffProfile.Port = ParamsLib.BrwsrOptions.Port;
            }

            ffProfile.AcceptUntrustedCertificates = true;
            ffProfile.DeleteAfterUse = true;

            this.Instance = new FirefoxDriver(ffProfile);
        }

        private void InitLocalPhantomJs()
        {
            ReInitPhantomJs(
                ParamsLib.BrwsrOptions.UserAgent,
                ParamsLib.BrwsrOptions.ProxyOn,
                ParamsLib.BrwsrOptions.ProxyHost,
                ParamsLib.BrwsrOptions.ProxyPort,
                ParamsLib.BrwsrOptions.ProxyLogin,
                ParamsLib.BrwsrOptions.ProxyPassword);

            Log.InfoMsg("PhantomJs Browser session started.\r\n-----------------------------------------");
        }

        /// <summary>
        /// Start session of the PhantomJs browser
        /// Is needed for configure specific parameters of the browser
        /// </summary>
        public void ReInitPhantomJs(string userAgent, bool proxyOn, string proxyHost, int proxyPort, string proxyLogin, string proxyPassword)
        {
            // Proxy settings
            string proxyHostStr = string.Format("{0}:{1}", proxyHost, proxyPort);
            string proxyAuth = string.Format("{0}:{1}", proxyLogin, proxyPassword);

            try
            {
                var currDirr = Directory.GetCurrentDirectory() + "\\" + "phantomjs.exe";

                var serviceJs = PhantomJSDriverService.CreateDefaultService();
                serviceJs.HideCommandPromptWindow = true;
                serviceJs.SslProtocol = "tlsv1";
                serviceJs.IgnoreSslErrors = true;
                serviceJs.LoadImages = false;
                

                //serviceJs.Port = ParamsLib.BrwsrOptions.Port;

                if (proxyOn)
                {
                    serviceJs.AddArguments("--proxy=" + proxyHostStr, "--proxy-type=http", "--proxy-auth=" + proxyAuth);
                }

                var options = new PhantomJSOptions();
                options.AddAdditionalCapability("phantomjs.page.settings.userAgent", userAgent);
                //options.AddAdditionalCapability("phantomjs.page.settings.resourceTimeout", 50);


                Instance = new PhantomJSDriver(serviceJs, options, TimeSpan.FromSeconds(180));
            }
            catch (Exception ex)
            {
                Log.FatalMsg(ex.Message);
                throw new BrowserWasntStartedException();
            }
        }

        /// <summary>
        /// Close browsers session. 
        /// Do not forget to assign null to browser element!
        /// </summary>
        public void Close()
        {
            if (Instance != null)
            {
                Instance.Quit();
                Log.InfoMsg("Current Browser session closed.\r\n-----------------------------------------");
                this.BrwsrSessionId = null;
            }
            else
            {
                Log.ErrorMsg("Looks like browser was already closed. Something going wrong");
            }
        }

        /// <summary>
        /// Open new window and make it active
        /// </summary>
        public void WindowNew()
        {
            Page.ExecuteJs("window.open('', '"+DateTime.Now +"', 'height=600,width=800');");
            
            WindowSetNext();
        }

        /// <summary>
        /// Open new window and make it active + go to Url
        /// Does not work in FJS
        /// </summary>
        public void WindowNew(string href)
        {
            WindowNew();

            Nav.GoTo(href);
        }

        public void WindowChange(int index)
        {
            index = Math.Abs(index);

            UpdateWindowsInfo();

            if (index > _windowsList.Count - 1)
            {
                throw new Exception("value is out of range");
            }
            
            Instance.SwitchTo().Window(_windowsList[index]);
        }

        public void WindowChange(string pageTitle)
        {
            foreach (var window in _windowsList)
            {
                Instance.SwitchTo().Window(pageTitle);
            }
        }

        public void WindowSetNext()
        {
            UpdateWindowsInfo();

            if (_lastWindowIndex != 0)
            {
                if (_currentWindowIndex == _lastWindowIndex)
                {
                    Instance.SwitchTo().Window(_windowsList[0]);
                }
                else
                {
                    Instance.SwitchTo().Window(_windowsList[_currentWindowIndex + 1]);
                }
            }
        }

        public void WindowSetPrevious()
        {
            UpdateWindowsInfo();

            if (_lastWindowIndex != 0)
            {
                if (_currentWindowIndex == 0)
                {
                    Instance.SwitchTo().Window(_windowsList[_lastWindowIndex]);
                }
                else
                {
                    Instance.SwitchTo().Window(_windowsList[_currentWindowIndex - 1]);
                }
            }
        }

        private void UpdateWindowsInfo()
        {
            _windowsList = new List<String>(Instance.WindowHandles);
            _lastWindowIndex = _windowsList.Count - 1;
            try
            {
                _currentWindowIndex = _windowsList.FindIndex(a => a == Instance.CurrentWindowHandle);
            }
            catch (Exception)
            {
                //// Current window possibly was closed
            }
        }

        /// <summary>
        /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// POSSIBLY NEED FIX TO PARAMETER!
        /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// </summary>
        public void TimeOutPagePing(int milliseconds)
        {
            if (milliseconds > 0)
            {
                Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(milliseconds));
            }
        }
        
        /// <summary>
        /// Sets the amount of time to wait for a page load to complete before throwing an error.
        /// + Sets the amount of time to wait for an asynchronous script to finish execution before throwing an error.
        /// _____________________________
        /// Set to 0 for leave default Selenium value
        /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// POSSIBLY NEED FIX TO PARAMETER!
        /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// </summary>
        public void TimeOutPageLoad(int milliseconds)
        {
            if (milliseconds > 0)
            {
                Instance.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromMilliseconds(milliseconds));

                Instance.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromMilliseconds(milliseconds));
            }
        }

        /// <summary>
        /// Change wait period for Wait() and failing time of some Loops.
        /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// POSSIBLY NEED FIX TO PARAMETER!
        /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// </summary>
        public void TimeOutWaitPeriod(int timePeriodInSeconds)
        {
            _timeOutWaitPeriod = timePeriodInSeconds;
        }

        

    }
}
