namespace CSharp.DS.Trie
{
    public class TrieNode
    {
        public char val;
        public bool isWord;
        public TrieNode[] children;

        public TrieNode(char val)
        {
            this.val = val;
            children = new TrieNode[26];
        }
    }
}
