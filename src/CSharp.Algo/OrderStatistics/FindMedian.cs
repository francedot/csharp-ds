using CSharp.DS.Heap;
using System;
using System.Linq;

namespace CSharp.DS.Algo.OrderStatistics
{
    public partial class OrderStatistics
    {
        /*
            In computer science, a selection algorithm is an algorithm for finding
            the kth smallest number in a list or array; such a number is called
            the kth order statistic. This includes the cases of finding
            the minimum, maximum, and median elements.
        */

        /// <summary>
        /// Find median of an unsorted array using K-th order statistic
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public double FindMedianUnsortedArray(int[] nums)
        {
            if (nums.Length % 2 == 1)
                return QuickSelect.Quickselect(nums, 0, nums.Length - 1, nums.Length / 2);

            var rightCenter = nums.Length / 2;
            var leftCenter = rightCenter - 1;
            return (QuickSelect.Quickselect(nums, 0, nums.Length - 1, leftCenter) +
                QuickSelect.Quickselect(nums, 0, nums.Length - 1, rightCenter)) / 2.0;
        }

        /// <summary>
        /// Find median of 2 sorted arrays balancing heaps
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            var minHeap = new MinHeap<int>();
            var maxHeap = new MaxHeap<int>();

            // An integer from the array is first added to the minheap.
            int i = 0, j = 0;
            for (; i < nums1.Length && j < nums2.Length;)
            {
                int nextNum;
                if (nums1[i] > nums2[j])
                {
                    nextNum = nums2[j];
                    j++;
                }
                else
                {
                    nextNum = nums1[i];
                    i++;
                }

                BalanceHeaps(minHeap, maxHeap, nextNum);
            }

            while (i < nums1.Length)
            {
                BalanceHeaps(minHeap, maxHeap, nums1[i]);
                i++;
            }

            while (j < nums2.Length)
            {
                BalanceHeaps(minHeap, maxHeap, nums2[j]);
                j++;
            }

            // In the end, the median is found by using the peek element from min-heap and peek element from the max-heap
            double median;
            if (minHeap.Count() == maxHeap.Count())
                median = (minHeap.Peek() + maxHeap.Peek()) / 2.0;
            else if (minHeap.Count() > maxHeap.Count())
                median = minHeap.Peek();
            else
                median = maxHeap.Peek();

            return median;
        }

        private static void BalanceHeaps(MinHeap<int> minHeap, MaxHeap<int> maxHeap, int nextNum)
        {
            // An integer from the file is first added to the min-heap.
            // Then while the max-heap peek element is smaller than the min-heap peek element, insert max-heap elements into min-heap.
            minHeap.Push(nextNum);
            while (maxHeap.Any() && maxHeap.Peek() < minHeap.Peek())
                minHeap.Push(maxHeap.Pop());

            // Now we adjust the heap by repeatedly moving min-heap peek element into the max-heap until their size difference is at the max 1 element.
            // Now adjust their sizes to |min size - max size| == 1
            while (System.Math.Abs(minHeap.Count() - maxHeap.Count()) > 1)
            {
                maxHeap.Push(minHeap.Pop());
            }
        }
    }
}
