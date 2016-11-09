namespace BotAgent.Ifrit.Exceptions
{
    public class NoBrwsrFileException : BrwsrException
    {
        public NoBrwsrFileException(string browserPath)
            : base(Lib.NoBrwsrFileMsg + browserPath)
        {
        }
    }
}
