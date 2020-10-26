using System.Collections.Generic;
using System.Linq;

namespace CSharp.DS.Tree.Binary
{
    public class BinaryTree<T>
    {
        public BinaryTreeNode<T> root;

        /*
           Reference Tree:
                      9
                    /   \
                   3     4
                  / \   / \
                 1   2 8   7
                            \
                             6        
        */

        /*
            Given a binary tree, determine if it is height-balanced.
            For this problem, a height-balanced binary tree is defined as:
            A binary tree in which the left and right subtrees of
            every node differ in height by no more than 1.
            height(node.left) - height(node.right) <= 1
         */

        public bool IsBalanced(BinaryTreeNode<T> root)
        {
            // Time: O(N)
            // Space: Best case if balanced O(H), O(N) if unbalanced
            return IsBalancedRec(root) != -1;
        }

        public int IsBalancedRec(BinaryTreeNode<T> node)
        {
            if (node == null)
                return 0;

            var leftHeight = IsBalancedRec(node.left);
            var rightHeight = IsBalancedRec(node.right);

            if (leftHeight == -1 || rightHeight == -1)
                return -1;

            // Console.WriteLine($"node:{node.val}, leftHeight:{leftHeight}, rightHeight:{rightHeight}");

            // Parent node: checks property of Balanced tree
            if (System.Math.Abs(leftHeight - rightHeight) > 1)
                return -1;

            return 1 + System.Math.Max(leftHeight, rightHeight);
        }

        /// <summary>
        /// Visit current, left, right.
        /// </summary>
        /// <example>[9,3,1,2,4,8,7,6]</example>
        /// <param name="node"></param>
        /// <param name="result"></param>
        public void PreorderTraversalRec(BinaryTreeNode<T> node, List<T> result)
        {
            if (node == null)
                return;

            result.Add(node.val); // Do work
            PreorderTraversalRec(node.left, result);
            PreorderTraversalRec(node.right, result);
        }

        /// <summary>
        /// Visit current, left, right.
        /// Iterative Implementation based on stack.
        /// </summary>
        /// <example>[9,3,1,2,4,8,7,6]</example>
        /// <param name="node"></param>
        /// <param name="result"></param>
        public IList<T> PreorderTraversalIt(BinaryTreeNode<T> root)
        {
            var result = new List<T>();

            if (root == null)
                return result;

            var preorderStack = new Stack<BinaryTreeNode<T>>();
            preorderStack.Push(root);

            while (preorderStack.Any())
            {
                var node = preorderStack.Pop();
                result.Add(node.val); // Visit

                if (node.right != null)
                    preorderStack.Push(node.right);
                if (node.left != null)
                    preorderStack.Push(node.left);
            }

            return result;
        }

        /// <summary>
        /// Visit left, current, right.
        /// </summary>
        /// <example>[1,3,2,9,8,4,6,7]</example>
        /// <param name="node"></param>
        /// <param name="result"></param>
        public void InorderTraversalRec(BinaryTreeNode<T> node, List<T> result)
        {
            if (node == null)
                return;

            InorderTraversalRec(node.left, result);
            result.Add(node.val); // Do work
            InorderTraversalRec(node.right, result);
        }

        /// <summary>
        /// Visit left, current, right.
        /// Iterative Implementation based on stack.
        /// </summary>
        /// <example>[1,3,2,9,8,4,6,7]</example>
        /// <param name="node"></param>
        /// <param name="result"></param>
        public IList<T> InorderTraversalIt(BinaryTreeNode<T> root)
        {
            var result = new List<T>();

            if (root == null)
                return result;

            var stack = new Stack<BinaryTreeNode<T>>();
            var curNode = root;
            // Loop until either:
            // there are elements in the stack (parent nodes)
            // curNode is not null (holds right node and init node)
            while (curNode != null || stack.Any())
            {
                // Go left
                while (curNode != null)
                {
                    stack.Push(curNode);
                    curNode = curNode.left; // until leaf node
                }
                // Popping in 3 cases:
                // Leftmost node
                // A parent node climbing up the tree
                // A right node
                curNode = stack.Pop();

                // Visit
                result.Add(curNode.val);

                // Explore right subtree of this leftmost 
                curNode = curNode.right;
            }

            return result;
        }

