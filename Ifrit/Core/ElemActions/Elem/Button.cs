namespace BotAgent.Ifrit.Core.ElemActions.Elem
{
    public class Button : Link
    {
        public Button(IfrPage parent) : base(parent)
        {
            HierarhicalParent = parent;
        }

        ////No need to add any methods
    }
}
