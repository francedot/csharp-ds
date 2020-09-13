namespace CSharp.DS.Core.Queue
{
    /// <summary>
    /// Circular Queue implementation using a Linked List
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CircularQueueLinked<T>
    {
        public class ListNode
        {
            public T Value;
            public ListNode Previous, Next;
            public ListNode(T val)
            {
                this.Value = val;
            }
        }

        private readonly int _capacity;
        private readonly ListNode _head;
        private readonly ListNode _tail;
        private int _size;

        public CircularQueueLinked(int capacity)
        {
            _capacity = capacity;
            _size = 0;
            _head = new ListNode(default); // dummy nodes
            _tail = new ListNode(default);
            _head.Next = _tail;
            _tail.Previous = _head;
        }

        public bool Enqueue(T value)
        {
            if (IsFull())
            {
                return false;
            }

            var newNode = new ListNode(value);
            newNode.Next = _tail;
            newNode.Previous = _tail.Previous;
            _tail.Previous.Next = newNode;
            _tail.Previous = newNode;
            _size++;

            return true;
        }

        public bool Dequeue()
        {
            if (IsEmpty())
            {
                return false;
            }

            var deletedNode = _head.Next;
            _head.Next = deletedNode.Next;
            deletedNode.Next.Previous = _head;
            deletedNode.Next = null;
            deletedNode.Previous = null;

            _size--;

            return true;
        }

        public T Front()
        {
            if (IsEmpty())
                return default;

            return _head.Next.Value;
        }

        public T Rear()
        {
            if (IsEmpty())
                return default;

            return _tail.Previous.Value;
        }

        public bool IsEmpty()
        {
            return _size == 0;
        }

        public bool IsFull()
        {
            return _size == _capacity;
        }
    }
}
