namespace BotAgent.Ifrit.Core.ElemActions.Elem
{
    using OpenQA.Selenium;
    using System;
    using System.Drawing;

    /// <summary>
    /// Methods that wasnt included to all another elemnt's types classes
    /// </summary>
    public class Other : ActionsBase
    {
        public Other(IfrPage parent)
        {
            HierarhicalParent = parent;
        }

        /// <summary>
        /// [get] Returns text that exactly this tag contains, but withoud subtags texts.
        /// [set] Clean-up text field and send keys -- not recommeded for use. Please, use correct type of web-element!
        /// </summary>
        public string Text
        {
            get
            {
                try
                {
                    return HtmlHelper.RemoveHtmlTagsAndTegsBody(this.HtmlInner);
                }
                catch (Exception)
                {
                    //// Normal behaviour
                }

                return string.Empty;
            }

            set
            {
                this.Element.Clear();
                this.Element.SendKeys(value);
            }
        }

        /// <summary>
        /// Returns text that contains in all sub-tags
        /// </summary>
        public string TextOfTagsTree
        {
            get 
            {
                try
                {
                    return this.Element.Text;
                }
                catch (Exception)
                {
                    //// Normal behaviour
                }

                return string.Empty;
            }
        }

        public bool Selected
        {
            get { return this.Element.Selected; }
            set
            {
                if (value == true)
                {
                    if (this.Element.Selected != true)
                    {
                        this.Element.Click();
                    }
                }
                else
                {
                    if (this.Element.Selected != false)
                    {
                        this.Element.Click();
                    }
                }
            }
        }

        public bool Clear()
        {
            try
            {
                this.Element.Clear();
                return true;
            }
            catch (Exception)
            {
                //// Normal behaviour
            }

            return false;
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

        public void PressEnter()
        {
            this.Element.SendKeys(Keys.Enter);
        }

        public Bitmap GetElementScreenshot()
        {
            Size elemSize = Element.Size;
            Point point = Element.Location;

            return HierarhicalParent.GetScreenshotBitmap(point, elemSize);
        }

        public string HtmlInner
        {
            get
            {
                try
                {
                    return Element.GetAttribute("innerHTML");
                }
                catch (Exception)
                {
                    if (ParamsLib.IfritOptions.LoggingEnabled)
                    {
                        HierarhicalParent.HierarhyParent.Log.ErrorMsg("Failed to find InnerHtml of the Element");
                    }
                }

                return string.Empty;
            }
        }

        public string HtmlOuter
        {
            get
            {
                try
                {
                    return Element.GetAttribute("outerHTML");
                }
                catch (Exception)
                {
                    if (ParamsLib.IfritOptions.LoggingEnabled)
                    {
                        HierarhicalParent.HierarhyParent.Log.ErrorMsg("Failed to find OuterHtml of the Element");
                    }
                }

                return string.Empty;
            }
        }

        public void DragElementTo(ActionsBase dropToElement)
        {
            var elemEndPoint = dropToElement.GetIWebElement();
                        
            HierarhicalParent.WebActions.DragAndDrop(Element, elemEndPoint).Perform();
        }
    }
}
