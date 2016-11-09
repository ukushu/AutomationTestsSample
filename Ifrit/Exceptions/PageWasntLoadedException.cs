namespace BotAgent.Ifrit.Exceptions
{
    public class PageWasntLoadedException : BrwsrException
    {
        public PageWasntLoadedException()
            : base(Lib.PageWasntLoadedMsg)
        {
        }
    }
}
