/*NameLookup.cs
 * By Swap Gupta
 */
using KansasStateUniversity.TreeViewer2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ksu.Cis300.BTrees
{
    public partial class NameLookup : Form
    {
        private BTree<string, NameInformation> _names; //globally stores the tree that is created by reading in a name information file

        /// <summary>
        /// The public constructor that only initializes the UI components
        /// </summary>
        public NameLookup()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The click event handler for the open data file button. This opens a OpenFileDialog box for selecting a name information text file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxOpen_Click(object sender, EventArgs e)
        {
            if (uxOpenDialog.ShowDialog() == DialogResult.OK)
            {
                _names = ReadFile(uxOpenDialog.FileName); // This should load the selected file into the _name field by calling ReadFile
                new TreeForm(_names, 100).Show(); //Then we can draw the resulting tree
            }
        }

        /// <summary>
        /// This method takes in the file name of a name information file and reads it, making a BTree
        /// </summary>
        /// <param name="fn">the file name</param>
        /// <returns>the btree made</returns>
        private BTree<string, NameInformation> ReadFile(string fn)
        {
            BTree<string, NameInformation> t = new BTree<string, NameInformation>(Convert.ToInt32(uxMinDegree.Text));
            using (StreamReader input = new StreamReader(fn))
            {
                while (!input.EndOfStream)
                {
                    try
                    {
                        string name = input.ReadLine().Trim();
                        float freq = Convert.ToSingle(input.ReadLine());
                        int rank = Convert.ToInt32(input.ReadLine());
                        t.Insert(name, new NameInformation(name, freq, rank));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            return t;
        }

        /// <summary>
        /// This is the click event handler for the Get Statistics Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxLookup_Click(object sender, EventArgs e)
        {
            NameInformation name = _names.Find(uxName.Text.Trim().ToUpper());
            if (name.Name == null)
            {
                MessageBox.Show("Name not found.");
                uxFrequency.Text = "";
                uxRank.Text = "";
            }
            else
            {
                uxFrequency.Text = name.Frequency.ToString();
                uxRank.Text = name.Rank.ToString();
                return;
            }
        }

        /// <summary>
        /// This creates a B-tree with the key and value type as integer and a minimum degree that is taken from the uxMinDegree TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxMakeTree_Click(object sender, EventArgs e)
        {
            BTree<int, int> newTree = new BTree<int, int>(Convert.ToInt32(uxMinDegree.Text));

            for (int i = 1; i < Convert.ToInt32(uxCount.Text) + 1; i++)
            {
                newTree.Insert(i, i);
            }

            new TreeForm(newTree, 100).Show();
        }
    }
}
