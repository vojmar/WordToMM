using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Xml.Linq;

namespace OdtToMm
{
    class OdtParser
    {
        #region var's declaration
        private const string tmpPath = "temp";
        private string filePath;
        private XmlDocument odtContent;
        #endregion var's declaration
        /// <param name="odtFilePath">Full path to .odt file</param>
        public OdtParser(string odtFilePath)
        {
            this.filePath = odtFilePath;
        }
        /// <summary>
        /// Returns collection of nodes stored in .odt file
        /// </summary>
        public FreeMindNodeCollection GetOdtContent()
        {
            FreeMindNodeCollection nodeCol = new FreeMindNodeCollection();
            #region XML Extraction
            XmlNode xmlTitleNode = odtContent.GetElementsByTagName("text:p")[0];
            FreeMindNode titleNode = new FreeMindNode(xmlTitleNode.InnerText);
            nodeCol.Add(titleNode);
            XmlNodeList xmlNodes = odtContent.GetElementsByTagName("text:h");
            #endregion XML Extraction
            #region Cycle var's declaration
            Stack<int> tree = new Stack<int>();
            int currentId = 1;
            int lastLayer = 0;
            tree.Push(0);
            #endregion Cycle declaration
            foreach (XmlNode node in xmlNodes)
            {
                #region Parent id calculation
                int layer = Convert.ToInt32(node.Attributes["text:style-name"].Value);
                int parentId = 0;
                if (layer < lastLayer)
                {
                    tree.Pop();
                    tree.Pop();
                    parentId = tree.Peek();
                    tree.Push(currentId);
                    lastLayer = layer;
                }
                else if (layer > lastLayer)
                {
                    parentId = tree.Peek();
                    tree.Push(currentId);
                    lastLayer = layer;
                }
                else if (layer == lastLayer)
                {
                    tree.Pop();
                    parentId = tree.Peek();
                    tree.Push(currentId);
                    lastLayer = layer;
                }
                #endregion Parent id calculation
                nodeCol.Add(new FreeMindNode(parentId, node.InnerText, currentId));
                currentId++;
            }
            return nodeCol;
        }
        private void ExtractOdt()
        {
            Directory.CreateDirectory(tmpPath);
            ZipFile.ExtractToDirectory(filePath, tmpPath);
        }
        private void DeleteOdtFiles()
        {
            Directory.Delete(tmpPath, true);
        }
        private void LoadOdt()
        {
            odtContent = new XmlDocument();
            odtContent.Load(tmpPath + @"/content.xml");
        }
    }
}
