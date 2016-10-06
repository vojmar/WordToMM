using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdtToMm
{
    class FreeMindNode
    {
        static int lastId;
        public int id { get; private set; }
        public int parentId { get; private set; }
        public string text { get; private set; }
        private bool topNode;

        FreeMindNode()
        {
            lastId = 0;
            this.id = lastId;
            topNode = true;
        }
        
        FreeMindNode(int parentId, string text)
        {
            lastId++;
            this.id = lastId;
            this.parentId = parentId;
            this.text = text;
            this.topNode = false;
        }

        private void blabal()
        {

        }
    }
}
