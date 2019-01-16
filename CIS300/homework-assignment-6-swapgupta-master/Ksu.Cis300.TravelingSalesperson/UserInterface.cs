using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KansasStateUniversity.TreeViewer2;
using System.IO;

namespace Ksu.Cis300.TravelingSalesperson
{
    public partial class UserInterface : Form
    {
        public UserInterface()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method creates and returns an UndirectedGraph 
        /// from the given graph text file using a StreamReader
        /// </summary>
        /// <param name="fileName">the graph text file</param>
        /// <returns>the undirected graph</returns>
        private UndirectedGraph ReadGraph(string fileName)
        {
            UndirectedGraph graph = new UndirectedGraph(0);
            using (StreamReader input = new StreamReader(fileName))
            {
                graph = new UndirectedGraph(Convert.ToInt32(input.ReadLine()));
                while (input.EndOfStream == false)
                {
                    string[] sub = input.ReadLine().Split(',');
                    graph.AddEdge(sub[0], sub[1], Convert.ToInt32(sub[2]));
                }
            }
            return graph;
        }

        /// <summary>
        /// the event handler for the Load Graph button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxOpenButton_Click(object sender, EventArgs e)
        {
            if (uxOpenDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    UndirectedGraph graph = ReadGraph(uxOpenDialog.FileName);
                    MSTNode tree = graph.GetMinSpanningTree();
                    new TreeForm(tree, 100).Show();
                    uxTour.Text = tree.Walk();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("The following error occurred: " + ex);
                }
            }
        }
    }
}
