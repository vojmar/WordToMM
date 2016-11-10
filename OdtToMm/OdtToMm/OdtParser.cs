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
    static class OdtParser
    {
        #region var's declaration
        private const string tmpPath = "temp";
        #endregion var's declaration
        /// <param name="odtFilePath">Full path to .odt file</param>
        public static FreeMindNodeCollection ParseOdt(string odtFilePath)
        {
            ExtractOdt(odtFilePath);
            var odtContent = LoadOdt();
            return GetOdtContent(odtContent);
        }
        /// <summary>
        /// Returns collection of nodes stored in .odt file
        /// </summary>
        private static FreeMindNodeCollection GetOdtContent(XmlDocument odtContent)
            {
            FreeMindNodeCollection nodeCol = new FreeMindNodeCollection();
            #region XML content extraction
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
                int layer = Convert.ToInt32(node.Attributes["text:outline-level"].Value);
                int parentId = 0;
                if (layer < lastLayer)
                {
                    int difference = lastLayer - layer;
                    for (int i = 0; i < difference; i++)
                    {
                        tree.Pop();
                    }
                    
                    tree.Pop();
                    parentId = tree.Peek();
                    tree.Push(currentId);
                    lastLayer = layer;
                }
                else if (layer > lastLayer)
                {
                    int difference = layer - lastLayer;
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
            DeleteOdtFiles();
            return nodeCol;
        }
        private static void ExtractOdt(string filePath)
        {
            Directory.CreateDirectory(tmpPath);
            ZipFile.ExtractToDirectory(filePath, tmpPath);
        }
        private static void DeleteOdtFiles()
        {
            Directory.Delete(tmpPath, true);
        }
        private static XmlDocument LoadOdt()
        {
            var odtContent = new XmlDocument();
            odtContent.Load(tmpPath + @"/content.xml");
            return odtContent;
        }
    }
}
