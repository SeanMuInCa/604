using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStuctureCommon
{
    public interface I_Collection<T> : IEnumerable<T> where T : IComparable<T>
    {
        //add the given data to the collection
        void Add(T data);

        //remove all datas from the collection
        void Clear();

        //determin the given data is in the collection or not
        bool Contains(T data);

        //compare with other one to see if they are equals
        bool Equals(Object other);
        
        //remove a specific data occurence the first from the collection
        bool Remove(T data);

        //call to get a property
        int Count { get; }
    }
}
