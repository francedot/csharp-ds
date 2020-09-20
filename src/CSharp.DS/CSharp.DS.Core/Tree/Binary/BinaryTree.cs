﻿using System.Collections.Generic;
using System.Linq;

namespace CSharp.DS.Core.Tree.Binary
{
    public class BinaryTree
    {
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
        /// <summary>
        /// Visit current, left, right.
        /// </summary>
        /// <example>[9,3,1,2,4,8,7,6]</example>
        /// <param name="node"></param>
        /// <param name="result"></param>
        public void PreorderTraversalRec(BinaryTreeNode node, List<int> result)
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
        public IList<int> PreorderTraversalIt(BinaryTreeNode root)
        {
            var result = new List<int>();

            if (root == null)
                return result;

            var preorderStack = new Stack<BinaryTreeNode>();
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
        public void InorderTraversalRec(BinaryTreeNode node, List<int> result)
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
        public IList<int> InorderTraversalIt(BinaryTreeNode root)
        {
            var result = new List<int>();

            if (root == null)
                return result;

            var stack = new Stack<BinaryTreeNode>();
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

        /// <summary>
        /// Visit left, right, current.
        /// </summary>
        /// <example>[1,2,3,8,6,7,4,9]</example>
        /// <param name="node"></param>
        /// <param name="result"></param>
        public void PostorderTraversalRec(BinaryTreeNode node, List<int> result)
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
        public IList<int> PostorderTraversalIt(BinaryTreeNode root)
        {
            if (root == null)
                return new List<int>();

            var s1 = new Stack<BinaryTreeNode>();
            var s2 = new Stack<BinaryTreeNode>();

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
        public void LevelOrderTraversalRec(BinaryTreeNode node, int level, Dictionary<int, IList<int>> levelToNodesDict)
        {
            if (node == null)
                return;

            if (!levelToNodesDict.ContainsKey(level))
                levelToNodesDict.Add(level, new List<int>());
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
        public IList<IList<int>> LevelOrderTraversalIt(BinaryTreeNode node)
        {
            var result = new List<IList<int>>();

            if (node == null)
                return result;

            var queue = new Queue<BinaryTreeNode>();
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
        public IList<IList<int>> ZigzagLevelOrderTraversal(BinaryTreeNode root)
        {
            var result = new List<IList<int>>();

            if (root == null)
                return result;

            var curLevelStack = new Stack<BinaryTreeNode>();
            var nextLevelStack = new Stack<BinaryTreeNode>();

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
        public IList<IList<int>> VerticalTraversal(BinaryTreeNode root)
        {
            var xToNodesDictionary = new SortedDictionary<int, SortedDictionary<int, SortedSet<int>>>();

            VerticalTraversalRec(root, 0, 0, xToNodesDictionary);

            return xToNodesDictionary.Keys.Select(
                    xKey => xToNodesDictionary[xKey].Values.SelectMany(
                        x => x).ToList() as IList<int>).ToList();
        }

        public void VerticalTraversalRec(BinaryTreeNode node, int x, int y, SortedDictionary<int, SortedDictionary<int, SortedSet<int>>> xToNodesDictionary)
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

        /// <summary>
        /// Construct a Binary Search Tree from a sorted array
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static BinaryTreeNode FromSortedArray(int[] nums)
        {
            // BST properties
            // 1) For every subtree, node.left < node < node.right.
            // 2) Balanced: depth of the two subtrees of every node never differ by more than 1 (int this case).
            
            return BuildBSTByPreorder(nums, 0, nums.Length - 1);
        }

        public static BinaryTreeNode BuildBSTByPreorder(int[] nums, int left, int right)
        {
            if (left > right)
                return null;

            // Choose left middle node as current root
            var center = left + (right - left) / 2;
            // if ((left + right) % 2 == 1) ++center; // for right middle node as root
            // if ((left + right) % 2 == 1) center += rand.nextInt(2); // for random middle node as root

            // Preorder traversal
            var node = new BinaryTreeNode(nums[center]);
            node.left = BuildBSTByPreorder(nums, left, center - 1); ;
            node.right = BuildBSTByPreorder(nums, center + 1, right); ;

            return node;
        }
    }
}