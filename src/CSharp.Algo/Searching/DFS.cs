using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.DS.Algo.DFS
{
    public partial class DFS
    {
        /*
         Input:
             [[0,0,1,0,0,0,0,1,0,0,0,0,0],
             [0,0,0,0,0,0,0,1,1,1,0,0,0],
             [0,1,1,0,1,0,0,0,0,0,0,0,0],
             [0,1,0,0,1,1,0,0,1,0,1,0,0],
             [0,1,0,0,1,1,0,0,1,1,1,0,0],
             [0,0,0,0,0,0,0,0,0,0,1,0,0],
             [0,0,0,0,0,0,0,1,1,1,0,0,0],
             [0,0,0,0,0,0,0,1,1,0,0,0,0]]
        Output: 6
         */
        public int MaxAreaOfIslandRecursive(int[][] grid)
        {
            // Time: O(ROWS*Columns)
            // Space: O(ROWS*Columns)
            
            // Recursive DFS

            // Goal max area
            // Constraints: 4 directionally
            // Params: indexes, visited

            var visited = new bool[grid.Length][];
            for (var i = 0; i < grid.Length; i++)
                visited[i] = new bool[grid[0].Length];

            var maxArea = 0;
            for (var i = 0; i < grid.Length; i++)
                for (var j = 0; j < grid[0].Length; j++)
                    maxArea = System.Math.Max(maxArea, MaxAreaOfIslandDFSRec(grid, i, j, visited));

            return maxArea;
        }

        public int MaxAreaOfIslandDFSRec(int[][] grid, int i, int j, bool[][] visited)
        {
            if (i < 0 || j < 0 || i >= grid.Length || j >= grid[0].Length || visited[i][j] || grid[i][j] == 0)
            {
                return 0;
            }
            visited[i][j] = true;

            var curArea = MaxAreaOfIslandDFSRec(grid, i - 1, j, visited);
            curArea += MaxAreaOfIslandDFSRec(grid, i, j - 1, visited);
            curArea += MaxAreaOfIslandDFSRec(grid, i + 1, j, visited);
            curArea += MaxAreaOfIslandDFSRec(grid, i, j + 1, visited);

            return 1 + curArea;
        }

        public int MaxAreaOfIslandIterative(int[][] grid)
        {
            // Time: O(ROWS*Columns)
            // Space: O(ROWS*Columns)

            // Recursive DFS

            // Goal max area
            // Constraints: 4 directionally
            // Params: indexes, visited

            var visited = new bool[grid.Length][];
            for (var i = 0; i < grid.Length; i++)
                visited[i] = new bool[grid[0].Length];

            int[] directionsRow = new int[] { 1, -1, 0, 0 };
            int[] directionColumn = new int[] { 0, 0, 1, -1 };
            var maxArea = 0;
            for (var i = 0; i < grid.Length; i++)
            {
                for (var j = 0; j < grid[0].Length; j++)
                {
                    if (grid[i][j] == 1 && !visited[i][j])
                    {
                        int shape = 0;
                        var stack = new Stack<int[]>();
                        stack.Push(new int[] { i, j });
                        visited[i][j] = true;
                        while (stack.Any()) // Explore 4-directionally
                        {
                            var node = stack.Pop();
                            int r = node[0], c = node[1];
                            shape++;
                            for (int k = 0; k < 4; k++)
                            {
                                int nr = r + directionsRow[k];
                                int nc = c + directionColumn[k];
                                if (0 <= nr && nr < grid.Length &&
                                        0 <= nc && nc < grid[0].Length &&
                                        grid[nr][nc] == 1 && !visited[nr][nc])
                                {
                                    stack.Push(new int[] { nr, nc });
                                    visited[nr][nc] = true;
                                }
                            }
                        }
                        maxArea = System.Math.Max(maxArea, shape);
                    }
                }
            }
            return maxArea;
        }
    }
}
