using CSharp.DS.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Algo.Sorting
{
    public partial class Sorting
    {
        public IList<string> TopKFrequent(string[] words, int k)
        {
            // Input: ["the", "day", "is", "sunny", "the", "the", "the", "sunny", "is", "is"], k = 4
            // Output: ["the", "is", "sunny", "day"]


            // Brute Force:
            // Starting from each word, walk through each item and store the frequency of rach word in a tuple
            // O(N^2)

            // v1. Optimal with tradeoff on Space Complexity:
            // the -> 4
            // day -> 1
            // is -> 3
            // ...
            // + Post reordering by val: NLogN
            // O(N*avg(strlen) + NLogN)
            // avg(strlen) due to creation of the hash

            // v2. Using a IndexedHeap or the equivalent PriorityQueue+Map 
            //  O(N*avg(strlen) + NLogK)

            //// v1: NLogN
            //// Using size guarantees we don't have to re-hash the hashmap
            //// if the load factor goes above the threshold
            //var wordMap = new Dictionary<string, int>(words.Length);
            //foreach (var word in words)
            //{
            //    if (!wordMap.ContainsKey(word))
            //        wordMap.Add(word, 0);
            //    wordMap[word]++; // O(1) amortized cost
            //}

            //var topKFrequentWords =
            //    wordMap.OrderByDescending(kv => kv.Value).ThenBy(kv => kv.Key).Take(k).Select(kv => kv.Key).ToList();

            //return topKFrequentWords;

            // v2: NLogk
            // Using size guarantees we don't have to re-hash the hashmap
            // if the load factor goes above the threshold
            var wordMap = new Dictionary<string, int>(words.Length);
            foreach (var word in words)
            {
                if (!wordMap.ContainsKey(word))
                    wordMap.Add(word, 0);
                wordMap[word]++; // O(1) amortized cost
            }

            var pq = new PriorityQueue<string>(
                (w1, w2) =>
                {
                    var cmpFreq = wordMap[w1].CompareTo(wordMap[w2]);
                    return cmpFreq == 0 ? -w1.CompareTo(w2) : cmpFreq;
                });

            // NLogK
            foreach (var wordK in wordMap.Keys)
            {
                // Console.WriteLine(wordK);
                pq.Offer(wordK); // LogK

                if (pq.Count() > k)
                    pq.Poll(); // LogK
            }

            var topKFrequentWords = new List<string>();
            while (pq.Any())
                topKFrequentWords.Add(pq.Poll());
            topKFrequentWords.Reverse();

            return topKFrequentWords;
        }
    }
}
