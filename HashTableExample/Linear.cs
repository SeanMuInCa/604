using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableExample
{
    public class Linear<K, V> : A_OpenAddressing<K, V> where K : IComparable<K> where V : IComparable<V>
    {
        protected override int GetIncrement(int iAttempt, K key)
        {
            int iIncrement = 1;


            return iIncrement * iAttempt;
        }
    }
}
