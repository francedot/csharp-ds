using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.DS.Heap
{
    public class MinHeap<T> : HeapBase<T> where T : IComparable
    {
        public MinHeap(IList<T> elements) : base(elements, (T n1, T n2) => n1.CompareTo(n2))
        {
        }

        public MinHeap() : this(new List<T>())
        {
        }
    }
}
