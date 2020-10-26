namespace CSharp.DS.Algo.Sorting
{
    public partial class Sorting
    {        
        public static void QuickSort(int[] array)
        {
            // Time: Avg O(NLogN) | Worst O(N^2) | Best O(n) 
            // Space: Avg O(LogN) | Worst O(N) or O(LogN) with Hoare’s Partition Scheme

            QuickSort(array, 0, array.Length - 1);
        }

        private static void QuickSort(int[] array, int left, int right)
        {
            if (left >= right)
                return;

            int pivot = array[(left + right) / 2];
            int index = Partition(array, left, right, pivot);

            QuickSort(array, left, index - 1);
            QuickSort(array, index, right);
        }

        private static int Partition(int[] array, int left, int right, int pivot)
        {
            while (left <= right)
            {
                while (array[left] < pivot)
                    left++;

                while (array[right] > pivot)
                    right--;

                if (left <= right)
                {
                    Swap(array, left, right);
                    left++;
                    right--;
                }
            }

            return left;
        }

        private static void Swap(int[] array, int i, int j)
        {
            var tmp = array[i];
            array[i] = array[j];
            array[j] = tmp;
        }
    }
}
