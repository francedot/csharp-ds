﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.DS.Heap
{
    /// <summary>
    /// Heap providing constant time access and removal to its elements.
    /// Useful for sifting up the elements starting from the index of its element.
    /// An practical example is Prim and Dijstra implementations in weighted graph.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IndexedHeap<T> : IEnumerable<T>
    {
        public readonly IList<T> _elements;
        private readonly Func<T, T, int> _compareFunc;
        private readonly Dictionary<T, int> _indexes;

        public IndexedHeap(Func<T, T, int> compareFunc) : this(new List<T>(), compareFunc) { }

        public IndexedHeap(IList<T> elements, Func<T, T, int> compareFunc)
        {
            _elements = elements;
            _compareFunc = compareFunc;

            _indexes = new Dictionary<T, int>();
            for (var i = 0; i < elements.Count(); i++)
                _indexes.Add(elements.ElementAt(i), i);

            Heapify();
        }

        public void Heapify()
        {
            int i = _elements.Count - 1;
            while (i > 0) // We stop just before visiting root node
            {
                if (TryGetParentNode(i, out var parentNodeIndex))
                {
                    SiftDown(fromIndex: parentNodeIndex.Value);
                    i -= 2;
                }
            }
        }

        public void SiftDown(int fromIndex)
        {
            while (TryGetChildNodeLeft(fromIndex, out var childNodeLeftIndex))
            {
                var indexNodeLeft = ((childNodeLeftIndex.Value), _elements[childNodeLeftIndex.Value]);

                (int, T) prevNode;
                if (TryGetChildNodeRight(fromIndex, out var childNodeRightIndex))
                {
                    var indexNodeRight = ((childNodeRightIndex.Value), _elements[childNodeRightIndex.Value]);
                    prevNode = _compareFunc(indexNodeLeft.Item2, indexNodeRight.Item2) > 0 ? indexNodeRight : indexNodeLeft;
                }
                else
                {
                    prevNode = indexNodeLeft;
                }

                if (_compareFunc(prevNode.Item2, _elements[fromIndex]) < 0)
                {
                    Swap(prevNode.Item1, fromIndex, _elements);
                }

                fromIndex = prevNode.Item1;
            }
        }

        public void SiftUp(int fromIndex)
        {
            while (TryGetParentNode(fromIndex, out var parentNodeIndex)
                    && _compareFunc(_elements[parentNodeIndex.Value], _elements[fromIndex]) > 0)
            {
                Swap(parentNodeIndex.Value, fromIndex, _elements);
                fromIndex = parentNodeIndex.Value;
            }
        }

        public T Peek()
        {
            if (!_elements.Any())
            {
                return default;
            }

            return _elements.FirstOrDefault();
        }

        public T Pop()
        {
            if (!_elements.Any())
            {
                return default;
            }

            var result = Peek();

            Swap(0, _elements.Count - 1, _elements);
            _elements.RemoveAt(_elements.Count - 1);
            _indexes.Remove(result);

            SiftDown(fromIndex: 0);

            return result;
        }

        public void Push(T element)
        {
            _elements.Add(element);
            _indexes.Add(element, _elements.Count - 1);

            SiftUp(_elements.Count - 1);
        }

        public bool Remove(T element)
        {
            var index = IndexOf(element);
            if (!_elements.Remove(element))
            {
                return false;
            }
            _indexes.Remove(element);

            // Sift down the indexes
            for (var i = _elements.Count() - 1; i >= index; i--)
                _indexes[_elements.ElementAt(i)] = i - 1;

            Heapify();
            return true;
        }

        public bool Contains(T element)
        {
            return _indexes.ContainsKey(element);
        }

        public int IndexOf(T element)
        {
            return _indexes[element];
        }

        private bool CheckIndexInBoundaries(int nodeIndex)
        {
            return nodeIndex >= 0 && nodeIndex < _elements.Count;
        }

        private bool TryGetChildNodeLeft(int parentNodeIndex, out int? childNodeLeftIndex)
        {
            childNodeLeftIndex = null;

            if (!CheckIndexInBoundaries(parentNodeIndex))
            {
                return false;
            }

            var childNodeLeftIndex_ = 2 * parentNodeIndex + 1;
            if (!CheckIndexInBoundaries(childNodeLeftIndex_))
            {
                return false;
            }

            childNodeLeftIndex = childNodeLeftIndex_;
            return true;
        }

        private bool TryGetChildNodeRight(int parentNodeIndex, out int? childNodeRightIndex)
        {
            childNodeRightIndex = null;

            if (!CheckIndexInBoundaries(parentNodeIndex))
            {
                return false;
            }

            var childNodeRightIndex_ = 2 * parentNodeIndex + 2;
            if (!CheckIndexInBoundaries(childNodeRightIndex_))
            {
                return false;
            }

            childNodeRightIndex = childNodeRightIndex_;
            return true;
        }

        private bool TryGetParentNode(int childNodeIndex, out int? parentNodeIndex)
        {
            parentNodeIndex = null;

            if (!CheckIndexInBoundaries(childNodeIndex))
            {
                return false;
            }

            var parentNodeIndex_ = (int)Math.Floor((childNodeIndex - 1) / 2.0);
            if (!CheckIndexInBoundaries(parentNodeIndex_))
            {
                return false;
            }

            parentNodeIndex = parentNodeIndex_;
            return true;
        }

        private void Swap(int i, int j, IList<T> list)
        {
            var temp = list[j];
            list[j] = list[i];
            list[i] = temp;

            _indexes[list[i]] = i;
            _indexes[list[j]] = j;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