        /// <summary>
        /// Visit left, right, current.
        /// </summary>
        /// <example>[1,2,3,8,6,7,4,9]</example>
        /// <param name="node"></param>
        /// <param name="result"></param>
        public void PostorderTraversalRec(BinaryTreeNode<T> node, List<T> result)
        {
            if (node == null)
                return;

            PostorderTraversalRec(node.left, result);
            PostorderTraversalRec(node.right, result);
            result.Add(node.val); // Do work
        }

        /// <summary>
        /// Visit left, right, current.
        /// Iterative implementation based on 2 stacks.
        /// </summary>
        /// <example>[1,2,3,8,6,7,4,9]</example>
        /// <param name="node"></param>
        /// <param name="result"></param>
        public IList<T> PostorderTraversalIt(BinaryTreeNode<T> root)
        {
            if (root == null)
                return new List<T>();

            var s1 = new Stack<BinaryTreeNode<T>>();
            var s2 = new Stack<BinaryTreeNode<T>>();

            s1.Push(root);

            while (s1.Any())
            {
                var curNode = s1.Pop();

                s2.Push(curNode); // [9,4,7,6,8,3,2,1]

                if (curNode.left != null)
                    s1.Push(curNode.left);

                if (curNode.right != null)
                    s1.Push(curNode.right);
            }

            return s2.Select(n => n.val).ToList();
        }

        /// <summary>
        /// Level Order Traversal.
        /// </summary>
        /// <example>[9,3,4,1,2,8,7,6]</example>
        /// <param name="node"></param>
        /// <param name="level"></param>
        /// <param name="levelToNodesDict"></param>
        public void LevelOrderTraversalRec(BinaryTreeNode<T> node, int level, Dictionary<int, IList<T>> levelToNodesDict)
        {
            if (node == null)
                return;

            if (!levelToNodesDict.ContainsKey(level))
                levelToNodesDict.Add(level, new List<T>());
            levelToNodesDict[level].Add(node.val);

            LevelOrderTraversalRec(node.left, level + 1, levelToNodesDict);
            LevelOrderTraversalRec(node.right, level + 1, levelToNodesDict);
        }

        /// <summary>
        /// Level Order Traversal.
        /// Iterative implementation based on Queue.
        /// </summary>
        /// <example>[9,3,4,1,2,8,7,6]</example>
        /// <param name="node"></param>
        public IList<IList<T>> LevelOrderTraversalIt(BinaryTreeNode<T> node)
        {
            var result = new List<IList<T>>();

            if (node == null)
                return result;

            var queue = new Queue<BinaryTreeNode<T>>();
            queue.Enqueue(node);

            while (queue.Any())
            {
                var levelSize = queue.Count();
                var levelList = new List<T>();

                for (var i = 0; i < levelSize; i++)
                {
                    var curNode = queue.Dequeue();
                    levelList.Add(curNode.val);

                    var left = curNode.left;
                    var right = curNode.right;

                    if (left != null)
                        queue.Enqueue(left);

                    if (right != null)
                        queue.Enqueue(right);
                }

                result.Add(levelList);
            }

            return result;
        }

