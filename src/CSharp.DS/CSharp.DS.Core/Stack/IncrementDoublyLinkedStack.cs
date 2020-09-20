namespace CSharp.DS.Core.Stack
{
    public class IncrementDoublyLinkedStack : DoublyLinkedStack<int>
    {
        public IncrementDoublyLinkedStack(int maxSize) : base(maxSize)
        {
        }

        /// <summary>
        /// Increment last K elemnents by val
        /// </summary>
        /// <param name="k"></param>
        /// <param name="val"></param>
        public void Increment(int k, int val)
        {
            if (!stackLinkedList.Any())
            {
                return;
            }

            var count = 0;
            var curNode = stackLinkedList.tail;
            while (count < k && curNode != null)
            {
                // Traverse until dummy head
                curNode.value += val;
                curNode = curNode.prev;
                count++;
            }
        }
    }
}
