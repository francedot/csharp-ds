using System;

namespace CSharp.DS.Algo.Games
{
    public partial class Games
    {
        public int[][] CandyCrush(int[][] board)
        {
            // Time: O(R * C^2), where R, C is the number of rows and columns in board
            // Space: O(1)
            while (IsReadyToCrash(board))
            {
                Crash(board);
                Gravity(board);
            }

            return board;
        }

        public bool IsReadyToCrash(int[][] board)
        {
            var isReadyToCrash = false;
            for (var i = 0; i < board.Length; i++)
            {
                for (var j = 0; j < board[i].Length - 2; j++)
                {
                    if (board[i][j] != 0 &&
                            System.Math.Abs(board[i][j]) == System.Math.Abs(board[i][j + 1]) &&
                                System.Math.Abs(board[i][j]) == System.Math.Abs(board[i][j + 2]))
                    {
                        isReadyToCrash = true;
                        var flag = -System.Math.Abs(board[i][j]);
                        board[i][j] = flag;
                        board[i][j + 1] = flag;
                        board[i][j + 2] = flag;
                    }
                }
            }

            for (var i = 0; i < board[0].Length; i++)
            {
                for (var j = 0; j < board.Length - 2; j++)
                {
                    if (board[j][i] != 0 &&
                            System.Math.Abs(board[j][i]) == System.Math.Abs(board[j + 1][i]) &&
                                System.Math.Abs(board[j][i]) == System.Math.Abs(board[j + 2][i]))
                    {
                        isReadyToCrash = true;
                        int flag = -System.Math.Abs(board[j][i]);
                        board[j][i] = flag;
                        board[j + 1][i] = flag;
                        board[j + 2][i] = flag;
                    }
                }
            }
            return isReadyToCrash;
        }

        public void Crash(int[][] board)
        {
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (board[i][j] < 0)
                        board[i][j] = 0;
                }
            }
        }

        public void Gravity(int[][] board)
        {
            for (int i = 0; i < board[0].Length; i++)
            {
                int k = board.Length - 1;
                for (int j = board.Length - 1; j >= 0; j--)
                {
                    if (board[j][i] != 0)
                    {
                        board[k][i] = board[j][i];
                        k--;
                    }
                }
                // Remaining upward or leftmost 0s
                while (k >= 0)
                    board[k--][i] = 0;
            }
        }
    }
}
