using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BST
{
    public class BST<T> : A_BST<T>, ICloneable where T : IComparable<T>
    {
        public BST() {
            root = null;
            count = 0;
        }
        public override void Add(T data)
        {
            if (root == null)
            {
                root = new TreeNode<T>(data);
            }
            else
            {
                RecAdd(data, root);
                root = Balance(root);
            }
            count++;
        }
        // virtual balance might be override in a child class
        internal TreeNode<T> Balance(TreeNode<T> root)
        { 
            return root;
        }

        private void RecAdd(T data, TreeNode<T> root)
        {
            if (data.CompareTo(root.Data) < 0)
            {
                if (root.Left == null)
                    root.Left = new TreeNode<T>(data);
                else
                {
                    RecAdd(data, root.Left);
                    root.Left = Balance(root.Left);
                }
            }
            else
            {
                if (root.Right == null)
                    root.Right = new TreeNode<T>(data);
                else
                {
                    RecAdd(data, root.Right);
                    root.Right = Balance(root.Right);
                }
            }
        }

        public override void Clear()
        {
            root = null; 
            count = 0;
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public override T Find(T data)
        {
            T tFound = default(T);
            tFound = RecFind(data, root);
            return tFound;
        }
        private T RecFind(T data, TreeNode<T> nCurrent)
        {
            T tReturn = default(T);
            if (nCurrent == null)
            {
                throw new ApplicationException(data + "is not found is the BST!");
            }
            else
            {
                int iResult = data.CompareTo(nCurrent.Data);
                //Base case
                if (iResult == 0)  // Means that you found the targert
                {
                    tReturn = nCurrent.Data;
                }
                //recursive case
                else if (iResult < 0)
                {
                    tReturn = RecFind(data, nCurrent.Left);
                }
                else
                {
                    tReturn = RecFind(data, nCurrent.Right);
                }
            }
            return tReturn;
        }


        public override int Height()
        {
            int height = -1;
            if (root != null)
            {
                height = RecHeight(root);
            }
            return height;
        }

        private int RecHeight(TreeNode<T> cur)
        {
            /*int lefts = 0;
            int rights = 0;
            if (cur == null)
            {
                return -1;
            }
            if (!cur.IsLeaf())
            {
                if(cur.Left != null)
                    lefts = RecHeight(root.Left) + 1;
                if(cur.Right != null)
                   rights = RecHeight(root.Right) + 1;  
            }
            return Math.Max(lefts, rights);*/

            if (cur == null) return 0;
            //后序遍历
            int leftDepth = RecHeight(cur.Left);
            int rightDepth = RecHeight(cur.Right);
            int depth = 1 + Math.Max(leftDepth, rightDepth);
            return depth;
            //return Math.max(getDepth(root.left), getDepth(root.right)) + 1;
        }

        public override void Iterate(ProcessData<T> pd, TRAVERSALORDER or)
        {
            if (root != null)
            {
                RecIterate(root, pd, or);
            }
        }
        private void RecIterate(TreeNode<T> nCurrent, ProcessData<T> pd, TRAVERSALORDER to)
        {
            if (to == TRAVERSALORDER.PRE_ORDER)
            {
                //nCurrent, left, right
                //Process nCurrent data
                pd(nCurrent.Data);
                if (nCurrent.Left != null)
                {
                    RecIterate(nCurrent.Left, pd, to);
                }
                if (nCurrent.Right != null)
                {
                    RecIterate(nCurrent.Right, pd, to);
                }
            }
            if (to == TRAVERSALORDER.IN_ORDER)
            {
                //Left, nCurrent, Rigth

                if (nCurrent.Left != null)
                {
                    RecIterate(nCurrent.Left, pd, to);
                }
                pd(nCurrent.Data);
                if (nCurrent.Right != null)
                {
                    RecIterate(nCurrent.Right, pd, to);
                }
            }
            if (to == TRAVERSALORDER.POST_ORDER)
            {
                //Left, Rigth, nCurrent
                if (nCurrent.Left != null)
                {
                    RecIterate(nCurrent.Left, pd, to);
                }

                if (nCurrent.Right != null)
                {
                    RecIterate(nCurrent.Right, pd, to);
                }
                pd(nCurrent.Data);
            }
        }

        /*private void RecIterate(TreeNode<T> nCurrent, ProcessData<T> pd, TRAVERSALORDER to)
        {
            //Simplify the code
            if (to == TRAVERSALORDER.PRE_ORDER)
            {
                pd(nCurrent.Data);
            }
            if (nCurrent.Left != null)
            {
                RecIterate(nCurrent.Left, pd, to);
            }
            if (to == TRAVERSALORDER.IN_ORDER)
            {
                pd(nCurrent.Data);
            }
            if (nCurrent.Right != null)
            {
                RecIterate(nCurrent.Right, pd, to);
            }
            if (to == TRAVERSALORDER.POST_ORDER)
            {
                pd(nCurrent.Data);
            }
        }*/
        
        public override bool Remove(T data)
        {
            bool isRemoved = false;
            root = RecRemove(root, data, ref isRemoved);
            return isRemoved;
        }
        private TreeNode<T> RecRemove(TreeNode<T> cur, T data, ref bool isRemoved)
        { 
            T subTitute = default(T);
            int compare = 0;
            if (cur != null)
            {
                compare = data.CompareTo(cur.Data);
                if (compare > 0)
                {
                    cur.Right = RecRemove(cur.Right, data, ref isRemoved);
                }
                else if (compare < 0)
                {
                    cur.Left = RecRemove(cur.Left, data, ref isRemoved);
                }
                else
                { 
                    isRemoved = true;
                    if (cur.IsLeaf())
                    {
                        cur = null;
                        count--;
                       
                    }
                    else if (cur.Left != null && cur.Right != null)
                    {
                        //finde the largest in the left tree
                        subTitute = RecFindLargerst(cur.Left);
                        //update the cur node data
                        cur.Data = subTitute;

                        cur.Left = RecRemove(cur.Left, subTitute, ref isRemoved);
                    }
                    else if (cur.Left != null)
                    {
                        cur = cur.Left;
                        count--;
                    }
                    else
                    {
                        cur = cur.Right;
                        count--;
                    }
                }
            }
            return cur;
        }
        //Find the largest for RecRemove
        public T FindLargest()
        {
            if (root != null)
            {
                return RecFindLargerst(root);
            }
            else
            {
                throw new ApplicationException("Root is null");
            }
        }
        private T RecFindLargerst(TreeNode<T> cur)
        {
            T tReturn = default(T);
            if (cur.Right != null)
            {
                tReturn = RecFindLargerst(cur.Right);
            }
            else
            {
                tReturn = cur.Data;
            }
            return tReturn;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            //return new DepthFirstEnumerator(this);
            return new BreadthFirstEnumberator(this);
        }
        #region Enumberator
        public class DepthFirstEnumerator : IEnumerator<T>
        { 
            public BST<T> parent = null;

            private TreeNode<T> cur = null;

            private Stack<TreeNode<T>> st = new Stack<TreeNode<T>>();
            public DepthFirstEnumerator(BST<T> parent) {
                this.parent = parent;
                Reset();
            }

            public T Current => cur.Data;

            object IEnumerator.Current => cur.Data;

            public void Dispose()
            {
                parent = null;
                cur = null;
                st = null; 
            }

            public bool MoveNext()
            {
                bool isMoved = false;
                if (st.Count > 0)
                { 
                    isMoved = true; 
                    cur = st.Pop();//get the next node

                    if(cur.Right != null) st.Push(cur.Right);
                    if(cur.Left != null) st.Push(cur.Left);
                }
                return isMoved; 
            }

            public void Reset()
            {
                st = new Stack<TreeNode<T>>();

                if (parent.root != null)
                { 
                    st.Push(parent.root);
                }
                cur = null;
            }
        }

        public class BreadthFirstEnumberator : IEnumerator<T>
        {
            public BST<T> parent = null;

            private TreeNode<T> cur = null;

            private Queue<TreeNode<T>> st = new Queue<TreeNode<T>>();
            public BreadthFirstEnumberator(BST<T> parent)
            {
                this.parent = parent;
                Reset();
            }

            public T Current => cur.Data;

            object IEnumerator.Current => cur.Data;

            public void Dispose()
            {
                parent = null;
                cur = null;
                st = null;
            }

            public bool MoveNext()
            {
                bool isMoved = false;
                if (st.Count > 0)
                {
                    isMoved = true;
                    cur = st.Dequeue();//get the next node

                    if (cur.Left != null) st.Enqueue(cur.Left);
                    if (cur.Right != null) st.Enqueue(cur.Right);
                }
                return isMoved;
            }

            public void Reset()
            {
                st = new Queue<TreeNode<T>>();

                if (parent.root != null)
                {
                    st.Enqueue(parent.root);
                }
                cur = null;
            }
        }
        #endregion
    }
}
