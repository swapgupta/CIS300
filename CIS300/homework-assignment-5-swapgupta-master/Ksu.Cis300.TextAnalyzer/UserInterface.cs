using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ksu.Cis300.Sort;

namespace Ksu.Cis300.TextAnalyzer
{
    public partial class UserInterface : Form
    {
        public UserInterface()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Select the first text and put it in uxText1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxSelectText1_Click(object sender, EventArgs e)
        {
            if(uxOpenDialog.ShowDialog() == DialogResult.OK)
            {
                uxText1.Text = uxOpenDialog.FileName;
                if(uxText2.Text != "")
                {
                    uxAnalyze.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Select the second text and put it in uxText2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxSelectText2_Click(object sender, EventArgs e)
        {
            if (uxOpenDialog.ShowDialog() == DialogResult.OK)
            {
                uxText2.Text = uxOpenDialog.FileName;
                if (uxText1.Text != "")
                {
                    uxAnalyze.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Used to analyze the two texts and make a message box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxAnalyze_Click(object sender, EventArgs e)
        {
            Dictionary<string, WordCount> dictionary = new Dictionary<string, WordCount>();
            int[] wordCount = new int[2];
            wordCount[0] = TextAnalyzer.ProcessFile(uxText1.Text, 0, dictionary);
            wordCount[1] = TextAnalyzer.ProcessFile(uxText2.Text, 1, dictionary);
            MinPriorityQueue<float, WordFrequency> minQueue = TextAnalyzer.GetMostCommonWords(dictionary, wordCount, (int)uxNumberOfWords.Value);
            float diff = TextAnalyzer.GetDifference(minQueue);
            MessageBox.Show("Difference measure: " + diff.ToString());
        }
    }
}
