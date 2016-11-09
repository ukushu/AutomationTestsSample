namespace BotAgent.Ifrit.Core.Xml
{
    using OpenQA.Selenium;
    using HtmlAgilityPack;

    public partial class XmlActions
    {
        private HtmlDocument _xmlDoc;
        private HtmlNode _rootNode;
        private IWebDriver Instance;

        public XmlActions(IWebDriver instance)
        {
            _xmlDoc = new HtmlDocument();

            Instance = instance;
        }

        private void Update()
        {
            string pageSource = Instance.PageSource.Replace("\r\n", string.Empty);

            _xmlDoc.LoadHtml(pageSource);
            _rootNode = _xmlDoc.DocumentNode;
        }

        public NodeSingle Elmnt(string xpath)
        {
            Update();

            var currNode = _rootNode.SelectSingleNode(xpath);

            return new NodeSingle(currNode);
        }

        public NodesMultiple Elmnts(string xpath)
        {
            Update();

            var nodesGroup = _rootNode.SelectNodes(xpath);

            return new NodesMultiple(nodesGroup);
        }
    }
}
