namespace BotAgent.Ifrit.Core.ElemActions.Elem
{
    public class Label : ActionsBase
    {
        public Label(IfrPage parent)
        {
            HierarhicalParent = parent;
        }

        public string Text
        {
            get
            {
                try
                {
                    return this.Element.Text;
                }
                catch
                {
                    // Normal behavior that it throwns exception on empty Text
                }

                return string.Empty;
            }
        }
    }
}
