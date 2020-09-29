# CSharp Data Structures
*From solving 200+ Leetcode problems*


#### My 2 cents on how to implement the most common DS and Algorithms using C#:

* **Linked List**
  * [Singly Linked List](./src/CSharp.DS/CSharp.DS.Core/LinkedList/SinglyLinkedList.cs)
    * [Reverse List](./src/CSharp.DS/CSharp.DS.Core/LinkedList/SinglyLinkedList.cs#L122)
    * [Reverse K groups](./src/CSharp.DS/CSharp.DS.Core/LinkedList/SinglyLinkedList.cs#L147)
    * [Detect Cycle](./src/CSharp.DS/CSharp.DS.Core/LinkedList/SinglyLinkedList.cs#L204)
    * [Find Intersection](./src/CSharp.DS/CSharp.DS.Core/LinkedList/SinglyLinkedList.cs#L230)
    * [Swap pairs](./src/CSharp.DS/CSharp.DS.Core/LinkedList/SinglyLinkedList.cs#L258)
    * [Swap opposites](./src/CSharp.DS/CSharp.DS.Core/LinkedList/SinglyLinkedList.cs#L288)
  * [Doubly Linked List](./src/CSharp.DS/CSharp.DS.Core/LinkedList/DoublyLinkedList.cs)
* **Queue**
  * [Queue (using Array)](./src/CSharp.DS/CSharp.DS.Core/Queue/QueueArray.cs)
  * [Circular Queue (using Array)](./src/CSharp.DS/CSharp.DS.Core/Queue/CircularQueueArray.cs)
  * [Circular Queue (using LinkedList)](./src/CSharp.DS/CSharp.DS.Core/Queue/CircularQueueLinked.cs)
  * [Priority Queue (Binary Heap implementation)](./src/CSharp.DS/CSharp.DS.Core/Queue/PriorityQueue.cs)
* **Stack**
  * [Stack (using Doubly Linked List)](./src/CSharp.DS/CSharp.DS.Core/Stack/DoublyLinkedStack.cs)
  * [Stack (increment K latest)](./src/CSharp.DS/CSharp.DS.Core/Stack/IncrementDoublyLinkedStack.cs)
* **Union Find (Disjoint Set)**
  * [Union Find](./src/CSharp.DS/CSharp.DS.Core/UnionFInd/UnionFind.cs)
* **Heap**
  * [Base Heap](./src/CSharp.DS/CSharp.DS.Core/Heap/Heap.cs)
  * [Max Heap](./src/CSharp.DS/CSharp.DS.Core/Heap/MaxHeap.cs)
  * [Min Heap](./src/CSharp.DS/CSharp.DS.Core/Heap/MinHeap.cs)
  * [Heap (Indexed)](./src/CSharp.DS/CSharp.DS.Core/Heap/IndexedHeap.cs)
* **Cache**
  * [LRU (Least Recently Used) Cache](./src/CSharp.DS/CSharp.DS.Core/Cache/LRUCache.cs)
  * [LFU (Least frequently Used) Cache](./src/CSharp.DS/CSharp.DS.Core/Cache/LFUCache.cs)
* **Graph**
  * [Graph (Unweighted)](./src/CSharp.DS/CSharp.DS.Core/Graph/Graph.cs)
    * [Depth First Traversal](./src/CSharp.DS/CSharp.DS.Core/Graph/Graph.cs#L48)
    * [Breadth First Traversal](./src/CSharp.DS/CSharp.DS.Core/Graph/Graph.cs#L102)
  * [Graph (Weighted)](./src/CSharp.DS/CSharp.DS.Core/Graph/WeightedGraph.cs)
    * [Kruskal's Minimum Spanning Tree](./src/CSharp.DS/CSharp.DS.Core/Graph/WeightedGraph.cs#L86)
    * [Prim's Minimum Spanning Tree](./src/CSharp.DS/CSharp.DS.Core/Graph/WeightedGraph.cs#L120)
    * [Dijstra's Shortest Path](./src/CSharp.DS/CSharp.DS.Core/Graph/WeightedGraph.cs#L163)
  * [Graph (Topological Sort)](./src/CSharp.DS/CSharp.DS.Core/Graph/TopologicalGraph.cs)
* **N-ary Tree**
  * [N-ary Tree](./src/CSharp.DS/CSharp.DS.Core/Tree/N-ary/Tree.cs)
    * [Depth First Traversal](./src/CSharp.DS/CSharp.DS.Core/Tree/N-ary/Tree.cs#L13)
    * [Breadth First Traversal](./src/CSharp.DS/CSharp.DS.Core/Tree/N-ary/Tree.cs#L57)
  * [Tree Encoding](./src/CSharp.DS/CSharp.DS.Core/Tree/N-ary/Codec.cs)
* **Binary Tree**
  * [Binary Tree](./src/CSharp.DS/CSharp.DS.Core/Tree/Binary/BinaryTree.cs)
    * [Preorder Traversal](./src/CSharp.DS/CSharp.DS.Core/Tree/Binary/BinaryTree.cs#L24)
    * [Inorder Traversal](./src/CSharp.DS/CSharp.DS.Core/Tree/Binary/BinaryTree.cs#L71)
    * [Postorder Traversal](./src/CSharp.DS/CSharp.DS.Core/Tree/Binary/BinaryTree.cs#L140)
    * [Level order Traversal](./src/CSharp.DS/CSharp.DS.Core/Tree/Binary/BinaryTree.cs#L190)
    * [Zigzag Level order Traversal](./src/CSharp.DS/CSharp.DS.Core/Tree/Binary/BinaryTree.cs#L252)
    * [Vertical order Traversal](./src/CSharp.DS/CSharp.DS.Core/Tree/Binary/BinaryTree.cs#L313)
  * [Binary Search Tree](./src/CSharp.DS/CSharp.DS.Core/Tree/Binary/BinarySearchTree.cs)
    * [Construct from Sorted Array](./src/CSharp.DS/CSharp.DS.Core/Tree/Binary/BinarySearchTree.cs#L10)
  * [Segment Tree](./src/CSharp.DS/CSharp.DS.Core/Tree/Binary/SegmentTree.cs)
* **Trie**
  * [Trie](./src/CSharp.DS/CSharp.DS.Core/Trie/Trie.cs)
  * [WordDictionary](./src/CSharp.DS/CSharp.DS.Core/Trie/WordDictionary.cs)
  
#### Most useful for competitive programming as they are not part of the C# language specification:
* [Priority Queue (Binary Heap implementation)](./src/CSharp.DS/CSharp.DS.Core/Queue/PriorityQueue.cs)
* [Heap (Indexed)](./src/CSharp.DS/CSharp.DS.Core/Heap/IndexedHeap.cs)
* [Union Find](./src/CSharp.DS/CSharp.DS.Core/UnionFInd/UnionFind.cs)
* [Graph (Weighted)](./src/CSharp.DS/CSharp.DS.Core/Graph/WeightedGraph.cs)
* [Trie](./src/CSharp.DS/CSharp.DS.Core/Trie/Trie.cs)
