using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.DS.Core.Heap
{
    public class MaxHeap<T> : Heap<T> where T : IComparable
    {
        public MaxHeap(List<T> elements) : base(elements, (T n1, T n2) => -n1.CompareTo(n2))
        {
        }

        public MaxHeap() : this(new List<T>())
        {
        }
    }
}
