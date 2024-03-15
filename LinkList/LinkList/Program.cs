using System;
using LinkedList;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkList
{
    internal class Program
    {
        static void TestAdd(LinkedList.LinkedList<int> myList) 
        {
            myList.Add(3);
            myList.Add(3);
            myList.Add(5);
            myList.Add(1);
            myList.Add(7);
            myList.Add(11);
        }
        static void TestInsert(LinkedList.LinkedList<int> myList)
        {
            myList.Insert(0, 100);
            myList.Insert(1, 200);
            myList.Insert(2, 300);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("HelloWorld!");
            LinkedList.LinkedList<int> myList = new LinkedList.LinkedList<int>();
            TestAdd(myList);
            TestInsert(myList);
            Console.WriteLine(myList.ToString());
            Console.WriteLine("Count " + myList.Count);

            Console.WriteLine(myList.ToString());
        }
    }
}
