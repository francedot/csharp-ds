using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp.DS.Graph
{
    /// <summary>
    /// DAG (Directed Acyclic Graph) used to model topological sort problems
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TopologicalGraph<T>
    {
        public class Node
        {
            public T val;

            // (x, y): x is a prerequisite to y => y depends to x (y is a dependency)
            public LinkedList<Node> dependencies;
            public int numPrerequisites;

            public Node(T val)
            {
                this.val = val;
                dependencies = new LinkedList<Node>();
            }
        }

        // Constant time removal for first/last Vertex
        public Dictionary<T, Node> Vertexes;

        public TopologicalGraph(T[] nodes, T[][] prerequisites)
        {
            Vertexes = new Dictionary<T, Node>();

            // Create the Topological graph
            foreach (var node in nodes)
                AddVertex(nodeKey: node, node);
            foreach (var prerequisite in prerequisites)
                AddDependency(prerequisite[1], prerequisite[0]);
        }

        public void AddVertex(T nodeKey, T nodeValue)
        {
            if (Vertexes.ContainsKey(nodeKey))
                throw new ArgumentException(nameof(nodeKey));

            Vertexes.Add(nodeKey, new Node(nodeValue));
        }

        public void AddDependency(T prerequisiteKey, T dependencyKey)
        {
            if (!Vertexes.ContainsKey(prerequisiteKey))
                throw new ArgumentException(nameof(prerequisiteKey));

            if (!Vertexes.ContainsKey(dependencyKey))
                throw new ArgumentException(nameof(dependencyKey));

            Vertexes[prerequisiteKey].dependencies.AddLast(Vertexes[dependencyKey]);
            Vertexes[dependencyKey].numPrerequisites++;
        }

        public List<T> TopologicalSort()
        {
            var topologicalList = new List<T>();

            // Get nodes with no prerequisites
            var noPrereqQueue = new Queue<Node>(
                Vertexes.Where(kv => kv.Value.numPrerequisites == 0).Select(kv => kv.Value));

            while (noPrereqQueue.Any())
            {
                var noPrereq = noPrereqQueue.Dequeue();
                topologicalList.Add(noPrereq.val);

                while (noPrereq.dependencies.Any())
                {
                    var dependency = noPrereq.dependencies.Last();
                    noPrereq.dependencies.RemoveLast();

                    dependency.numPrerequisites--;
                    if (dependency.numPrerequisites == 0)
                    {
                        noPrereqQueue.Enqueue(dependency);
                    }
                }
            }

            if (Vertexes.Values.Any(v => v.numPrerequisites > 0))
            {
                return new List<T>(); // dependency cycle
            }

            return topologicalList;
        }
    }
}
