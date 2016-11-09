namespace BotAgent.Ifrit.Exceptions
{
    class BrowserWasntStartedException : BrwsrException
    {
        public BrowserWasntStartedException()
            : base(Lib.BrowserWasntStartedMsg)
        {
        }
    }
}
