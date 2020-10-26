using System.Collections.Generic;

namespace CSharp.DS.LinkedList
{
    public class SinglyLinkedList<T>
    {
        public class SLLNode
        {
            public T value;

            public SLLNode next;
            public SLLNode random;

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
        /// Reverse the Linked List in-place
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public SLLNode ReverseIt(SLLNode head)
        {
            SLLNode prev = null, curr = head, next = null;

            // 1->2->3->4
            while (curr != null)
            {
                next = curr.next; // Save Next
                curr.next = prev; // Reverse

                prev = curr;
                curr = next; // Advance prev and curr

                // 0.
                //            cur
                //      prev  next
                // null <-1   2->3->4 

                // 1.
                //                 cur
                //          prev   next
                // null <-1<-2     3->4 

                // 2.
                //                    cur
                //             prev   next
                // null <-1<-2<-3     4->null 

                // 3.
                //                       cur
                //                prev   next
                // null <-1<-2<-3<-4     4->null 
            }
            return prev;
        }

        public SLLNode ReverseRec(SLLNode head)
        {
            if (head == null || head.next == null)
                return head;

            var reversed = ReverseRec(head.next);
            head.next.next = head;
            head.next = null;

            return reversed;
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

        public SLLNode FindLoopBeginning(SLLNode head)
        {
            // Intuition:
            // Fast Pointer us LOOP_SIZE - K steps behind SlowPointer in the loop,
            // And F_pointer catches up at a rate of 1 step per unit of time.
            // Then they meet at LOOP_SIZE - K steps, where they will be both K
            // steps from the start of the loop

            var slow = head;
            var fast = head;

            // Find meeting point, if exists. This will be LOOP_SIZE - K steps into the linked list
            while (fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;

                if (slow == fast)
                    break;
            }

            // No meeting point
            if (fast == null || fast.next == null)
                return null;

            // Move Slow to the head. Keep fast at meeting point.
            // Each are k steps from start of the loop. Make them move at same speed.

            slow = head;
            while (slow != fast)
            {
                slow = slow.next;
                fast = fast.next;
            }

            // Start of the loop
            return fast;
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

        public SLLNode DeleteSortedDuplicatesRec(SLLNode head)
        {
            if (head == null)
                return head;

            head.next = DeleteSortedDuplicatesRec(head.next);

            if (head.next != null && head.value.Equals(head.next.value))
                return head.next;

            return head;
        }

        public SLLNode DeleteSortedDuplicatesIt(SLLNode head)
        {
            SLLNode cur = head;
            while (cur != null)
            {
                if (cur.next != null && cur.value.Equals(cur.next.value))
                    cur.next = cur.next.next;
                else cur = cur.next;
            }

            return head;
        }

        // Visited dictionary to hold old node reference as "key" and new node reference as the "value"
        public SLLNode GetClonedNode(Dictionary<SLLNode, SLLNode> originalToClonedDict, SLLNode node)
        {
            // If the node exists then
            if (node == null)
                return null;
            
            // Check if the node is in the visited dictionary
            if (!originalToClonedDict.ContainsKey(node))
            {
                // Otherwise create a new node, add to the dictionary and return it
                originalToClonedDict.Add(node, new SLLNode(node.value));
            }

            // If its in the visited dictionary then return the new node
            // reference from the dictionary

            return originalToClonedDict[node];
        }

        public SLLNode CopyRandomList(SLLNode head)
        {
            if (head == null)
                return null;

            var originalToClonedDict = new Dictionary<SLLNode, SLLNode>();

            var oldNode = head;
            // Creating the new head node.
            var newNode = new SLLNode(oldNode.value);
            originalToClonedDict.Add(oldNode, newNode);

            // Iterate on the linked list until all nodes are cloned.
            while (oldNode != null)
            {
                // Get the clones of the nodes referenced by random and next pointers.
                newNode.random = this.GetClonedNode(originalToClonedDict, oldNode.random);
                newNode.next = this.GetClonedNode(originalToClonedDict, oldNode.next);

                // Move one step ahead in the linked list.
                oldNode = oldNode.next;
                newNode = newNode.next;
            }

            return originalToClonedDict[head];
        }

        public SLLNode CopyRandomListOpt(SLLNode head)
        {
            if (head == null)
                return null;

            SLLNode current = head;
            SLLNode next = current.next;

            // Insert copy nodes between
            // 1 -> 1' -> 2 -> 2' -> NULL
            while (current != null)
            {
                next = current.next;

                SLLNode copy = new SLLNode(current.value);
                current.next = copy;
                copy.next = next;

                current = next;
            }

            // Assign Random Pointers
            current = head;
            while (current != null)
            {
                if (current.random != null)
                    current.next.random = current.random.next;

                current = current.next.next;
            }

            // Extract Copy Nodes
            current = head;
            SLLNode copyHead = new SLLNode(default);
            SLLNode currentCopy = copyHead;

            while (current != null)
            {
                next = current.next.next;

                currentCopy.next = current.next;
                currentCopy = currentCopy.next;

                current.next = next;
                current = current.next;
            }

            return copyHead.next;
        }
    }
}
