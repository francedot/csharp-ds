using System;

namespace CSharp.DS.Algo.intervals
{
    public partial class Intervals
    {
        public int EraseOverlapIntervals(int[][] intervals)
        {
            if (intervals.Length == 0)
                return 0;

            // 3 Cases: https://i.imgur.com/w9YqdoE.jpg
            // If two intervals are overlapping,
            // we want to remove the interval that has the longer end point
            // The longer interval will always overlap with more or
            // the same number of future intervals compared to the shorter one

            Array.Sort(intervals, new StartTimeComparer());
            int prev = 0, count = 0;
            for (int i = 1; i < intervals.Length; i++)
            {
                if (intervals[prev][1] > intervals[i][0])
                {
                    if (intervals[prev][1] > intervals[i][1])
                    {
                        // In this case, we can simply take the later interval
                        // (therefore remove the current)
                        // The choice is obvious since choosing an interval of smaller
                        // width will lead to more available space labelled as A and B,
                        // in which more intervals can be accommodated. 
                        prev = i;
                    }
                    // else stick with the current one and skip the later https://i.imgur.com/88Y3Mzt.jpg
                    count++;
                }
                else
                {
                    prev = i;
                }
            }
            return count;
        }
    }
}
