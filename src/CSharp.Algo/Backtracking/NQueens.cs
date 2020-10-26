using System.Collections.Generic;
using System.Linq;

namespace CSharp.DS.Algo.BinarySearch
{
    public class NQueens
    {
        public IList<IList<string>> SolveNQueens(int n)
        {
            // Test Case:
            // No two queens are on the same row, column, or diagonal.
            // Sol 1
            // . Q . .
            // . . . Q
            // Q . . .
            // . . Q .

            // Sol 2
            // . . Q .
            // Q . . .
            // . . . Q
            // . Q . .

            // BF. Generate all possible Q combinations and check they meet Queen properties.
            // Time O(n choose k))) where N = 64 and K=8 = N!

            // Q . . . 
            // . . Q .
            // Q . . .
            // . . Q .

            // Opt
            // . Q . .
            // . . . Q
            // Q . Q .
            // . . Q .

            // Row 1
            // Q . . .
            // . . Q Q
            // . Q . Q
            // . Q Q .

            var result = new List<IList<string>>();

            SolveNQueensBacktrack(n, 0, new List<int>(), ref result);

            return result;
        }

        public void SolveNQueensBacktrack(int n, int row, List<int> colPlacements, ref List<IList<string>> result)
        {
            if (row == n)
            {
                // Build output
                // E.g = first solution. [1,3,0,2]
                result.Add(BuildSolutionFromPlacement(colPlacements));
                return;
            }

            for (var i = 0; i < n; i++)
            {

                colPlacements.Add(i);
                
                // [0] // At the current row 0, we made a placement of 0. // [0,2] // At row 1 we made a colplacement of 2
                // Halved the space for next step

                // Q . . .
                // . . Q .
                // . . . .
                // . . . .

                if (IsValidPlacement(colPlacements))
                {
                    SolveNQueensBacktrack(n, row + 1, colPlacements, ref result);
                }

                colPlacements.RemoveAt(colPlacements.Count() - 1);
            }
        }

        public bool IsValidPlacement(List<int> colPlacements)
        {
            // horizontal
            // vertical // [0,1,1,1]
            // diagonal
            // [0,1,2,3]

            /*
          rowWeAreValidatingOn is the row that we just placed a queen on
          and we need to validate the placement
        */
            int rowWeAreValidatingOn = colPlacements.Count() - 1;

            /*
              Loop and check our placements in every row previous against
              the placement that we just made
            */
            for (int ithQueenRow = 0; ithQueenRow < rowWeAreValidatingOn; ithQueenRow++)
            {
                /*
                  Get the absolute difference between:

                  1.) The column of the already placed queen we are comparing against -> colPlacements.get(ithQueenRow)

                  2.) The column of the queen we just placed -> colPlacements.get(rowWeAreValidatingOn)
                */
                int absoluteColumnDistance = System.Math.Abs(
                  colPlacements.ElementAt(ithQueenRow) - colPlacements.ElementAt(rowWeAreValidatingOn)
                );

                /*
                  1.) absoluteColumnDistance == 0
                    If the absolute difference in columns is 0 then we placed in a column being
                    attacked by the i'th queen.

                  2.) absoluteColumnDistance == rowDistance
                    If the absolute difference in columns equals the distance in rows from the
                    i'th queen we placed, then the queen we just placed is attacked diagonally.

                  For Constraint #2 imagine this:

                  [
                    "--Q-",  <--- row 0 (Queen 1)
                    "Q---",  <--- row 1 (Queen 2)
                    "-Q--",  <--- row 2 (Queen 3)
                    "----"
                  ]

                  1.) 
                    Absolute Column Distance Between Queen 2 & 3 == 1.
                    Queen 2 is in col 0, Queen 3 is in col 1. 1 - 0 = 1.

                  2.)
                    Absolute Row Distance Between Queen 2 & 3 == 1
                    Queen 2 is in row 1, Queen 3 is in row 2. 2 - 1 = 1.
                */
                int rowDistance = rowWeAreValidatingOn - ithQueenRow;
                if (absoluteColumnDistance == 0 || absoluteColumnDistance == rowDistance)
                {
                    return false;
                }
            }

            return true;
        }

        public List<string> BuildSolutionFromPlacement(List<int> colPlacements)
        {

            var formattedPlacement = new List<string>();

            for (var i = 0; i < colPlacements.Count(); i++)
            {

                var row = "";
                var colPlacement = colPlacements[i];
                for (var j = 0; j < colPlacements.Count(); j++)
                {
                    row += colPlacement == j ? "Q" : ".";
                }

                formattedPlacement.Add(row);
            }

            return formattedPlacement;
        }
    }
}