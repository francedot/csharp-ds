using System.Collections.Generic;

namespace CSharp.DS.Algo.DFS
{
    public partial class BFS
    {
        public int OrangesRotting(int[][] grid)
        {
            var bfsQueue = new Queue<int[]>();
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] == 2)
                        bfsQueue.Enqueue(new int[] { i, j });
                }
            }

            var directions = new int[4][];
            directions[0] = new int[] { 0, 1 };
            directions[1] = new int[] { 0, -1 };
            directions[2] = new int[] { 1, 0 };
            directions[3] = new int[] { -1, 0 };

            int mins = 0;
            while (bfsQueue.Count > 0)
            {
                int count = bfsQueue.Count;
                for (int i = 0; i < count; ++i)
                {
                    var cell = bfsQueue.Dequeue();
                    foreach (var direction in directions)
                    {
                        int row = cell[0] + direction[0];
                        int col = cell[1] + direction[1];
                        if (row >= 0 && col >= 0 && row < grid.Length && col < grid[0].Length && grid[row][col] == 1)
                        {
                            bfsQueue.Enqueue(new int[] { row, col }); // Enqueue for next minute
                            grid[row][col] = 2; // Set to rotten
                        }
                    }
                }
                if (bfsQueue.Count > 0)
                    mins++;
            }

            for (int i = 0; i < grid.Length; ++i)
            {
                for (int j = 0; j < grid[i].Length; ++j)
                {
                    if (grid[i][j] == 1) // Any orange left out
                        return -1;
                }
            }

            return mins;
        }
    }
}
