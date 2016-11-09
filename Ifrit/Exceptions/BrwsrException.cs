namespace BotAgent.Ifrit.Exceptions
{
    using System;

    public partial class BrwsrException: Exception
    {
        public BrwsrException(string message)
            : base(message)
        {
        }
    }
}
