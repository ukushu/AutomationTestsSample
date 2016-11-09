using System.Threading;
using OpenQA.Selenium;

namespace BotAgent.Ifrit.Core.ElemActions.Elem
{
    using System;

    /// <summary>
    /// Methods for multi-line text field [text area]
    /// </summary>
    public class TextArea : ActionsBase
    {
        public TextArea(IfrPage parent)
        {
            HierarhicalParent = parent;
        }

        public bool SendEnterAndWaitForPageLoad()
        {
            SendKeyEnter();

            return HierarhicalParent.WaitForPageLoad();
        }

        public void SendKeyEnterAndWaitForAjax()
        {
            SendKeyEnter();

            HierarhicalParent.WaitForAjax();
        }

        public string Text
        {
            get
            {
                try
                {
                    return this.Element.GetAttribute("value");
                }
                catch (Exception)
                {
                    //// Normal behavior if element does not exist or element text is empty
                }

                return string.Empty;
            }

            set
            {
                try
                {
                    this.Element.Clear();
                    this.Element.SendKeys(value);
                }
                catch (Exception)
                {
                    //// Normal behavior if element does not exist
                }
            }
        }
        
        /// <summary>
        /// Clean-up field and send Enter key
        /// </summary>
        public bool SetTextAndSubmit(string str)
        {
            Text = str;

            var rez = SendEnterAndWaitForPageLoad();

            return rez;
        }
        
        public bool Clear()
        {
            try
            {
                this.Element.Clear();
                return true;
            }
            catch (Exception)
            {
                // Ошибку выдавать не нужно. Нормально если там и так нету текста.
            }

            return false;
        }

        /// <summary>
        /// Add some text after specific substring
        /// </summary>
        public bool AddTextAfter(string addAfterStr, string textToAdd)
        {
            try
            {
                string newText = this.Element.Text;
                var startFrom = newText.IndexOf(addAfterStr, 0) + addAfterStr.Length;
                newText = newText.Insert(startFrom, textToAdd);

                this.Element.Clear();
                this.Element.SendKeys(newText);
                return true;
            }
            catch
            {
                string errorStr = string.Format("Failed to add text '{0}' after '{1}'", textToAdd, addAfterStr);
            }

            return false;
        }

        /// <summary>
        /// Add text before some specific substring
        /// </summary>
        public bool AddTextBefore(string addBeforeStr, string textToAdd)
        {
            try
            {
                string newText = this.Element.Text;
                var startFrom = newText.IndexOf(addBeforeStr, 0);
                newText = newText.Insert(startFrom, textToAdd);

                this.Element.Clear();
                this.Element.SendKeys(newText);
                return true;
            }
            catch
            {
                string errorStr = string.Format("Failed to add text '{0}' before '{1}'", textToAdd, addBeforeStr);
            }

            return false;
        }

        /// <summary>
        /// Max letters test for field
        /// lengthOfSymbols = 10 0000
        /// symbol4Test = 1
        /// </summary>
        /// <param name="expectedMax">expected number of written symbols</param>
        /// <returns>"ok" if all is ok, error string in there is error</returns>
        public string Test4MaxLetters(int expectedMax)
        {
            return Test4MaxLetters('1', 10000, expectedMax);
        }

        /// <summary>
        /// Max letters test for field
        /// </summary>
        /// <param name="symbol4Test">this param added for field with non-standard limitations</param>
        /// <param name="lengthOfSymbols">length of symbols for try to write into the field</param>
        /// <param name="expectedMax">expected number of written symbols</param>
        /// <returns>"ok" if all is ok, error string in there is error</returns>
        public string Test4MaxLetters(char symbol4Test, int lengthOfSymbols, int expectedMax)
        {
            string symbol = symbol4Test.ToString();

            string textToInsert = new String(symbol4Test, lengthOfSymbols);

            this.Element.SendKeys(textToInsert);
            
            var currentFldValueLength = this.Text.Length;

            if (currentFldValueLength != expectedMax)
            {
                return String.Format("FAIL! Expeced '{0}', but real max letters for this field is equalls or more than '{1}'", expectedMax, currentFldValueLength);
            }
            
            return "ok";
        }

        /// <summary>
        /// Gets list of allowed or unallowed symbols
        /// </summary>
        /// <param name="returnAllowed">if true -- returns allowed. in other case -- unalowed string</param>
        /// <returns></returns>
        private string Test4GetAllowedSymbols(bool returnAllowed)
        {
            string symbols = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz(){}[]?!.,-_=+\\|/<>:;`~\"@#$%^&*";

            string allowed = String.Empty;
            string unallowed = String.Empty;

            foreach (var character in symbols)
            {
                Text = character.ToString();

                if (Text == character.ToString())
                {
                    allowed += character;
                }
                else
                {
                    unallowed += character;
                }
            }

            if (returnAllowed)
            {
                return allowed;
            }

            return unallowed;
        }

        /// <summary>
        /// !!!!!!!!!!!!!!!!!
        /// WASNT TESTED!
        /// !!!!!!!!!!!!!!!!!
        /// </summary>
        public string Test4CheckUnallowedChars(string charsMustBeUnallowed)
        {
            return Test4CheckAllowedChars(charsMustBeUnallowed, false);
        }

        private string Test4CheckAllowedChars(string charsMustBeAllowed)
        {
            return Test4CheckAllowedChars(charsMustBeAllowed, true);
        }

        private string Test4CheckAllowedChars(string charsMustBeAllowed, bool returnRez4Allowed)
        {
            string realAllowed = Test4GetAllowedSymbols(returnRez4Allowed);

            var listIsAllowed = string.Empty;
            var listMustBeUnAllowed = string.Empty;
            var listMustBeAllowed = string.Empty;

            foreach (var realAllowedChar in realAllowed/*abcd*/)
            {
                if (/*abe*/charsMustBeAllowed.Contains(realAllowedChar.ToString()))
                {
                    listIsAllowed += realAllowedChar.ToString();//ab
                }
                else
                {
                    listMustBeUnAllowed += realAllowedChar.ToString();//cd
                }
            }

            foreach (var mustBeAllowedChar in charsMustBeAllowed/*abe*/)
            {
                if (/*abcd*/!realAllowed.Contains(mustBeAllowedChar.ToString()))
                {
                    listMustBeAllowed += mustBeAllowedChar.ToString();//e
                }
            }

            if (listMustBeUnAllowed.Length == 0 && listMustBeAllowed.Length == 0)
            {
                return "ok";
            }

            if (returnRez4Allowed)
            {
                return String.Format("FAIL! ReallAllowed chars is: {0} ||| MustBe allowed[for test success]: {1} ||| Is allowed: {2} ||| Must be chaned to allowed: {3} ||| Must be chaned to unAllowed: {4}", realAllowed, charsMustBeAllowed, listIsAllowed, listMustBeAllowed, listMustBeUnAllowed);
            }
            
            return String.Format("FAIL! ReallUnAllowed chars is: {0} ||| MustBe unAllowed[for test success]: {1} ||| Is unAllowed: {2} ||| Must be chaned to unAllowed: {3} ||| Must be chaned to Allowed: {4}", realAllowed, charsMustBeAllowed, listIsAllowed, listMustBeAllowed, listMustBeUnAllowed);
        }

        /// <summary>
        /// Checking of password fields for correct attrubute "type"
        /// </summary>
        public bool Test4FieldIsMasked()
        {
            if (Element.GetAttribute("type") == "password")
                return true;

            return false;
        }



    }

}
