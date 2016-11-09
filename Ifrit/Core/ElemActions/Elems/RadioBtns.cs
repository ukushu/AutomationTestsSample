namespace BotAgent.Ifrit.Core.ElemActions.Elems
{
    using System.Collections.Generic;
    using OpenQA.Selenium;

    public class RadioBtns
    {
        /// <summary>
        /// Elements list 
        /// </summary>
        private List<IWebElement> elements = new List<IWebElement>();

        /// <summary>
        /// Setting of elements list
        /// </summary>
        /// <param name="list">elements list</param>
        public void SetElements(List<IWebElement> list)
        {
            this.elements.Clear();
            this.elements.AddRange(list);
        }
    }
}
