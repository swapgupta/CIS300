using KansasStateUniversity.TreeViewer2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.TravelingSalesperson
{
    class MSTNode : ITree
    {
        //These are the private global fields for this class
        private int _data; //the weight of the graph edge that took us to this node
        private int _parent; //the array index from the adjacency matrix of the parent node
        private string _label; //the string representation of the node
        private List<MSTNode> _children = new List<MSTNode>(); //the children of this node

        //public property to get and set the private _data field
        public int Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }

        //public property to get and set the private _parent field
        public int Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
            }
        }

        /// <summary>
        /// public property for the ITree interface to retrieve the representation of the node.  
        /// This returns a string that is _label plus _data in parentheses, for example: A (4)
        /// </summary>
        public object Root
        {
            get
            {
                return _label + " (" + _data + ")";
            }
        }

        /// <summary>
        /// property for the ITree interface to return the children of this node
        /// returns _children as an array
        /// </summary>
        ITree[] ITree.Children
        {
            get
            {
                return _children.ToArray();
            }
        }

        /// <summary>
        /// public property that returns whether or not the node is empty
        /// For this project, it returns false since we never have any empty nodes
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// This is the public constructor for the class
        /// </summary>
        /// <param name="data">the _data</param>
        /// <param name="parent">the _parent</param>
        /// <param name="label">the _label</param>
        public MSTNode(int data, int parent, string label)
        {
            _data = data;
            _parent = parent;
            _label = label;
        }

        /// <summary>
        /// This method should add the given child to the _children list
        /// </summary>
        /// <param name="child">the child to be added</param>
        public void AddChild(MSTNode child)
        {
            _children.Add(child);
        }


        /// <summary>
        /// This is a recursive function to do a pre-walk on the children of the MST
        /// </summary>
        /// <param name="sb">a stringbuilder to hold the pre-walk</param>
        private void Walk(StringBuilder sb)
        {
                sb.Append(" to ");
                sb.Append(_label);
                sb.Append(Environment.NewLine);
                sb.Append(_label);
                for (int i = 0; i < _children.Count; i++)
                {
                    _children[i].Walk(sb);
                }
        }

        /// <summary>
        /// This method initiates the pre-walk from the other Walk method
        /// </summary>
        /// <returns>the walk</returns>
        public string Walk()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_label);
            for (int i = 0; i < _children.Count; i++)
            {
                _children[i].Walk(sb);
            }
            sb.Append(" to ");
            sb.Append(_label);
            return sb.ToString();
        }
    }
}
