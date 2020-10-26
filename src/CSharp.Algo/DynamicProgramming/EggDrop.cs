using System;

namespace CSharp.DS.Algo.DP
{
    partial class EggDrop
    {
        public int SuperEggDrop(int totalEggs, int totalFloors)
        {
            // Visualization: https://i.imgur.com/Mkaok0U.png
            return SuperEggDropRec(totalEggs, totalFloors);
        }

        private int SuperEggDropRec(int eggs, int floors)
        {
            /*
              Base Case 1:
              If we have 0 floors we need 0 trials, no matter the egg amount given
              If we have 1 floor we need 1 trial, no matter the egg amount given
              We just return totalFloors since it matches up to that logic
            */
            if (floors == 1 || floors == 0)
                return floors;

            /*
              Base Case 2:
              If I have 1 egg...I will have to be conservative and try every floor
              1 by 1 starting from floor 1 all the way up to totalFloors
              This entails 'totalFloors' drops as the BEST we can do to guarantee
              we find the pivotal floor
            */
            if (eggs == 1)
                return floors;

            /*
             Try all floors up to the floor we are working on. See the bottom explanation
             below for how the situations break down
             This does not change the asymptotic complexity of the algorithm
           */
            var minCost = int.MaxValue;
            for (var floor = 1; floor <= floors; floor++)
            {
                var costEggDropIfBreak = SuperEggDropRec(eggs - 1, floor - 1);
                var costEggDropNotBreak = SuperEggDropRec(eggs, floors - floor);
                var costOfWorstOutcome =
                    System.Math.Max(costEggDropIfBreak, costEggDropNotBreak);

                // Adding 1 simulates the actual drop for each subproblem
                minCost = System.Math.Min(minCost, 1 + costOfWorstOutcome);
            }

            return minCost;
        }

        int[,] memo;
        public int EggDropTopDown(int totalEggs, int totalFloors)
        {
            // Init Memo
            memo = new int[totalEggs + 1, totalFloors + 1];
            for (int eggs = 2; eggs <= totalEggs; eggs++)
                for (int floor = 2; floor <= totalFloors; floor++)
                    memo[eggs, floor] = int.MaxValue;

            // Visualization: https://i.imgur.com/Mkaok0U.png
            return EggDropTopDownRec(totalEggs, totalFloors);
        }

        private int EggDropTopDownRec(int eggs, int floors)
        {
            /*
              Base Case 1:
              If we have 0 floors we need 0 trials, no matter the egg amount given
              If we have 1 floor we need 1 trial, no matter the egg amount given
              We just return totalFloors since it matches up to that logic
            */
            if (floors == 1 || floors == 0)
                return floors;

            /*
              Base Case 2:
              If I have 1 egg...I will have to be conservative and try every floor
              1 by 1 starting from floor 1 all the way up to totalFloors
              This entails 'totalFloors' drops as the BEST we can do to guarantee
              we find the pivotal floor
            */
            if (eggs == 1)
                return floors;

            // If we already know the answer to this subproblem
            if (memo[eggs, floors] != int.MaxValue)
                return memo[eggs, floors];

            /*
             Try all floors up to the floor we are working on. See the bottom explanation
             below for how the situations break down
             This does not change the asymptotic complexity of the algorithm
           */
            for (var floor = 1; floor <= floors; floor++)
            {
                var costEggDropIfBreak = EggDropTopDownRec(eggs - 1, floor - 1);
                var costEggDropNotBreak = EggDropTopDownRec(eggs, floors - floor);
                var costOfWorstOutcome =
                    System.Math.Max(costEggDropIfBreak, costEggDropNotBreak);

                // Adding 1 simulates the actual drop for each subproblem
                memo[eggs, floors] = System.Math.Min(memo[eggs, floors], 1 + costOfWorstOutcome);
            }

            return memo[eggs, floors];
        }

