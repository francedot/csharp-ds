using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.DS.Core.Tree
{
    public class BinaryTree
    {
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int val)
            {
                this.val = val;
            }
        }

        // Reference Graph
        //          9
        //        /   \
        //       3     4
        //      / \   / \
        //     1   2 8   7
        //                \
        //                 6        

        /// <summary>
        /// Visit current, left, right.
        /// </summary>
        /// <example>[9,3,1,2,4,8,7,6]</example>
        /// <param name="node"></param>
        /// <param name="result"></param>
        public void PreorderTraversalRec(TreeNode node, List<int> result)
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
        public IList<int> PreorderTraversalIt(TreeNode root)
        {
            var result = new List<int>();

            if (root == null)
                return result;

            var preorderStack = new Stack<TreeNode>();
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
        public void InorderTraversalRec(TreeNode node, List<int> result)
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
        public IList<int> InorderTraversalIt(TreeNode root)
        {
            var result = new List<int>();

            if (root == null)
                return result;

            var stack = new Stack<TreeNode>();
            stack.Push(root);

            while (stack.Any())
            {
                var curNode = stack.Pop();

                // Find leftmost node
                while (curNode.left != null) // until leaf node
                {
                    stack.Push(curNode);
                    curNode = curNode.left;
                }

                result.Add(curNode.val); // Visit
                if (curNode.right != null)
                {
                    // if it has right node, start exploration of left subtree
                    stack.Push(curNode.right);
                }
                else if (stack.Any()) // Else use the one of parent
                {
                    var parentNode = stack.Pop();
                    result.Add(parentNode.val);

                    while (stack.Any() && parentNode.right == null)
                    {
                        parentNode = stack.Pop();
                        result.Add(parentNode.val);
                    }

                    if (parentNode.right != null)
                        stack.Push(parentNode.right);
                }
            }

            return result;
        }

        // [1,2,3,8,6,7,4,9] post-order traversal
        // [9,4,7,6,8,3,2,1] inorder mirrored
        // Inorder traversal but mirrored and starting with rightmost

        // Post-order traversal 2 stacks

        /// <summary>
        /// Visit left, right, current.
        /// </summary>
        /// <example>[1,2,3,8,6,7,4,9]</example>
        /// <param name="node"></param>
        /// <param name="result"></param>
        public void PostorderTraversalRec(TreeNode node, List<int> result)
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
        public IList<int> PostorderTraversalIt(TreeNode root)
        {
            if (root == null)
                return new List<int>();

            var s1 = new Stack<TreeNode>();
            var s2 = new Stack<TreeNode>();

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
        // [9,3,4,1,2,8,7,6] level-order traversal
        // Use a queue and at each level collect L nodes where L is the level. L=1 for root
        // Using a queue instead of a stack guarantees that we visit the neighbors first
        // Level-order traversal (BFS)

        /// <summary>
        /// Level Order Traversal.
        /// </summary>
        /// <example>[9,3,4,1,2,8,7,6]</example>
        /// <param name="node"></param>
        /// <param name="level"></param>
        /// <param name="levelToNodesDict"></param>
        public void LevelOrderRec(TreeNode node, int level, Dictionary<int, IList<int>> levelToNodesDict)
        {
            if (node == null)
                return;

            if (!levelToNodesDict.ContainsKey(level))
            {
                levelToNodesDict.Add(level, new List<int> { node.val });
            }
            else
            {
                levelToNodesDict[level].Add(node.val);
            }

            LevelOrderRec(node.left, level + 1, levelToNodesDict);
            LevelOrderRec(node.right, level + 1, levelToNodesDict);
        }

        /// <summary>
        /// Level Order Traversal.
        /// Iterative implementation based on Queue.
        /// </summary>
        /// <example>[9,3,4,1,2,8,7,6]</example>
        /// <param name="node"></param>
        /// <param name="level"></param>
        /// <param name="levelToNodesDict"></param>
        public IList<IList<int>> LevelOrderIt(TreeNode node)
        {
            var result = new List<IList<int>>();

            if (node == null)
                return result;

            var queue = new Queue<TreeNode>();
            queue.Enqueue(node);

            while (queue.Any())
            {
                var levelSize = queue.Count();
                var levelList = new List<int>();

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
        public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
        {
            var result = new List<IList<int>>();

            if (root == null)
                return result;

            var curLevelStack = new Stack<TreeNode>();
            var nextLevelStack = new Stack<TreeNode>();

            curLevelStack.Push(root);

            var leftToRight = true;
            while (curLevelStack.Any())
            {
                var levelSize = curLevelStack.Count();
                var levelList = new List<int>();

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
        public IList<IList<int>> VerticalTraversal(TreeNode root)
        {
            var xToNodesDictionary = new SortedDictionary<int, SortedDictionary<int, SortedSet<int>>>();

            VerticalTraversalRec(root, 0, 0, xToNodesDictionary);

            return xToNodesDictionary.Keys.Select(
                    xKey => xToNodesDictionary[xKey].Values.SelectMany(
                        x => x).ToList() as IList<int>).ToList();
        }

        public void VerticalTraversalRec(TreeNode node, int x, int y, SortedDictionary<int, SortedDictionary<int, SortedSet<int>>> xToNodesDictionary)
        {
            if (node == null)
                return;

            VerticalTraversalRec(node.left, x - 1, y + 1, xToNodesDictionary);

            // Do Inorder work
            // Add this node to the nodes with key x
            if (!xToNodesDictionary.ContainsKey(x))
            {
                xToNodesDictionary.Add(x, new SortedDictionary<int, SortedSet<int>>());
            }
            var yToNodesDictionary = xToNodesDictionary[x];

            // Add this node to the nodes with key y
            if (!yToNodesDictionary.ContainsKey(y))
            {
                yToNodesDictionary.Add(y, new SortedSet<int>());
            }
            yToNodesDictionary[y].Add(node.val);

            VerticalTraversalRec(node.right, x + 1, y + 1, xToNodesDictionary);
        }
    }
}
