using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableExample
{
    public class DoubleHash<K, V> : A_OpenAddressing<K, V> where K : IComparable<K> where V : IComparable<V>
    {
        public DoubleHash() 
        { 
            //this.dLoadFactor = 0.5;
        }
        protected override int GetIncrement(int iAttempt, K key)
        {
            return (1 + Math.Abs(key.GetHashCode()) % (HTSize - 1)) * iAttempt;
        }
    }
}
