using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST
{
    public class TreeNode<T> where T : IComparable<T>
    {
        private T data;
        private TreeNode<T> left;
        private TreeNode<T> right;
        public TreeNode() : this(default(T)) { }
        public TreeNode(T data) : this(data, null, null) { }

        public TreeNode(T data, TreeNode<T> left, TreeNode<T> right)
        {
            this.data = data;
            this.left = left;
            this.right = right;
        }

        public T Data { get { return data; } set { data = value; } }
        public TreeNode<T> Left { get { return left; } set { left = value; } }
        public TreeNode<T> Right { get { return right; } set { right = value; } }
        public bool IsLeaf() { 
            return this.left == null && this.right == null; 
        }
    }
}
