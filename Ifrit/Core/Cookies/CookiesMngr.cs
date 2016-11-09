namespace BotAgent.Ifrit.Core.Cookies
{
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;

    public partial class CookiesMngr
    {
        private Core cmCore;
        public CookiesControl CookiesController;

        public CookiesMngr(IWebDriver instance, IfrBrowser parent)
        {
            cmCore = new Core();
            CookiesController = new CookiesControl(instance, parent);
        }

        /// <summary>
        /// Load cookies from XML to browser
        /// </summary>
        /// <param name="username">Profile name to save cookies</param>
        public void LoadFromXmlAll(string username)
        {
            List<Core.MyCookie> cookiesList = cmCore.GetCookiesFromXml(username);

            foreach (Core.MyCookie myCookie in cookiesList)
            {
                DateTime expDate = new DateTime();

                if (myCookie.Expiries == "")
                {
                    expDate = DateTime.Now.AddDays(2);
                }
                else
                {
                    expDate = DateTime.Parse(myCookie.Expiries);
                }

                CookiesController.Add(myCookie.Name, myCookie.Domain, myCookie.Value, myCookie.Path, expDate);
            }
        }

        /// <summary>
        /// Save cookies from browser to XML
        /// </summary>
        /// <param name="username">Profile name to save cookies</param>
        public void SaveToXmlAll(string username)
        {
            cmCore.RemoveCookiesByProfile(username);
                    
            var brwsrCookies = CookiesController.GetAll();

            foreach (Cookie cook in brwsrCookies)
            {
                var expirity = cook.Expiry.ToString();

                cmCore.AddCookiesToXml(username, cook.Name, cook.Value, cook.Domain, cook.Path, expirity,
                    cook.Secure.ToString());
            }

            cmCore.Save();
        }

        /// <summary>
        /// Save cookies from browser to XML
        /// </summary>
        /// <param name="username">Profile name to save cookies</param>
        public void SaveToXmlSecureOnly(string username)
        {
            cmCore.RemoveCookiesByProfile(username);

            var brwsrCookies = CookiesController.GetAll();

            foreach (Cookie cook in brwsrCookies)
            {
                var expirity = cook.Expiry.ToString();

                if (cook.Secure == true)
                    cmCore.AddCookiesToXml(username, cook.Name, cook.Value, cook.Domain, cook.Path, expirity,
                        cook.Secure.ToString());
            }

            cmCore.Save();
        }
    }
}
