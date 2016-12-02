using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ODTtoMM_WPF
{
    partial class MMParser
    {

        public MMParser()
        {

        }

        /// <summary>
        /// Parses FreeMindNodeCollection and saves it as .mm file (overwrites existing file)
        /// </summary>
        /// <param name="path">Path to saved file</param>
        /// <param name="col">FreeMindNodeCollection to parse</param>
        /// <returns></returns>
        public async Task<bool> ParseAndSaveMM(string path, FreeMindNodeCollection col)
        {

            XDocument ts = await ParseCollection(col);
            await Task.Run(() => ts.Save(path));
            return true;
        }

        //PRIVATE CLASSES FOR CONVERTION MEANS
        private async Task<XDocument> ParseCollection(FreeMindNodeCollection col)
        {

            return await Task.Run(() =>
        {
            MMParseStart(); //EVENT CALL
            XDocument parsed = new XDocument();
            XElement map = new XElement("map");
            map.SetAttributeValue("version", "1.0.1");
            parsed.Add(map);
            int x = 1;
            foreach (FreeMindNode n in col)
            {

                NodeParseStep(x, col.Count); //EVENT CALL
                x++;
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
                    catch
                    {
                        MMParseEnded(false);
                        throw new Exception("Error parsing XElement in MMParser.ParseCollection()");
                        
                    }
                    if (p != null)
                    {
                        p.Add(ParseNode(n));
                    } 
                }
            }
            MMParseEnded(true);
            return parsed;
            });
        }

        private XElement ParseNode(FreeMindNode f)
        {
            XElement n;
            string tt = htmlParser.htmlParse(f.text);
            n = new XElement("node");
            n.SetAttributeValue("ID", f.id);
            n.SetAttributeValue("TEXT", tt);
            if(f.Comment != null)
            {
                XElement richcontent = new XElement("richcontent");
                richcontent.SetAttributeValue("TYPE", "NOTE");
                XElement html = new XElement("html");
                XElement head = new XElement("head");
                XElement body = new XElement("body");
                foreach(Comment c in f.Comment)
                {
                    XElement p = new XElement(c.tag);
                    p.Value = c.text;
                    body.Add(p);
                }
                html.Add(head);
                html.Add(body);
                richcontent.Add(html);
                n.Add(richcontent);
            }

            return n;
        }

        public async Task<bool> MMParseAndSave(string path, FreeMindNodeCollection col)
        {
            try
            {
                XDocument parsed = await ParseCollection(col);
                parsed.Save(path);
            }
            catch(Exception exc)
            {
                throw exc;
            }
            return true;
        }

    }
}

