using CSharp.DS.Core.LinkedList;

namespace CSharp.DS.Core.Stack
{
    public class DoublyLinkedStack<T>
    {
        public DoublyLinkedList<T> stackLinkedList;
        public readonly int maxSize;
        public int size;

        public DoublyLinkedStack(int maxSize)
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

            var removed = stackLinkedList.RemoveHead().value;
            size--;

            return removed;
        }
    }
}
