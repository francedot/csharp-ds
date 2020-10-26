using System;

namespace CSharp.DS.LinkedList
{
    public class DoublyLinkedList<T>
    {
        public class DLLNode
        {
            public T val;
            public DLLNode prev;
            public DLLNode next;

            public DLLNode(T val)
            {
                this.val = val;
            }

            public void Detach()
            {
                if (prev != null)
                    prev.next = this.next;
                if (next != null)
                    this.next.prev = prev;
            }
        }

        public DLLNode dummyHead;
        public DLLNode dummyTail;

        public DoublyLinkedList()
        {
            dummyHead = new DLLNode(default);
            dummyTail = new DLLNode(default);
            dummyHead.next = dummyTail;
            dummyTail.prev = dummyHead;
        }

        public DLLNode Head() => dummyHead.next;
        public DLLNode Tail() => dummyTail.prev;
        public bool Any() => dummyHead.next != dummyTail;

        public void Prepend(DLLNode node)
        {
            var prevHead = dummyHead.next;
            dummyHead.next = node;
            node.prev = dummyHead;
            node.next = prevHead;
            prevHead.prev = node;
        }

        public void Append(DLLNode node)
        {
            var prevTail = dummyTail.prev;
            dummyTail.prev = node;
            node.next = dummyTail;
            node.prev = prevTail;
            prevTail.next = node;
        }

        public DLLNode RemoveFirst()
        {
            DLLNode removed = null;
            if (!Any())
            {
                return removed;
            }

            removed = dummyHead.next;
            removed.Detach();

            return removed;
        }

        public DLLNode RemoveLast()
        {
            DLLNode removed = null;
            if (!Any())
            {
                return removed;
            }

            removed = dummyTail.prev;
            removed.Detach();

            return removed;
        }

        public bool Contains(DLLNode node)
        {
            var p = Head();
            while (p != dummyTail)
            {
                if (p == node)
                {
                    return true;
                }
                p = p.next;
            }

            return false;
        }

        public bool Remove(DLLNode node)
        {
            if (!Contains(node))
                return false;

            node.Detach();

            return true;
        }
    }
}
