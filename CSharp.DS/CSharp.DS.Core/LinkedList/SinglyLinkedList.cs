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
            while ((oneToLast = oneToLast?.next) != tail) ; // Find one to last

            tail = oneToLast;
            tail.next = oneToLast;
        }
    }
}
