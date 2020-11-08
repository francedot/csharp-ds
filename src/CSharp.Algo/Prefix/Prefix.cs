using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Algo.Prefix
{
    public partial class Prefix
    {
        /*
            Given an array find the number that is sum of its left and right subarrays.
         */
        public int FindLeftRightSumTarget(int[] nums)
        {
            // TC
            //       5, 10, 4, 0, 2, 4, 6, 31, 6, 6, 8, 5, 1, 2, 3
            // left: 0  5  15 19 19 21 25  31 ...
            // right:                  ... 31 25 19 11  6  5  3  0

            int[] prefixSum = new int[nums.Length + 1], suffixSum = new int[nums.Length + 1];
            int cumSumLeft = 0, cumSumRight = 0;
            for (int i = 0, j = nums.Length - 1; i < nums.Length; i++, j--)
            {
                prefixSum[i] = cumSumLeft;
                suffixSum[j] = cumSumRight;

                cumSumLeft += nums[i];
                cumSumRight += nums[j];
            }

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == prefixSum[i] && nums[i] == suffixSum[i])
                    return nums[i];
            }

            return -1;
        }
    }
}
