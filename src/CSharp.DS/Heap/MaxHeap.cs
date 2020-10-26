using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.DS.Heap
{
    public class MaxHeap<T> : HeapBase<T> where T : IComparable
    {
        public MaxHeap(IList<T> elements) : base(elements, (T n1, T n2) => -n1.CompareTo(n2))
        {
        }

        public MaxHeap() : this(new List<T>())
        {
        }
    }
}
