using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.TravelingSalesperson
{
    class UndirectedGraph
    {
        //These are the private global fields for this class
        private int[,] _adjMatrix; //a 2D array to store the matrix representing the graph
        private List<string> _nodes; //the string representation of each node
        private int _size; //the number of nodes in the graph

        /// <summary>
        /// This method sets _size and initializes _adjMatrix
        /// </summary>
        /// <param name="size">the size of the matrix</param>
        public UndirectedGraph(int size)
        {
            _size = size;
            _adjMatrix = new int[_size, _size];
            _nodes = new List<string>();
        }

        /// <summary>
        /// This function adds a string representation of a node to the node list _nodes
        /// </summary>
        /// <param name="data"></param>
        private void AddNode(string data)
        {
            if (_nodes.Count == _size)
            {
                throw new InvalidOperationException();
            }
            else
            {
                _nodes.Add(data);
            }
        }

        /// <summary>
        /// This function simply returns the string of a node at the given index
        /// </summary>
        /// <param name="index">the index of the node</param>
        /// <returns>a node</returns>
        public string GetNode(int index)
        {
            return _nodes[index];
        }

        /// <summary>
        /// This method adds the edge source, destination, and weight parameters to the graph
        /// </summary>
        /// <param name="source">the start of the edge</param>
        /// <param name="destination">the end of the edge</param>
        /// <param name="weight">the weight of the edge</param>
        public void AddEdge(string source, string destination, int weight)
        {
            if (!_nodes.Contains(source))
            {
                AddNode(source);
            }
            int sourceInd = _nodes.IndexOf(source);

            if (!_nodes.Contains(destination))
            {
                AddNode(destination);
            }
            int destInd = _nodes.IndexOf(destination);

            _adjMatrix[sourceInd, destInd] = weight;
            _adjMatrix[destInd, sourceInd] = weight;
        }

        /// <summary>
        /// This method calculates the minimum cost spanning 
        /// tree of the graph by using Prim’s algorithm.
        /// </summary>
        /// <returns></returns>
        public MSTNode GetMinSpanningTree()
        {
            MSTNode[] tree = new MSTNode[_size];
            bool[] added = new bool[_size];

            for (int i = 1; i < tree.Length; i++)
            {
                tree[i] = new MSTNode(Int32.MaxValue, 0, _nodes[i]);
            }

            tree[0] = new MSTNode(0, -1, _nodes[0]);



            for (int j = 0; j < _size; j++)
            {
                int minIndex = 0;
                int minData = Int32.MaxValue;
                for (int i = 0; i < _size; i++)
                {
                    if (tree[i].Data < minData && !added[i])
                    {
                        minData = tree[i].Data;
                        minIndex = i;
                    }
                }

                added[minIndex] = true;

                if (j != 0)
                {
                    int parent = tree[minIndex].Parent;
                    tree[parent].AddChild(tree[minIndex]);
                }

                for (int i = 0; i < _size; i++)
                {
                    if (!added[i] && _adjMatrix[minIndex, i] != 0 && _adjMatrix[minIndex, i] < tree[i].Data)
                    {
                        tree[i].Data = _adjMatrix[minIndex, i];
                        tree[i].Parent = minIndex;
                    }
                }
            }

            return tree[0];
        }
    }
}
