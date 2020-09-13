namespace CSharp.DS.Core.Queue
{
    /// <summary>
    /// Queue implementation using an Array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueueArray<T>
    {
        private int size;
        private int head = -1;
        private int tail = -1;
        private readonly T[] _elements;

        public QueueArray(int capacity)
        {
            _elements = new T[capacity];
            size = 0;
        }

        public bool Enequeue(T e)
        {
            if (size == _elements.Length)
                return false;

            head = (head + 1) % _elements.Length;
            _elements[head] = e;
            size++;

            if (tail == -1)
            {
                tail = head;
            }

            return true;
        }

        public bool Dequeue()
        {
            if (size == 0)
            {
                return false;
            }

            size--;
            tail = (tail + 1) % _elements.Length;

            if (size == 0)
            {
                head = -1;
                tail = -1;
            }

            return true;
        }

        public T Front()
        {
            if (size == 0)
                return default;

            return _elements[head];
        }


        public T Rear()
        {
            if (size == 0)
                return default;

            return _elements[tail];
        }

        public int Size()
        {
            return size;
        }
    }
}
