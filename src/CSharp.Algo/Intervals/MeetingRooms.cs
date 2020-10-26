using CSharp.DS.Heap;
using System;
using System.Collections;
using System.Linq;

namespace CSharp.DS.Algo.intervals
{
    public partial class Intervals
    {
        /*
            Meeting Rooms I
            Given an array of meeting time intervals consisting of start and end times
            [[s1,e1],[s2,e2],...] (si < ei), determine if a person could attend all meetings.
         
            Input: [[0,30],[5,10],[15,20]]
            Output: false
         */

        public bool CanAttendMeetings(int[][] intervals)
        {
            // Brute Force. Time: O(N^2)
            // Compare every two meetings in the array,
            // and see if they conflict with each other(i.e. if they overlap).
            // Two meetings overlap if one of them starts while the other is still taking place.

            // Optimal. Time: O(NLogN) | worst case O(N^2) if quicksort

            if (intervals == null || intervals.Length == 0)
                return true;

            Array.Sort(intervals, new EndTimeComparer());

            var currentInterval = intervals[0];
            for (var i = 1; i < intervals.Length; i++)
            {
                // If the next interval starts before the end of the current interval
                if (intervals[i][0] < currentInterval[1])
                    return false;
                else currentInterval = intervals[i]; // [8,10] // Expand the interval
            }

            return true;
        }

        public class EndTimeComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                var end = ((int[])x)[1] - ((int[])y)[1];
                return end != 0 ? end : ((int[])x)[0] - ((int[])y)[0];
            }
        }

        /*
            Meeting Rooms II
            Given an array of meeting time intervals consisting of start and
            end times [[s1,e1],[s2,e2],...] (si < ei), find the minimum
            number of conference rooms required.

            Example 1:

            Input: [[0, 30],[5, 10],[15, 20]]
            Output: 2
         */

        public static int MinMeetingRooms(int[][] intervals)
        {
            // TC (1, 10), (2, 7), (3, 19), (8, 12), (10, 20), (11, 30)

            // If there is no meeting to schedule then no room needs to be allocated.
            if (intervals == null || !intervals.Any())
                return 0;

            // Sort the meetings in increasing order of their start time.
            Array.Sort(intervals, new StartTimeComparer());

            var availableRoomsHeap = new MinHeap<int>();
            // Add the first meeting. We have to give a new room to the first meeting.
            availableRoomsHeap.Push(intervals[0][1]);

            for (var i = 1; i < intervals.Length; i++)
            {
                // If the room due to free up the earliest is free, assign that room to this meeting.
                if (availableRoomsHeap.Peek() <= intervals[i][0])
                    availableRoomsHeap.Pop();

                // If a new room is to be assigned, then also we add to the heap,
                // If an old room is allocated, then also we have to add to the heap with updated end time.
                availableRoomsHeap.Push(intervals[i][1]);
            }

            // The size of the heap tells us the minimum rooms required for all the meetings.
            return availableRoomsHeap.Count();
        }

        public class StartTimeComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                return ((int[])x)[0] - ((int[])y)[0];
            }
        }
    }
}
