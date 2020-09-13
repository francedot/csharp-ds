using CSharp.DS.Core.Heap;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharp.DS.Core
{
    /// <summary>
    /// Adapter DS to mimic the Java Priority Queue interface.
    /// Implementation based on Binary Heap (no Fibonacci heap).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PriorityQueue<T> : IEnumerable<T>
    {
        private readonly Heap<T> _heap;

        public PriorityQueue(Func<T, T, int> compareFunc, IList<T> elements = default)
        {
            if (elements == default)
            {
                elements = new List<T>();
            }

            _heap = new Heap<T>(elements, compareFunc);
        }

        public void Offer(T element)
        {
            _heap.Push(element);
        }

        public T Peek()
        {
            return _heap.Peek();
        }

        public T Poll()
        {
            return _heap.Pop();
        }

        public bool Remove(T element)
        {
            return _heap.Remove(element);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _heap.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
