namespace CSharp.DS.Tree.Binary
{
    public class BinaryTreeNode<T>
    {
        public T val;
        public BinaryTreeNode<T> left;
        public BinaryTreeNode<T> right;
        public BinaryTreeNode(T val)
        {
            this.val = val;
        }
    }
}
