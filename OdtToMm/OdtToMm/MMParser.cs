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
        /// <summary>
        /// Provides methods for convertion means
        /// </summary>
        /// <param name="nodeCollection">Collection to use when converting</param>
        public MMParser()
        {

        }

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
                string tt = f.text; //PROTÁHNOUT PŘES HTML ENTITY PARSER
                n = new XElement("node");
                n.SetAttributeValue("TEXT", tt);

            }
            return n;
        }

        //PRIVATE CLASSES FOR CONVERTION MEANS



    }
}
