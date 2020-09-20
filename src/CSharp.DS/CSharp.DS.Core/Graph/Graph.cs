using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp.DS.Core.Graph
{
    /// <summary>
    /// Undirected unweighted Graph representation using Vertexes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Graph<T>
    {
        public class Vertex
        {
            public T val;

            public Vertex(T val)
            {
                this.val = val;
            }
        }

        private Dictionary<Vertex, LinkedList<Vertex>> adjacencyList;

        public Graph()
        {
            adjacencyList = new Dictionary<Vertex, LinkedList<Vertex>>();
        }

        public void AddEdge(Vertex source, Vertex target)
        {
            if (!adjacencyList.ContainsKey(source))
                adjacencyList.Add(source, new LinkedList<Vertex>());

            if (!adjacencyList.ContainsKey(target))
                adjacencyList.Add(target, new LinkedList<Vertex>());

            adjacencyList[source].AddLast(target);
        }

        readonly HashSet<Vertex> _visited = new HashSet<Vertex>();

        /// <summary>
        /// Depth First Traversal (recursive)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="result"></param>
        public void DepthFirstTraversalRec(Vertex node, List<T> result)
        {
            if (node == null)
                return;

            if (_visited.Contains(node))
                return;
            _visited.Add(node);

            result.Add(node.val);
            foreach (var childNode in adjacencyList[node])
                DepthFirstTraversalRec(childNode, result);
        }

        /// <summary>
        /// Depth First Traversal (iterative)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="result"></param>
        public IList<T> DepthFirstTraversalIt(Vertex node)
        {
            var result = new List<T>();

            if (node == null)
                return result;

            var dfsStack = new Stack<Vertex>();
            dfsStack.Push(node);

            while (dfsStack.Any())
            {
                var curNode = dfsStack.Pop();
                _visited.Add(curNode); // Mark as visited
                result.Add(curNode.val); // Visit

                // Right to left
                foreach (var childNode in adjacencyList[node].AsEnumerable().Reverse())
                {
                    if (_visited.Contains(curNode))
                        continue;

                    dfsStack.Push(childNode);
                }
            }

            return result;
        }

        /// <summary>
        /// Breadth First Order of nodes.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="level"></param>
        /// <param name="levelToNodesDict"></param>
        public void BreadthFirstTraversalRec(Vertex node, int level, Dictionary<int, IList<T>> levelToNodesDict)
        {
            if (node == null)
                return;

            if (_visited.Contains(node))
                return;
            _visited.Add(node);

            if (!levelToNodesDict.ContainsKey(level))
                levelToNodesDict.Add(level, new List<T>());
            levelToNodesDict[level].Add(node.val);

            foreach (var childNode in adjacencyList[node])
                BreadthFirstTraversalRec(childNode, level + 1, levelToNodesDict);
        }

        /// <summary>
        /// Breadth First Search.
        /// Iterative implementation based on Queue.
        /// </summary>
        /// <param name="node"></param>
        public IList<IList<T>> BreadthFirstTraversalIt(Vertex node)
        {
            var result = new List<IList<T>>();

            if (node == null)
                return result;

            var bfsQueue = new Queue<Vertex>();
            bfsQueue.Enqueue(node);

            while (bfsQueue.Any())
            {
                var levelSize = bfsQueue.Count();
                var levelList = new List<T>();

                for (var i = 0; i < levelSize; i++)
                {
                    var curNode = bfsQueue.Dequeue();
                    if (_visited.Contains(curNode))
                        continue;
                    _visited.Add(curNode); // Mark as visited

                    levelList.Add(curNode.val);

                    foreach (var childNode in adjacencyList[curNode])
                        bfsQueue.Enqueue(childNode);
                }

                result.Add(levelList);
            }

            return result;
        }
    }
}
