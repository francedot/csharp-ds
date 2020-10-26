namespace CSharp.DS.Algo.DP
{
    public partial class MaxSubArraySum_Kadane
    {
        /*
            Given nums = [-2, 1, -3, 4], find the maximum contigous sum.
            Compare all elements with the cumulative sum stored in the previous index.

            Since -2 < 0, value -2 doesn't contribute to the sum. Thus, ignore it and proceed to the next index.
            Since 1 > 0, value 1 does contribute. Hence, compute -3+1 = -2 and store it in index 2.
            The result vector is so far: [-2, 1, -2, 4]. Last element to evaluate is 4.
            Since -2 < 0, -2 does not contribute positively to the sum.Thus, ignore it.
            Having checked all elements, the final result vector is: [-2, 1, -2, 4].
            The maximum subarray is max(num)= 4.
        */

        public static int MaxSubArraySumKadane(int[] nums)
        {
            // -2, 1, -3, 4
            // Start from index 1
            // i-1=0: -2<0 -> Ignore -2 as it cannot contribute to the max sum.
            // i-1=1:  1>0 -> Consider the sum starting at index 1 as
            //                it can contribtue to the max subarray sum -> nums[i=2]=-2
            // i-1=2: -2<0 -> Ignore -2 as it cannot contribute to the max sum.
            // At each step: nums[i] may carry the maxSum, so we have to save it
            
            var maxSum = nums[0]; // Edge case: the max is given by the 1st number
            for (var i = 1; i < nums.Length; i++)
            {
                if (nums[i - 1] > 0)
                    nums[i] += nums[i - 1];
                maxSum = System.Math.Max(nums[i], maxSum);
            }

            return maxSum;
        }
    }
}
