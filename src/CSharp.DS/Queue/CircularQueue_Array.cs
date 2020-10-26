using System;

namespace CSharp.DS.Queue
{
    /// <summary>
    /// Circular Queue implementation using an Array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CircularQueue_Array<T>
    {
        private readonly T[] _list;
        private readonly int _capacity;
        private int _head;
        private int _tail;

        public CircularQueue_Array(int capacity)
        {
            _list = new T[capacity];
            _capacity = capacity;

            _head = -1;
            _tail = -1;
        }

        public bool Enqueue(T value)
        {
            if (IsFull())
                return false;

            if (IsEmpty())
                _head = 0;

            _tail = (_tail + 1) % _capacity;
            _list[_tail] = value;

            return true;
        }

        public bool Dequeue()
        {
            if (IsEmpty())
                return false;

            if (_head == _tail)
            {
                _head = -1;
                _tail = -1;
                return true;
            }

            _head = (_head + 1) % _capacity;

            return true;
        }

        public T Front()
        {
            if (IsEmpty())
                throw new Exception("Queue is empty");

            return _list[_head];
        }

        public T Rear()
        {
            if (IsEmpty())
                throw new Exception("Queue is empty");

            return _list[_tail];
        }

        public bool IsEmpty()
        {
            return _head == -1;
        }

        public bool IsFull()
        {
            return ((_tail + 1) % _capacity) == _head;
        }
    }
}
