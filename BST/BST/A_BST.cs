using DataStuctureCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST
{
    public abstract class A_BST<T> : A_Collection<T>, I_BST<T> where T : IComparable<T>
    {
        protected TreeNode<T> root;
        protected int count;
        public override int Count { get { return count; } }
        public abstract T Find(T data);
        public abstract int Height();
        public abstract void Iterate(ProcessData<T> pd, TRAVERSALORDER or);
    }
}
