namespace BotAgent.Ifrit.Core.BaseClasses
{
    using System;

    public class By
    {
        public enum PossibleTypes
        {
            Id,
            Class,
            Xpath,
            CssSelector,
            Name
        }
        
        public int CurrType;
        public string CurrValue;
        public string CurrTag;

        public By(string currValue, int type, string currTag)
        {
            this.CurrValue = currValue;
            this.CurrType = type;
            this.CurrTag = currTag;
        }

        public static By Id(string idToFind)
        {
            return new By(idToFind, (int)PossibleTypes.Id, String.Empty);
        }

        public static By Class(string classToFind)
        {
            return new By(classToFind, (int)PossibleTypes.Class ,String.Empty);
        }

        /// <summary>
        /// Exact class name with several classes added.
        /// Example: class='price price1 globalPrice'
        /// </summary>
        public static By ClassCompound(string classToFind)
        {
            return new By(".//*[@class='" + classToFind + "']", (int)PossibleTypes.Xpath, String.Empty);
        }
        
        public static By Xpath(string xpath)
        {
            return new By(xpath, (int)PossibleTypes.Xpath, String.Empty);
        }

        public static By CssSelector(string cssSelector)
        {
            return new By(cssSelector, (int)PossibleTypes.CssSelector, String.Empty);
        }

        public static By Name(string name)
        {
            return new By(name, (int)PossibleTypes.Name, String.Empty);
        }

        public static By Href(string href)
        {
            return new By(".//a[@href='" + href + "']", (int)PossibleTypes.Xpath, String.Empty);
        }

        public static By HrefPart(string subString)
        {
            return new By(".//a[contains(@href,'" + subString + "')]", (int)PossibleTypes.Xpath, String.Empty);
        }

        public static By Text(string text)
        {
            return new By(".//*[text()='" + text + "']", (int)PossibleTypes.Xpath, String.Empty);
        }
    }
}


