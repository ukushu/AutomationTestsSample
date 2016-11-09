namespace BotAgent.Ifrit.Core.ElemActions.Elem
{
    using System;
    using OpenQA.Selenium;

    /// <summary>
    /// Absract class for create childclasses with actions for web-elements
    /// </summary>
    abstract public class ActionsBase
    {
        /// <summary>
        /// Original selenium Web-element for doing actions. 
        /// Its must be assigned via SetElement(IWebElement obj)
        /// For internal use only
        /// </summary>
        protected IWebElement Element;

        /// <summary>
        /// Returns page where this web-element located
        /// </summary>
        public IfrPage HierarhicalParent;
        
        /// <summary>
        /// This method is for internal use only.
        /// Please ignore it.
        /// </summary>
        public void SetElement(IWebElement obj)
        {
            this.Element = obj;
        }

        /// <summary>
        /// Gets value of some specific attribute of html element
        /// </summary>
        /// <param name="atrName">attribute name</param>
        /// <returns>value of the attrubute. If its empty or not exist -- will be returned string.empty</returns>
        public string GetAttribute(string atrName)
        {
            string attr = string.Empty;

            try
            {
                attr = this.Element.GetAttribute(atrName);
            }
            catch (Exception)
            {
            }

            return attr;
        }

        /// <summary>
        /// Check for existense of element on the page
        /// </summary>
        public bool IsExist()
        {
            if (this.Element != null)
            {
                if (this.Element.Size.Width > 1)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check for visibility
        /// </summary>
        public bool IsVisible()
        {
            if (this.Element != null && this.Element.Displayed)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns value Enabled attribute of the element
        /// </summary>
        public bool IsEnabled()
        {
            if (this.Element != null && this.Element.Enabled)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Send keys to the element
        /// </summary>
        /// <param name="str">string to send</param>
        /// <returns>success of sending</returns>
        public bool SendKeys(string str)
        {
            try
            {
                this.Element.SendKeys(str);

                return true;
            }
            catch (Exception)
            {
                // Не нужно фейлить если невозможно послать клавиши    
            }

            return false;
        }

        /// <summary>
        /// Send Enter Press
        /// </summary>
        public void SendKeyEnter()
        {
            Element.SendKeys(Keys.Enter);
        }

        /*
        /// <summary>
        /// Calling of context menu on element (like RMB click)
        /// </summary>
        public void ContextMenu()
        {
            var driver = Brwsr.Instance;

            Actions act = new Actions(driver);

            var context = act.ContextClick(this.element).Build();
            context.Perform();
        }*/
    }
}
