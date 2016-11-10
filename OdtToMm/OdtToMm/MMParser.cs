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
        /// Provides methods for convertion means
        /// </summary>
        
        /// <summary>
        /// Parses collection into XDocument
        /// </summary>
        /// <param name="col">FreeMindNideCollection to parse</param>
        /// <returns>Parsed XDocument</returns>
        private static async Task<XDocument> ParseCollection(FreeMindNodeCollection col)
        {
            return await Task.Run(() =>
            {
                XDocument parsed = new XDocument();
                XElement map = new XElement("map");

                foreach (FreeMindNode n in col)
                {
                    if (n.topNode)
                    {
                        parsed.Add(ParseNode(n));
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

        //PRIVATE CLASSES FOR CONVERTION MEANS

        private static XElement ParseNode(FreeMindNode f)
        {
            XElement n;
            if (f.topNode)
            {
                n = new XElement("node");
                n.SetAttributeValue("ID", 0);
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

        public static bool MMParseAndSave(string path, FreeMindNodeCollection col)
        {
 //           try
 //           {
                XDocument parsed = ParseCollection(col);
                parsed.Save(path);
 //           }
 //           catch(Exception exc)
 //           {
 //               return false;
 //           }
            return true;
        }

    }
}
