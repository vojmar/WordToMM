using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OdtToMm
{
    static class MMParser
    {
        /// <summary>
        /// Parses FreeMindNodeCollection and saves it as .mm file (overwrites existing file)
        /// </summary>
        /// <param name="path">Path to saved file</param>
        /// <param name="col">FreeMindNodeCollection to parse</param>
        /// <returns></returns>
        public static async Task<bool> ParseAndSaveMM(string path, FreeMindNodeCollection col)
        {

            XDocument ts = await ParseCollection(col);
            await Task.Run(() => ts.Save(path));
            return true;
        }

        //PRIVATE CLASSES FOR CONVERTION MEANS
        private static async Task<XDocument> ParseCollection(FreeMindNodeCollection col)
        {
            return await Task.Run(() =>
        {
            XDocument parsed = new XDocument();
                XElement map = new XElement("map");
                map.SetAttributeValue("version", "1.0.1");
                parsed.Add(map);
                foreach (FreeMindNode n in col)
            {
                if (n.topNode)
                {
                        parsed.Descendants("map").Single().Add(ParseNode(n));
                }
                else
                {
                        XElement p;
                        try
                        {
                            p = parsed
                        .Descendants("node")
                        .Where(g => g.Attribute("ID").Value == n.parentId.ToString())
                        .Single();
                        }
                        catch (Exception e)
                    {
                            //if exception is invalid system operation
                            p = map;
                        }
                        if (p != null)
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
            });
        }

        private static XElement ParseNode(FreeMindNode f)
        {
            XElement n;
                string tt = htmlParser.htmlParse(f.text);
                n = new XElement("node");
                n.SetAttributeValue("TEXT", tt);
                n.SetAttributeValue("ID", f.id);

            return n;
        }

        public static async Task<bool> MMParseAndSave(string path, FreeMindNodeCollection col)
        {
            try
            {
                XDocument parsed = await ParseCollection(col);
                parsed.Save(path);
            }
            catch(Exception exc)
            {
                return false;
            }
            return true;
        }

    }
}

