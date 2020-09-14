# CSharp Data Structures
*From solving 200+ Leetcode problems*


#### My 2 cents on how to implement the most common DS using C#:

* **Linked List**
  * [Singly Linked List](./src/CSharp.DS/CSharp.DS.Core/LinkedList/SinglyLinkedList.cs)
  * [Doubly Linked List](./src/CSharp.DS/CSharp.DS.Core/LinkedList/DoublyLinkedList.cs)
* **Queue**
  * [Queue (using Array)](./src/CSharp.DS/CSharp.DS.Core/Queue/QueueArray.cs)
  * [Circular Queue (using Array)](./src/CSharp.DS/CSharp.DS.Core/Queue/CircularQueueArray.cs)
  * [Circular Queue (using LinkedList)](./src/CSharp.DS/CSharp.DS.Core/Queue/CircularQueueLinked.cs)
  * [Priority Queue (Binary Heap implementation)](./src/CSharp.DS/CSharp.DS.Core/Queue/PriorityQueue.cs)
* **Cache**
  * [LRU (Least Recently Used) Cache](./src/CSharp.DS/CSharp.DS.Core/Cache/LRUCache.cs)
  * [LFU (Least frequently Used) Cache](./src/CSharp.DS/CSharp.DS.Core/Cache/LFUCache.cs)
* **Heap**
  * [Base Heap](./src/CSharp.DS/CSharp.DS.Core/Heap/Heap.cs)
  * [Max Heap](./src/CSharp.DS/CSharp.DS.Core/Heap/MaxHeap.cs)
  * [Min Heap](./src/CSharp.DS/CSharp.DS.Core/Heap/MinHeap.cs)
  * [Heap (Indexed)](./src/CSharp.DS/CSharp.DS.Core/Heap/IndexedHeap.cs)
* **Union Find (Disjoint Set)**
  * [Union Find](./src/CSharp.DS/CSharp.DS.Core/UnionFInd/UnionFind.cs)
* **Graph**
  * [Graph (Unweighted)](./src/CSharp.DS/CSharp.DS.Core/Graph/Graph.cs)
  * [Graph (Weighted)](./src/CSharp.DS/CSharp.DS.Core/Graph/WeightedGraph.cs) with Dijstra, Prim, Kruskal greedy algorithms
  * [Graph (Topological Sort)](./src/CSharp.DS/CSharp.DS.Core/Graph/TopologicalGraph.cs)
* **Tree**
  * [Binary Tree](./src/CSharp.DS/CSharp.DS.Core/Tree/BinaryTree.cs) with pre/in/post/level/zigzag/vertical traversals
* **Trie**
  * [Trie](./src/CSharp.DS/CSharp.DS.Core/Trie/Trie.cs)
  * [WordDictionary](./src/CSharp.DS/CSharp.DS.Core/Trie/WordDictionary.cs)
  
#### Most useful for competitive programming as not part of the C# language specification:
* [Priority Queue (Binary Heap implementation)](./src/CSharp.DS/CSharp.DS.Core/Queue/PriorityQueue.cs)
* [Heap (Indexed)](./src/CSharp.DS/CSharp.DS.Core/Heap/IndexedHeap.cs)
* [Union Find](./src/CSharp.DS/CSharp.DS.Core/UnionFInd/UnionFind.cs)
* [Graph (Weighted)](./src/CSharp.DS/CSharp.DS.Core/Graph/WeightedGraph.cs)
* [Trie](./src/CSharp.DS/CSharp.DS.Core/Trie/Trie.cs)
