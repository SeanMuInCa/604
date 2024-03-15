using System.Collections;
using System.Xml.Linq;

namespace SimpleHash
{
    class Program
    { 
        static void Main(string[] args) 
        {
            String[] names = new string[]
            {
                "abc","def","xyz","jackson","Ohnio","jordan","peppon","robert"
            };
            string[] myHashTable = new string[101];

           /* foreach (string name in names) 
            {
                *//*int hashindex = SimpleHash(myHashTable, name);*//*
                int hashindex = BetterHash(myHashTable, name);
                Console.WriteLine(hashindex + " " + name);
                myHashTable[hashindex] = name;
            }
            int count = 0;
            Console.WriteLine();
            foreach (string name in myHashTable) 
            {
                if (name != null)
                { 
                    Console.WriteLine(count + " " + name);
                    count++;
                }
                
            }*/
            BucketHash bucketHash = new BucketHash();
            foreach (string name in names) 
            {
                bucketHash.Insert(name);
            }
            bucketHash.PrintArray();
            bucketHash.Remove("Ohnio");
            Console.WriteLine("----------------------");
            bucketHash.PrintArray();
        }

        static int SimpleHash(string[] names, string name)
        { 
            int total = 0;
            char[] chars = name.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            { 
                total += (int)chars[i];
            }
            return total % names.Length;
        }

        static int BetterHash(string[] names, string name)
        {
            long total = 0;
            char[] chars = name.ToCharArray();

            for(int i = 0;i < chars.Length;i++) 
            {
                total += 23 * total + (int)chars[i];
            }
            return Math.Abs((int)total % names.Length);
        }
    }

    public class BucketHash 
    {
        private const int SIZE = 101;
        ArrayList[] data;

        public BucketHash() 
        { 
            data = new ArrayList[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                data[i] = new ArrayList();
            }
        }

        public void PrintArray()
        {
            int count = 0;
            foreach (ArrayList item in data)
            {
                if(item.Count != 0)
                    Console.Write(count + ": ");
                foreach (string name in item)
                { 
                    Console.Write(name + " ");
                }
                if(item.Count != 0)
                    Console.WriteLine();
                count++;
            }
        }

        public int Hash(string str)
        {
            long total = 0;
            char[] chars = str.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                total += 21 * total + (int)chars[i];
            }
            total = total % data.Length;

            return (int)total;
        }

        public void Insert(string item)
        { 
            int hash = Hash(item);
            if (!data[hash].Contains(item))
            {
                data[hash].Add(item);//adding to arraylist first, not directly to hashtable
            }
        }

        public void Remove(string item) 
        {
            int hash = Hash(item);
            if (data[hash].Contains(item))
            {
                data[hash].Remove(item);
            }
        }
    }
}