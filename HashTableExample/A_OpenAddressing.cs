using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableExample
{
    public abstract class A_OpenAddressing<K,V>: A_HashTable<K,V> where K: IComparable<K> where V: IComparable<V> 
    {
        /**
         * this is specified as abstract here as it will be different
         * for each of the Linear, Quadratic and Double-Hash impliments
         */
        protected abstract int GetIncrement(int iAttempt, K key);

        //instance of prime number generator class
        private PrimeNumbers pn = new PrimeNumbers();
        public A_OpenAddressing()
        {
            oDataArray = new object[pn.GetNextPrime()];
        }

        public override void Add(K key, V value)
        {
            //need to keep track of how many attempts for getincrement method
            int iAttempt = 1;
            //get the initial hash of the key
            int iInitialHash = HashFunction(key);
            //what the current location is as we move through the table looking for a location to insert to
            int iCurrentLocation = iInitialHash;
            //need a keyvalue instance to hold the key and value to insert into the hash table
            KeyValue<K,V> kvNew = new KeyValue<K,V>(key, value);

            //separate variable to store eventually insertion point
            int iPositionToAdd = -1;

            //find a empty location in the table and try to insert
            while (oDataArray[iCurrentLocation] != null) 
            {
                //if this location is taken and it's a valid value
                if (oDataArray[iCurrentLocation].GetType() == typeof(KeyValue<K, V>))
                {
                    KeyValue<K, V> kv = (KeyValue<K, V>)oDataArray[iCurrentLocation];
                    if (kv.Equals(kvNew))//check duplicate
                    {
                        throw new ApplicationException("it's already in the table");
                    }
                }
                else
                {
                    //it must be the first tombstone to be able to insert
                    if (iPositionToAdd == -1)
                    {
                        iPositionToAdd = iCurrentLocation;//update it to indicate it's not the first tombstone we found
                    }
                }
                //get a new hash
                iCurrentLocation = iInitialHash + GetIncrement(iAttempt++, key);
                //rehash the table
                iCurrentLocation %= HTSize;

                iNumCollision++;//log there is one collision
            }
            if (iPositionToAdd == -1)//its possible that we encountered a tombstone, if iPositionToAdd has been changed, we did so we'll insert there
            {
                iPositionToAdd = iCurrentLocation;
            }
            oDataArray[iPositionToAdd] = kvNew;
            iCount++;

            if (IsOverLoaded())
            {
                ExpandHashTable();
            }
        }

        public void ExpandHashTable() 
        {
            object[] oOldArray = oDataArray;
            oDataArray = new object[pn.GetNextPrime()];
            iCount = 0;
            iNumCollision = 0;

            for (int i = 0; i < oOldArray.Length; i++)
            {
                if (oOldArray[i] != null)
                {
                    if (oOldArray[i].GetType() == typeof(KeyValue<K, V>))
                    {
                        KeyValue<K,V> kv = (KeyValue<K, V>)oOldArray[i];
                        this.Add(kv.Key, kv.Value);
                    }
                }
            }
        }
        public bool IsOverLoaded() 
        {
            return (iCount / (double)HTSize) > dLoadFactor;
        }

        public override V Get(K key)
        {
            //need to keep track of how many attempts for getincrement method
            int iAttempt = 1;
            //get the initial hash of the key
            int iInitialHash = HashFunction(key);
            //what the current location is as we move through the table looking for a location to insert to
            int iCurrentLocation = iInitialHash;

            V result = default(V);// means initial this result to null, c# doesnt allowed to assign null directly
            bool found = false;

            while ((oDataArray[iCurrentLocation] != null) && !found)
            {
                //if this location is taken and it's a valid value
                if (oDataArray[iCurrentLocation].GetType() == typeof(KeyValue<K, V>))
                {   //check for match
                    KeyValue<K, V> kv = (KeyValue<K, V>)oDataArray[iCurrentLocation];
                    if (kv.Key.CompareTo(key) == 0)//check duplicate
                    {
                        found = true;
                        result = kv.Value;
                    }
                }
                
                //get a new hash
                iCurrentLocation = iInitialHash + GetIncrement(iAttempt++, key);
                //rehash the table
                iCurrentLocation %= HTSize;
            }

            if (!found) throw new ApplicationException("key does not exist in the table");

            return result;
        }

        public override void Remove(K key)
        {
            //need to keep track of how many attempts for getincrement method
            int iAttempt = 1;
            //get the initial hash of the key
            int iInitialHash = HashFunction(key);
            //what the current location is as we move through the table looking for a location to insert to
            int iCurrentLocation = iInitialHash;

            bool found = false;

            while ((oDataArray[iCurrentLocation] != null) && !found)
            {
                //if this location is taken and it's a valid value
                if (oDataArray[iCurrentLocation].GetType() == typeof(KeyValue<K, V>))
                {   //check for match
                    KeyValue<K, V> kv = (KeyValue<K, V>)oDataArray[iCurrentLocation];
                    if (kv.Key.CompareTo(key) == 0)//check duplicate
                    {
                        found = true;
                        oDataArray[iCurrentLocation] = new Tombstone();
                        iCount--;
                    }
                }

                //get a new hash
                iCurrentLocation = iInitialHash + GetIncrement(iAttempt++, key);
                //rehash the table
                iCurrentLocation %= HTSize;
            }

            if (!found) throw new ApplicationException("key does not exist in the table");

        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < oDataArray.Length; i++) 
            {
                sb.Append("Bucket " + i + ": ");
                if (oDataArray[i] != null)
                {
                    if (oDataArray[i].GetType() == typeof(KeyValue<K, V>))
                    {
                        KeyValue<K, V> kv = (KeyValue<K, V>)oDataArray[i];
                        sb.Append(kv.Key.ToString() + " " + kv.Value.ToString()+ " " + " IH = " + HashFunction(kv.Key));
                    }
                    else
                    { 
                        sb.Append("Tombstone");
                    }
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }
        public override IEnumerator<V> GetEnumerator()
        {
            throw new NotImplementedException();
        }
        
    }
}
