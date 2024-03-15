using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStuctureCommon;

namespace LinkedList
{
    public abstract class A_List<T> : A_Collection<T>, I_List<T> where T : IComparable<T>
    {
        #region Abstract Methods
        public abstract void Insert(int index, T data);
        public abstract T RemoveAt(int index);
        public abstract T ReplaceAt(int index, T data);
        #endregion


        #region I_List Implementation

        public T ElementAt(int index) {
            T original;
            int count = 0;

            if (index < 0 || index >= this.Count) { 
                throw new IndexOutOfRangeException("Invalid index" + index);
            }

            IEnumerator<T> myEnum = GetEnumerator();
            while (myEnum.MoveNext() && count != index) {
                count++;
            }
            original = myEnum.Current;
            return original;
        }

        public int IndexOf(T item)
        {
            int index = -1;
            for (int i = 0; i < Count; i++)
            {
                if (ElementAt(i).Equals(item))
                {
                    index = i;
                    break;
                }
            }


            return index;
        }
        #endregion
    }
}