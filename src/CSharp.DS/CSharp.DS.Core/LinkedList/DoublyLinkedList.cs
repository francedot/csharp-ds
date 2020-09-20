using System;

namespace CSharp.DS.Core.LinkedList
{
    public class DoublyLinkedList<T>
    {
        public class DLLNode
        {
            public T value;

            public DLLNode next;
            public DLLNode prev;

            public DLLNode(T value)
            {
                this.value = value;
            }

            /// <summary>
            /// Helper function to detach a node from the List
            /// </summary>
            public void Detach()
            {
                if (prev != null)
                {
                    prev.next = next;
                }
                if (next != null)
                {
                    next.prev = prev;
                }
                prev = null;
                next = null;
            }
        }

        internal bool Any()
        {
            throw new NotImplementedException();
        }

        public DLLNode head;
        public DLLNode tail;
        public int count;

        public DoublyLinkedList()
        {
        }

        public void Append(DLLNode node)
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
                node.prev = tail;
                tail = node;
                head.next = tail;
            }
            else
            {
                if (head == node)
                {
                    RemoveHead();
                }
                node.Detach();
                tail.next = node;
                node.prev = tail;
                tail = node;
            }

            count++;
        }

        public void Prepend(DLLNode node)
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
                tail.prev = node;
                head = node;
                head.next = tail;
            }
            else
            {
                if (tail == node)
                {
                    RemoveTail();
                }
                node.Detach();
                head.prev = node;
                node.next = head;
                head = node;
            }

            count++;
        }

        public DLLNode RemoveHead()
        {
            var toRemove = head;
            if (head == null)
                return null;
            if (tail == head)
            {
                head = null;
                tail = null;
                return null;
            }
            head = head.next;
            head.prev = null;

            return toRemove;
        }

        public DLLNode RemoveTail()
        {
            var toRemove = tail;

            if (tail == null)
                return null;
            if (tail == head)
            {
                head = null;
                tail = null;
                return null;
            }
            tail = tail.prev;
            tail.next = null;

            return toRemove;
        }
    }
}
