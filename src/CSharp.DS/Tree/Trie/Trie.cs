namespace CSharp.DS.Trie
{
    public class Trie
    {
        public TrieNode root;

        public Trie()
        {
            root = new TrieNode('/');
        }

        /// <summary>
        /// Insert a word into the trie.
        /// </summary>
        /// <param name="word"></param>
        public void Insert(string word)
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

            if (currentNode != root)
                currentNode.isWord = true; // Mark last node as word
        }

        /// <summary>
        /// Returns if the word is in the trie.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool Search(string word)
        {
            var currentNode = root; //
            for (var i = 0; i < word.Length; i++)
            {
                var foundChild = currentNode.children[word[i] - 'a'];
                if (foundChild == null)
                {
                    return false;
                }

                currentNode = foundChild;
            }

            if (currentNode.isWord)
            { 
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns if there is any words in the trie that starts with the given prefix.
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public bool StartsWith(string prefix)
        {
            var currentNode = root;
            for (var i = 0; i < prefix.Length; i++)
            {
                var foundChild = currentNode.children[prefix[i] - 'a'];
                if (foundChild == null)
                {
                    return false;
                }

                currentNode = foundChild;
            }

            return true;
        }
    }
}