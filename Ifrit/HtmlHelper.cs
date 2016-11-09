using System.Linq;

namespace BotAgent.Ifrit
{
    using System.Text.RegularExpressions;
    using HtmlAgilityPack;

    /// <summary>
    /// Additional methods for work with html
    /// </summary>
    public static class HtmlHelper
    {
        public static string ReplaceSpecSymbolsFromHtml(string str)
        {
            if (str!= null)
            {
                return str
                .Replace("&nbsp;", " ")
                .Replace("&amp;", "&")
                .Replace("&quot;", "\"")
                .Replace("&lt;", "<")
                .Replace("&gt;", ">");
                //http://www.ascii.cl/htmlcodes.htm
            }

            return str;
        }

        /// <summary>
        /// Removes html tags from the string
        /// </summary>
        public static string RemoveHtmlTagsFromString(string html)
        {
            return Regex.Replace(html, @"<[^>]+>|&nbsp;", "").Replace("\n\r", string.Empty).Replace("\r\n", string.Empty).Trim(' ');
        }

        /// <summary>
        /// Removes some some specific tag body from the string
        /// </summary>
        public static string RemoveTagBodyFromString(string html, string tag)
        {
            string tagStart = string.Format("<{0}", tag);
            string tagEnd = string.Format("</{0}>", tag);

            int start = html.IndexOf(tagStart);
            int length = html.IndexOf(tagEnd) + tagEnd.Length - start;

            if (start != -1)
            {
                html = html.Remove(start, length);
            }

            return html;
        }

        public static string RemoveHtmlTagsAndTegsBody(string html)
        {
            html = html.Replace("\n\r", string.Empty).Replace("\r\n", string.Empty).Replace("&nbsp;"," ").Replace("&amp;", "&");

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            if (doc.DocumentNode.ChildNodes.Count > 1)
            {
                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("*"))
                {
                    node.Remove();
                }
            }

            return doc.DocumentNode.InnerText.Trim(' ');
        }
        
        public static string RemoveTagWithAttributeFromString(string html, string tag, string attribute, string attributeValue)
        {
            string regexTag = string.Format(@"<{0}\w*\s*{1}=.{2}.*?{0}>", tag, attribute, attributeValue);

            html = Regex.Replace(html, regexTag, "", RegexOptions.IgnoreCase);

            return html;
        }
    }
}
