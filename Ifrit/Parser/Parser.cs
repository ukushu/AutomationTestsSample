namespace BotAgent.Ifrit.Parser
{
    using HtmlAgilityPack;
    using System.Collections.Generic;

    /// <summary>
    /// The same actions can be done with CORE actions only.
    /// But exactly parsing much faster can be done with using of this Parser class
    /// </summary>
    public class Parser
    {
        HtmlDocument doc;
        HtmlNode docNode;

        public Parser(string html)
        {
            doc.LoadHtml(html);
            docNode = doc.DocumentNode;
        }

        public List<WebNode> Elems(string xpath)
        {
            return new WebNode(docNode).Elems(xpath);
        }

        public WebNode Elem(string xpath)
        {
            return new WebNode(docNode).Elem(xpath);
        }
    }
}
