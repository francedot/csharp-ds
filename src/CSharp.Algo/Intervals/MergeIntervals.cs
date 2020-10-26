using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp.DS.Algo.intervals
{
    public partial class Intervals
    {
        /*
            Given a collection of intervals, merge all overlapping intervals.

            Example 1:

            Input: intervals = [[1,3],[2,6],[8,10],[15,18]]
            Output: [[1,6],[8,10],[15,18]]
            Explanation: Since intervals [1,3] and [2,6] overlaps, merge them into [1,6].
         */

        public int[][] MergeIntervals(int[][] intervals)
        {
            // Time: O(NLogN)
            // Space: O(1)

            var result = new List<int[]>();
            if (intervals == null || !intervals.Any())
                return result.ToArray();

            Array.Sort(intervals, new StartTimeComparer());

            var currentInterval = intervals[0];
            result.Add(currentInterval);
            for (var i = 1; i < intervals.Length; i++)
            {
                if (intervals[i][0] <= currentInterval[1])
                {
                    // Overlap
                    if (intervals[i][1] > currentInterval[1])
                    {
                        // Extend the Interval
                        currentInterval[1] = intervals[i][1]; // [1,6] // [8,18]
                    }
                }
                else
                {
                    // No overlap. Move to the next interval // 8 > 6
                    currentInterval = intervals[i]; // [8,10]
                    result.Add(currentInterval);
                }
            }

            return result.ToArray();
        }
    }
}
