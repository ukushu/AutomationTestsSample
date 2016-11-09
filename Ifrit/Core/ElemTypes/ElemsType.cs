namespace BotAgent.Ifrit.Core.ElemTypes
{
    using System.Collections.Generic;
    using OpenQA.Selenium;
    using ElemActions.Elems;

    public class ElemsType
    {
        private IfrLog _log;

        /// <summary>
        /// Page where this _elements is locaed.
        /// </summary>
        public IfrPage HierarhicalParent;

        /// <summary>
        /// Creates wrapper for selenium web element to show exactly needed actions
        /// </summary>
        /// <param name="parentPage">Needt to set "this"</param>
        public ElemsType(IfrPage parentPage)
        {
            _log = parentPage.HierarhyParent.Log;
            HierarhicalParent = parentPage;
        }

        private List<IWebElement> _elements;

        public int Count
        {
            get
            {
                return this._elements.Count;
            }
        }

        /// <summary>
        /// For internal use only
        /// </summary>
        public void SetElements(List<IWebElement> webElements)
        {
            this._elements = webElements;
        }

        /// <summary>
        /// Mark _elements as Radiobuttons
        /// </summary>
        /// <returns>Methods for work with radiobuttons</returns>
        public RadioBtns AsRadioButtons()
        {
            RadioBtns radioButtonsBlock = new RadioBtns();
            bool allFoundElementsIsRadButtons = true;

            if (this._elements != null)
            {
                int foundElements = this._elements.Count;

                for (int i = 0; i <= foundElements; i++)
                {
                    if (this._elements[i].TagName.ToLower() != "input" || this._elements[i].GetAttribute("type").ToLower() != "radio")
                    {
                        allFoundElementsIsRadButtons = false;
                    }
                }

                if (allFoundElementsIsRadButtons)
                {
                    radioButtonsBlock.SetElements(this._elements);
                }
            }

            return radioButtonsBlock;
        }

        /// <summary>
        /// Returns element by index
        /// </summary>
        /// <param name="index">number of element</param>
        /// <returns>element by index</returns>
        public ElemType ReturnElementByIndex(int index)
        {
            ElemType element = new ElemType(HierarhicalParent);
            element.SetElement(this._elements[index]);

            return element;
        }

        public List<ElemType> AsList()
        {
            List<ElemType> elemsList = new List<ElemType>();

            if (_elements!= null && _elements.Count > 0)
            {
                foreach (var item in _elements)
                {

                    ElemType element = new ElemType(HierarhicalParent);
                    element.SetElement(item);

                    elemsList.Add(element);
                }
            }

            return elemsList;
        }

        public List<string> AsListOfImgHrefs()
        {
            List<string> elemsList = new List<string>();
            
            foreach (var item in _elements)
            {

                ElemType element = new ElemType(HierarhicalParent);
                element.SetElement(item);

                elemsList.Add(element.AsImage().SrcValue);
            }

            return elemsList;
        }

        public List<string> AsListOfHrefs()
        {
            List<string> elemsList = new List<string>();

            foreach (var item in _elements)
            {

                ElemType element = new ElemType(HierarhicalParent);
                element.SetElement(item);

                elemsList.Add(element.AsLink().Href);
            }

            return elemsList;
        }

        public List<string> AsListOfTexts()
        {
            List<string> elemsList = new List<string>();

            foreach (var item in _elements)
            {

                ElemType element = new ElemType(HierarhicalParent);
                element.SetElement(item);

                elemsList.Add(element.AsOther().Text);
            }

            return elemsList;
        }
    }
}
