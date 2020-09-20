using System.Collections.Generic;
using System.Linq;

namespace CSharp.DS.Core.Tree.N_ary
{
    public class Tree
    {
        /// <summary>
        /// Depth First Traversal (recursive)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="result"></param>
        public void DepthFirstTraversalRec(TreeNode node, List<int> result)
        {
            if (node == null)
                return;

            result.Add(node.val);
            foreach(var childNode in node.children)
                DepthFirstTraversalRec(childNode, result);
        }

        /// <summary>
        /// Depth First Traversal (iterative)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="result"></param>
        public IList<int> DepthFirstTraversalIt(TreeNode root)
        {
            var result = new List<int>();

            if (root == null)
                return result;

            var dfsStack = new Stack<TreeNode>();
            dfsStack.Push(root);

            while (dfsStack.Any())
            {
                var node = dfsStack.Pop();
                result.Add(node.val); // Visit

                // Right to left
                for (var i = node.children.Count() - 1; i >= 0; i--)
                    dfsStack.Push(node.children[i]);
            }

            return result;
        }

        /// <summary>
        /// Breadth First Order of nodes.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="level"></param>
        /// <param name="levelToNodesDict"></param>
        public void BreadthFirstTraversalRec(TreeNode node, int level, Dictionary<int, IList<int>> levelToNodesDict)
        {
            if (node == null)
                return;

            if (!levelToNodesDict.ContainsKey(level))
                levelToNodesDict.Add(level, new List<int>());
            levelToNodesDict[level].Add(node.val);

            foreach (var childNode in node.children)
                BreadthFirstTraversalRec(childNode, level + 1, levelToNodesDict);
        }

        /// <summary>
        /// Breadth First Search.
        /// Iterative implementation based on Queue.
        /// </summary>
        /// <param name="node"></param>
        public IList<IList<int>> BreadthFirstTraversalIt(TreeNode node)
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

                    foreach (var childNode in curNode.children)
                        queue.Enqueue(childNode);
                }

                result.Add(levelList);
            }

            return result;
        }
    }
}
