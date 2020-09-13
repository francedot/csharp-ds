using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.DS.Core.Heap
{
    public class MinHeap<T> : Heap<T> where T : IComparable
    {
        public MinHeap(List<T> elements) : base(elements, (T n1, T n2) => n1.CompareTo(n2))
        {
        }

        public MinHeap() : this(new List<T>())
        {
        }
    }
}
