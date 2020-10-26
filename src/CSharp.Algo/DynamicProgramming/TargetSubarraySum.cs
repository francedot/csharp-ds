using System;
using System.Collections.Generic;

namespace CSharp.DS.Algo.DP
{
    public partial class TargetSubarraySum
    {
        /*
            Given an array of integers and an integer k,
            you need to find the total number of continuous subarrays
            whose sum equals to k.
         */

        /*
            Consider every possible subarray of the given nums array
            Find the sum of the elements of each of those subarrays/
            Check for the equality of the sum obtained with the given k.
            Whenever the sum equals k, we can increment the count.
            Time: O(N^3)
        */
        public int TargetSubarraySumK_BruteForce(int[] nums, int k)
        {
            var count = 0;
            for (var start = 0; start < nums.Length; start++)
            {
                for (var end = start + 1; end <= nums.Length; end++)
                {
                    int sum = 0;
                    for (int i = start; i < end; i++)
                        sum += nums[i];
                    if (sum == k)
                        count++;
                }
            }

            return count;
        }

        /*
            Instead of determining the sum of elements everytime for every
            new subarray considered, we can make use of a cumulative sum array, sum.
            Then, in order to calculate the sum of elements lying between two indices,
            we can subtract the cumulative sum corresponding to the two indices
            to obtain the sum directly.
            Time: O(N^2)
            Time: O(N)
         */
        public int TargetSubarraySumK_PrefixSum(int[] nums, int k)
        {
            int count = 0;
            int[] sum = new int[nums.Length + 1];
            sum[0] = 0;

            // sum[i] is used to store the cumulative sum of nums array
            // upto the element corresponding to the (i-1)th.
            // Thus, to determine the sum of elements for the subarray nums[i:j],
            // we can directly use sum[j+1]-sum[i].

            for (var i = 1; i <= nums.Length; i++)
                sum[i] = sum[i - 1] + nums[i - 1];

            for (int start = 0; start < nums.Length; start++)
            {
                for (int end = start + 1; end <= nums.Length; end++)
                {
                    if (sum[end] - sum[start] == k)
                        count++;
                }
            }
            return count;
        }

        /*
            Instead of considering all the start and end points and then finding the sum
            for each subarray corresponding to those points, we can directly find
            the sum on the go while considering different end points
            Time: O(N^2)
            Time: O(1)
         */
        public int TargetSubarraySumK_Sum(int[] nums, int k)
        {
            var count = 0;
            for (int start = 0; start < nums.Length; start++)
            {
                int sum = 0;
                for (int end = start; end < nums.Length; end++)
                {
                    sum += nums[end];
                    if (sum == k)
                        count++;
                }
            }
            return count;
        }

        // Time: O(N)
        // Space: O(N)
        public int TargetSubarraySumK(int[] nums, int k)
        {
            int count = 0, cumulativeSum = 0;
            var map = new Dictionary<int, int>
            {
                { 0, 1 } // There is 1 element with sum = 0
            };

            for (var i = 0; i < nums.Length; i++)
            {
                cumulativeSum += nums[i];

                // Check in the map, whether we have an element whose starting point had sum: sum - k
                // If so, must keep track of all those occurrences
                // Looking for sumStart starting from sumEnd: sumStart = sumEnd - k.
                var sumStart = cumulativeSum - k;
                if (map.ContainsKey(sumStart))// 76 - 26
                    count += map[cumulativeSum - k];

                // k = 26
                // If a sub-array sums up to k, then the sum at the end of this sub-array will be
                // sumEnd = sumStart + k. That implies: sumStart = sumEnd - k.
                // Suppose, at index 10, sum = 50, and the next 6 numbers are 8,-5,-3,10,15,1.
                // At index 13, sum will be 50 again (the numbers from indexes 11 to 13 add up to 0).
                // Then at index 16, sum = 76.
                // Now, when we reach index 16, sum - k = 76 - 26 = 50
                // So, if this is the end index of a sub-array(s) which sums up to k,
                // then before this, just before the start of the sub-array, the sum should be 50.
                // As we found sum = 50 at two places before reaching index 16, we indeed have two sub-arrays which sum up to k(26):
                // from indexes 14 to 16 and from indexes 11 to 16.
                // Update the map with the info we have found 1 more element with this sum

                // At each step we're recording earlier cumulative sums 
                // so that when we encounter the latest cumsum that is k
                // away from an earlier cumsum, we know to increment count
                // Recording sumStart = sumEnd - k.
                map[cumulativeSum] = (map.TryGetValue(cumulativeSum, out var a) ? a : 0) + 1;
            }

            return count;
        }

        // Time: O(N)
        // Space: O(N)
        public static IList<IList<int>> GetTargetSubarraySumK(int[] nums, int k)
        {
            var result = new List<IList<int>>();
            int cumulativeSum = 0;
            var map = new Dictionary<int, List<int>>()
            {
                { 0,  new List<int>() { -1 } }
                // Init logic: Required for subarrays starting at index 0
            };

            for (var i = 0; i < nums.Length; i++)
            {
                cumulativeSum += nums[i];

                // k = 26
                // Suppose, at index 0, sum = 50, and the next 6 numbers are 8,-5,-3,10,15,1.

                // Looking for sumStart starting from sumEnd: sumStart = sumEnd - k.
                var sumStart = cumulativeSum - k;
                if (map.TryGetValue(sumStart, out var indexes)) // 76 - 26 = 50
                {
                    foreach (var startIndex in indexes)
                        result.Add(nums[(startIndex+1)..(i+1)]);
                }
                
                // Recording sumStart = sumEnd - k.
                if (!map.ContainsKey(cumulativeSum))
                    map.Add(cumulativeSum, new List<int>());
                map[cumulativeSum].Add(i);
            }

            return result;
        }
    }
}
