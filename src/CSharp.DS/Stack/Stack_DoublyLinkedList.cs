using CSharp.DS.LinkedList;

namespace CSharp.DS.Stack
{
    public class Stack_DoublyLinkedList<T>
    {
        public DoublyLinkedList<T> stackLinkedList;
        public readonly int maxSize;
        public int size;

        public Stack_DoublyLinkedList(int maxSize)
        {
            this.maxSize = maxSize;
            size = 0;

            stackLinkedList = new DoublyLinkedList<T>();
        }

        public void Push(T x)
        {
            if (size == maxSize)
                return;

            stackLinkedList.Prepend(new DoublyLinkedList<T>.DLLNode(x));
            size++;
        }

        public T Pop()
        {
            if (!stackLinkedList.Any())
            {
                return default;
            }

            var removed = stackLinkedList.RemoveFirst();
            size--;

            return removed.val;
        }
    }

    public class StackArray
    {
        private int[] _elements;
        private int _top;
        private int _maxCapacity;
        public StackArray(int maxCapacity)
        {
            _maxCapacity = maxCapacity;

            _elements = new int[maxCapacity];
            _top = -1;
        }

        public void Push(int item)
        {
            if (_top == _maxCapacity - 1)
                return; // Console.WriteLine("Stack Overflow");

            _elements[++_top] = item;
        }

        public int Pop()
        {
            if (_top == -1) // Underflow
                return -1;
             
            //Console.WriteLine("Poped element is: " + ele[top]);
            return _elements[_top--];
        }

        public void printStack()
        {
            if (_top == -1)
            {
                //Console.WriteLine("Stack is Empty");
                return;
            }
            else
            {
                for (int i = 0; i <= _top; i++)
                {
                    //Console.WriteLine("Item[" + (i + 1) + "]: " + ele[i]);
                }
            }
        }
    }
}
