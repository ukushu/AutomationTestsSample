using OpenQA.Selenium;

namespace BotAgent.Ifrit.Core
{
    using System;
        
    /// <summary>
    /// Block of methods for Набор методов для управления попап-Алертами
    /// </summary>
    public class PopupAlertActions
    {
        private const string CannotClickErrorStr = "Was tried to click on the {0} button in the popup alert, but looks like its does not exist!";
        private const string CannotGetTextStr = "Was tried to get text from popup alert, but looks like its does not exist!";

        private IWebDriver Instance;

        private IfrLog Log;

        public PopupAlertActions(IWebDriver instance, IfrBrowser parent)
        {
            Instance = instance;

            Log = parent.Log;
        }

        /// <summary>
        /// Push Ok/Accept button iside of the alert popup
        /// </summary>
        /// <returns>Bool object that was button successfully clicked or not</returns>
        public bool Accept()
        {
            try
            {
                Instance.SwitchTo().Alert().Accept();
                return true;
            }
            catch (Exception)
            {
                string str = string.Format(CannotClickErrorStr, "Ok/Accept");
                Log.ErrorMsg(str);
            }

            return false;
        }

        /// <summary>
        /// Push Cancel button iside of the alert popup
        /// </summary>
        /// <returns>Bool object that was button successfully clicked or not</returns>
        public bool Dismiss()
        {
            try
            {
                Instance.SwitchTo().Alert().Dismiss();
                return true;
            }
            catch (Exception)
            {
                string str = string.Format(CannotClickErrorStr, "Cancel");
                Log.ErrorMsg(str);
            }

            return false;
        }

        /// <summary>
        /// Get alert text
        /// </summary>
        /// <returns>Alert's information text</returns>
        public string GetText()
        {
            string alertText = string.Empty;

            try
            {
                alertText = Instance.SwitchTo().Alert().Text;
                return alertText;
            }
            catch
            {
                Log.ErrorMsg(CannotGetTextStr);
            }

            return alertText;
        }
    }
}
