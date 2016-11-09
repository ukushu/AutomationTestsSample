namespace BotAgent.Ifrit.Core.ElemTypes
{
    using ElemActions.Elem;
    using OpenQA.Selenium;
    
    public class ElemType
    {
        private IfrLog _log;
        private IWebElement _element;

        /// <summary>
        /// Returns page where this element is located
        /// </summary>
        public IfrPage HierarhicalParent;

        /// <summary>
        /// Creates wrapper for selenium web element to show exactly needed actions
        /// </summary>
        /// <param name="parentPage">Needt to set "this"</param>
        public ElemType(IfrPage parentPage)
        {
            _log = parentPage.HierarhyParent.Log;

            HierarhicalParent = parentPage;
        }

        public bool IsNull
        {
            get 
            {
                if (_element == null)
                    return true;
                else
                    return false;
            }
        }

        public void SetElement(IWebElement obj)
        {
            this._element = obj;
        }

        /// <summary>
        /// Mark element as Drop Down List
        /// </summary>
        /// <returns>Methods for this element type</returns>
        public DdList AsDropDownList()
        {
            DdList dropDownList = new DdList(HierarhicalParent);

            if (this._element != null)
            {
                if (this._element.TagName.ToLower() == "select")
                {
                    dropDownList.SetElement(this._element);
                }
            }

            return dropDownList;
        }

        /// <summary>
        /// Mark element as CheckBox
        /// </summary>
        /// <returns>Methods for this element type</returns>
        public CheckBox AsCheckBox()
        {
            CheckBox chkBox = new CheckBox(HierarhicalParent);

            if (this._element != null)
            {
                if (this._element.TagName.ToLower() == "input" && this._element.GetAttribute("type").ToLower() == "checkbox")
                {
                    chkBox.SetElement(this._element);
                }
            }

            return chkBox;
        }

        /// <summary>
        /// Mark element as Text Area (multi-string field)
        /// </summary>
        /// <returns>Methods for this element type</returns>
        public TextArea AsTextArea()
        {
            TextArea textArea = new TextArea(HierarhicalParent);

            if (this._element != null)
            {
                string tag = this._element.TagName.ToLower();

                if ((tag == "textarea") | (tag == "input"))
                {
                    textArea.SetElement(this._element);
                }
            }

            return textArea;
        }

        /// <summary>
        /// Mark element as Input (single-string field)
        /// </summary>
        /// <returns>Methods for this element type</returns>
        public Input AsInput()
        {
            Input inputElement = new Input(HierarhicalParent);

            if (this._element != null)
            {
                if (this._element.TagName.ToLower() == "input" && (this._element.GetAttribute("type").ToLower() == "text" || this._element.GetAttribute("type").ToLower() == "password"))
                {
                    inputElement.SetElement(this._element);
                }
            }

            return inputElement;
        }

        /// <summary>
        /// Mark element as Other (All methods that wasnt implemented to some specific element type)
        /// </summary>
        /// <returns>Methods for this element type</returns>
        public Other AsOther()
        {
            var anotherElement = new Other(HierarhicalParent);

            if (this._element != null)
            {
                anotherElement.SetElement(this._element);
            }

            return anotherElement;
        }

        /// <summary>
        /// Mark element as Link
        /// </summary>
        /// <returns>Methods for this element type</returns>
        public Link AsLink()
        {
            Link anotherElement = new Link(HierarhicalParent);

            if (this._element != null)
            {
                anotherElement.SetElement(this._element);
            }

            return anotherElement;
        }

        /// <summary>
        /// Mark element as Button
        /// </summary>
        /// <returns>Methods for this element type</returns>
        public Button AsButton()
        {
            Button anotherElement = new Button(HierarhicalParent);

            if (this._element != null)
            {
                anotherElement.SetElement(this._element);
            }

            return anotherElement;
        }

        public IfrImage AsImage()
        {
            IfrImage anotherElement = new IfrImage(HierarhicalParent);

            if (this._element != null)
            {
                anotherElement.SetElement(this._element);
            }

            return anotherElement;
        }
        
        /// <summary>
        /// Get parent of current web element
        /// </summary>
        /// <returns>Web-element parent</returns>
        public ElemType GetParent()
        {
            return GetParent(1);
        }

        /// <summary>
        /// Get some specific generation parent of current element.
        /// </summary>
        /// <param name="parentCounter">Number of generation</param>
        /// <returns>parent element</returns>
        public ElemType GetParent(int parentCounter)
        {
            string xpath = string.Empty;
            for (int i = 0; i < parentCounter; i++)
            {
                xpath += "../";
            }

            ElemType parentElement = new ElemType(HierarhicalParent);

            try
            {
                parentElement.SetElement(this._element.FindElement(By.XPath(xpath + "self::*")));
            }
            catch
            {
                _log.ErrorMsg("cannot get parent element, as this element is empty");
            }

            return parentElement;
        }
    }
}
