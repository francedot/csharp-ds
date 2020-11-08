using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Algo.Trie
{
    public class HotDegreeTrieNode
    {
        public string text;
        public int hotDegree;
        public Dictionary<char, HotDegreeTrieNode> children;

        public HotDegreeTrieNode(string text = default)
        {
            this.text = text;
            this.children = new Dictionary<char, HotDegreeTrieNode>();
        }

    }

    public class HotDegreeTrie
    {
        private readonly HotDegreeTrieNode _root = new HotDegreeTrieNode();

        public void Insert(string sentence, int curDegree = 1)
        {
            var curNode = _root;
            foreach (char c in sentence)
            {
                if (curNode.children.ContainsKey(c))
                    curNode = curNode.children[c];
                else
                {
                    var newNode = new HotDegreeTrieNode(curNode.text + c);
                    curNode.children.Add(c, newNode);
                    curNode = newNode;
                }
            }

            curNode.hotDegree += curDegree;
        }

        public IList<string> Search(string prefix)
        {
            var curNode = _root;
            
            var bfsQueue = new Queue<HotDegreeTrieNode>();
            foreach (char c in prefix)
            {
                if (curNode.children.ContainsKey(c))
                    curNode = curNode.children[c];
                else
                    return Array.Empty<string>();
            }

            // If we still here, then we have such prefix in the trie
            bfsQueue.Enqueue(curNode);

            var sortedSet = new SortedSet<(string, int)>(
                Comparer<(string, int)>.Create((a, b) => {
                    var cmpDeg = -a.Item2.CompareTo(b.Item2);
                    return cmpDeg == 0 ? a.Item1.CompareTo(b.Item1) : cmpDeg;
                }));

            // Build result list
            while (bfsQueue.Count > 0)
            {
                curNode = bfsQueue.Dequeue();

                if (curNode.hotDegree > 0)
                    sortedSet.Add((curNode.text, curNode.hotDegree));

                foreach (var child in curNode.children)
                    bfsQueue.Enqueue(child.Value);
            }

            return sortedSet.Take(3).Select(a => a.Item1).ToList();
        }
    }

    public class AutocompleteSystem
    {
        private readonly HotDegreeTrie _trie = new HotDegreeTrie();
        private readonly StringBuilder _currentInput = new StringBuilder();

        public AutocompleteSystem(string[] sentences, int[] times)
        {
            // BF:
            // O(S) || O(S*Avg(strLen)) because of hash creation for a string

            // Opt using Trie:
            // O(S*Avg(strLen)) for constructing the trie

            // Insert sentences to the trie.
            for (int i = 0; i < sentences.Length; i++)
                _trie.Insert(sentences[i], times[i]);
        }

        public IList<string> Input(char c)
        {
            // BF:
            // O(S+FLogF)
            // FLogF for sorting the found sentences

            // Opt using Trie:
            // O(Cur_Sentence_Length + #_Trie_Nodes_ + FLogF))

            // Record sentence and clear input cache
            if (c == '#')
            {
                _trie.Insert(_currentInput.ToString());
                _currentInput.Clear();

                return Array.Empty<string>();
            }

            _currentInput.Append(c);
            return _trie.Search(_currentInput.ToString());
        }
    }
}
