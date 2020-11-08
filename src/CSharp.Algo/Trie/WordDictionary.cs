using CSharp.DS.Trie;
using System.Collections.Generic;
using System.Linq;

namespace CSharp.Algo.Trie
{
    /// <summary>
    /// Data structure based on a Trie that supports adding and finding words if a string matches any previously added string.
    /// </summary>
    public class WordDictionary
    {
        public TrieNode root;

        public WordDictionary()
        {
            root = new TrieNode('/');
        }

        /// <summary>
        /// Insert a word into the dictionary.
        /// </summary>
        /// <param name="word"></param>
        public void AddWord(string word)
        {
            var currentNode = root;
            for (var i = 0; i < word.Length; i++)
            {
                var foundChild = currentNode.children[word[i] - 'a'];
                if (foundChild == null)
                {
                    foundChild = new TrieNode(word[i]);
                    currentNode.children[word[i] - 'a'] = foundChild; // a -> null // p->null // e->null
                }

                currentNode = foundChild;
            }

            // Mark last node as word
            if (currentNode != root)
                currentNode.isWord = true;
        }

        /// <summary>
        /// Returns if the word is in the data structure. A word could contain the dot character '.' to represent any one letter.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool Search(string word)
        {
            var currentNodes = new Queue<TrieNode>();
            currentNodes.Enqueue(root);

            for (var i = 0; i < word.Length; i++)
            {
                int childIndex = 0, count = currentNodes.Count;
                while (childIndex++ < count)
                {
                    var currentNode = currentNodes.Dequeue();
                    if (word[i] == '.')
                        foreach (var child in currentNode.children)
                        {
                            if (child != null)
                                currentNodes.Enqueue(child);
                        }
                    else
                    {
                        var foundChild = currentNode.children[word[i] - 'a'];
                        if (foundChild != null)
                            currentNodes.Enqueue(foundChild);
                    }
                }
            }

            if (!currentNodes.Any())
            {
                return false;
            }

            while (currentNodes.Any())
            {
                var curNode = currentNodes.Dequeue();
                if (curNode.isWord)
                    return true;
            }

            return false;
        }
    }
}
