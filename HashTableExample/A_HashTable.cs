using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableExample
{
    public abstract class A_HashTable<K,V>: I_HashTable<K,V> where K : IComparable<K> where V : IComparable<V> 
    {
        // in separte chaining , we would have an array of arraylist
        //now we will store objects taht will be instances of our 
        //key-value class
        protected object[] oDataArray;
        protected int iCount;
        protected double dLoadFactor = 0.72;
        protected int iNumCollision = 0;//how many collisions we have during the store processing for each table size

        public int Count { get => iCount; }
        public int NumCollision { get => iNumCollision; }
        public int HTSize//table size
        {
            get
            { 
                return oDataArray.Length;
            }
        }
        //public int HTSize { get => oDataArray.Length; }

        //implementation
        public abstract void Add(K key, V value);
        public abstract V Get(K key);
        public abstract void Remove(K key);
        public void Clear()
        { 
            throw new NotImplementedException();
        }

        public abstract IEnumerator<V> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() 
        { 
            return GetEnumerator();
        }

        //helper functions
        protected int HashFunction(K key)
        {
            return Math.Abs(key.GetHashCode() % HTSize);
        }
    }
}
