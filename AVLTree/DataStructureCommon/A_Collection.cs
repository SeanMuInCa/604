using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStuctureCommon
{
    public abstract class A_Collection<T> : I_Collection<T> where T : IComparable<T>
    {
        #region Abstract Methods

        public abstract void Add(T data);

        public abstract void Clear();

        public abstract bool Remove(T data);

        public abstract IEnumerator<T> GetEnumerator();


        #endregion

        public virtual int Count 
        {
            get 
            { 
                int count = 0;
                foreach (T data in this)
                {
                    count++;
                }
                return count;
            }
        }
        public bool Contains(T data)
        { 
            bool found = false;

            IEnumerator<T> list = GetEnumerator();
            list.Reset();

            while (list.MoveNext())
            {
                if (data.Equals(list.Current))
                {
                    found = true;
                    break;
                }
            }
            return found;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("[");
            string sep = ", ";
            foreach (T data in this) 
            {
                sb.Append(data + sep);
            }
            if (this.Count > 0)
            {
                sb.Remove(sb.Length - sep.Length, sep.Length);
            }
            sb.Append(']');
            return sb.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
