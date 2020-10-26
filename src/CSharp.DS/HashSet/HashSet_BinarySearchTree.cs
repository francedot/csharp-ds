using System.Collections.Generic;
using CSharp.DS.Tree.Binary;

namespace CSharp.DS.Hash
{
    /*
        - Hash function: the goal of the hash function is to assign an address to store a given value.
                            Simple hash function: modulo operator on a prime number
            1. Static Hashing: the range of addresses for the hash function is fixed (e.g. the base of modulo operator is fixed)
            2. Dynamic Hashing: One could set up a threshold on the load factor (# elements / total_space),
                                and double the range of address, once the load factor exceeds the threshold (e.g 80%).
                                The increase of address space could potentially reduce the collisions,
                                therefore improve the overall performance of HashSet.
                                However, one should also take into account the cost of rehashing
                                and redistributing the existing values. Consistent hashing can help.
        - Collision handling: since the nature of a hash function is to map a value
                                from a space A into a corresponding value in a smaller space B,
                                it could happen that multiple values from space A might be 
                                mapped to the same value in space B

            There are several strategies to resolve the collisions:
                1. Separate Chaining: for values with the same hash key, we keep them in a bucket, and each bucket is independent from each other.
                2. Open Addressing: whenever there is a collision, we keep on probing on the main space with certain strategy until a free slot is found.
                3. 2-Choice Hashing: we use two hash functions rather than one, and we pick the generated address with fewer collision.
                4. Consistent Hashing: special kind of hashing s.t. when a hash table is resized,
                                    only N/M keys need to be remapped on average where N is the
                                    number of keys and M is the number of slots.
        */
    /// <summary>
    /// Hashset - Separate Chaining of Bucket using Binary Search Tree
    /// </summary>
    public class HashSet_BinarySearchTree
    {
        // Time: O(Log(N/K)) where N is the number of all possible values and K is the number of predefined buckets, which is 431.
        //       Worst case, we would need to scan the entire bucket, hence the time complexity is O(N/K) 
        // Space: O(K + M) where M is the number of unique values that have been inserted into the HashSet.
        public class Bucket
        {
            private readonly BinarySearchTree<int> _bst;

            public Bucket()
            {
                _bst = new BinarySearchTree<int>();
            }

            public void Add(int key)
            {
                // Root might be updated
                _bst.root = _bst.Insert(_bst.root, key);
            }

            public void Remove(int key)
            {
                _bst.root = _bst.Delete(_bst.root, key);
            }

            public bool Any(int key) => _bst.BinarySearch(_bst.root, key) != null;
        }

        private readonly IList<Bucket> _buckets;
        private readonly int _keyRange;

        public HashSet_BinarySearchTree()
        {
            _keyRange = 431; // Use a Prime Number to minimize collision
            _buckets = new Bucket[_keyRange];
            for (var i = 0; i < _keyRange; i++)
                _buckets[i] = new Bucket();
        }

        protected int Hash(int key)
        {
            return key % _keyRange;
        }

        public void Add(int key)
        {
            _buckets[Hash(key)].Add(key);
        }

        public void Remove(int key)
        {
            _buckets[Hash(key)].Remove(key);
        }

        public bool Contains(int key)
        {
            return _buckets[Hash(key)].Any(key);
        }
    }
}
