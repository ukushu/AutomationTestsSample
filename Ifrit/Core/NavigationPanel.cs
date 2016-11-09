using System.Threading;

namespace BotAgent.Ifrit.Core
{
    using System.Drawing;
    using OpenQA.Selenium;

    /// <summary>
    /// Browser's navigation panel buttons imitation
    /// </summary>
    public class NavigationPanel
    {
        private IWebDriver Instance;

        private IfrBrowser HierarhicalParent;

        private IfrLog Log;

        /// <summary>
        /// Get or set browser window position
        /// </summary>
        public Point Position
        {
            get { return Instance.Manage().Window.Position; }

            set { Instance.Manage().Window.Position = value; }
        }

        /// <summary>
        /// Get or set browser window size
        /// </summary>
        public Size Size
        {
            get { return Instance.Manage().Window.Size; }

            set { Instance.Manage().Window.Size = value; }
        }

        public NavigationPanel(IWebDriver instance, IfrBrowser parent)
        {
            Instance = instance;

            HierarhicalParent = parent;

            Log = parent.Log;
        }

        /// <summary>
        /// Go backward by browsers history of the current tab
        /// </summary>
        public void Backward()
        {
            Instance.Navigate().Back();
        }

        /// <summary>
        /// Go forward by browsers history of the current tab
        /// </summary>
        public void Forward()
        {
            Instance.Navigate().Forward();
        }

        /// <summary>
        /// Refresh/Reload current page
        /// </summary>
        public void Refresh()
        {
            Instance.Navigate().Refresh();
        }

        /// <summary>
        /// Stop loading current page
        /// </summary>
        public void StopPageLoad()
        {
            IJavaScriptExecutor js = Instance as IJavaScriptExecutor;
            js.ExecuteScript("window.stop();");
        }

        /// <summary>
        /// Checks browsers active page for url "about:blank"
        /// </summary>
        /// <returns></returns>
        public bool PageEmptyCheck()
        {

            if (Instance.Url == "about:blank")
            {
                Log.InfoMsg("Page is empty.");
                return true;
            }

            return false;
        }

        /// <summary>
        /// Navigates to URL. 
        /// It's important to use http:// or https:// prefix!
        /// </summary>
        public bool GoTo(string siteUrl)
        {
            Instance.Navigate().GoToUrl(siteUrl);

            HierarhicalParent.Page.WaitForPageLoad();

            Thread.Sleep(800);

            if (Instance.Url == siteUrl)
            {
                Log.InfoMsg("Url changed to '{0}'", siteUrl);
                return true;
            }
            else
            {
                Log.ErrorMsg("for some reason was loaded wrong page or page wasnt loaded. \r\nPageRequested: {0}\r\nPageLoaded:    {1}\r\n", siteUrl, Instance.Url);
                return false;
            }

            
        }

        /// <summary>
        /// Navigate to page and wait of page load.
        /// Its bad idea try to open XML page vith this method
        /// </summary>
        public bool GoToAndWaitForPageLoad(string siteUrl)
        {
            bool rezult = GoTo(siteUrl);

            if (rezult)
            {
                rezult = HierarhicalParent.Page.WaitForPageLoad();
            }

            return rezult;
        }

        /// <summary>
        /// Maximize browsers window (if window have UI)
        /// </summary>
        public void Maximize()
        {
            Instance.Manage().Window.Maximize();
        }
    }
}
