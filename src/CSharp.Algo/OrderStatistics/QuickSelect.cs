using System;
namespace CSharp.DS.Algo.OrderStatistics
{
    /// <summary>
    /// Time Complexity:
    /// Best: O(n)
    /// Average: O(n)
    /// Worst: O(n^2) for std implementation, o(n) if pivot selection based on medians of medians
    /// </summary>
    public class QuickSelect
    {
        /// <summary>
        /// Select the K-th smallest/largest element from an array
        /// </summary>
        /// <param name="k">0 indexed</param>
        /// <param name="compareFunc"></param>
        /// <returns></returns>
        public static T Quickselect<T>(T[] array, int start, int end, int k, Func<T, T, int> compareFunc = default) where T : IComparable
        {
            if (compareFunc == default)
                compareFunc = (n1, n2) => n1.CompareTo(n2);

            int right = -1;
            while (right != k)
            {
                if (start > end)
                {
                    throw new Exception();
                }

                int pivot = start;
                int left = start + 1;
                right = end;
                while (left <= right)
                {
                    if (compareFunc(array[left], array[pivot]) > 0 &&
                            compareFunc(array[right], array[pivot]) < 0)
                        Swap(array, left, right);

                    if (compareFunc(array[left], array[pivot]) <= 0)
                        left++;

                    if (compareFunc(array[right], array[pivot]) >= 0)
                        right--;
                }

                Swap(array, pivot, right);
                if (right < k)
                    start = right + 1;
                else if (right > k)
                    end = right - 1;
            }

            return array[right];
        }

        /// <summary>
        /// Select the K-th smallest/largest element from an array selecting pivot based on Median of Medians
        /// </summary>
        /// <param name="k">0 indexed</param>
        /// <param name="compareFunc"></param>
        /// <returns></returns>
        public static T FastQuickSelect<T>(T[] array, int start, int end, int k, Func<T, T, int> compareFunc = default) where T : IComparable
        {
            int n = end - start;
            if (n < 2)
                return array[start];

            T pivot = array[start + (k * 7919) % n]; // Pick a random pivot

            int nLess = 0, nSame = 0, nMore = 0;
            int lo3 = start;
            int hi3 = end;
            while (lo3 < hi3)
            {
                T e = array[lo3];
                int cmp = e.CompareTo(pivot);
                if (cmp < 0)
                {
                    nLess++;
                    lo3++;
                }
                else if (cmp > 0)
                {
                    Swap(array, lo3, --hi3);
                    if (nSame > 0)
                        Swap(array, hi3, hi3 + nSame);
                    nMore++;
                }
                else
                {
                    nSame++;
                    Swap(array, lo3, --hi3);
                }
            }

            if (k >= n - nMore)
                return FastQuickSelect(array, end - nMore, end, k - nLess - nSame);
            else if (k < nLess)
                return FastQuickSelect(array, start, start + nLess, k);
            return array[start + k];
        }

        public static void Swap<T>(T[] array, int i, int j)
        {
            var tmp = array[j];
            array[j] = array[i];
            array[i] = tmp;
        }
    }
}
