using System;

namespace CSharp.DS.Algo.Sliding_Window
{
    public partial class SlidingWindow
    {
        /*
            Problems such as finding longest substring or shortest substring with some contraints are mostly based on sliding window.

            Basic template of such problems is basically 3 steps.
        
            1. Have a counter or hash-map to count specific array input and
               keep on increasing the window toward right using outer loop.
            2. Have a while loop inside to reduce the window side by
               sliding toward right. Movement will be based on constraints of problem.
            3. Store the current maximum window size or minimum window size or
               number of windows based on problem requirement.
         */

        /*
            Given an array A of 0s and 1s, we may change up to K values from 0 to 1.
            Return the length of the longest (contiguous) subarray that contains only 1s.
            Input: A = [1,1,1,0,0,0,1,1,1,1,0], K = 2
            Output: 6
         */
        public int LongestOnes(int[] A, int K)
        {
            // 1,1,1,0,0,0,1,1,1,1,0 | k=2
            // BF: O(N^2)

            // Sliding Window
            // i: outer loop index, increase to the right
            // j: inner loop index, decrease window size based on problem constraints
            //    1,1,1,0,0,0,1,1,1,1,0 | k=2
            // i:                     _
            // j:         _
            // O(N)

            int length = int.MinValue, countZeros = 0;
            for (int i = 0, j = 0; i < A.Length; i++)
            {
                // Outer loop, i increases window

                if (A[i] == 0)
                {
                    // Increment counter based on condition
                    countZeros++;
                }

                while (countZeros > K && j < A.Length)
                {
                    // Decrease window based on condition
                    if (A[j] == 0)
                    {
                        countZeros--;
                    }

                    j++;
                }

                length = System.Math.Max(length, i - j + 1);
            }

            if (length == int.MinValue)
            {
                return countZeros <= K ? A.Length : 0;
            }

            return length;
        }

        /*
            Given a string s consisting only of characters a, b and c.
            Return the number of substrings containing at least one occurrence of all these characters a, b and c.
         
            Input: s = "abcabc"
            Output: 10
            Explanation: The substrings containing at least one occurrence of the characters a, b and c are "abc", "abca", "abcab", "abcabc", "bca", "bcab", "bcabc", "cab", "cabc" and "abc" (again). 
         */
        public int NumberOfSubstrings(string s)
        {
            // "abcabc"
            // BF:
            // Walk throigh S from i=0 to s.Length - 3
            //  Walk throigh S from j=i+1 to s.Length
            // Time: O(N^2)
            //  _
            //    _

            // Sliding window:
            // Keep occurrences of chars:
            // "abcabc"

            var occurrences = new int[] { 0, 0, 0 };
            int numSubstrings = 0;
            for (int r = 0, l = 0; r < s.Length; r++)
            {
                // Outer loop, i increases window
                occurrences[s[r] - 'a']++; // increment counter (or hashmap)

                while (l < s.Length && occurrences[0] > 0 && occurrences[1] > 0 && occurrences[2] > 0)
                {
                    // decrease window
                    occurrences[s[l] - 'a']--;
                    l++;

                    // for(var k = 0; k < l; k++){
                    //     Console.WriteLine("############# l="+l + " k="+k + " r="+r);
                    //     Console.WriteLine(s.Substring(k, r + 1 - k));
                    // }             
                }

                numSubstrings += l;
            }

            return numSubstrings;
        }

        public int _3SumClosest(int[] nums, int target)
		{
			int min = int.MaxValue;
			int result = 0;

			Array.Sort(nums);

			for (int i = 0; i < nums.Length; i++)
			{
				int j = i + 1;
				int k = nums.Length - 1;
				while (j < k)
				{
					int sum = nums[i] + nums[j] + nums[k];
					int diff = System.Math.Abs(sum - target);

					if (diff == 0)
						return sum;

					if (diff < min)
					{
						min = diff;
						result = sum;
					}

					if (sum <= target)
						j++;
					else
						k--;
				}
			}

			return result;
		}
	}
}
