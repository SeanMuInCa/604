using DataStuctureCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST
{
    public delegate void ProcessData<T>(T data);
    public enum TRAVERSALORDER { PRE_ORDER, IN_ORDER, POST_ORDER};
    public interface I_BST<T> : I_Collection<T> where T : IComparable<T>
    {
        //Given a data element, find the corresponding element of equal value
        //Returns a Reference to the first occurrenance of the item found, else return the default value for Type of T
        T Find(T data);
        //Returns the height of the tree
        int Height();
        //It uses a delegate to perform some actions on each data item
        void Iterate(ProcessData<T> pd, TRAVERSALORDER or);
    }
}
