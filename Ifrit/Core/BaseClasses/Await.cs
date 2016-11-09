namespace BotAgent.Ifrit.Core.BaseClasses
{
    public class Await
    {
        public enum Status
        {
            Visible, 
            NotVisible, 
            Present, 
            NotPresent, 
            ReadOnly, 
            NotReadOnly, 
            Empty, 
            NotEmpty, 
            Enabled, 
            NotEnabled, 
            TextMatch, 
            TextEquals,
            TextNotEquals,
            TextEqualsCaseSensitive,
            TextNotEqualsCaseSensitive,
            TextPartExist,
            TextPartNotExist,
            TextPartExistCaseSensitive,
            TextPartNotExistCaseSensitive
        }

        public int CurrStatus;
        public string Value;
        public int TimeOut;
        private const int _defaultTimeOut  = ParamsLib.BrwsrOptions.DefaultWaitPeriod;

        public Await(int currStatus, string value, int timeOut)
        {
            this.Value = value;
            this.CurrStatus = currStatus;
            this.TimeOut = timeOut;
        }

        public static Await Visible()
        {
            return Visible(_defaultTimeOut);
        }

        public static Await Visible(int timeOut)
        {
            return new Await((int)Status.Visible, string.Empty, timeOut);
        }

        public static Await NotVisible()
        {
            return NotVisible(_defaultTimeOut);
        }

        public static Await NotVisible(int timeOut)
        {
            return new Await((int)Status.NotVisible, string.Empty, timeOut);
        }

        public static Await Present()
        {
            return Present(_defaultTimeOut);
        }

        public static Await Present(int timeOut)
        {
            return new Await((int)Status.Present, string.Empty, timeOut);
        }

        public static Await NotPresent()
        {
            return NotPresent(_defaultTimeOut);
        }

        public static Await NotPresent(int timeOut)
        {
            return new Await((int)Status.NotPresent, string.Empty, timeOut);
        }

        public static Await ReadOnly()
        {
            return ReadOnly(_defaultTimeOut);
        }

        public static Await ReadOnly(int timeOut)
        {
            return new Await((int)Status.ReadOnly, string.Empty, timeOut);
        }

        public static Await NotReadOnly()
        {
            return NotReadOnly(_defaultTimeOut);
        }

        public static Await NotReadOnly(int timeOut)
        {
            return new Await((int)Status.NotReadOnly, string.Empty, timeOut);
        }
        
        public static Await Empty()
        {
            return Empty(_defaultTimeOut);
        }

        public static Await Empty(int timeOut)
        {
            return new Await((int)Status.Empty, string.Empty, timeOut);
        }

        public static Await NotEmpty()
        {
            return NotEmpty(_defaultTimeOut);
        }

        public static Await NotEmpty(int timeOut)
        {
            return new Await((int)Status.NotEmpty, string.Empty, timeOut);
        }

        public static Await Enabled()
        {
            return Enabled(_defaultTimeOut);
        }

        public static Await Enabled(int timeOut)
        {
            return new Await((int)Status.Enabled, string.Empty, timeOut);
        }

        public static Await NotEnabled()
        {
            return NotEnabled(_defaultTimeOut);
        }

        public static Await NotEnabled(int timeOut)
        {
            return new Await((int)Status.NotEnabled, string.Empty, timeOut);
        }

        public static Await TextMatch(string regex)
        {
            return TextMatch(regex, _defaultTimeOut);
        }

        public static Await TextMatch(string regex, int timeOut)
        {
            return new Await((int)Status.TextMatch, regex, timeOut);
        }

        public static Await TextEquals(string str)
        {
            return TextEquals(str, _defaultTimeOut);
        }

        public static Await TextEquals(string str, int timeOut)
        {
            return new Await((int)Status.TextEquals, str, timeOut);
        }
        
        public static Await TextNotEquals(string str)
        {
            return TextNotEquals(str, _defaultTimeOut);
        }

        public static Await TextNotEquals(string str, int timeOut)
        {
            return new Await((int)Status.TextNotEquals, str, timeOut);
        }

        public static Await TextEqualsCaseSensitive(string str)
        {
            return TextEqualsCaseSensitive(str, _defaultTimeOut);
        }

        public static Await TextEqualsCaseSensitive(string str, int timeOut)
        {
            return new Await((int)Status.TextEqualsCaseSensitive, str, timeOut);
        }

        public static Await TextNotEqualsCaseSensitive(string str)
        {
            return TextNotEqualsCaseSensitive(str, _defaultTimeOut);
        }

        public static Await TextNotEqualsCaseSensitive(string str, int timeOut)
        {
            return new Await((int)Status.TextNotEqualsCaseSensitive, str, timeOut);
        }

        public static Await TextPartExist(string subString)
        {
            return TextPartExist(subString, _defaultTimeOut);
        }

        public static Await TextPartExist(string subString, int timeOut)
        {
            return new Await((int)Status.TextPartExist, subString, timeOut);
        }

        public static Await TextPartNotExist(string subString)
        {
            return TextPartNotExist(subString, _defaultTimeOut);
        }

        public static Await TextPartNotExist(string subString, int timeOut)
        {
            return new Await((int)Status.TextPartNotExist, subString, timeOut);
        }
        
        public static Await TextPartExistCaseSensitive(string subString)
        {
            return TextPartExistCaseSensitive(subString, _defaultTimeOut);
        }

        public static Await TextPartExistCaseSensitive(string subString, int timeOut)
        {
            return new Await((int)Status.TextPartExistCaseSensitive, subString, timeOut);
        }

        public static Await TextPartNotExistCaseSensitive(string subString)
        {
            return TextPartNotExistCaseSensitive(subString, _defaultTimeOut);
        }

        public static Await TextPartNotExistCaseSensitive(string subString, int timeOut)
        {
            return new Await((int)Status.TextPartNotExistCaseSensitive, subString, timeOut);
        }
    }
}


