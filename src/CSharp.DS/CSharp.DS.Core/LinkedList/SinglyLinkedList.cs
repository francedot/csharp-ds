using System.Collections.Generic;

namespace CSharp.DS.Core.LinkedList
{
    public class SinglyLinkedList<T>
    {
        public class SLLNode
        {
            public T value;

            public SLLNode next;

            public SLLNode(T value)
            {
                this.value = value;
            }
        }

        public SLLNode head;
        public SLLNode tail;
        public int count;

        public SinglyLinkedList()
        {
        }

        public void Append(SLLNode node)
        {
            if (tail == node)
            {
                return;
            }

            if (tail == null)
            {
                head = node;
                tail = node;
            }
            else if (head == tail)
            {
                tail = node;
                head.next = tail;
            }
            else
            {
                if (head == node)
                    RemoveHead();

                tail.next = node;
                tail = node;
            }
        }

        public void Prepend(SLLNode node)
        {
            if (head == node)
            {
                return;
            }

            if (head == null)
            {
                head = node;
                tail = node;
            }
            else if (head == tail)
            {
                head = node;
                head.next = tail;
            }
            else
            {
                if (tail == node)
                {
                    RemoveTail();
                }
                node.next = head;
                head = node;
            }
        }

        public void RemoveHead()
        {
            if (head == null)
            {
                return;
            }
            if (tail == head)
            {
                head = null;
                tail = null;
                return;
            }
            head = head.next;
        }

        public void RemoveTail()
        {
            if (tail == null)
            {
                return;
            }
            if (tail == head)
            {
                head = null;
                tail = null;
                return;
            }

            var oneToLast = head;
            while ((oneToLast = oneToLast?.next) != tail); // Find 2nd to last

            tail = oneToLast;
            tail.next = oneToLast;
        }

        /// <summary>
        /// Reverse the Linked List inplace
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public SLLNode Reverse(SLLNode head)
        {
            SLLNode prevNode = head, curNode = head;

            while (curNode?.next != null)
            {
                var prevTmp = curNode.next;
                curNode.next = curNode.next?.next;
                prevTmp.next = prevNode;
                prevNode = prevTmp;
            }

            return prevNode;
        }

        /// <summary>
        /// Reverse the Linked List K groups at a time
        /// </summary>
        /// <example>
        /// Input: 1->2->3->4->5, k=2
        /// Output: 2->1->4->3->5
        /// </example>
        /// <param name="head"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public SLLNode ReverseKGroup(SLLNode head, int k)
        {
            // Get the Length of the List
            var p = head;
            int length = 1;
            while (p.next != null)
            {
                length++;
                p = p.next;
            }

            var leftOutIndex = length - (length % k);

            // Reverse k nodes at a time
            SLLNode newHead = null, prevTail = null;
            int numReversed = 0;
            p = head;
            while (p != null && numReversed < leftOutIndex)
            {
                var headTail = ReverseList(p, length, k);
                if (newHead == null)
                    newHead = headTail.Item1;

                numReversed += k;

                if (prevTail != null)
                    prevTail.next = headTail.Item1;
                prevTail = headTail.Item2;

                p = headTail.Item2?.next;
            }

            return newHead;
        }

        private (SLLNode, SLLNode) ReverseList(SLLNode fromNode, int length, int k)
        {
            var prev = fromNode;
            var next = prev.next;
            int i = 1;
            while (next != null && i++ < k)
            {
                var nextNext = next.next;
                next.next = prev;

                prev = next;
                next = nextNext;
            }
            fromNode.next = next;

            SLLNode tail = null;
            if (i < length)
                tail = fromNode;

            return (prev, tail);
        }

        public bool HasCycle(SLLNode head)
        {
            SLLNode p = head, r = head?.next;

            while (p != null)
            {
                if (p == r)
                    return true;

                p = p.next;
                r = r?.next?.next;
            }

            return false;
        }

        /// <summary>
        /// Get the intersection node of 2 linked list
        /// </summary>
        /// <example>
        /// Input: 1->2->3->4
        /// Output: 2->1->4->3.
        /// </example>
        /// <param name="headA"></param>
        /// <param name="headB"></param>
        /// <returns></returns>
        public SLLNode FindIntersectionNode(SLLNode headA, SLLNode headB)
        {
            if (headA == null || headB == null)
                return null;

            SLLNode pa = headA, pb = headB;

            while (pa != pb)
            {
                if (pa == null)
                    pa = headB;
                else
                    pa = pa?.next; ;

                if (pb == null)
                    pb = headA;
                else
                    pb = pb?.next;
            }

            return pa;
        }

        /// <summary>
        /// Swap pairs of nodes in a linked list
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public SLLNode SwapPairs(SLLNode head)
        {
            SLLNode p1 = head, p2 = head?.next, newHead = head?.next, prev = null;
            if (p2 == null)
                return p1;

            while (p1 != null && p2 != null)
            {
                var next = p2.next;
                p2.next = p1;
                p1.next = next;
                if (prev != null)
                    prev.next = p2;
                prev = p1;

                p1 = next;
                p2 = next?.next;
            }

            return newHead;
        }

        /// <summary>
        /// Reorder a linked list swapping opposite nodes
        /// </summary>
        /// <example>
        /// Input: 1->2->3->4
        /// Output: 1->4->2->3
        /// </example>
        /// <param name="head"></param>
        public void ReorderList(SLLNode head)
        {
            var reversedListStack = new Stack<SLLNode>();
            var p = head;
            while (p != null)
            {
                reversedListStack.Push(p);
                p = p.next;
            }

            int i = 0, steps = reversedListStack.Count / 2;
            p = head;
            while (i++ < steps)
            {
                var next = p.next;
                p.next = reversedListStack.Pop();
                p.next.next = next;
                p = next;
            }

            if (p != null)
                p.next = null;
        }
    }
}
