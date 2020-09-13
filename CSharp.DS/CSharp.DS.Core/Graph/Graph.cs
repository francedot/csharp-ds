using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
