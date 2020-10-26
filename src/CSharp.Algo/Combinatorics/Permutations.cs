using System.Collections.Generic;
using System.Linq;

namespace CSharp.DS.Algo.Combinatorics
{
    public partial class Combinatorics
    {
        /*
         Given a collection of distinct integers, return all possible permutations (without repetition of its elements).
         
         Input: [1,2,3]
         Output:
         [
             [1,2,3],
             [1,3,2],
             [2,1,3],
             [2,3,1],
             [3,1,2],
             [3,2,1]
         ]
         */

        public static void Swap<T>(T[] array, int i, int j)
        {
            var tmp = array[j];
            array[j] = array[i];
            array[i] = tmp;
        }

        // Upper Bound: O(n^2*n!) time | O(n*n!) space
        // Roughly: O(n*n!) time | O(n*n!) space
        /// <summary>
        /// Generate all the possible permutation
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static IList<IList<int>> PermuteBF(int[] nums)
        {
            // Time: O(N!*N^2)
            // Space: O(N!*N)

            var result = new List<IList<int>>();
            PermuteRecBF(nums, new List<int>(), result);
            
            return result;
        }

        public static void PermuteRecBF(IList<int> nums, List<int> currentPern, List<IList<int>> result)
        {
            if (!nums.Any() && currentPern.Count > 0)
                result.Add(currentPern);
            else
            {
                for (int i = 0; i < nums.Count(); i++)
                {
                    var newArray = new List<int>(nums);
                    newArray.RemoveAt(i);
                    var newPermutation = new List<int>(currentPern) { nums[i] };
                    PermuteRecBF(newArray, newPermutation, result);
                }
            }
        }

        /// <summary>
        /// Generate all the possible permutation using Heap's algorithm
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static IList<IList<int>> Permute(int[] nums)
        {
            // Time: N!*N where N! number of leaves of recursion tree and N is the depth of the tree
            // Space: N!*N

            var result = new List<IList<int>>();
            PermuteRec(nums, 0, result);

            return result;
        }

        private static void PermuteRec(int[] nums, int index, List<IList<int>> result)
        {
            if (index == nums.Length - 1)
            {
                result.Add(new List<int>(nums));
                return;
            }

            for (var i = index; i < nums.Length; i++)
            {
                Swap(nums, index, i);
                PermuteRec(nums, index + 1, result);

                // Backtrack
                Swap(nums, index, i);
            }
        }

        /// <summary>
        /// Generate all the possible permutation with repetitions using Heap's algorithm
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static IList<IList<int>> PermuteDup(int[] nums)
        {
            // https://en.wikipedia.org/wiki/Permutation#k-permutations_of_n
            // Time: O(P(N,K)*N) = O((N+1)! / (N - K)!) where K is the number of repetitions
            // Space: N!*N

            var result = new List<IList<int>>();
            PermuteDupRec(nums, 0, result);

            return result;
        }

        private static void PermuteDupRec(int[] nums, int index, List<IList<int>> result)
        {
            if (index == nums.Length - 1)
            {
                result.Add(new List<int>(nums));
                return;
            }

            var appeared = new HashSet<int>();
            for (var i = index; i < nums.Length; i++)
            {
                if (!appeared.Contains(nums[i]))
                    continue;

                // E.g. 'num[i]' has already been at this index of current recursion level,
                // so the last possibility is redundant. We can use a hash set to
                // mark which elements have been at the index of current
                // recursion level, so that if we meet the element again,
                // we can just skip it.

                Swap(nums, index, i);
                PermuteDupRec(nums, index + 1, result);

                // Backtrack
                Swap(nums, index, i);
            }
        }

        /*
         Given a collection of distinct integers, return all possible permutations.
         Input: [1,2,3], length=2
         Output:
         [
             [1,2]
             [1,3]
             [2,1]
             [2,3]
             [3,1]
             [3,2]
         ]
         */

        /// <summary>
        /// Generate all the possible combinations of length 'length' using Heap's algorithm
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static IList<IList<int>> PermutationsOfLength(int[] nums, int length)
        {
            // Time: N!*N where N! number of leaves of recursion tree and N is the depth of the tree
            // Space: N!*N

            var result = new List<IList<int>>();
            PermutationsOfLengthRec(nums, length, 0, result);

            return result;
        }

        private static void PermutationsOfLengthRec(int[] nums, int length, int index, List<IList<int>> result)
        {
            if (index == length)
            {
                result.Add(nums.Take(length).ToList());
                return;
            }

            for (var i = index; i < nums.Length; i++)
            {
                Swap(nums, index, i);
                PermutationsOfLengthRec(nums, length, index + 1, result);

                // Backtrack
                Swap(nums, index, i);
            }
        }
    }
}
