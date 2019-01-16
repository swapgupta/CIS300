/*BTree.cs
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
    class BTree<TKey, TValue> : ITree
        where TKey : IComparable<TKey>
    {
        private int _size; //the minimum degree of the tree
        private int _maxChildren; //the maximum number of children for the nodes in the tree
        private int _minKeys; //the minimum number of keys for each node in the tree
        private int _maxKeys; //the maximum number of keys for each node in the tree
        private BTreeNode<TKey, TValue> _root; // the root node of the tree

        /// <summary>
        /// a public property that returns of root is null
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return (_root == null);
            }
        }

        /// <summary>
        /// a public property that returns the children of the root node
        /// </summary>
        public ITree[] Children
        {
            get
            {
                return _root.Children;
            }
        }

        /// <summary>
        /// A public property that gets the root of the node
        /// </summary>
        public object Root
        {
            get
            {
                return _root;
            }
        }

        /// <summary>
        /// this is the constructor for BTree
        /// </summary>
        /// <param name="size">the minimum degree of the tree</param>
        public BTree(int size)
        {
            _size = size;
            _maxChildren = _size * 2;
            _minKeys = _size - 1;
            _maxKeys = (_size * 2) - 1;
            _root = new BTreeNode<TKey, TValue>(_minKeys, _maxKeys, _maxChildren, true);
        }

        /// <summary>
        /// The method that inserts a node into the B-tree starting at the root node.
        /// </summary>
        /// <param name="key">the key to be added</param>
        /// <param name="value">the value to be added</param>
        public void Insert(TKey key, TValue value)
        {
            //If the root is empty, then we simply add the key/value to the root node
            if (_root.IsEmpty)
            {
                _root.AddItem(key, value);
            }
            else
            {
                if (_root.KeyCount == _maxKeys)
                {
                    BTreeNode<TKey, TValue> newNode = new BTreeNode<TKey, TValue>(_minKeys, _maxKeys, _maxChildren, false); //First, we need to create a new node which will be the new root
                    newNode.AddChild(0, _root); //Add the old root as a child in the new root at index 0
                    newNode.SplitChild(0); //SplitChild on the new root, passing in 0 for the index and the old root at parameters
                    newNode.InsertNonFull(key, value); //After the split, call InsertNonFull on the new root to insert the key/value
                    _root = newNode; //set _root equal to the new root

                }
                //Otherwise, the root is full so we need to split it
                else//If the root is not full, then we call the InsertNonFull method from the root node
                {
                    _root.InsertNonFull(key, value);
                }
            }

        }


        /// <summary>
        /// This is just a helper function that calls Find on the root node. It should return the results of the search.
        /// </summary>
        /// <param name="key">the key to find</param>
        /// <returns>the value related to the key</returns>
        public TValue Find(TKey key)
        {
            return _root.Find(key);
        }
    }
}
