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
        public MMParser(string path)
        {
            this._path = path;
        }

        public XDocument ParseCollection(FreeMindNodeCollection col)
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


    }
}
