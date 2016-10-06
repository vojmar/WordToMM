using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdtToMm
{
    public class FreeMindNode
    {

        static int lastId;


        public int id { get; private set; }
        public int parentId { get; private set; }
        public string text { get; private set; }
        private bool topNode;

        public FreeMindNode()
        {
            lastId = 0;
            this.id = lastId;
            topNode = true;
        }
        
        public FreeMindNode(int parentId, string text)
        {
            lastId++;
            this.id = lastId;
            this.parentId = parentId;
            this.text = text;
            this.topNode = false;
        }
    }

    public class FreeMindNodeCollection : ICollection
    {
        private object syncRoot;
        private FreeMindNode[] col;

        public FreeMindNodeCollection()
        {
            this.col = new FreeMindNode[0];
        }

        public FreeMindNodeCollection(FreeMindNode[] nodeArray)
        {
            this.col = nodeArray;
        }

        public int Count
        {
            get
            {
                return this.col.Length;
            }
        }

        public bool IsSynchronized
        {
            get { return (this.syncRoot != null) ? true : false; }
        }

        public object SyncRoot
        {
            get
            {
                return syncRoot;
            }
        }

        bool ICollection.IsSynchronized
        {
            get
            {
                return IsSynchronized;
            }
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
            // TODO: Implement
        }

        public FreeMindNodeEnum GetEnumerator()
        {
            return new FreeMindNodeEnum(col);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Add(FreeMindNode node)
        {
            throw new NotImplementedException();
            //TODO: Implement
        }
    }

    public class FreeMindNodeEnum : IEnumerator
    {

        private FreeMindNode[] col;
        int index = -1;

        public FreeMindNodeEnum(FreeMindNode[] nodeCol)
        {
            this.col = nodeCol;
        }

        public object Current
        {
            get
            {
                return col[index];
            }
        }

        public bool MoveNext()
        {
            index++;
            return (index < col.Length) ? true : false;
        }

        public void Reset()
        {
            index = -1;
        }
    }
}
