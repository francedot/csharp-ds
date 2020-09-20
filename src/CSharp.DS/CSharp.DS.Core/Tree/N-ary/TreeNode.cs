using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.DS.Core.Tree.N_ary
{
    public class TreeNode
    {
        public int val;
        public IList<TreeNode> children;

        public TreeNode(int val)
        {
            this.val = val;
        }

        public TreeNode(int val, IList<TreeNode> _children)
        {
            this.val = val;
            children = _children;
        }
    }
}
