using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OdtToMm
{
    class MMParser
    {
        private string _path;

        /// <summary>
        /// Provides methods for convertion means
        /// </summary>
        /// <param name="path">Path to saved file</param>
        /// <param name="col">FreeMindNodeCollection to parse</param>
        /// <returns></returns>
        public static bool ParseAndSaveMM(string path, FreeMindNodeCollection col)
        {
            XDocument ts = ParseCollection(col);
            ts.Save(path);
            return true;
        }

        //PRIVATE CLASSES FOR CONVERTION MEANS
        private static XDocument ParseCollection(FreeMindNodeCollection col)
        {
            return await Task.Run(() =>
            {
            XDocument parsed = new XDocument();
            foreach(FreeMindNode n in col)
            {
                if (n.topNode)
                {
                    parsed.Add(ParseNode(n));
                }
                else
                {
                    XElement p = parsed
                        .Descendants("node")
                        .Where(g => g.Attribute("ID").Value == n.parentId.ToString())
                        .Single();
                    if(p != null)
                    {
                        p.Add(ParseNode(n));
                    }
                    else
                    {
                        throw new Exception("Error parsing XElement in MMParser.ParseCollection()");
                    }
                }
            }
            return parsed;
        }

        //PRIVATE CLASSES FOR CONVERTION MEANS

        private XElement ParseNode(FreeMindNode f)
        {
            XElement n;
            if (f.topNode)
            {
                n = new XElement("map");
                n.SetAttributeValue("version", "1.0.1");
            }
            else
            {
                string tt = htmlParser.htmlParse(f.text);
                n = new XElement("node");
                n.SetAttributeValue("TEXT", tt);
                n.SetAttributeValue("ID", f.id);

            }
            return n;
        }

        public static async Task<bool> MMParseAndSave(string path, FreeMindNodeCollection col)
        {
            try
            {
                XDocument parsed = await ParseCollection(col);
                await Task.Run(() => parsed.Save(path));
            }
            catch(Exception exc)
        {
                return false;
            }
            return true;
        }

    }
}
