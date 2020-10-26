using System.Linq;

namespace CSharp.DS.Algo.DP
{
    public partial class DynamicProgramming
    {
        /*
            Plain Recursive
            Time Complexity O(N^2)
            Space Complexity O(N)

            Recurrence: T(N) = T(N-1) + T(N-2)
        */
        public int FibRec(int n)
        {
            if (n <= 1)
                return n;

            return FibRec(n - 1) + FibRec(n - 2);
        }

        /*
            Top-down using memoization
            Time Complexity O(N)
            Space Complexity O(N)
        */
        private int[] memoFib;
        public int FibTopDownMemo(int N)
        {
            if (N <= 1)
                return N;

            memoFib = Enumerable.Repeat(-1, N + 1).ToArray();

            memoFib[0] = 0;
            memoFib[1] = 1;

            return FibTopDownRecMemo(N);
        }

        public int FibTopDownRecMemo(int N)
        {
            if (memoFib[N] != -1)
                return memoFib[N];

            return memoFib[N] = FibTopDownRecMemo(N - 1) + FibTopDownRecMemo(N - 2);
        }

        /*
            Bottom-up using tabulation
            Time Complexity O(N)
            Space Complexity O(N)
        */
        public int FibBottomUp(int N)
        {
            if (N <= 1)
                return N;

            // Tabulation
            int[] dp = new int[N + 1];
            dp[1] = 1;

            for (int i = 2; i <= N; i++)
                dp[i] = dp[i - 1] + dp[i - 2];

            return dp[N];
        }

        /*
            Bottom-up without tabulation
            Time Complexity O(N)
            Space Complexity O(1)
        */
        public int FibBottomUpConstant(int N)
        {
            if (N <= 1)
                return N;

            if (N == 2)
                return 1;

            int current = 0;
            int prev1 = 1;
            int prev2 = 1;

            for (int i = 3; i <= N; i++)
            {
                current = prev1 + prev2;
                prev2 = prev1;
                prev1 = current;
            }
            return current;
        }
    }
}
