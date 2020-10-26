using CSharp.DS.Heap;
using CSharp.DS.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using static CSharp.DS.LinkedList.SinglyLinkedList<int>;

namespace CSharp.DS.Algo.Sorting
{
    public partial class Sorting
    {
        /*
            Write a function to merge two sorted arrays assuming one of them have enough space to hold all the members 
         */
        public void MergeSortedArrays(int[] nums1, int m, int[] nums2, int n)
        {
            // Pointers for nums1 and nums2
            int p1 = m - 1;
            int p2 = n - 1;

            // Set pointer to the end of nums1
            int p = m + n - 1;

            while (p1 >= 0 && p2 >= 0)
            {
                // Add the largest element to nums1
                if (nums1[p1] < nums2[p2])
                {
                    nums1[p] = nums2[p2];
                    p2--;
                }
                else
                {
                    nums1[p] = nums1[p1];
                    p1--;
                }
                p--;
            }

            // Add missing elements from nums2
            Array.Copy(nums2, 0, nums1, 0, p2 + 1);
        }

        public SLLNode MergeKSortedLinkedLists(SLLNode[] lists)
        {
            SLLNode dummyHead, p;
            dummyHead = p = new SLLNode(0); // Dummy Node init
            var pq = new PriorityQueue<(int, SLLNode)>(
                (e1, e2) => e1.Item1.CompareTo(e2.Item1));

            // Init
            foreach (var headList in lists)
            {
                if (headList != null)
                    pq.Offer((headList.value, headList));
            }

            while (pq.Any())
            {
                var polled = pq.Poll();
                p.next = new SLLNode(polled.Item1);
                p = p.next;

                if (polled.Item2.next != null)
                    pq.Offer((polled.Item2.next.value, polled.Item2.next));
            }

            return dummyHead.next;
        }

        public static List<T> MergeKSortedLists<T>(IList<IList<T>> lists) where T : IComparable
        {
            var result = new List<T>();
            var pq = new PriorityQueue<(T, (IList<T>, int))>(
                (e1, e2) => e1.Item1.CompareTo(e2.Item1));

            // Init
            foreach (var headList in lists)
            {
                if (headList != null)
                    pq.Offer((headList[0], (headList, 0)));
            }

            while (pq.Any())
            {
                var polled = pq.Poll();
                result.Add(polled.Item1);
                polled.Item2.Item2++;

                if (polled.Item2.Item2 < polled.Item2.Item1.Count())
                {
                    polled.Item1 = polled.Item2.Item1[polled.Item2.Item2];
                    pq.Offer(polled);
                }
            }

            return result;
        }

        public static List<T> MergeKSortedLists<T>(IList<IList<T>> lists, Func<T, T, int> compareFunc)
        {
            var result = new List<T>();
            var pq = new PriorityQueue<(T, (IList<T>, int))>(
                (e1, e2) => compareFunc(e1.Item1, e2.Item1));

            // Init
            foreach (var headList in lists)
            {
                if (headList != null)
                    pq.Offer((headList[0], (headList, 0)));
            }

            while (pq.Any())
            {
                var polled = pq.Poll();
                result.Add(polled.Item1);
                polled.Item2.Item2++;

                if (polled.Item2.Item2 < polled.Item2.Item1.Count())
                {
                    polled.Item1 = polled.Item2.Item1[polled.Item2.Item2];
                    pq.Offer(polled);
                }
            }

            return result;
        }
    }
}
