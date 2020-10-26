using CSharp.DS.Heap;
using System.Linq;

namespace CSharp.DS.OrderStatistics
{
    public partial class OrderStatistics
    {
        /// <summary>
        /// Find the median value continously from a data stream 
        /// </summary>
        public class MedianFinder
        {
            private readonly MaxHeap<int> _lowerHalf;
            private readonly MinHeap<int> _higherHalf;
            private int _count;

            public MedianFinder()
            {
                // Sorting is an invariant
                _lowerHalf = new MaxHeap<int>();
                _higherHalf = new MinHeap<int>();
            }

            public void AddNum(int num)
            {
                // Add to max heap
                _lowerHalf.Push(num);

                // Balancing the Tree
                _higherHalf.Push(_lowerHalf.Peek());
                _lowerHalf.Pop();

                if (_lowerHalf.Count() < _higherHalf.Count())
                {
                    // Maintain the size property
                    _lowerHalf.Push(_higherHalf.Peek());
                    _higherHalf.Pop();
                }

                _count++;
            }

            public double FindMedian()
            {
                if (_count % 2 == 0)
                    return (_higherHalf.Peek() + _lowerHalf.Peek()) / 2.0;
                else return _lowerHalf.Peek();
            }
        }
    }
}
