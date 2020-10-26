namespace CSharp.DS.Stack
{
    public class IncrementStack_DoublyLinkedList : Stack_DoublyLinkedList<int>
    {
        public IncrementStack_DoublyLinkedList(int maxSize) : base(maxSize)
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
            var curNode = stackLinkedList.dummyTail;
            while (count < k && curNode != null)
            {
                // Traverse until dummy head
                curNode.val += val;
                curNode = curNode.prev;
                count++;
            }
        }
    }
}
