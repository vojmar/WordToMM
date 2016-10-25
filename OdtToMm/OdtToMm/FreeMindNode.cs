using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OdtToMm
{
    /// <summary>
    /// Basic data format for WordToMM, used for storing data parsed from word and provide readable format for parsing to mm.
    /// </summary>
    public class FreeMindNode
    {

        public int id { get; private set; }
        public int parentId { get; private set; }
        public string text { get; private set; }
        public bool topNode { get; private set; }
        /// <summary>
        /// Constuctor for FreeMindNode, this constructor creates top node.
        /// </summary>
        public FreeMindNode(string text)
        {
            this.id = 0;
            topNode = true;
            this.text = text;
        }
        
        /// <summary>
        /// Consturctor for FreeMindNode.
        /// </summary>
        /// <param name="parentId">ID of parent node.</param>
        /// <param name="text">Text node contains.</param>
        public FreeMindNode(int parentId, string text, int id)
        {
            this.id = id;
            this.parentId = parentId;
            this.text = text;
            this.topNode = false;
        }
    }

    /// <summary>
    /// ICollection derived class for storing nodes in collection.
    /// </summary>
    public class FreeMindNodeCollection : ICollection
    {
        private object syncRoot;
        private FreeMindNode[] col;
        /// <summary>
        /// Creates empty FreeMindNodeCollection
        /// </summary>
        public FreeMindNodeCollection()
        {
            this.col = new FreeMindNode[0];
        }
        /// <summary>
        /// Creates FreeMindNodeCollection from FreeMindNode array.
        /// </summary>
        /// <param name="nodeArray">Entries to save in newly created collection.</param>
        public FreeMindNodeCollection(FreeMindNode[] nodeArray)
        {
            this.col = nodeArray;
        }
        /// <summary>
        /// Number of FreeMindNodes this collection contains.
        /// </summary>
        public int Count
        {
            get
            {
                return this.col.Length;
            }
        }
        /// <summary>
        /// Returns whether the collection is locked and synchronised for access from multiple threads.
        /// </summary>
        public bool IsSynchronized
        {
            get { return (this.syncRoot != null) ? true : false; }
        }
        /// <summary>
        /// Returns object this collection locks on when synchronised.
        /// </summary>
        public object SyncRoot
        {
            get
            {
                return syncRoot;
            }
        }
        /// <summary>
        /// Returns object this collection locks on when synchronised.
        /// </summary>
        bool ICollection.IsSynchronized
        {
            get
            {
                return IsSynchronized;
            }
        }
        /// <summary>
        ///Copies the elements of the FreeMindNodeCollection to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied from ICollection.The Array must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in array at which copying begins.</param>
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
            return (IEnumerator)GetEnumerator();
        }
        /// <summary>
        /// Adds FreeMindNode to this FreeMindNodeCollection
        /// </summary>
        /// <param name="node">FreeMindNode to be added to FreeMindNodeCollection</param>
        public void Add(FreeMindNode node)
        {
            var tmp = this.col;
            this.col = new FreeMindNode[col.Length +1];
            for (int i = 0; i < tmp.Length; i++)
            {
                col[i] = tmp[i];
            }
            col[col.Length-1] = node;
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
