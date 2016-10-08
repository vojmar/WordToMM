using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace OdtToMm
{
    class OdtParser
    {
        private const string tmpPath = "temp";
        private string filePath;
        private XmlDocument odtContent;
        
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
            //TODO: Implement
            throw new NotImplementedException();
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
            odtContent.Load(tmpPath+ @"/content.xml"); //TODO: Check path! (not sure if its correct)
        }
    }
}
