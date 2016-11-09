namespace BotAgent.Ifrit.Core.Cookies
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    public partial class CookiesMngr
    {
        private class Core
        {
            private XDocument xmlDoc;
            private string xml_path;

            private const string RootElem = "body";
            private const string CookListElem = "cookies_list";
            private const string CookElem = "cookie";
            private const string UserNameAtr = "profileName";
            private const string CookNameAtr = "cName";
            private const string CookValueAtr = "cValue";
            private const string CookDomainAtr = "cDomain";
            private const string CookPathAtr = "cPath";
            private const string CookExpiriesAtr = "cExpiries";
            private const string CookSecureAtr = "cSecure";

            /// <summary>
            /// Initialization of CookiesControl manager core
            /// </summary>
            public Core()
            {
                xml_path = ParamsLib.BrwsrOptions.BrowserCookiesFile;

                xmlDoc = new XDocument();

                if (File.Exists(xml_path))
                {
                    xmlDoc = XDocument.Load(xml_path);
                }
                else
                {
                    var xmlBodyNode = new XElement(RootElem, string.Empty);
                    var xmlCList = new XElement(CookListElem, string.Empty);

                    xmlBodyNode.Add(xmlCList);

                    xmlBodyNode.Save(xml_path);
                    xmlDoc = XDocument.Load(xml_path);
                }
            }

            /// <summary>
            /// Load CookiesControl from XML document
            /// </summary>
            /// <param name="profileName">Name of cookieProfile</param>
            /// <returns></returns>
            public List<MyCookie> GetCookiesFromXml(string profileName)
            {
                List<MyCookie> cookiesList = new List<MyCookie>();

                var elements = xmlDoc.Root.Descendants(CookElem);

                foreach (var cookieElem in elements)
                {
                    if (cookieElem.Attribute(UserNameAtr).Value == profileName)
                    {
                        var name = (string) cookieElem.Attribute(CookNameAtr);
                        var value = (string) cookieElem.Attribute(CookValueAtr);
                        var domain = (string) cookieElem.Attribute(CookDomainAtr);
                        var path = (string) cookieElem.Attribute(CookPathAtr);
                        var expiries = (string) cookieElem.Attribute(CookExpiriesAtr);
                        var secure = (string) cookieElem.Attribute(CookSecureAtr);

                        cookiesList.Add(new MyCookie(name, value, domain, path, expiries, secure));
                    }
                }

                return cookiesList;
            }

            /// <summary>
            /// Method for adding CookiesControl to XML document
            /// </summary>
            public void AddCookiesToXml(string profileName, string cookieName, string cookieValue, string domainName,
                string path, string expiries, string secure)
            {
                var xmlNode = new XElement(CookElem, new XAttribute(UserNameAtr, profileName),
                    new XAttribute(CookNameAtr, cookieName),
                    new XAttribute(CookValueAtr, cookieValue),
                    new XAttribute(CookDomainAtr, domainName),
                    new XAttribute(CookPathAtr, path),
                    new XAttribute(CookExpiriesAtr, expiries),
                    new XAttribute(CookSecureAtr, secure)
                    );

                xmlDoc.Element(RootElem).Element("cookies_list").Add(xmlNode);
            }

            /// <summary>
            /// Save changes to XML file
            /// </summary>
            public void Save()
            {
                xmlDoc.Save(xml_path);
            }

            /// <summary>
            /// Removes list of cookies with specific profile name from XML
            /// </summary>
            /// <param name="profileName"></param>
            public void RemoveCookiesByProfile(string profileName)
            {
                try
                {
                    xmlDoc.Element(RootElem).Element(CookListElem).Descendants(CookElem)
                        .Where(x => (string) x.Attribute(UserNameAtr) == profileName)
                        .Remove();
                }
                catch (Exception)
                {
                    //nothing need to do
                }
            }

            public class MyCookie
            {
                public string Name { get; set; }
                public string Value { get; set; }
                public string Domain { get; set; }
                public string Path { get; set; }
                public string Expiries { get; set; }
                public string Secure { get; set; }

                public MyCookie(string name, string value, string domain, string path, string expiries,
                    string secure)
                {
                    this.Name = name;
                    this.Value = value;
                    this.Domain = domain;
                    this.Path = path;
                    this.Expiries = expiries;
                    this.Secure = secure;
                }
            }
        }
    }

}
