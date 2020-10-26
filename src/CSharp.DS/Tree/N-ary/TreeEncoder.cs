using System;
using System.Text;

namespace CSharp.DS.Tree.N_ary
{
    public class TreeEncoder
    {
        /*
           Reference Tree:
                        2
                  /    /  \    \
                3   5       3   7
                   / \       \
                  4   3       10
        */
        /// <summary>
        /// Encodes a tree to a single string
        /// Based on https://leetcode.com/problems/serialize-and-deserialize-n-ary-tree/solution/
        /// </summary>
        /// <param name="root"></param>
        /// <example>23#54#3##310##7##</example>
        /// <returns></returns>
        public string Serialize(TreeNode root)
        {
            if (root == null)
                return string.Empty;

            var sb = new StringBuilder();
            DFSSerialize(root, sb);

            return sb.ToString();
        }

        /// <summary>
        /// Decodes your encoded data to tree
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public TreeNode Deserialize(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
                return null;

            int index = 1, sentinelLevel = 0;
            var root = new TreeNode(data[0]);
            DFSDeserialize(root, data, ref index, ref sentinelLevel);

            return root;
        }

        private void DFSSerialize(TreeNode node, StringBuilder sb)
        {
            if (node == null)
            {
                sb.Append("~");
                return;
            }

            // As Unicode char. This exclude integer 126 (Unicode ~) from the input range.
            sb.Append(Convert.ToChar(node.val));

            foreach (var child in node.children)
                DFSSerialize(child, sb);

            sb.Append("~");
        }

        private void DFSDeserialize(TreeNode node, string data, ref int index, ref int sentinelLevel)
        {
            if (index == data.Length)
                return;

            var curLevel = sentinelLevel;
            while (index < data.Length && curLevel == sentinelLevel)
            {
                if (data[index] == '~')
                {
                    sentinelLevel--;
                    index++;

                    continue;
                }

                var childNode = new TreeNode(data[index]);
                node.children.Add(childNode);

                index++; sentinelLevel++;
                DFSDeserialize(childNode, data, ref index, ref sentinelLevel);
            }
        }
    }
}
