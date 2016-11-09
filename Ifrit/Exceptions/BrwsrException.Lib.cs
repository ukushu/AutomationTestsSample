namespace BotAgent.Ifrit.Exceptions
{
    public partial class BrwsrException
    {
        public class Lib
        {
            public const string PageWasntLoadedMsg = "Page failed to load for some reason. Possibly this is code error.";

            public const string BrowserWasntStartedMsg = "Failed to start browser instanse. Possibly unable to find browser driver by path.";

            public const string NoBrwsrFileMsg = "Cannot find Browser driver file at the following location: ";
        }
    }
}
