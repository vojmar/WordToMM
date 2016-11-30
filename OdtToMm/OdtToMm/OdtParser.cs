using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Xml.Linq;
using System.Collections;

namespace OdtToMm
{
    static class OdtParser
    {
        #region var's declaration
        private const string tmpPath = "temp";
        #endregion var's declaration
        /// <param name="odtFilePath">Full path to .odt file</param>
        public static async Task<FreeMindNodeCollection> ParseOdt(string odtFilePath)
        {
            ExtractOdt(odtFilePath);
            try
            {
                var odtContent = await Task.Run(() => LoadOdt());
                return await GetOdtContent(odtContent);
                DeleteOdtFiles();
            }
            catch (Exception e)
            {
                DeleteOdtFiles();
                throw e;
            }

        }
        /// <summary>
        /// Returns collection of nodes stored in .odt file
        /// </summary>
        private static Task<FreeMindNodeCollection> GetOdtContent(XmlDocument odtContent)
        {
            return Task.Run(() =>
            {
                FreeMindNodeCollection nodeCol = new FreeMindNodeCollection();
                #region XML content extraction
                XmlNodeList pNodes = odtContent.GetElementsByTagName("text:p");
                XmlNode xmlTitleNode = pNodes[0];
                FreeMindNode titleNode = new FreeMindNode(xmlTitleNode.InnerText);
                nodeCol.Add(titleNode);
                XmlNodeList xmlNodes = odtContent.GetElementsByTagName("text:h");
                #endregion XML Extraction
                int currentId = 1;
                foreach (XmlNode node in xmlNodes)
                {
                    FreeMindNode nod = ParseXmlNode(node, currentId);
                    XmlNode sibling = node.NextSibling;
                    while (sibling != null && sibling.Name == "text:p")
                    {
                        if (nod.Comment == null)
                        {
                            nod.Comment = new CommentCollection();
                        }
                        nod.Comment.Add(new Comment(sibling.InnerText, "p"));
                        sibling = sibling.NextSibling;
                    }
                    nodeCol.Add(nod);
                    currentId++;
                }
                DeleteOdtFiles();
                return nodeCol;
            });
        }
        private static FreeMindNode ParseXmlNode(XmlNode xmlNode, int currentId)
        {

            #region Cycle var's declaration
            Stack<int> tree = new Stack<int>();
            int lastLayer = 0;
            tree.Push(0);
            #endregion Cycle declaration
            #region Parent id calculation
            int layer = Convert.ToInt32(xmlNode.Attributes["text:outline-level"].Value);
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
            return new FreeMindNode(parentId, xmlNode.InnerText, currentId);
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