using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableExample
{
    public interface I_HashTable<K,V>: IEnumerable<V> where K:IComparable<K> where V:IComparable<V>
    {
        /**
         * return the value stored, given the key
         */
        V Get(K key);

        /**
         * add the key-value pair, using the key to hash with
         */
        void Add(K key, V value);

        /**
         * remove the key-value pair, by the given key
         */
        void Remove(K key);
        /**
         * clear the table
         */
        void Clear();
    }
}
