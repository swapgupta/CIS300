using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.TextAnalyzer
{
    struct WordFrequency
    {
        private string _word;//A string storing a word
        private float[] _frequencies;//A float[] storing the frequency of the word in each file

        /// <summary>
        /// A constructor for WordFrequency
        /// </summary>
        /// <param name="wordCount">the wordcount class</param>
        /// <param name="numWords">the number of words in each file</param>
        public WordFrequency(WordCount wordCount, int[] numWords)
        {
            _word = wordCount.Word;
            _frequencies = new float[numWords.Length];
            if(numWords.Length != wordCount.NumberOfFiles)
            {
                throw new ArgumentException();
            }
            else
            {
                for(int i = 0; i < numWords.Length; i++)
                {
                    _frequencies[i] = (float)wordCount[i] / numWords[i];
                }
            }
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
        /// An indexer to get the number of occurrences of the word in a specified file
        /// </summary>
        /// <param name="fileNum">a file number (i.e., an index into the int[ ])</param>
        /// <returns>an int giving the number of occurrences of the word in that file</returns>
        public float this[int fileNum]
        {
            get
            {
                if (fileNum > _frequencies.Length)
                {
                    throw new ArgumentException();
                }
                else
                {
                    return _frequencies[fileNum];
                }
            }
        }
    }
}
