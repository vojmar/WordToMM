﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Xml.Linq;
using System.Collections;

namespace ODTtoMM_WPF
{
    public class OdtParser
    {
        #region var's declaration
        public event EventHandler<NodeParsedEventArgs> nodeParsed;
        public event EventHandler nodesParsingCompleted;
        public event EventHandler nodesParsingStarted;
        public event EventHandler<OdtExtractedEventArgs> odtExtracted;
        public event EventHandler<OdtLoadedEventArgs> odtLoaded;
        public event EventHandler started;
        private const string tmpPath = "temp";
        #endregion var's declaration
        /// <param name="odtFilePath">Full path to .odt file</param>
        public async Task<FreeMindNodeCollection> ParseOdt(string odtFilePath)
        {
            ExtractOdt(odtFilePath);
            try
            {
                var odtContent = await Task.Run(() => LoadOdt());
                return await GetOdtContent(odtContent);
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
        private async Task<FreeMindNodeCollection> GetOdtContent(XmlDocument odtContent)
        {
            return await Task.Run(() =>
            {
                if (nodesParsingStarted != null)
                    nodesParsingStarted(this, null);
                FreeMindNodeCollection nodeCol = new FreeMindNodeCollection();
                #region XML content extraction
                XmlNodeList pNodes = odtContent.GetElementsByTagName("text:p");
                XmlNode xmlTitleNode = pNodes[0];
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
                    if (nodeParsed != null)
                        nodeParsed(this, new NodeParsedEventArgs(currentId + 1, xmlNodes.Count));
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
                    var nod = new FreeMindNode(parentId, node.InnerText, currentId);
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
                if (nodesParsingCompleted != null)
                    nodesParsingCompleted(this, null);
                DeleteOdtFiles();
                return nodeCol;
            });
        }
        private void ExtractOdt(string filePath)
        {
            Directory.CreateDirectory(tmpPath);
            ZipFile.ExtractToDirectory(filePath, tmpPath);
            //TBD: KONTROLA EXTRAKCE, ODESLÁNÍ PATŘIČNÝCH DAT
            odtExtracted(this, new OdtExtractedEventArgs(true));
        }
        private void DeleteOdtFiles()
        {
            Directory.Delete(tmpPath, true);
        }
        private XmlDocument LoadOdt()
        {
            var odtContent = new XmlDocument();
            odtContent.Load(tmpPath + @"/content.xml");
            //TBD: KONTROLA NAČTENÍ, ODESLÁNÍ PATŘIČNÝCH DAT
            odtLoaded(this, new OdtLoadedEventArgs(true));
            return odtContent;
        }
    }

    public class OdtExtractedEventArgs : EventArgs
    {
        public bool success { get; private set; }
        public OdtExtractedEventArgs(bool success)
        {
            this.success = success;
        }
    }

    public class OdtLoadedEventArgs : EventArgs
    {
        public bool success { get; private set; }
        public OdtLoadedEventArgs(bool success)
        {
            this.success = success;
        }
    }

    public class NodeParsedEventArgs : EventArgs
    {
        public int parsedCount { get; private set; }
        public int parsedTotal { get; private set; }
        public int parsedReamining { get; private set; }
        public byte percentage { get; private set; }
        public NodeParsedEventArgs(int parsedCount, int parsedTotal)
        {
            this.parsedCount = parsedCount;
            this.parsedTotal = parsedTotal;
            this.parsedReamining = parsedTotal - parsedCount;
            this.percentage = (byte)(parsedCount / parsedTotal * 100);
        }
    }
}