        public int EggDropBottomUp(int totalEggs, int totalFloors)
        {
            // Time: O(F*E) subproblems (upperbound) * O(F) (computation at each subproblems)
            // O(E*F^2)
            // Space: O(F*E)
            // Visualization: https://i.imgur.com/3qkSVfL.png

            // Recurrence: dp[K][N] = 1 + max(dp[K - 1][i - 1], dp[K][N - i])

            /*
             We do +1 to index off of 1. So that the final answer that
             we want will be at cache[totalEggs][totalFloors]...remember
             we index off of 0 so this is for convenience
             cache[totalEggs][totalFloors] is literally the answer to the
             subproblem given those literal amounts...'totalEggs' and
             'totalFloors'
           */
            var dp = new int[totalEggs + 1, totalFloors + 1];

            /*
              If we have 0 floors we need 0 trials, no matter the egg amount given
              If we have 1 floor we need 1 trial, no matter the egg amount given
            */
            for (var eggs = 1; eggs <= totalEggs; eggs++)
            {
                dp[eggs, 0] = 0;
                dp[eggs, 1] = 1;
            }

            /*
              If we have 1 egg...no matter what floors we get, our approach will
              be to do 'floorAmount' drops...this is because we want to start from
              floor 1, drop...then go to floor 2, drop...and so on until we get to
              in the worst case 'floorAmount'
              Remember, we want to know the minimum amount of drops that will always
              work. So we want to MINIMIZE...to the absolute LEAST...worst...amount
              of drops so that our drop count ALWAYS works
              Any worse then the MINIMIZED WORST will be suboptimal
            */
            for (var floor = 1; floor <= totalFloors; floor++)
                dp[1, floor] = floor;

            /*
             Solve the rest of the subproblems now that we have base cases defined
             for us
           */
            for (int eggs = 2; eggs <= totalEggs; eggs++)
            {
                for (int floor = 2; floor <= totalFloors; floor++)
                {
                    /*
                      Initialize the answer to this subproblem to a very large
                      value that will be easily overtaken and provide a hard upper
                      comparison wall
                    */
                    dp[eggs, floor] = int.MaxValue;

                    /*
                        As further optimization, we should check for the unnecessary iteration in the for-loops.
                        More specifically, to get the k that best fits each drop,
                        we don't need to go over all floors from 1 to j.
                        As for a fixed k, dp[i][k] goes up as k increases.
                        This means dp[i-1][k-1] will increase and dp[i][j-k] will decrease as k goes from 1 to j.
                        The optimal value of k will be the middle point where the two meet.
                        So to get the optimal k value for dp[i][j], we can do a binary search for k from 1 to j.
                        Therefore cutting down inner work to O(LogF)
                     */
                    
                    /*
                      We do a theoretical test on every floor from 1 to the 'floor'
                      amount for this subproblem.
                      At each 'attemptFloor' we express both possibilities described below
                    */
                    for (int attemptFloor = 1; attemptFloor <= floor; attemptFloor++)
                    {
                        /*
                          We want to know the cost of the 2 outcomes:
                          1.) We drop an egg and it breaks.
                            We move 1 floor down. We have 1 less egg.
                          2.) We drop an egg and it doesn't break.
                            We have this many floors left: the difference between the total floors and our current
                            floor. We have the same number of eggs.
                        */

                        var costEggDropIfBreak = dp[eggs - 1, attemptFloor - 1];
                        var costEggDropNotBreak = dp[eggs, floor - attemptFloor];
                        var costOfWorstOutcome =
                            System.Math.Max(costEggDropIfBreak, costEggDropNotBreak);

                        /*
                          After we get the cost of the WORST outcome we add 1 to that worst outcome
                          to simulate the fact that we are going to do a test from THIS subproblem.
                          The answer to this problem is 1 PLUS the cost of the WORST SITUATION that
                          happens after our action at this subproblem.
                        */

                        /*
                          Did we reach a BETTER (lower) amount of drops that guarantee that we can
                          find the pivotal floor where eggs begin to break?
                        */
                        dp[eggs, floor] = System.Math.Min(dp[eggs, floor], 1 + costOfWorstOutcome);
                    }
                }
            }
            /*
             Remember we added +1 so we are indexed off of 1 now. We can reap our answer from
             cache[totalEggs][totalFloors] instead of cache[totalEggs - 1][totalFloors - 1]
           */
            return dp[totalEggs, totalFloors];
        }

        public int EggDropBinarySearch(int eggs, int floors)
        {
            return BinarySearch(eggs, floors);
        }

        // Now we just do binary search to figure out the first such floor where we get this value of x
        private int BinarySearch(int eggs, int floors)
        {
            static bool condition(int eggs, int floors, int minNumberOfTries)
            {
                // Calculate the Binomial(eggs, floor) = 
                // Sum from j=i to 'eggs' of 'floors' over j
                int res = 1, sum = 0;
                for (int i = 1; i <= eggs && sum < floors; i++)
                {
                    res *= minNumberOfTries - i + 1;
                    res /= i;
                    sum += res;
                }
                return sum >= floors;
            }

            int left = 1, right = floors;
            while (left < right)
            {
                var mid = left + (right - left) / 2;
                if (condition(eggs, floors, mid))
                    right = mid;
                else left = mid + 1;
            }
            return left;
        }
    }
}
