namespace CSharp.DS.Algo.Sorting
{
    public partial class Sorting
    {
        // Time: O(N + A) where A is the alphabet size
        public static int[] CountingSort(int[] nums, int alphabetSize)
        {
            // Counting sort is efficient if the range of input data 'alphabetSize'
            // is not significantly greater than the number of objects to be sorted
            
            var resultArray = new int[nums.Length];
            var countArray = new int[alphabetSize];

            for (var i = 0; i < nums.Length; i++)
                countArray[nums[i]]++;

            var sum = 0;
            for (var i = 0; i < alphabetSize; i++)
            {
                sum += countArray[i];
                countArray[i] = sum;
            }

            for (var i = 0; i < nums.Length; i++)
            {
                var unsortedVal = nums[i];
                var positionInSorted = countArray[unsortedVal];

                resultArray[positionInSorted - 1] = unsortedVal;

                countArray[unsortedVal]--;
            }

            return resultArray;
        }
    }
}
