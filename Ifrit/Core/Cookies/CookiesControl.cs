namespace BotAgent.Ifrit.Core.Cookies
{
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Block of methods for work with cookies
    /// </summary>
    public class CookiesControl
    {
        private IWebDriver Instance;

        private IfrLog Log;

        public CookiesControl(IWebDriver instance, IfrBrowser parent)
        {
            Instance = instance;
            Log = parent.Log;
        }

        /// <summary>
        /// Add cookie to the current session of the browser(Need to be on the page of correct site)
        /// </summary>
        /// <param name="name">Cookie Name</param>
        /// <param name="domain">Cookie Domain</param>
        /// <param name="value">Cookie Value</param>
        /// <param name="path">Cookie path</param>
        /// <param name="expDate">Cookie exirity date</param>
        public void Add(string name, string domain, string value, string path, DateTime expDate)
        {
            OpenQA.Selenium.Cookie myCookie = new Cookie(name, value, domain, path, expDate);

            try
            {
                Instance.Manage().Cookies.DeleteCookieNamed(name);
            }
            catch
            {
                Log.WarnMsg("Failed to delete Cookie from browser. Looks like its absent cookie with name '{0}'", name);
            }

            try
            {
                Instance.Manage().Cookies.AddCookie(myCookie);
            }
            catch
            {
                Log.ErrorMsg("Failed to add cookie. Most possible is Domain from Cookie and current domain in browser is not the same.");
            }

        }

        /// <summary>
        /// Get all cookies from the current browser's session
        /// </summary>
        /// <returns>CookiesControl List</returns>
        public List<OpenQA.Selenium.Cookie> GetAll()
        {
            List<OpenQA.Selenium.Cookie> cookies = new List<OpenQA.Selenium.Cookie>();

            cookies.AddRange(Instance.Manage().Cookies.AllCookies);

            return cookies;
        }

        /// <summary>
        /// Remove all cookies from the browser
        /// </summary>
        public void DeleteAll()
        {
            Instance.Manage().Cookies.DeleteAllCookies();
        }

        /// <summary>
        /// Rename cookie by Cookie Name
        /// </summary>
        /// <param name="name">Cookie Name</param>
        public void Delete(string name)
        {
            Instance.Manage().Cookies.DeleteCookieNamed(name);
        }

        /// <summary>
        /// Remove cookie form the browser by cookie object
        /// </summary>
        /// <param name="cookie">cookie object</param>
        public void Delete(Cookie cookie)
        {
            Instance.Manage().Cookies.DeleteCookie(cookie);
        }

        /// <summary>
        /// Get cookie from the current browser session by name
        /// </summary>
        /// <param name="name">cookie name</param>
        /// <returns>found cookie</returns>
        public OpenQA.Selenium.Cookie Get(string name)
        {
            Cookie result;

            result = Instance.Manage().Cookies.GetCookieNamed(name);

            return result;
        }
    }
}