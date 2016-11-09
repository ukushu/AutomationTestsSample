using OpenQA.Selenium.Firefox;

namespace BotAgent.Ifrit.Core.ElemActions.Elem
{
    using System;
    using System.Threading;
    using OpenQA.Selenium;

    public class Link : ActionsBase
    {
        public Link(IfrPage parent)
        {
            HierarhicalParent = parent;
        }

        public string Href
        {
            get
            {
                for (int i = 0; i <= 3; i++)
                {
                    var href = GetAttribute("href");

                    if (href != null)
                    {
                        return href;
                    }
                }

                return null;
            }
        }

        public bool Click()
        {
            try
            {
                this.Element.Click();

                return true;
            }
            catch (Exception)
            {
                //// Normal behaviour
            }

            return false;
        }

        /// <summary>
        /// You can use this if clicking doesnt work
        /// </summary>
        /// <returns></returns>
        public bool GoByHref()
        {
            var host = HierarhicalParent.Url;

            int index = host.IndexOf("/",8);
            if (index > 0)
                host = host.Substring(0, index);

            var currHref = Href;

            if (currHref == null || !host.Contains("http"))
            {
                return false;
            }

            if (currHref.Contains("http://") || currHref.Contains("https://"))
            {
                HierarhicalParent.Url = Href;
            }
            else
            {
                HierarhicalParent.Url = host + Href;
            }

            return true;
        }

        /// <summary>
        /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// At the moment there is no ability to change active tab
        /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// </summary>
        public bool OpenInNewTab()
        {
            HierarhicalParent.WebActions.KeyDown(Keys.LeftControl).Click(Element).KeyUp(Keys.LeftControl).Build().Perform();
            
            return false;
        }

        public void OpenInNewWindow()
        {
            HierarhicalParent.WebActions.KeyDown(Keys.Shift).Click(Element).KeyUp(Keys.Shift).Build().Perform();
            Thread.Sleep(600);
        }

        public bool ClickAndWaitForPageLoad()
        {
            if ( Click() )
            {
                return HierarhicalParent.WaitForPageLoad();
            }

            return false;
        }

        public bool ClickAndWaitForAjax()
        {
            if ( Click() )
            {

                return HierarhicalParent.WaitForAjax();
            }

            return false;
        }
    }
}
