namespace BotAgent.Ifrit.Parser
{
    using HtmlAgilityPack;
    using System;
    using System.Collections.Generic;

    public class WebNode
    {
        HtmlDocument doc;
        HtmlNode Node;

        public WebNode(HtmlNode htmlNode)
        {
            Node = htmlNode;
        }

        /// <summary>
        /// returns atrubute value. If value wasnt found, returns defVal
        /// </summary>
        public string GetAttrValue(string atributeName, string defVal)
        {
            return Node.GetAttributeValue(atributeName, defVal);
        }

        public bool HasAtrubutes
        {
            get
            {
                return Node.HasAttributes;
            }
        }

        public bool HasChildNodes
        {
            get 
            {
                return Node.HasChildNodes;
            }
        }

        /// <summary>
        /// Checks if element have ANY attributes on closing tag
        /// </summary>
        public bool HasClosingAttributes
        {
            get
            {
                return Node.HasClosingAttributes;
            }
        }

        public string InnerHtml
        {
            get 
            {
                return Node.InnerHtml;
            }
        }

        public string OuterHtml
        {
            get 
            {
                return Node.OuterHtml;
            }
        }

        /// <summary>
        /// Returns text that exactly this tag contains, but withoud subtags texts.
        /// </summary>
        public string Text 
        {
            get 
            {
                try
                {
                    HtmlHelper.RemoveHtmlTagsAndTegsBody(Node.InnerHtml);
                }
                catch (Exception)
                {
                    //// Normal behaviour
                }

                return string.Empty;
            }
        }
        
        /// <summary>
        /// Returns text that contains in all sub-tags
        /// </summary>
        public string TextOfTagsTree
        {
            get
            {
                try
                {
                    return Node.InnerText;
                }
                catch (Exception)
                {
                    //// Normal behaviour
                }

                return string.Empty;
            }
        }

        public string Name 
        {
            get 
            {
                return Node.Name;
            }
        }

        public string Id 
        {
            get 
            {
                return Node.Id;
            }
        }

        public string Class
        {
            get 
            {
                return GetAttrValue("class", string.Empty);
            }
        }

        public WebNode ParentNode
        {
            get 
            {
                return new WebNode(Node.ParentNode);
            }
        }

        public WebNode ChildFirst
        {
            get
            {
                return new WebNode(Node.FirstChild);
            }
        }

        public WebNode ChildLast
        {
            get
            {
                return new WebNode(Node.LastChild);
            }
        }

        public string Xpath 
        {
            get
            {
                return Node.XPath;
            }
        }

        public WebNode Elem(string xpath)
        {
            return new WebNode(Node.SelectSingleNode(xpath));
        }

        public List<WebNode> Elems(string xpath)
        {
            var nodes = Node.SelectNodes(xpath);

            var nodesList = new List<WebNode>();

            foreach (var node in nodes)
            {
                nodesList.Add(new WebNode(node));
            }

            return nodesList;
        }
    }
}
