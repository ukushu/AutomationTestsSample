namespace BotAgent.Ifrit.Core.ElemActions.Elem
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Linq;

    /// <summary>
    /// Methods for DropDownList [select in html]
    /// </summary>
    public class DdList : ActionsBase
    {
        public SelActions Select = new SelActions();

        public DeselActions Deselect = new DeselActions();

        public DdList(IfrPage parent)
        {
            HierarhicalParent = parent;
        }

        new public bool SetElement(IWebElement obj)
        {
            try
            {
                this.Element = obj;
                this.Select.SetElement(this.Element);
                this.Deselect.SetElement(this.Element);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public class SelActions
        {
            private IWebElement element;

            /// <summary>
            /// Select all items
            /// </summary>
            public bool All()
            {
                SelectElement select = new SelectElement(this.element);

                int numberOfValues = select.Options.Count();

                try
                {
                    for (int i = 0; i <= numberOfValues; i++)
                    {
                        select.SelectByIndex(i);
                    }

                    return true;
                }
                catch (Exception)
                {
                    // As designed
                }

                return false;
            }

            public bool SetElement(IWebElement obj)
            {
                this.element = obj;

                return true;
            }

            /// <summary>
            /// Select by text[s] value
            /// </summary>
            public bool ByText(params string[] selTexts)
            {
                SelectElement select = new SelectElement(this.element);

                int numberOfValues = selTexts.Count();

                try
                {
                    for (int i = 0; i <= numberOfValues; i++)
                    {
                        select.SelectByText(selTexts[i]);
                    }

                    return true;
                }
                catch (Exception)
                {
                    //// Nothing need to do. As designed.
                }

                return false;
            }

            /// <summary>
            /// Select by index(es)
            /// </summary>
            /// <param name="selIndexes">Option index(es)</param>
            /// <returns>success</returns>
            public bool ByIndex(params int[] selIndexes)
            {
                SelectElement select = new SelectElement(this.element);

                int numberOfValues = selIndexes.Count();

                try
                {
                    for (int i = 0; i <= numberOfValues; i++)
                    {
                        select.SelectByIndex(selIndexes[i]);
                    }

                    return true;
                }
                catch (Exception)
                {
                    //// Need to do nothing
                }

                return false;
            }

            /// <summary>
            /// Select by Value(s)
            /// </summary>
            /// <param name="selValues">Option Value(s)</param>
            /// <returns>Success</returns>
            public bool ByValue(params string[] selValues)
            {
                SelectElement select = new SelectElement(this.element);

                int numberOfValues = selValues.Count() - 1;

                try
                {
                    for (int i = 0; i <= numberOfValues; i++)
                    {
                        select.SelectByValue(selValues[i]);
                    }

                    return true;
                }
                catch (Exception)
                {
                    //// Need to do nothing
                }

                return false;
            }
        }

        public class DeselActions
        {
            private IWebElement element;

            public bool SetElement(IWebElement obj)
            {
                this.element = obj;

                return true;
            }

            /// <summary>
            /// Deselect all items
            /// </summary>
            public bool All()
            {
                try
                {
                    SelectElement select = new SelectElement(this.element);
                    select.DeselectAll();
                    return true;
                }
                catch (Exception)
                {
                    //// Need to do nothing
                }

                return false;
            }

            public bool ByText(params string[] deselTexts)
            {
                SelectElement select = new SelectElement(this.element);

                int numberOfValues = deselTexts.Count();

                try
                {
                    for (int i = 0; i <= numberOfValues; i++)
                    {
                        select.DeselectByText(deselTexts[i]);
                    }

                    return true;
                }
                catch (Exception)
                {
                    //// Need to do nothing
                }

                return false;
            }

            public bool ByIndex(params int[] deselIndexes)
            {
                SelectElement select = new SelectElement(this.element);

                int numberOfValues = deselIndexes.Count();

                try
                {
                    for (int i = 0; i <= numberOfValues; i++)
                    {
                        select.DeselectByIndex(deselIndexes[i]);
                    }

                    return true;
                }
                catch (Exception)
                {
                    //// Normal behaviour
                }

                return false;
            }

            public bool ByValue(params string[] deselValues)
            {
                SelectElement select = new SelectElement(this.element);

                int numberOfValues = deselValues.Count() - 1;

                try
                {
                    for (int i = 0; i <= numberOfValues; i++)
                    {
                        select.DeselectByValue(deselValues[i]);
                    }

                    return true;
                }
                catch (Exception)
                {
                    //// Normal behaviour
                }

                return false;
            }
        }
    }
}
