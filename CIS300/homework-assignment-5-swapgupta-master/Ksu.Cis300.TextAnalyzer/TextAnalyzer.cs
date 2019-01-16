using Ksu.Cis300.Sort;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ksu.Cis300.TextAnalyzer
{
    static class TextAnalyzer
    {
        /// <summary>
        /// A method to read and process a file
        /// </summary>
        /// <param name="fileName">A string giving the name of the file to read.</param>
        /// <param name="fileNum">An int giving the file number (i.e., 0 or 1).</param>
        /// <param name="d">A Dictionary<string, WordCount> containing the number of occurrences of each word in any previously-processed files.</param>
        /// <returns>the number of words</returns>
        public static int ProcessFile(string fileName, int fileNum, Dictionary<string, WordCount> d)
        {
            int wordCount = 0;
            using (StreamReader input = new StreamReader(fileName))
            {
                while (!input.EndOfStream)
                {
                    string line = input.ReadLine().ToLower();
                    string[] words = Regex.Split(line, "[^abcdefghijklmnopqrstuvwxyz]+");

                    for(int i = 0; i < words.Length; i++)
                    {
                        if (words[i] != "")
                        {
                            wordCount++;
                            WordCount word;
                            bool wordExists = d.TryGetValue(words[i], out word);
                            if (!wordExists)
                            {
                                word = new WordCount(words[i], 2);
                                d.Add(words[i], word);
                            }
                            word.Increment(fileNum);
                        }
                    }
                }
            }
            return wordCount;
        }

        /// <summary>
        /// A method to get the words with highest combined frequencies
        /// </summary>
        /// <param name="d">A Dictionary<string, WordCount> giving the number of occurrences of each word in each file.</param>
        /// <param name="numWords">An int[ ]  of size 2 giving the number of words in each file.</param>
        /// <param name="getNum">An int giving the number of words to get.</param>
        /// <returns>a MinPriorityQueue<float, WordFrequency> whose elements contain the frequencies in each file of the most common words, and whose priorities are the combined frequencies of each of these words</returns>
        public static MinPriorityQueue<float, WordFrequency> GetMostCommonWords(Dictionary<string, WordCount> d, int[] wordCount, int getNum)
        {
            MinPriorityQueue<float, WordFrequency> minQueue = new MinPriorityQueue<float, WordFrequency>();
            foreach(WordCount w in d.Values)
            {
                WordFrequency freq = new WordFrequency(w, wordCount);
                minQueue.Add(freq[0] + freq[1], freq);
                if(minQueue.Count > getNum)
                {
                    minQueue.RemoveMinimumPriority();
                }
            }
            return minQueue;
        }

        /// <summary>
        /// A method to compute the difference measure
        /// </summary>
        /// <param name="p">contains the frequencies of the words to use in computing the difference measure</param>
        /// <returns>a float giving the difference measure</returns>
        public static float GetDifference(MinPriorityQueue<float, WordFrequency> freq)
        {
            float diff = 0;
            while(freq.Count > 0)
            {
                WordFrequency wordFreq = freq.RemoveMinimumPriority();
                diff += (wordFreq[0] - wordFreq[1]) * (wordFreq[0] - wordFreq[1]);
            }
            float sqrt = 100 * (float)Math.Sqrt(diff);
            return sqrt;
        }
    }
}
