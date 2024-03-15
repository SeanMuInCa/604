using BST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVLTree
{
    public class AVLTree<T> : BST<T> where T : IComparable<T>
    {
        //single left
        private TreeNode<T> SingleLeft(TreeNode<T> oldRoot)
        { 
            //get a reference to the old root's right node, this is the new root
            TreeNode<T> newRoot = oldRoot.Right;

            //assign new root left child to old root right child
            oldRoot.Right = newRoot.Left;

            //new root's left child is set to the old root
            newRoot.Left = oldRoot;
            /*
             node newroot = oldroot.right;
             oldroot.right = newroot.left;
             newroot.left = oldroot; 
             */

            return newRoot; 
        }

        private TreeNode<T> SingleRight(TreeNode<T> oldRoot)
        {
            //get a reference to the old root's left node, this is the new root
            TreeNode<T> newRoot = oldRoot.Left;

            //assign new root right child to old root left child
            oldRoot.Left = newRoot.Right;

            //new root's right child is set to the old root
            newRoot.Right = oldRoot;

            
            return newRoot;
        }
        internal override TreeNode<T> Balance(TreeNode<T> cur)
        {
            TreeNode<T> newRoot = cur;
            if (cur != null)
            {
                int heightDiff = GetHeightDiff(cur);
                //if the tree is unbalanced on the right branch
                if (heightDiff < -1)
                {
                    //if the right child is left heavy
                    int rightChildDiff = GetHeightDiff(cur.Right);
                    if (rightChildDiff > 0)
                    {
                        newRoot = DoubleLeftRotation(cur);
                    }
                    else//if the right child is right heavy
                    {
                        newRoot = SingleLeft(cur);
                    }

                }
                else if (heightDiff > 1)//if the tree is unbalanced on the left branch
                {
                    //if the left child is left heavy
                    int leftChildDiff = GetHeightDiff(cur.Left);
                    if (leftChildDiff > 0)
                    {
                        newRoot = SingleRight(cur);
                    }
                    else//if the right child is right heavy
                    {
                        newRoot = DoubleRightRotation(cur);
                    }
                }
                
            }
            return newRoot;
        }

        // Perform a double right rotation
        private TreeNode<T> DoubleRightRotation(TreeNode<T> node)
        {
            node.Left = SingleLeft(node.Left);
            return SingleRight(node);
        }

        private TreeNode<T> DoubleLeftRotation(TreeNode<T> node)
        {
            node.Right = SingleRight(node.Right);
            return SingleLeft(node);
        }
        //helper method
        private int GetHeightDiff(TreeNode<T> node)
        { 
            int leftHeight = -1;
            int rightHeight = -1;
            int diff = 0;

            if (node != null)
            {
                if (node.Right != null)
                {
                    rightHeight = RecHeight(node.Right);
                }
                if(node.Left != null)
                {
                    leftHeight = RecHeight(node.Left);
                }
            }
            diff = leftHeight - rightHeight;

            return diff;
        }
    }
}
