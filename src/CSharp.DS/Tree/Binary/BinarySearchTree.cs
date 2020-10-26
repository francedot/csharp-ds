using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp.DS.Tree.Binary
{
    public class BinarySearchTree<T> where T : IComparable
    {
        public BinaryTreeNode<T> root = null;

        public BinaryTreeNode<T> BinarySearch(BinaryTreeNode<T> root, T val)
        {
            if (root == null || val.Equals(root.val))
                return root;

            return val.CompareTo(root.val) < 0
                   ? BinarySearch(root.left, val)
                   : BinarySearch(root.right, val);
        }

        public BinaryTreeNode<T> Insert(BinaryTreeNode<T> root, T val)
        {
            if (root == null)
                return new BinaryTreeNode<T>(val);

            if (val.CompareTo(root.val) > 0)
                // insert into the right subtree
                root.right = Insert(root.right, val);
            else if (val.CompareTo(root.val) == 0)
                // skip the insertion
                return root;
            else
                // insert into the left subtree
                root.left = Insert(root.left, val);

            return root;
        }

        /*
         * Successor = "after node", i.e. the next node,
         * or the smallest node after the current one.
         * One step right and then always left
         */
        public T Successor(BinaryTreeNode<T> root)
        {
            root = root.right;
            while (root.left != null)
                root = root.left;
            return root.val;
        }

        /*
         * Predecessor = "before node", i.e. the previous node,
         * or the largest node before the current one.
         * One step left and then always right
         */
        public T Predecessor(BinaryTreeNode<T> root)
        {
            root = root.left;
            while (root.right != null)
                root = root.right;
            return root.val;
        }

        public BinaryTreeNode<T> Delete(BinaryTreeNode<T> root, T key)
        {
            if (root == null)
                return null;

            // delete from the right subtree
            if (key.CompareTo(root.val) > 0)
                root.right = Delete(root.right, key);
            // delete from the left subtree
            else if (key.CompareTo(root.val) < 0)
                root.left = Delete(root.left, key);
            // delete the current node
            else
            {
                // Visualization: https://i.imgur.com/nawwXNM.png
                // the node is a leaf
                if (root.left == null && root.right == null)
                    root = null;
                // the node is not a leaf and has a right child
                else if (root.right != null)
                {
                    root.val = Successor(root);
                    root.right = Delete(root.right, root.val);
                }
                // the node is not a leaf, has no right child, and has a left child
                else
                {
                    root.val = Predecessor(root);
                    root.left = Delete(root.left, root.val);
                }
            }
            return root;
        }

        /// <summary>
        /// Construct a Binary Search Tree from a sorted array
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        public static BinaryTreeNode<T> FromSortedArray(T[] elements)
        {
            // BST properties
            // 1) For every subtree, node.left < node < node.right.
            // 2) Balanced: depth of the two subtrees of every node never differ by more than 1 (int this case).

            return BuildBSTByPreorder(elements, 0, elements.Length - 1);
        }

        public static BinaryTreeNode<T> BuildBSTByPreorder(IList<T> elements, int left, int right)
        {
            if (left > right)
                return null;

            // Choose left middle node as current root
            var center = left + (right - left) / 2;
            // if ((left + right) % 2 == 1) ++center; // for right middle node as root
            // if ((left + right) % 2 == 1) center += rand.nextInt(2); // for random middle node as root

            // Preorder traversal
            return new BinaryTreeNode<T>(elements[center])
            {
                left = BuildBSTByPreorder(elements, left, center - 1),
                right = BuildBSTByPreorder(elements, center + 1, right)
            };
        }

        public BinaryTreeNode<T> BalanceBST(BinaryTreeNode<T> root)
        {
            // 1. Store the noteds on a SortedSet using whatever traversal of nodes
            // 2. Build the BST from the preorder traversal

            var nodes = new SortedSet<BinaryTreeNode<T>>(
                Comparer<BinaryTreeNode<T>>.Create(
                    (e1, e2) => e1.val.CompareTo(e2.val)));

            var stack = new Stack<BinaryTreeNode<T>>();
            stack.Push(root);

            // Preorder Traversal
            while (stack.Any())
            {
                var popped = stack.Pop();
                nodes.Add(popped);

                if (popped.left != null)
                    stack.Push(popped.left);
                if (popped.right != null)
                    stack.Push(popped.right);
            }

            return BuildBSTByPreorderNodes(nodes.ToArray(), 0, nodes.Count() - 1);
        }

        public static BinaryTreeNode<T> BuildBSTByPreorderNodes(IList<BinaryTreeNode<T>> elements, int left, int right)
        {
            if (left > right)
                return null;

            // Choose left middle node as current root
            var center = left + (right - left) / 2;
            // if ((left + right) % 2 == 1) ++center; // for right middle node as root
            // if ((left + right) % 2 == 1) center += rand.nextInt(2); // for random middle node as root

            // Preorder traversal
            elements[center].left = BuildBSTByPreorderNodes(elements, left, center - 1);
            elements[center].right = BuildBSTByPreorderNodes(elements, center + 1, right);

            return elements[center];
        }
    }
}
