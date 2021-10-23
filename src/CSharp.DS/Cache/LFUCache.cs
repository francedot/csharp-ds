using System.Collections.Generic;

namespace CSharp.DS.Cache
{
    public class LFUCache
    {
        public class LFUCacheNode
        {
            public int Value;
            public int Frequency;
            public int Key;

            public LFUCacheNode(int key, int val)
            {
                Key = key;
                Value = val;
                Frequency = 1;
            }
        }

        private int _lowestFreq = 1;
        private readonly int _capacity;
        private int _count = 0;

        private readonly Dictionary<int, LinkedListNode<LFUCacheNode>> _items;
        private readonly Dictionary<int, LinkedList<LFUCacheNode>> _freq;

        public LFUCache(int capacity)
        {
            _items = new Dictionary<int, LinkedListNode<LFUCacheNode>>();
            _freq = new Dictionary<int, LinkedList<LFUCacheNode>>();
            _capacity = capacity;
        }

        /// <summary>
        /// Get the value (will always be positive) of the key if the key exists in the cache, otherwise return -1.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int? GetValue(int key)
        {
            if (!_items.TryGetValue(key, out var item))
                return null;
            
            var currentFreq = item.Value.Frequency++;
            var list = _freq[currentFreq];
            list.Remove(item);
            if (list.Count == 0)
            {
                _freq.Remove(currentFreq);
                if (_lowestFreq == currentFreq)
                    _lowestFreq++;
            }

            _freq.TryAdd(currentFreq + 1, new LinkedList<LFUCacheNode>());
            _freq[currentFreq + 1].AddFirst(item);

            return item.Value.Value;
        }

        /// <summary>
        /// Set or insert the value if the key is not already present. When the cache reaches its capacity, it should invalidate the least frequently used item before inserting a new item. For the purpose of this problem, when there is a tie (i.e., two or more keys that have the same frequency), the least recently used key would be evicted.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Insert(int key, int value)
        {
            if (_capacity == 0)
                return;

            var existing = GetValue(key);
            if (existing != null)
            {
                _items[key].Value.Value = value;
                return;
            }

            if (_count >= _capacity)
                Evict();

            _lowestFreq = 1;
            _count++;
            _freq.TryAdd(1, new LinkedList<LFUCacheNode>());
            var node = _freq[1].AddFirst(new LFUCacheNode(key, value));
            _items[key] = node;
        }

        private void Evict()
        {
            var lowestItems = _freq[_lowestFreq];
            var node = lowestItems.Last;
            lowestItems.Remove(node);
            if (lowestItems.Count == 0)
            {
                _freq.Remove(_lowestFreq);
            }
            _items.Remove(node.Value.Key);
            _count--;
        }
    }
}
