using CSharp.DS.Heap;
using System;
using System.Collections.Generic;

namespace CSharp.DS.Queue
{
    /// <summary>
    /// Facade to mimic the Java Priority Queue spec.
    /// Implementation based on Binary Heap (no Fibonacci heap).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PriorityQueue<T> : HeapBase<T>
    {
        public PriorityQueue(IList<T> elements, Func<T, T, int> compareFunc) : base(elements, compareFunc)
        {
        }

        public PriorityQueue(Func<T, T, int> compareFunc) : this(new List<T>(), compareFunc)
        {
        }

        public void Offer(T element) => base.Push(element);

        public T Poll() => base.Pop();
    }
}
