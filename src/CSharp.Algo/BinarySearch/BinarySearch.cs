using System;
using System.Linq;

namespace CSharp.DS.Algo.BinarySearch
{
    public partial class BinarySearch
    {
        public static bool TryBinarySearch<T>(T[] elements, T target, out int index) where T : IComparable
        {
            int left = 0, right = elements.Length - 1;
            index = default;
            while (left <= right)
            {
                var mid = left + (right - left) / 2;
                if (elements[mid].CompareTo(target) == 0)
                {
                    index = mid;
                    return true;
                }

                // If target is greater, ignore left half 
                if (elements[mid].CompareTo(target) < 0)
                    left = mid + 1;
                // If target is smaller, ignore right half 
                else right = mid - 1;
            }

            return false;
        }

        /* 
         Template for range-type of problems.
         In this kind of problems you are asked to search for a particular k
         such that it minimize/maximize some property and the search space is contigous and satisy
         some kind of monotonicity, i.e. if condition(k) is True then condition(+1) is true
        */
        private static int BinarySearchRange(int min, int max)
        {
            // E.g search for minimal k satisfying condition a1 < 2
            static bool conditionFunc(int a1) => a1 < 2; // TODO
            int left = min, right = max;
            while (left < right)
            {
                var mid = left + (right - left) / 2;
                if (conditionFunc(mid))
                    right = mid;
                else left = mid + 1;
            }

            // After exiting the while loop, left is the minimal k
            // satisfying the condition function
            return left;
        }

        /*
        Find the missing number from a sorted array.
        Input = [1, 2, 3, 4, 6, 7, 8, 9] Output = 5. I used binary search.
     */

        public static int FindMissingNumberSortedArray(int[] nums)
        {
            // 1, 2, 3, 4, 6, 7, 8, 9
            // 1  2  3  4  5  6  7  8

            // cut at 4, 4<=4 => go right

            // 1, 3, 4, 5, 6, 7, 8, 9
            // 1  2  3  4  5  6  7  8

            // cut at 6, 6>5 => go left

            int left = 0, right = nums.Length - 1;
            while (left < right)
            {
                var mid = left + (right - left) / 2;
                if (nums[mid] > mid + 1)
                    right = mid;
                else left = mid + 1; // go right
            }

            // After exiting the while loop, left is the minimal k satisfying the condition function
            return left + 1;
        }

        public static int Sqrt(int x)
        {
            // First we need to search for minimal k satisfying condition
            // k ^ 2 > x, then k - 1 is the answer to the question.
            static bool conditionFunc(long mid, long x) => mid * mid > x;
            int left = 0, right = x + 1;
            while (left < right)
            {
                var mid = left + (right - left) / 2;
                if (conditionFunc(mid, x))
                    right = mid;
                else left = mid + 1;
            }

            // After exiting the while loop, left is the minimal k
            // satisfying the condition function
            return left;
        }

        /*
            A conveyor belt has packages that must be shipped from one port to another within D days.
            The i-th package on the conveyor belt has a weight of weights[i]. Each day, we load the ship with packages on the conveyor belt (in the order given by weights).
            We may not load more weight than the maximum weight capacity of the ship.
            Return the least weight capacity of the ship that will result in all the packages on the conveyor belt being shipped within D days.
         
            Input: weights = [1,2,3,4,5,6,7,8,9,10], D = 5
            Output: 15
            Explanation: 
            A ship capacity of 15 is the minimum to ship all the packages in 5 days like this:
            1st day: 1, 2, 3, 4, 5
            2nd day: 6, 7
            3rd day: 8
            4th day: 9
            5th day: 10

            Note that the cargo must be shipped in the order given, so using a ship of capacity 14 and splitting the packages into parts like (2, 3, 4, 5), (1, 6, 7), (8), (9), (10) is not allowed. 
         */

        public static int ShipWithinDays(int[] weights, int D)
        {
            // Time: O(N * Log(sum of weights))
            // Space: O(N)

            // Step 0
            // We can dig out the monotonicity of this problem:
            // if we can successfully ship all packages within D days with capacity m,
            // then we can definitely ship them all with any capacity larger than m.

            // Step 1
            // Capacity at least max(weights), otherwise the conveyor belt couldn't ship the heaviest package
            // Capacity need not be more than sum(weights), because then we can ship all packages in just one day.

            // Step 2
            // Now we can design a condition function, let's call it feasible,
            // given an input capacity, it returns whether it's possible to ship
            // all packages within D days.
            // This can run in a greedy way: if there's still room for the current package,
            // we put this package onto the conveyor belt,
            // otherwise we wait for the next day to place this package.
            // If the total days needed exceeds D, we return False, otherwise we return True.
            bool conditionFunc(int capacity)
            {
                int days = 1, total = 0;
                foreach (var weight in weights)
                {
                    total += weight;
                    if (total > capacity) // Too heavy, wait for next day
                    {
                        total = weight;
                        days += 1;
                        if (days > D)
                            return false;
                    }
                }

                return true;
            }

            int left = weights.Max(), right = weights.Sum();
            while (left < right)
            {
                var mid = left + (right - left) / 2;
                if (conditionFunc(mid))
                    right = mid;
                else left = mid + 1;
            }

            // After exiting the while loop, left is the minimal k
            // satisfying the condition function
            return left;
        }

        /*
            Given an array nums which consists of non-negative integers and an integer m,
            you can split the array into m non-empty continuous subarrays.
            Write an algorithm to minimize the largest sum among these m subarrays.

            Input: nums = [7,  2,  5,  10, 8], m = 2
            Output: 18
            Explanation:
            There are four ways to split nums into two subarrays.
            The best way is to split it into [7,2,5] and [10,8],
            where the largest sum among the two subarrays is only 18.
         */

        public static int SplitArray(int[] nums, int m)
        {
            // Time: O(N * Log(sum of array))
            // Space: O(N)

            // Step 0
            // We can dig out the monotonicity of this problem:
            // if we can successfully divide the nums array into m parts less than a threshold,
            // then we can definitely divide the array in m+1 parts using a greater threshold.

            // Step 1:
            // Capacity at least max(nums), otherwise we cannot form a subarray using the greatest value.
            // Capacity need not be more than sum(nums), because then we can use 1 subarray only if m=1.

            // Step 2
            // Now we can design a condition function, let's call it feasible,
            // given an input threshold, it returns whether it's possible to build
            // 'm' arrays with a sum less or equal the threshols.
            // This can run in a greedy way.
            bool conditionFunc(int threshold)
            {
                int count = 1, total = 0;
                foreach (var num in nums)
                {
                    total += num;
                    if (total > threshold) // Too big for this threshold, use the next subarray
                    {
                        total = num;
                        count += 1;
                        if (count > m)
                            return false;
                    }
                }

                return true;
            }

            int left = nums.Max(), right = nums.Sum();
            while (left < right)
            {
                var mid = left + (right - left) / 2;
                if (conditionFunc(mid))
                    right = mid;
                else left = mid + 1;
            }

            // After exiting the while loop, left is the minimal k
            // satisfying the condition function
            return left;
        }
    }
}
