using System.Collections.Generic;
using System.Linq;

namespace CSharp.DS.Algo.Combinatorics
{
    public partial class Combinatorics
    {
        /*
            Given two integers n and k, return all possible combinations of k numbers out of 1 ... n.
            You may return the answer in any order. Order does not matter in combination.
         
            Input: n = 4, k = 2
            Output:
            [
              [2,4],
              [3,4],
              [2,3],
              [1,2],
              [1,3],
              [1,4],
            ]
         
         */

        /// <summary>
        /// Generate all the possible combinations (aka subsequences) using a simple DFS + Backtracking
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static IList<IList<int>> Combine(int n, int k)
        {
            // Time: O(K*C_N^k) = O(N!/((N-K)!(K-1)!)) where C_N^k is the number of leaves of recursion tree and K is the depth of the tree
            // Space: O(K*C_N^k)

            var result = new List<IList<int>>();
            CombineRec(n, k, 1, new List<int>(), result);

            return result;
        }

        private static void CombineRec(int n, int k, int i, IList<int> comb, IList<IList<int>> result)
        {
            if (comb.Count() == k)
            {
                result.Add(new List<int>(comb));
                return;
            }
            
            for (var j = i; j <= n; j++)
            {
                comb.Add(j);
                CombineRec(n, k, j + 1, comb, result);

                // Backtrack
                comb.RemoveAt(comb.Count() - 1); // O(1)
            }
        }
    }
}
