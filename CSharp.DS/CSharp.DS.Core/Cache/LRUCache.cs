using System;
using System.Collections.Generic;

namespace CSharp.DS.Core.Cache
{
    public class LRUCache
    {
        public class LRUCacheNode
        {
            public LRUCacheNode prev;
            public LRUCacheNode next;

            public string key;
            public int val;

            public LRUCacheNode(string key, int val)
            {
                this.key = key;
                this.val = val;
            }

            /// <summary>
            /// Helper function to detach a node from the DLL List
            /// </summary>
            public void Detach()
            {
                if (prev != null)
                {
                    prev.next = next;
                }
                if (next != null)
                {
                    next.prev = prev;
                }
                prev = null;
                next = null;
            }
        }

        private readonly int _maxSize;
        public int _currentSize;

        public LRUCacheNode _head; // Most Recently Used
        public LRUCacheNode _tail; // Least Recently Used
        public Dictionary<string, LRUCacheNode> _keyToNodeDictionary;

        public LRUCache(int size)
        {
            if (size < 1)
            {
                throw new ArgumentException("Size of LRU Cache must be greater than 0", nameof(size));
            }

            _maxSize = size;
            _keyToNodeDictionary = new Dictionary<string, LRUCacheNode>(size);
        }

        /// <summary>
        /// Set or insert the value if the key is not already present. When the cache reached its capacity, it should invalidate the least recently used item before inserting a new item.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Insert(string key, int value)
        {
            LRUCacheNode newEntry;
            if (_keyToNodeDictionary.ContainsKey(key))
            {
                _keyToNodeDictionary[key].val = value;
                newEntry = _keyToNodeDictionary[key];

                if (newEntry == _tail)
                    _tail = _tail.prev;
                else if (newEntry == _head)
                    _head = _head.next;

                // Remove newEntry and patch the DLL
                newEntry.Detach();

                // Detach the node
                newEntry.prev = newEntry.next = null;
            }
            else
            {
                newEntry = new LRUCacheNode(key, value);

                // Eviction based on LRU
                if (_currentSize == _maxSize)
                    Evict();
                else
                    _currentSize++; // Only increment if it is not contained and currentSize < maxsize

                _keyToNodeDictionary.Add(newEntry.key, newEntry);
            }

            // Update MRU
            var prevHead = _head;
            _head = newEntry;
            _head.next = prevHead;
            if (prevHead != null)
                prevHead.prev = _head;

            if (_tail == null)
                _tail = newEntry;
        }

        /// <summary>
        ///  Get the value (will always be positive) of the key if the key exists in the cache, otherwise return -1.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int? GetValue(string key)
        {
            if (!_keyToNodeDictionary.ContainsKey(key))
            {
                return null;
            }

            // Fake-update the value to sift the mru to the current key
            Insert(key, _keyToNodeDictionary[key].val);
            return _keyToNodeDictionary[key].val;
        }

        public string GetMostRecentKey()
        {
            return _head?.key;
        }

        private void Evict()
        {
            _keyToNodeDictionary.Remove(_tail.key);
            _tail = _tail?.prev;
            if (_tail?.next != null)
                _tail.next = null;
        }
    }
}
