namespace CSharp.DS.Core.Tree.Binary
{
    public class BinarySearchTree
    {
        /// <summary>
        /// Construct a Binary Search Tree from a sorted array
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static BinaryTreeNode FromSortedArray(int[] nums)
        {
            // BST properties
            // 1) For every subtree, node.left < node < node.right.
            // 2) Balanced: depth of the two subtrees of every node never differ by more than 1 (int this case).

            return BuildBSTByPreorder(nums, 0, nums.Length - 1);
        }

        public static BinaryTreeNode BuildBSTByPreorder(int[] nums, int left, int right)
        {
            if (left > right)
                return null;

            // Choose left middle node as current root
            var center = left + (right - left) / 2;
            // if ((left + right) % 2 == 1) ++center; // for right middle node as root
            // if ((left + right) % 2 == 1) center += rand.nextInt(2); // for random middle node as root

            // Preorder traversal
            var node = new BinaryTreeNode(nums[center]);
            node.left = BuildBSTByPreorder(nums, left, center - 1); ;
            node.right = BuildBSTByPreorder(nums, center + 1, right); ;

            return node;
        }
    }
}