        /// <summary>
        /// Level order traversal with change of direction at each level
        /// Using 2 stacks for change of direction.
        /// </summary>
        /// <example>[9,4,3,1,2,8,7,6]</example>
        /// <param name="root"></param>
        /// <returns></returns>
        public IList<IList<T>> ZigzagLevelOrderTraversal(BinaryTreeNode<T> root)
        {
            var result = new List<IList<T>>();

            if (root == null)
                return result;

            var curLevelStack = new Stack<BinaryTreeNode<T>>();
            var nextLevelStack = new Stack<BinaryTreeNode<T>>();

            curLevelStack.Push(root);

            var leftToRight = true;
            while (curLevelStack.Any())
            {
                var levelSize = curLevelStack.Count();
                var levelList = new List<T>();

                for (var i = 0; i < levelSize; i++)
                {
                    var curNode = curLevelStack.Pop();
                    levelList.Add(curNode.val); // 9, 4, 3 | 1,2,8,7 | 

                    if (leftToRight)
                    {
                        if (curNode.left != null)
                            nextLevelStack.Push(curNode.left);

                        if (curNode.right != null)
                            nextLevelStack.Push(curNode.right); // 4, 3 | 6
                    }
                    else
                    {
                        if (curNode.right != null)
                            nextLevelStack.Push(curNode.right);

                        if (curNode.left != null)
                            nextLevelStack.Push(curNode.left); // 1,2,8,7
                    }
                }

                result.Add(levelList);

                // Swap stacks
                var tmpStack = curLevelStack;
                curLevelStack = nextLevelStack;
                nextLevelStack = tmpStack;

                // Invert add
                leftToRight = !leftToRight;
            }

            return result;
        }

        /// <summary>
        /// Vertical order traversal from top to bottom, column by column.
        /// If two nodes are in the same row and column, the order should be from left to right.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IList<IList<T>> VerticalTraversal(BinaryTreeNode<T> root)
        {
            var xToNodesDictionary = new SortedDictionary<int, SortedDictionary<int, SortedSet<T>>>();

            VerticalTraversalRec(root, 0, 0, xToNodesDictionary);

            return xToNodesDictionary.Keys.Select(
                    xKey => xToNodesDictionary[xKey].Values.SelectMany(
                        x => x).ToList() as IList<T>).ToList();
        }

        public void VerticalTraversalRec(BinaryTreeNode<T> node, int x, int y, SortedDictionary<int, SortedDictionary<int, SortedSet<T>>> xToNodesDictionary)
        {
            if (node == null)
                return;

            VerticalTraversalRec(node.left, x - 1, y + 1, xToNodesDictionary);

            // Do Inorder work
            // Add this node to the nodes with key x
            if (!xToNodesDictionary.ContainsKey(x))
            {
                xToNodesDictionary.Add(x, new SortedDictionary<int, SortedSet<T>>());
            }
            var yToNodesDictionary = xToNodesDictionary[x];

            // Add this node to the nodes with key y
            if (!yToNodesDictionary.ContainsKey(y))
            {
                yToNodesDictionary.Add(y, new SortedSet<T>());
            }
            yToNodesDictionary[y].Add(node.val);

            VerticalTraversalRec(node.right, x + 1, y + 1, xToNodesDictionary);
        }

        /*
            Given a binary tree, find the lowest common ancestor (LCA)
            of two given nodes in the tree.
            According to the definition of LCA on Wikipedia:
            “The lowest common ancestor is defined between two nodes
             p and q as the lowest node in T that has both p and q
            as descendants (where we allow a node to be a descendant of itself).”
            
            Input: p=5, q=4

             root:   3
                  /     \
                 5       1
                / \     / \
               6   2   0   8
                  / \
                 7   4
            

            Output: 5
         */

        public BinaryTreeNode<T> LowestCommonAncestor(BinaryTreeNode<T> root, BinaryTreeNode<T> p, BinaryTreeNode<T> q)
        {
            // Time: O(N)
            // Space: O(N)
            var lca = default(BinaryTreeNode<T>);
            LCARec(root, p, q, ref lca);

            return lca;
        }

        private bool LCARec(BinaryTreeNode<T> node, BinaryTreeNode<T> p, BinaryTreeNode<T> q, ref BinaryTreeNode<T> lca)
        {
            if (node == null)
                return false;

            var foundPQ = node == p || node == q;

            var foundPQLeft = LCARec(node.left, p, q, ref lca);
            var foundPQRight = LCARec(node.right, p, q, ref lca);

            var foundLCA = foundPQLeft && foundPQRight || foundPQLeft && foundPQ || foundPQRight && foundPQ;
            if (lca == null && foundLCA) // make sure to return only once
                lca = node;

            return foundPQ || foundPQLeft || foundPQRight; // doesn't matter which one has been found
        }
    }
}
