using BotAgent.Ifrit.Core;

namespace BotAgent.Ifrit
{
    using NLog;
    using System.Diagnostics;
    using NLog.Targets;
    using System.IO;
    using System.Linq;
    
    public class IfrLog
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly string _logsPath;
        private readonly IfrPage _ifrPage;

        public TimerTest TimingTest; 

        public IfrLog(string sessionId, IfrPage browsersPage)
        {
            _logsPath = ParamsLib.IfritOptions.LogsDirPath.TrimEnd('\\') + @"\" + sessionId + @"\";
            _ifrPage = browsersPage;

            foreach (FileTarget target in LogManager.Configuration.AllTargets.Where(t => t is FileTarget).Cast<FileTarget>())
            {
                var filename = Path.GetFileName(target.FileName.ToString().Trim('\''));
                target.FileName = Path.Combine(_logsPath, filename);
            }
        }

        public void DebugMsg(string message)
        {
            DebugMsg(message, "");
        }

        public void DebugMsg(string message, params object[] args)
        {
            if (ParamsLib.IfritOptions.LoggingEnabled)
            {
                BuildCorrectString(ref message, args);

                Debug.WriteLine("DEBUG: " + message);

                _logger.Debug(message);
            }
        }

        public void WarnMsg(string message)
        {
            WarnMsg(message, "");
        }

        public void WarnMsg(string message, params object[] args)
        {
            if (ParamsLib.IfritOptions.LoggingEnabled)
            {
                string msg = string.Format(message, args);

                Debug.WriteLine("WARN: " + msg);

                _logger.Warn(msg);
            }
        }

        public void ErrorMsg(string message)
        {
            ErrorMsg(message, "");
        }

        public void ErrorMsg(string message, params object[] args)
        {
            if (ParamsLib.IfritOptions.LoggingEnabled)
            {
                message += CurrUrlStr();
                BuildCorrectString(ref message, args);

                Debug.WriteLine("ERROR: " + message);

                _logger.Error(message);
            }
        }

        public void FatalMsg(string message)
        {
            FatalMsg(message, "");
        }

        public void FatalMsg(string message, params object[] args)
        {
            if (ParamsLib.IfritOptions.LoggingEnabled)
            {
                message += CurrUrlStr();
                BuildCorrectString(ref message, args);

                Debug.WriteLine("FATAL: " + message);

                _logger.Fatal(message);
            }
        }

        public void InfoMsg(string message)
        {
            InfoMsg(message, "");
        }

        public void InfoMsg(string message, params object[] args)
        {
            if (ParamsLib.IfritOptions.LoggingEnabled)
            {
                BuildCorrectString(ref message, args);

                Debug.WriteLine("INFO: " + message);

                _logger.Info(message);
            }
        }

        public void LogCurrentPage()
        {
            if (ParamsLib.IfritOptions.LoggingEnabled)
            {
                string path = _logsPath + "ScreenShots/";

                _ifrPage.LogScreenshot(path, true);
            }
        }

        private void BuildCorrectString(ref string message, params object[] args)
        {
            if (args.Count() == 1 && args[0] == string.Empty)
            {
                return;
            }

            message = string.Format(message, args);
            message.Replace("\n\r", "${newline}");
        }

        private string CurrUrlStr()
        {
            var url = _ifrPage.Url;

            return string.Format("(Url: {0})", url);   
        }

        /// <summary>
        /// DOES NOT WORK IN IE!!!
        /// </summary>
        //public void LogTimings()
        //{
        //    if (ParamsLib.IfritOptions.LoggingEnabled)
        //    {
        //        string timingsStr = "";

        //        var tmngs = IfrBrowser.WebTimings;

        //        foreach (var timing in tmngs)
        //        {
        //            timingsStr += timing.Key + "\r\n";
        //        }

        //        InfoMsg(timingsStr);
        //    }
        //}
    }
}
