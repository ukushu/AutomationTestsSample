namespace BotAgent.Ifrit.Core.ElemActions.Elem
{
    /// <summary>
    /// Methos for single line text field [input]
    /// </summary>
    public class Input : TextArea
    {
        public Input(IfrPage parent) : base(parent)
        {
            HierarhicalParent = parent;
        }

        ////No need to add any methods
    }
}
