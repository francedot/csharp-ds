using System;

namespace CSharp.DS.Algo.Sorting
{
    public partial class Sorting
    {
        public static void MergeSort(int[] array)
        {
            // Time: Avg O(NLogN) | Worst O(NLogN) | Best O(NLogN) 
            // Space: Avg O(N) | Worst O(N)

            MergeSort(array, new int[array.Length], 0, array.Length - 1);
        }

        private static void MergeSort(int[] array, int[] temp, int leftStart, int rightEnd)
        {
            if (leftStart >= rightEnd)
                return;

            var middle = (leftStart + rightEnd) / 2;
            MergeSort(array, temp, leftStart, middle);
            MergeSort(array, temp, middle + 1, rightEnd);
            MergeHalves(array, temp, leftStart, rightEnd);
        }

        private static void MergeHalves(int[] array, int[] temp, int leftStart, int rightEnd)
        {
            var leftEnd = (rightEnd + leftStart) / 2;
            var rightStart = leftEnd + 1;
            var size = rightEnd - leftStart + 1;

            var left = leftStart;
            var right = rightStart;
            var index = leftStart;

            while (left <= leftEnd && right <= rightEnd)
            {
                if (array[left] <= array[right])
                {
                    temp[index] = array[left];
                    left++;
                }
                else
                {
                    temp[index] = array[right];
                    right++;
                }
                index++;
            }

            Array.Copy(array, left, temp, index, leftEnd - left + 1);
            Array.Copy(array, right, temp, index, rightEnd - right + 1);
            Array.Copy(temp, leftStart, array, leftStart, size);
        }
    }
}
