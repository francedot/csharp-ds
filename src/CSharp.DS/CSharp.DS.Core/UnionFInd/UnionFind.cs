using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp.DS.Core.UnionFind
{
    public class UnionFind<T>
    {
        public Dictionary<T, T> subsets;
        public Dictionary<T, int> sizes;

        public UnionFind(IEnumerable<T> elements)
        {
            ComponentsCount = elements.Count();

            subsets = elements.ToDictionary(el => el, el => el);
            sizes = elements.ToDictionary(el => el, el => 1);
        }

        public int ComponentsCount { get; private set; }

        /// <summary>
        /// Find the component (root) for this element
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public T Find(T element)
        {
            // Look for the root of id's subset
            var root = element;
            while (!subsets[root].Equals(root))
                root = subsets[root];

            // Path Compression to allow for O(1) amortized Find
            // Compress the path leading back to the root.
            var p = element;
            while (!p.Equals(root))
            {
                var next = subsets[p];
                subsets[p] = root;
                p = next; // Proceed to next child in the path
            }

            // Or recursive
            // public int Find(int p) {
            //   if (p == id[p]) return p;
            //   return find(id[p]);
            // }

            return root;
        }

        /// <summary>
        /// Merge 2 elements/groups into 1 component
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public T Union(T id1, T id2)
        {
            // Check if already connected
            if (AreConnected(id1, id2))
            {
                throw new ArgumentException("Already connected");
            }

            // Look for the components' roots
            var root1 = Find(id1);
            var root2 = Find(id2);

            T joinedSet;
            // Merge smaller sets into larger ones
            if (sizes[subsets[root1]] >= sizes[subsets[root2]])
            {
                joinedSet = root1;
                sizes[root1] += sizes[root2];
                subsets[root2] = root1;
            }
            else
            {
                joinedSet = root2;
                sizes[root2] += sizes[root1];
                subsets[root1] = root2;
            }

            ComponentsCount--;

            return joinedSet;
        }

        private bool AreConnected(T id1, T id2)
        {
            return Find(id1).Equals(Find(id2));
        }
    }
}
