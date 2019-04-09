using System;
using System.Linq;
using System.Xml;
using System.Xml.XPath;

namespace Attendence
{
    public class XmlLoader
    {
        private XmlDocument m_Document;
        private XPathDocument m_XPathDocument;
        private XPathNavigator m_XPathNavigator;
        private string m_DocumentPath;

        public bool ReadOnly { get; private set; }

        public XmlLoader(string i_Path, bool i_ReadOnly = false)
        {
            ReadOnly = i_ReadOnly;
            m_DocumentPath = i_Path;

            if (ReadOnly)
            {
                m_XPathDocument = new XPathDocument(m_DocumentPath);
                m_XPathNavigator = m_XPathDocument.CreateNavigator();
            }
            else
            {
                m_Document = new XmlDocument();
                m_Document.Load(m_DocumentPath);
            }
        }

        public void Set(string i_Path, string i_Value)
        {
            if (!ReadOnly)
            {
                GetXmlNode(m_Document, m_Document.DocumentElement, i_Path).InnerText = i_Value;
            }
        }

        public string Get(string i_Path, string i_Default = null)
        {
            string returnValue = i_Default;

            if (ReadOnly)
            {
                XPathNavigator node;
                if (i_Path.StartsWith("Settings/"))
                {
                    node = m_XPathNavigator.SelectSingleNode(i_Path);
                }
                else
                {
                    node = m_XPathNavigator.SelectSingleNode("Settings/" + i_Path);
                }

                if (node != null) returnValue = node.Value;
            }
            else
            {
                XmlNode node = m_Document.DocumentElement.SelectSingleNode(i_Path);

                if (node != null) returnValue = node.InnerText;
            }

            return returnValue;
        }

        public void Save()
        {
            if (!ReadOnly)
            {
                m_Document.Save(m_DocumentPath);
            }
        }

        static public XmlNode GetXmlNode(XmlDocument i_Document, XmlNode i_Parent, string i_Path)
        {
            // grab the next node name in the xpath; or return parent if empty
            string[] partsOfXPath = i_Path.Trim('/').Split('/');
            string nextNodeInXPath = partsOfXPath.First();
            if (string.IsNullOrEmpty(nextNodeInXPath))
                return i_Parent;

            // get or create the node from the name
            XmlNode node = i_Parent.SelectSingleNode(nextNodeInXPath);
            if (node == null)
                node = i_Parent.AppendChild(i_Document.CreateElement(nextNodeInXPath));

            // rejoin the remainder of the array as an xpath expression and recurse
            string rest = String.Join("/", partsOfXPath.Skip(1).ToArray());
            return GetXmlNode(i_Document, node, rest);
        }

    }
}
