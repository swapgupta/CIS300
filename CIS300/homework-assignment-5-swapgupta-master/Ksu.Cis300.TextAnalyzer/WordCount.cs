using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.TextAnalyzer
{
    class WordCount
    {
        private string _word;//A string storing a word
        private int[] _counts;//An int[] storing the number of occurrences of the word in each file


        /// <summary>
        /// A constructor that takes as parameters a word and the number of files to be processed, and initializes the two private fields
        /// </summary>
        public WordCount(string word, int numFiles) 
        {
            _word = word;
            _counts = new int[numFiles];
        }

        /// <summary>
        /// A property to get the word
        /// </summary>
        public string Word
        {
            get
            {
                return _word;
            }
        }

        /// <summary>
        /// A property to get the number of files being processed
        /// </summary>
        public int NumberOfFiles
        {
            get
            {
                return _counts.Length;
            }
        }

        /// <summary>
        /// An indexer to get the number of occurrences of the word in a specified file
        /// </summary>
        /// <param name="fileNum">a file number (i.e., an index into the int[ ])</param>
        /// <returns>an int giving the number of occurrences of the word in that file</returns>
        public int this[int fileNum]
        {
            get
            {
                if(fileNum > _counts.Length)
                {
                    throw new ArgumentException();
                }
                else
                {
                    return _counts[fileNum];
                }
            }
        }

        /// <summary>
        /// A method that takes a file number and increments the number of occurrences of the word in that file
        /// </summary>
        /// <param name="fileNum">a file number</param>
        public void Increment(int fileNum)
        {
            if(fileNum > _counts.Length)
            {
                throw new ArgumentException();
            }
            else
            {
                _counts[fileNum]++;
            }
        }
    }
}
