/*BTreeNode.cs
 * By Swap Gupta
 */
using KansasStateUniversity.TreeViewer2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.BTrees
{
    /// <summary>
    /// This class represents a node in a B-tree
    /// </summary>
    /// <typeparam name="TKey">the key</typeparam>
    /// <typeparam name="TValue">the value</typeparam>
    class BTreeNode<TKey, TValue> : ITree
        where TKey : IComparable<TKey>
    {
        private int _keyCount; //number of keys in this node

        /// <summary>
        /// public property for key count
        /// </summary>
        public int KeyCount
        {
            get
            {
                return _keyCount;
            }
            set
            {
                _keyCount = value;
            }
        }

        private int _minKeyCount; //minimum number of keys
        private TKey[] _keys; //array of keys in this node
        private int _childCount; //number of children in this node
        private BTreeNode<TKey, TValue>[] _children; //an array that stores the pointers to the children of this node

        /// <summary>
        /// the public property for _children
        /// </summary>
        public ITree[] Children
        {
            get
            {
                return _children;
            }
        }

        private TValue[] _values; //stores the values of the corresponding keys from the _keys array
        private bool _isLeaf; //indicates if this node is a leaf or not

        /// <summary>
        /// the public property for _isLeaf
        /// </summary>
        public bool IsLeaf
        {
            get
            {
                return _isLeaf;
            }
            set
            {
                _isLeaf = value;
            }
        }

        /// <summary>
        /// a property that returns if the number of keys in this node is 0
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return (_keyCount == 0);
            }
        }

        /// <summary>
        /// a property that returns this
        /// </summary>
        public object Root
        {
            get
            {
                return this;
            }
        }


        /// <summary>
        /// This is the constructor for the BtreeNode class.
        /// </summary>
        /// <param name="minKeyCount">minimum key count</param>
        /// <param name="maxKeyCount">max key count</param>
        /// <param name="maxChildCount">max child count</param>
        /// <param name="leaf">if the node is a leaf</param>
        public BTreeNode(int minKeyCount, int maxKeyCount, int maxChildCount, bool leaf)
        {
            _minKeyCount = minKeyCount;
            _childCount = maxChildCount;
            _isLeaf = leaf;
            _keys = new TKey[maxKeyCount];
            _values = new TValue[maxKeyCount];
            _children = new BTreeNode<TKey, TValue>[maxChildCount];
        }

        /// <summary>
        /// Adds the parameters to _keys and _values arrays
        /// </summary>
        /// <param name="key">the TKey added to _keys</param>
        /// <param name="value">the TValue added to _values</param>
        public void AddItem(TKey key, TValue value)
        {
            for (int i = _keyCount - 1; i >= 0; i--)
            {
                int comp = _keys[i].CompareTo(key);
                if(comp <= 0)
                {
                    _keys[i + 1] = key;
                    _values[i + 1] = value;
                    _keyCount++;
                    return;
                }
                else
                {
                    _keys[i + 1] = _keys[i];
                    _values[i + 1] = _values[i];
                }
            }
            _keys[0] = key;
            _values[0] = value;
            _keyCount++;
        }


        /// <summary>
        /// Adds child to _children
        /// </summary>
        /// <param name="i">the index to be added at</param>
        /// <param name="child">the child to be added</param>
        public void AddChild(int i, BTreeNode<TKey, TValue> child)
        {
            _children[i] = child;
            _childCount++;
        }


        /// <summary>
        /// Once a node in the B-tree is full, this method can be called to split part of it into a new node.
        /// </summary>
        /// <param name="index">The index parameter helps indicates which child to split.</param>
        public void SplitChild(int index)
        {
            BTreeNode<TKey, TValue> splitNode = _children[index]; //The index parameter helps indicates which child to split
            //we first need to create a new node to move part of the full node to
            BTreeNode<TKey, TValue> newNode = new BTreeNode<TKey, TValue>(_minKeyCount, this._keys.Length, this._children.Length, splitNode._isLeaf);

            //Then we need to move the larger half of the keys from the full node to the new node we just made
            //Since we should have at least t-1 keys, we will move keys starting at t (or _minKeyCount + 1)
            for (int i = 0, j = i + _minKeyCount + 1; i < _minKeyCount; i++, j++)
            {
                newNode.AddItem(splitNode._keys[j], splitNode._values[j]);
                splitNode._keys[j] = default(TKey);
                splitNode._values[j] = default(TValue);
            }



            //After the key/value pairs are moved into the new node, we should also move the corresponding children over too
            if (!newNode.IsLeaf) //If the new node is not a leaf
            {
                //then we will move the second half of the children from _children[index] to the new node
                for (int i = _children.Length / 2, j = 0; i < _children.Length; i++, j++)
                {
                    if (splitNode.Children[i] != null)
                    {
                        newNode.AddChild(j, splitNode._children[i]);
                        splitNode._children[i] = null;
                        splitNode._childCount--;
                    }

                }
            }

            splitNode._keyCount = _minKeyCount;

            //we need to make room in the parent node for the new node
            //shift the children of this, the parent node, over one and insert the new node at index + 1
            for (int i = _keyCount; i >= index + 1; i--)
            {
                _children[i + 1] = _children[i];
            }

            //we move the middle key/value from the _children[index] node over to the parent node
            _children[index + 1] = newNode;
            AddItem(splitNode._keys[_minKeyCount], splitNode._values[_minKeyCount]);
            splitNode._keys[_minKeyCount] = default(TKey);
            splitNode._values[_minKeyCount] = default(TValue);
            _childCount++;
        }


        /// <summary>
        /// This method inserts into a tree whose root node is not full already
        /// </summary>
        /// <param name="key">the key to be added</param>
        /// <param name="value">the value to be added</param>
        public void InsertNonFull(TKey key, TValue value)
        {
            if (this.IsLeaf)
            {
                AddItem(key, value);
            }
            else
            {
                int index = 0;

                for (; index < _keyCount && _keys[index].CompareTo(key) < 0; index++) ;


                if(_children[index]._keyCount == _keys.Length)
                {
                    SplitChild(index);
                    if(_keys[index].CompareTo(key) < 0)
                    {
                        index++;
                    }
                }
                _children[index].InsertNonFull(key, value);
            }
        }


        /// <summary>
        /// This method implements a modified recursive binary search
        /// </summary>
        /// <param name="key">the key to be found</param>
        /// <returns>the value associated with the key</returns>
        public TValue Find(TKey key)
        {
            int index = Array.IndexOf<TKey>(_keys, key); //use the IndexOf method to see if the key exits in the nodes array of keys
            if (index != -1) //If it is not -1
            {
                return _values[index]; //we found what we were looking for so return the value at that index
            }
            else if (IsLeaf) //If we didn’t find it, and this node is a leaf
            {
                return default(TValue); // the key doesn’t exist so return the default value of TValue
            }
            else
            {
                int val = 0;
                for(int i = KeyCount - 1; i >= 0; i--)  //Else, we need to find the first key k in this node that is greater than the key that we are looking for
                {
                    int compare = _keys[i].CompareTo(key);
                    if (compare < 0)
                    {
                        val = i + 1;
                        break;
                    }
                }

                return _children[val].Find(key); //Once found, call Find on child k and return its result
            }
        }


        /// <summary>
        /// Used to display nodes in a user-friendly manner
        /// </summary>
        /// <returns>the string of names</returns>
        override public string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < _keys.Length; i++)
            {
                if (_keys[i] != null && _keys[i].CompareTo(default(TKey)) != 0)
                {
                    sb.Append(_keys[i]);
                    sb.Append(" | ");
                }
            }
            if (sb.Length != 0) sb.Length-=3;
            return sb.ToString();
        }
    }
}
