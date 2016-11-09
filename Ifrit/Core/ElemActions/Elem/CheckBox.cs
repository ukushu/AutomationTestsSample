namespace BotAgent.Ifrit.Core.ElemActions.Elem
{
    public class CheckBox : ActionsBase
    {
        public CheckBox(IfrPage parent)
        {
            HierarhicalParent = parent;
        }

        public bool Checked
        {
            get { return this.Element.Selected; }

            set
            {
                if (value == true)
                {
                    if (this.Element.Selected == false)
                    {
                        this.Element.Click();
                    }
                }
                else
                {
                    if (this.Element.Selected == true)
                    {
                        this.Element.Click();
                    }
                }
            }
        }
    }
}
