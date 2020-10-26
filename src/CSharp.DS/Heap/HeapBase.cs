using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharp.DS.Heap
{
    public abstract class HeapBase<T> : IEnumerable<T>
    {
        public readonly IList<T> _elements;
        private readonly Func<T, T, int> _compareFunc;

        public HeapBase(IList<T> elements, Func<T, T, int> compareFunc)
        {
            _elements = elements;
            _compareFunc = compareFunc;

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

            SiftDown(fromIndex: 0);

            return result;
        }

        public void Push(T element)
        {
            _elements.Add(element);

            SiftUp(_elements.Count - 1);
        }

        public bool Remove(T element)
        {
            if (!_elements.Remove(element))
            {
                return false;
            }

            Heapify();
            return true;
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
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }
    }
}
