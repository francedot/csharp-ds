using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp.DS.Algo.intervals
{
    public partial class Intervals
    {
        public static int[][] FindFreeIntervals(int[][] intervals)
        {
            // Time: O(NLogN)
            // Space: O(1)
            // Visualization: https://i.imgur.com/9LjvVCA.jpg

            var result = new List<int[]>();
            if (intervals == null || !intervals.Any())
                return result.ToArray();

            Array.Sort(intervals, new StartTimeComparer());

            if (intervals[0][0] > 0)
                result.Add(new int[2] { 0, intervals[0][0] });

            int[] currentInterval = intervals[0];
            for (var i = 1; i < intervals.Length; i++)
            {
                if (intervals[i][0] > currentInterval[1])
                {
                    // No overlap - some free time available.
                    result.Add(new int[2] { currentInterval[1], intervals[i][0] });

                    // Move to the next interval
                    currentInterval = intervals[i];
                }
                else
                {
                    // Overlap intervals. Check if we can extend the end of the current interval.
                    if (intervals[i][1] > currentInterval[1])
                    {
                        currentInterval[1] = intervals[i][1];
                    }
                }
            }

            // Unary prefix "hat" operator
            //We call this the index from end operator. The predefined index from end operators are as follows:
            //var lastItem = array[^1];    // array[new Index(1, fromEnd: true)]

            if (intervals[^1][1] < 24)
                result.Add(new int[2] { intervals[^1][1], 24 });

            return result.ToArray();
        }

        // Definition for an Interval.
        public class Interval
        {
            public int start;
            public int end;

            public Interval()
            {
            }
            
            public Interval(int _start, int _end) {
                start = _start;
                end = _end;
            }
        }

        public IList<Interval> EmployeeFreeTime(IList<IList<Interval>> schedule)
        {
            // Time: O(KN) + O(KN) = O(KN) where KN is the length of the flatted list
            // Space: O(KN) for storing the flatted list
            var result = new List<Interval>();
            if (schedule == null || !schedule.Any())
                return result;

            // K-Way merge for sorted Lists: O(K*N)
            var intervals = Sorting.Sorting.MergeKSortedLists(
                schedule, (e1, e2) => e1.start.CompareTo(e2.start));
            // var intervals = schedule.SelectMany(i => i).OrderBy(i => i.start).ToArray(); O(KNLogKN)

            var currentInterval = intervals[0];
            for (var i = 1; i < intervals.Count(); i++)
            {
                if (intervals[i].start > currentInterval.end)
                {
                    // No overlap - some free time available.
                    result.Add(new Interval(currentInterval.end, intervals[i].start));

                    // Move to the next interval
                    currentInterval = intervals[i];
                }
                else
                {
                    // Overlap intervals. Check if we can extend the end of the current interval.
                    if (intervals[i].end > currentInterval.end)
                    {
                        currentInterval.end = intervals[i].end;
                    }
                }
            }

            return result.ToArray();
        }
    }
}
