using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp.Algo.DynamicProgramming
{
    public class TrappingRainWater
    {
        public class LeftRightMax
        {
            public int Left;
            public int Right;
        }


        public int Trap_Tabulation(int[] height)
        {
            if (height == null)
            {
                return 0;
            }

            if (!height.Any())
            {
                return 0;
            }

            // [0,1,0,2,1,0,1,3,2,1,2,1]
            // Optimal Solution using DP Bottom UP (Compromise on space)
            // Time: O(N) where N is the length of the map
            // Space: O(N*2) = O(N) to store the Left Right Max memo
            // Storing the highest bar size on the left and right of the index

            // DP computing maxLeft and maxRight for each index
            var memo = new Dictionary<int, LeftRightMax>();
            for (var i = 0; i < height.Length; i++)
            {
                memo[i] = new LeftRightMax
                {
                    Left = -1,
                    Right = -1
                };
            }

            int maxLeft = int.MinValue;
            for (var i = 0; i < height.Length; i++)
            {
                if (maxLeft > height[i])
                {
                    memo[i].Left = maxLeft;
                }
                else
                {
                    maxLeft = height[i];
                }
            }

            int maxRight = int.MinValue;
            for (var i = height.Length - 1; i >= 0; i--)
            {
                if (maxRight > height[i])
                {
                    memo[i].Right = maxRight;
                }
                else
                {
                    maxRight = height[i];
                }
            }

            int trappedWater = 0;
            for (var i = 1; i < height.Length; i++)
            {
                var minLeftRightHeight = Math.Min(memo[i].Left, memo[i].Right);
                if (height[i] < minLeftRightHeight)
                { // 1 >= 1 // 0 < 1 
                    trappedWater += minLeftRightHeight - height[i];
                }
            }

            return trappedWater;
        }

        public int Trap_InPlace(int[] height)
        {
            //BF slightly better

            // Time: O(N ^ 2) where N is the length of the map
            // Space: O(1)

            int trappedWater = 0;
            for (var i = 1; i < height.Length; i++)
            {
                // Get the max at left
                int maxLeft = int.MinValue;
                int left = i - 1;
                while (left >= 0)
                {
                    if (maxLeft < height[left])
                        maxLeft = height[left];
                    left--;
                }

                // Get the max at right
                int maxRight = int.MinValue;
                int right = i + 1;
                while (right < height.Length)
                {
                    if (maxRight < height[right])
                        maxRight = height[right];
                    right++;
                }

                var minLeftRightHeight = Math.Min(maxLeft, maxRight);
                if (height[i] < minLeftRightHeight)
                { // 1 >= 1 // 0 < 1 
                    trappedWater += minLeftRightHeight - height[i];
                }
            }

            return trappedWater;
        }
    }
}
